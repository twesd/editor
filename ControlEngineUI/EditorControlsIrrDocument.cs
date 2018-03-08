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
using ControlEngine;
using Serializable;
using Common.Geometry;
using CommonUI;
using Common;
using TransactionCore;
using ControlEngineUI.Transient;

namespace ControlEngineUI
{
    partial class EditorControlsIrrDocument : DockContent
    {
        /// <summary>
        /// Выбранный контрол
        /// </summary>
        public ControlBase SelectedControl { get; private set; }

        public EditorControlsInspector Inspector
        {
            get
            {
                return _inspector;
            }
        }

        public TransientManager TransientManager
        {
            get
            {
                return _transientManager;
            }
        }

        /// <summary>
        /// Объект для визулизации моделей
        /// </summary>
        IrrDevice _irrDevice;

        /// <summary>
        /// Объект для визулизации контролов
        /// </summary>
        ControlsW _irrControls;

        /// <summary>
        /// <summary>
        /// Вспомогательные модели
        /// </summary>
        List<SceneNodeW> _envModels;

        /// <summary>
        /// Класс для работы с камерой
        /// </summary>
        CameraWorker _cameraWorker;

        /// <summary>
        /// Инспектор объектов
        /// </summary>
        EditorControlsInspector _inspector;

        /// <summary>
        /// Менеджер временной графики
        /// </summary>
        TransientManager _transientManager;

        /// <summary>
        /// Менеджер для undo / redo
        /// </summary>
        TransactionManager _transManager;

        /// <summary>
        /// Объект изменён
        /// </summary>
        public event ChangedHandler Changed;

        public EditorControlsIrrDocument(EditorControlsInspector inspector, TransactionManager transManager)
        {
            if (inspector == null)
            {
                throw new ArgumentNullException("mainEditor");
            }
            if (transManager == null)
            {
                throw new ArgumentNullException("transManager");
            }

            InitializeComponent();

            _inspector = inspector;
            _transManager = transManager;

            _irrDevice = new IrrDevice(_panelIrrView);
            _irrControls = _irrDevice.DeviceW.Controls;

            _transientManager = new TransientManager(_irrDevice);

            _cameraWorker = new CameraWorker(_irrDevice);

            // Иницилизация инструмента перемещения
            //
            var imgMoveTool = new ImageMoveTool(this, _panelIrrView);
            imgMoveTool.Changed += ControlChanged;
            imgMoveTool.ChangeComplete += ControlChangeComplete;
            imgMoveTool.ChangeStart += ControlChangeStart;
        }

