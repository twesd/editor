using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IrrlichtWrap;
using IrrTools;
using WeifenLuo.WinFormsUI.Docking;
using Serializable;
using UnitEngine;
using UnitEngine.Behavior;
using CommonUI;

namespace UnitEngineUI.Behavior
{
    public partial class EditorBehaviorIrrDocument : DockContent
    {

        /// <summary>
        /// Объект для визулизации модели
        /// </summary>
        IrrDevice _irrDevice;

        /// <summary>
        /// Основная модель
        /// </summary>
        SceneNodeW _model;

        /// <summary>
        /// Вспомогательные модели
        /// </summary>
        List<SceneNodeW> _envModels;

        /// <summary>
        /// Класс для работы с камерой
        /// </summary>
        CameraWorker _cameraWorker;

        /// <summary>
        /// Показывать окно предпросмотра
        /// </summary>
        public bool EnableIrrDevice = true;

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

        public EditorBehaviorIrrDocument()
        {
            InitializeComponent();

            _irrDevice = new IrrDevice(_panelPreview);
            _cameraWorker = new CameraWorker(_irrDevice);
        }

        /// <summary>
        /// Очистка данных
        /// </summary>
        public void Clear()
        {
            _irrDevice.StopDrawing();
            _model = null;
            _envModels = new List<SceneNodeW>();

            if (_irrDevice != null)
            {
                _irrDevice.DeviceW.Selector.ClearSelection();
                _irrDevice.DeviceW.DeleteSceneNodes();
            }
            _irrDevice.StartDrawing();
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
        public void UpdateCamera(ContainerCamera containerCamera)
        {
            if (_cameraWorker != null && containerCamera != null)
                _cameraWorker.ApplyContainer(containerCamera);
        }

        /// <summary>
        /// Загрузить модели
        /// </summary>
        public void LoadModels(UnitBehavior container)
        {
            if (container == null) return;
            if (_irrDevice == null) return;
            _model = null;
            _envModels = new List<SceneNodeW>();
            try
            {
                _irrDevice.StopDrawing();

                _irrDevice.DeviceW.DeleteSceneNodes();
                NodeWorker nodeWorker;
                // Загружаем основную модель
                //
                if (container.UnitModel  is UnitModelAnim)
                {
                    string modelPath = (container.UnitModel as UnitModelAnim).ModelPath;
                    if (!string.IsNullOrEmpty(modelPath))
                    {
                        _model = _irrDevice.DeviceW.AddSceneNode(modelPath);
                        if (_model == null) throw new Exception("Модель не загружена: " + modelPath);
                        _irrDevice.DeviceW.Camera.ZoomToNode(_model);
                        nodeWorker = new NodeWorker(_model);
                        if (container.MainModel == null)
                            container.MainModel = nodeWorker.GetContainer(modelPath);
                        else
                            nodeWorker.Apply(container.MainModel);
                        // Обновляем индентификатор и путь до модели
                        container.MainModel.Id = _model.Id;
                        container.MainModel.Path = modelPath;
                    }
                }
                else if (container.UnitModel is UnitModelSphere)
                {
                    var sphere = container.UnitModel as UnitModelSphere;
                    _model = _irrDevice.DeviceW.AddSphere(sphere.Radius, sphere.PolyCount);
                    if (_model == null) throw new Exception("Модель не загружена: sphere");
                    _irrDevice.DeviceW.Camera.ZoomToNode(_model);
                    nodeWorker = new NodeWorker(_model);
                    if (container.MainModel == null)
                        container.MainModel = nodeWorker.GetContainer(string.Empty);
                    else
                        nodeWorker.Apply(container.MainModel);
                    // Обновляем индентификатор и путь до модели
                    container.MainModel.Id = _model.Id;
                    container.MainModel.Path = string.Empty;
                }

                // Загружаем вспомогательные модели
                //
                if (container.EnviromentModels != null)
                {
                    foreach (ContainerNode model in container.EnviromentModels)
                    {
                        if (!string.IsNullOrEmpty(model.Path))
                        {
                            SceneNodeW envModelW = _irrDevice.DeviceW.AddSceneNode(model.Path);
                            if (envModelW != null)
                            {
                                _envModels.Add(envModelW);
                                model.Id = envModelW.Id;
                                nodeWorker = new NodeWorker(envModelW);
                                nodeWorker.Apply(model);
                            }
                        }
                    }
                }

                _irrDevice.StartDrawing();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        /// <summary>
        /// Обновить поведение модели
        /// </summary>
        /// <param name="action"></param>
        public void UpdateModel(UnitAction action, UnitBehavior container)
        {
            if (_irrDevice == null) return;
            if (_model == null) return;
            if (action == null) return;
            _model.ClearTransforms();

            if (container.MainModel != null)
            {
                NodeWorker nodeWorker = new NodeWorker(_model);
                nodeWorker.Apply(container.MainModel);
            }

            // Устанавливаем анимацию
            //
            if (_model != null)
            {
                if (action.Animation.Enabled)
                {
                    _model.SetLoopMode(action.Animation.Loop);
                    _model.SetAnimationSpeed(action.Animation.Speed);
                    _model.SetFrameLoop(action.Animation.StartFrame, action.Animation.EndFrame);
                }
                else
                {
                    _model.SetFrameLoop(0, 0);
                    _model.SetLoopMode(true);
                }
            }

            // Устанавливаем трансформации
            //
            _model.ClearTransforms();
            foreach (ExecuteBase execute in action.Executes)
            {
                if (execute is ExecuteTransform)
                    TransformWorker.SetTransform((execute as ExecuteTransform), _model);
            }
        }

        /// <summary>
        /// Сохраняем изображение
        /// </summary>
        /// <param name="path"></param>
        public void CaptureFromScreenToFile(string path)
        {
            UtilImage.CaptureFromScreenToFile(
                _panelPreview,
                path);
        }

        public Vertex3dW GetPosition(Point mouseScreenLocation)
        {
            var clientPoint = _panelPreview.PointToClient(mouseScreenLocation);
            return Device.DeviceW.ScreenCoordToPosition3d(clientPoint.X, clientPoint.Y, 0);
        }

        /// <summary>
        /// Обработчик изменения модели
        /// </summary>
        /// <param name="node"></param>
        public void NodeSaveChange(SceneNodeW node, UnitBehavior container)
        {
            if (_irrDevice == null) return;
            if (container == null) return;
            NodeWorker nodeWorker = new NodeWorker(node);
            if (_model.Id == node.Id)
            {
                nodeWorker.Update(container.MainModel);
            }
            else if (_envModels != null)
            {
                foreach (var envModel in container.EnviromentModels)
                {
                    if (envModel.Id == node.Id)
                    {
                        nodeWorker.Update(envModel);
                    }
                }
            }
        }

    }
}
