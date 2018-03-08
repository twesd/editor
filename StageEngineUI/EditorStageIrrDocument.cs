using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using IrrlichtWrap;
using IrrTools;
using Serializable;
using StageEngine;
using UnitEngine;
using Common.Geometry;
using TransactionCore;

namespace StageEngineUI
{
    public partial class EditorStageIrrDocument : DockContent
    {
        /// <summary>
        /// Модели стадии
        /// </summary>
        List<SceneNodeW> _sceneNodes;

        /// <summary>
        /// Объект для визулизации модели
        /// </summary>
        IrrDevice _irrDevice;

        /// <summary>
        /// Класс для работы с камерой
        /// </summary>
        CameraWorker _cameraWorker;

        /// <summary>
        /// Устройство
        /// </summary>
        public IrrDevice Device
        {
            get
            {
                return _irrDevice;
            }
        }

        public Panel PanelIrrView
        {
            get
            {
                return _panelIrrView;
            }
        }

        /// <summary>
        /// Менеджер для undo / redo
        /// </summary>
        TransactionManager _transManager;

        public EditorStageIrrDocument(TransactionManager transManager)
        {
            InitializeComponent();
            _irrDevice = new IrrDevice(_panelIrrView);
            _cameraWorker = new CameraWorker(_irrDevice);
            _transManager = transManager;
        }

        public void Clear()
        {
            if (_irrDevice != null)
            {
                _irrDevice.DeviceW.Selector.ClearSelection();
            }
        }

        public void Destroy()
        {
            if (_irrDevice != null)
                _irrDevice.Dispose();
            _irrDevice = null;
        }

        public ContainerCamera GetContainerCamera()
        {
            if (_cameraWorker == null) return null;
            return _cameraWorker.CreateContainerCamera();
        }

        /// <summary>
        /// Обновляем положение камер
        /// </summary>
        public void SetCamera(ContainerCamera containerCamera)
        {
            if (_cameraWorker != null && containerCamera != null)
                _cameraWorker.ApplyContainer(containerCamera);
        }