        /// <summary>
        /// Загрузить модели
        /// </summary>
        public void LoadModels(List<ContainerNode> envNodes)
        {
            if (envNodes == null) return;
            if (_irrDevice == null) return;
            _envModels = new List<SceneNodeW>();
            try
            {
                _irrDevice.StopDrawing();
                _irrDevice.DeviceW.DeleteSceneNodes();
                NodeWorker nodeWorker;

                // Загружаем вспомогательные модели
                //
                if (envNodes != null)
                {
                    foreach (ContainerNode model in envNodes)
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
            {
                _irrDevice.StopDrawing();
                _irrDevice.Dispose();
            }
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

        public void SetSelection(object oItem)
        {
            ControlBase control = oItem as ControlBase;
            SelectedControl = control;
        }

        /// <summary>
        /// Обновить изображения
        /// </summary>
        public void UpdateIrrView()
        {
            if (_irrDevice == null)
            {
                return;
            }
            _irrControls.Clear();
            foreach (ControlBase currentControl in _inspector.GetControls())
            {
                if (!currentControl.IsVisibleInEditor)
                {
                    continue;
                }
                if (currentControl is ControlButton)
                {
                    var btn = currentControl as ControlButton;
                    if (!string.IsNullOrEmpty(btn.TextureNormal))
                    {
                        _irrControls.AddButton(
                            btn.Name,
                            btn.TextureNormal,
                            Convertor.CreateVertex(btn.Position));
                    }
                }
                else if (currentControl is ControlImage)
                {
                    var cntrl = currentControl as ControlImage;
                    if (!string.IsNullOrEmpty(cntrl.Texture))
                    {
                        _irrControls.AddButton(
                            cntrl.Name,
                            cntrl.Texture,
                            Convertor.CreateVertex(cntrl.Position));
                    }
                }
                else if (currentControl is ControlText)
                {
                    var cntrl = currentControl as ControlText;
                    if (!string.IsNullOrEmpty(cntrl.FontPath))
                    {
                        _irrControls.AddText(
                            cntrl.Text,
                            cntrl.FontPath,
                            Convertor.CreateVertex(cntrl.Position),
                            cntrl.KerningWidth,
                            cntrl.KerningHeight
                            );
                    }
                }
                else if (currentControl is ControlPanel)
                {
                    var cntrl = currentControl as ControlPanel;
                    _irrControls.AddRect(
                            Convertor.CreateVertex(cntrl.Position),
                            cntrl.GetSize().Width,
                            cntrl.GetSize().Height,
                            0xff0000ff,
                            true);
                    // TODO : добавить отрисовку
                }
                else if (currentControl is ControlCircle)
                {
                    var cntrl = currentControl as ControlCircle;
                    if (!string.IsNullOrEmpty(cntrl.TextureBackground))
                    {
                        _irrControls.AddButton(
                             cntrl.Name,
                             cntrl.TextureBackground,
                             Convertor.CreateVertex(cntrl.Position)
                             );

                        if (!string.IsNullOrEmpty(cntrl.TextureCenter))
                        {
                            Size size = cntrl.GetSize();
                            Size centerSize = cntrl.GetCenterSize();
                            float x = cntrl.Position.X + (size.Width / 2) - (centerSize.Width / 2);
                            float y = cntrl.Position.Y + (size.Height / 2) - (centerSize.Height / 2);
                            Vertex centerPos = new Vertex(x, y, 0);
                            _irrControls.AddButton(
                                 cntrl.Name,
                                 cntrl.TextureCenter,
                                 Convertor.CreateVertex(centerPos)
                                 );
                        }
                    }
                }
                else if (currentControl is ControlTapScene ||
                    currentControl is ControlBehavior)
                {
                    // nothing
                }
            }

            // Перерисовываем переходную графику
            _transientManager.Draw();

            // Отрисовываем выбранный контрол
            //
            if (SelectedControl != null)
            {
                Size size = new Size();
                Vertex position = new Vertex();
                IControlSizeable cntrlSizeable = SelectedControl as IControlSizeable;
                if (cntrlSizeable != null)
                {
                    size = cntrlSizeable.GetSize();
                    position = cntrlSizeable.Position;
                }
                else if (SelectedControl is ControlTapScene)
                {
                    ControlTapScene cntrl = SelectedControl as ControlTapScene;
                    position = cntrl.MinPoint;
                    size.Width = (int)(cntrl.MaxPoint.X - cntrl.MinPoint.X);
                    size.Height = (int)(cntrl.MaxPoint.Y - cntrl.MinPoint.Y);
                }
                if (size.IsEmpty)
                    size = new System.Drawing.Size(1, 1);
                _irrControls.AddRect(
                    Convertor.CreateVertex(position),
                    size.Width,
                    size.Height,
                    0xff00ff00,
                    true
                    );
            }
        }

        /// <summary>
        /// Обработчик начала  изменения контрола
        /// </summary>
        /// <param name="oEditItem"></param>
        void ControlChangeStart(object oEditItem)
        {
            Transaction trans = _transManager.StartTransaction();
            trans.AddObject(oEditItem);
        }

        /// <summary>
        /// Обработчик изменения контрола
        /// </summary>
        /// <param name="oEditItem"></param>
        void ControlChanged(object oEditItem)
        {
            UpdateIrrView();
        }

        /// <summary>
        /// Обработчик завершения  изменения контрола
        /// </summary>
        /// <param name="oEditItem"></param>
        void ControlChangeComplete(object oEditItem)
        {
            if (Changed != null)
            {
                Changed(oEditItem);
            }

            _transManager.TopTransaction.Commit();
        }

        /// <summary>
        /// Выборка объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelIrrView_MouseDown(object sender, MouseEventArgs e)
        {
            ControlBase control = _inspector.GetControlByPosition(e.X, e.Y);
            if (control != SelectedControl)
            {
                _inspector.SetSelection(control);
            }
        }

        private void ToolStrip2x_Click(object sender, EventArgs e)
        {
            _toolStripContainerCenter.Width = 980;
            _toolStripContainerCenter.Height = 670;
            _toolStripContainerCenter.MinimumSize = new System.Drawing.Size(980, 670); 
            //_toolStripContainerCenter.ContentPanel.VerticalScroll

            _panelIrrView.Width = 960;
            _panelIrrView.Height = 640;
        }

        private void ToolStrip2x2_Click(object sender, EventArgs e)
        {
            _toolStripContainerCenter.Width = 1156;
            _toolStripContainerCenter.Height = 670;
            _toolStripContainerCenter.MinimumSize = new System.Drawing.Size(1156, 670); 


            _panelIrrView.Width = 1136;
            _panelIrrView.Height = 640;
        }
    }
}