        /// <summary>
        /// Загрузить модели
        /// </summary>
        public void LoadModels(List<UnitInstanceBase> instances)
        {
            if (_irrDevice == null)
            {
                return;
            }
            _sceneNodes = new List<SceneNodeW>();
            try
            {
                lock (_irrDevice.GetLock())
                {
                    _irrDevice.DeviceW.DeleteSceneNodes();

                    // Загружаем модели
                    //
                    foreach (UnitInstanceBase unitInstance in instances)
                    {
                        LoadModel(unitInstance);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Загрузить модель
        /// </summary>
        /// <param name="unitInstance"></param>
        public void LoadModel(UnitInstanceBase unitInstance)
        {
            lock (_irrDevice.GetLock())
            {

                NodeWorkerEx nodeWorker;
                SceneNodeW sceneNodeW = null;
                string path = null;
                if (unitInstance is UnitInstanceBillboard)
                {
                    UnitInstanceBillboard instance = (unitInstance as UnitInstanceBillboard);
                    sceneNodeW = _irrDevice.DeviceW.AddBillboard(instance.Width, instance.Height);
                    if (System.IO.File.Exists(instance.Texture))
                    {
                        sceneNodeW.SetTexture(0, instance.Texture);
                    }
                }
                else if (unitInstance is UnitInstanceEmpty)
                {
                    UnitInstanceEmpty instanceEmpty = (unitInstance as UnitInstanceEmpty);

                    sceneNodeW = _irrDevice.DeviceW.AddCube(Convertor.CreateBoundbox(new Boundbox(
                        new Vertex(-1, -1, -1), new Vertex(1, 1, 1))));
                    sceneNodeW.SetScale(Convertor.CreateVertex(instanceEmpty.Scale));
                    sceneNodeW.SetMaterialType((int)EMaterialType.EMT_TRANSPARENT_ALPHA_CHANNEL);
                    sceneNodeW.SetTexture(0, "Textures/instanceEmpty.png");
                }
                else if (unitInstance is UnitInstanceCamera)
                {
                    UnitInstanceCamera instanceCamera = (unitInstance as UnitInstanceCamera);
                    var bounds = new Common.Geometry.Boundbox(new Vertex(0, 0, 0), new Vertex(1, 1, 1));
                    sceneNodeW = _irrDevice.DeviceW.AddCube(Convertor.CreateBoundbox(bounds));
                    sceneNodeW.SetTexture(0, "Textures/instanceCamera.png");
                }
                else if (unitInstance is UnitInstanceStandard)
                {
                    UnitInstanceStandard instanceStandard = (unitInstance as UnitInstanceStandard);
                    if (instanceStandard.Behavior != null)
                    {
                        var behavior = (instanceStandard.Behavior as UnitBehavior);
                        if (behavior.UnitModel is UnitModelAnim)
                        {
                            path = (behavior.UnitModel as UnitModelAnim).ModelPath;
                            sceneNodeW = _irrDevice.DeviceW.AddSceneNode(path);
                        }
                        else if (behavior.UnitModel is UnitModelParticleSystem)
                        {
                            var psystem = behavior.UnitModel as UnitModelParticleSystem;
                            var bounds = new Common.Geometry.Boundbox(new Vertex(0, 0, 0), new Vertex(1, 1, 1));
                            sceneNodeW = _irrDevice.DeviceW.AddCube(Convertor.CreateBoundbox(bounds));
                            sceneNodeW.SetTexture(0, "Textures/instanceCamera.png");
                        }
                        else if (behavior.UnitModel is UnitModelBillboard)
                        {
                            var billboard = behavior.UnitModel as UnitModelBillboard;
                            sceneNodeW = _irrDevice.DeviceW.AddBillboard(billboard.Width, billboard.Height);
                            sceneNodeW.SetMaterialType((int)billboard.MaterialType);
                            if (System.IO.File.Exists(billboard.Texture))
                            {
                                sceneNodeW.SetTexture(0, billboard.Texture);
                            }
                        }
                        else if (behavior.UnitModel is UnitModelSphere)
                        {
                            var sphere = behavior.UnitModel as UnitModelSphere;
                            sceneNodeW = _irrDevice.DeviceW.AddSphere(sphere.Radius, sphere.PolyCount);
                        }
                        else if (behavior.UnitModel is UnitModelEmpty)
                        {
                            sceneNodeW = _irrDevice.DeviceW.AddCube(new BoundboxW(new Vertex3dW(-2,0,-2), new Vertex3dW(2,4,2)));
                            sceneNodeW.SetTexture(0, "Textures/instanceEmpty.png");
                        }
                    }
                }
                else if (unitInstance is UnitInstanceEnv)
                {
                    path = (unitInstance as UnitInstanceEnv).ModelPath;
                    sceneNodeW = _irrDevice.DeviceW.AddSceneNode(path);
                }

                if (sceneNodeW != null)
                {
                    _sceneNodes.Add(sceneNodeW);
                    unitInstance.EditorModelId = sceneNodeW.Id;
                    nodeWorker = new NodeWorkerEx(sceneNodeW);
                    nodeWorker.Apply(unitInstance);
                }
            }
        }

        /// <summary>
        /// Обновление моделей в соответствии с юнитами
        /// </summary>
        public void UpdateModels(List<UnitInstanceBase> instances)
        {
            if (_irrDevice == null)
            {
                return;
            }
            try
            {
                lock (_irrDevice.GetLock())
                {
                    // Загружаем модели
                    //
                    foreach (UnitInstanceBase unitInstance in instances)
                    {
                        SceneNodeW sceneNodeW = GetSceneNodeW(unitInstance);
                        if (sceneNodeW == null)
                        {
                            continue;
                        }
                        var nodeWorker = new NodeWorkerEx(sceneNodeW);
                        nodeWorker.Apply(unitInstance);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Report.Error(ex);
            }
        }

        /// <summary>
        /// Обновить модель
        /// </summary>
        /// <param name="instance"></param>
        public void SelectSceneNode(UnitInstanceBase instance)
        {
            if (_irrDevice == null)
            {
                return;
            }
            _irrDevice.Selector.ClearSelection();
            SceneNodeW node = GetSceneNodeW(instance);
            if (node != null)
            {
                _irrDevice.Selector.SetSelection(node);
            }
        }

        /// <summary>
        /// Удалить модель
        /// </summary>
        /// <param name="instance"></param>
        public void DeleteSceneNode(UnitInstanceBase instance)
        {
            if (_irrDevice == null)
            {
                return;
            }
            _irrDevice.Selector.ClearSelection();
            SceneNodeW node = GetSceneNodeW(instance);
            if (node != null)
            {
                _irrDevice.DeviceW.DeleteSceneNode(node);
            }
        }

        /// <summary>
        ///  Получить модель по редактируемому юниту
        /// </summary>
        /// <param name="_editItem"></param>
        /// <returns></returns>
        public SceneNodeW GetSceneNodeW(UnitInstanceBase instance)
        {
            if (instance == null) return null;
            if (_sceneNodes == null) return null;
            foreach (SceneNodeW node in _sceneNodes)
            {
                if (node.Id == instance.EditorModelId)
                    return node;
            }
            return null;
        }

        public Vertex3dW GetPosition(Point mouseScreenLocation)
        {
            var clientPoint = _panelIrrView.PointToClient(mouseScreenLocation);
            return Device.DeviceW.ScreenCoordToPosition3d(clientPoint.X, clientPoint.Y, 0);
        }

    }
}
