using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IrrlichtWrap;
using System.Windows.Forms;
using Common.Geometry;

namespace IrrTools
{
    /// <summary>
    /// Инструмент перемещение объекта с помощью стрелочек
    /// </summary>
    public class InstrumentDynamicMove
    {
        /// <summary>
        /// Объект изменён
        /// </summary>
        public event Common.ChangedHandler Changed;

        /// <summary>
        /// Объект изменён (конец изменения положения) 
        /// </summary>
        public event Common.ChangedHandler ChangeComplete;

        /// <summary>
        /// Фильтр для стрелок
        /// </summary>
        const int ArrowFilterId = 16;

        IrrDevice _irrDevice;

        IrrDeviceW _irrDeviceW;

        /// <summary>
        /// Перемещение мыши по X
        /// </summary>
        int _mouseOffsetX;

        /// <summary>
        /// Перемещение мыши по Y
        /// </summary>
        int _mouseOffsetY;

        System.Drawing.Point _lastMousePos;

        bool _aborted;

        bool _isGrip;

        bool _isActivate;

        SceneNodeW _editNode;

        SceneNodeW _selectedNode;

        bool _restoreSelection;

        SceneNodeW _xArrow;

        SceneNodeW _yArrow;

        SceneNodeW _zArrow;

        public InstrumentDynamicMove(IrrDevice irrDevice)
        {
            _irrDevice = irrDevice;
            _irrDeviceW = irrDevice.DeviceW;
        }

        /// <summary>
        /// Активировать инструмент
        /// </summary>
        public void Activate()
        {
            if (_isActivate) return;
            _editNode = null;
            if (_irrDeviceW.Selector.SelectionResult.Count == 1)
            {
                _editNode = _irrDeviceW.Selector.SelectionResult[0];
            }
            else
            {
                return;
            }

            _isActivate = true;
            _irrDevice.PreviewPanel.MouseDown += PanelMouseDown;
            _irrDevice.PreviewPanel.MouseMove += PanelMouseMove;
            _irrDevice.PreviewPanel.MouseUp += PanelMouseUp;
            _irrDevice.PreviewPanel.PreviewKeyDown += PreviewKeyDown;


            _aborted = false;
            _lastMousePos = new System.Drawing.Point(int.MinValue, int.MinValue);
            _mouseOffsetX = 0;
            _mouseOffsetY = 0;
            _selectedNode = null;
            _restoreSelection = false;

            // Создаём модели стрелок
            //
            lock (_irrDevice.GetLock())
            {
                _xArrow = _irrDeviceW.AddSceneNode(Common.UtilPath.AssemblyDirectory + "/Models/XArrow.x");
                _yArrow = _irrDeviceW.AddSceneNode(Common.UtilPath.AssemblyDirectory + "/Models/YArrow.x");
                _zArrow = _irrDeviceW.AddSceneNode(Common.UtilPath.AssemblyDirectory + "/Models/ZArrow.x");

                _xArrow.FilterId = ArrowFilterId;
                _yArrow.FilterId = ArrowFilterId;
                _zArrow.FilterId = ArrowFilterId;

                _xArrow.SetPosition(_editNode.GetPosition());
                _yArrow.SetPosition(_editNode.GetPosition());
                _zArrow.SetPosition(_editNode.GetPosition());
            }

            // Устанавливаем полувидимость объекта
            //
            _editNode.EnableHalfTransparent(true);

            // Основной цикл инструмента
            //
            while (!_aborted)
            {
                Application.DoEvents();

                if (_irrDevice.DeviceW == null || _irrDevice.PreviewPanel.IsDisposed)
                {
                    return;
                }

                if (!_editNode.IsExist)
                {
                    break;
                }

                _editNode.Highlight();
                _editNode.EnableHalfTransparent(true);
                // Выбираем последную выделенную модель
                if (_restoreSelection)
                {
                    if (_selectedNode != null)
                    {
                        _irrDeviceW.Selector.ClearSelection();
                        _irrDeviceW.Selector.SelectNode(_selectedNode);
                    }
                    _selectedNode = null;
                    _restoreSelection = false;
                    continue;
                }

                Vertex nodePos = Convertor.CreateVertex(_editNode.GetPosition());
                Vertex camPos = Convertor.CreateVertex(_irrDeviceW.Camera.Position);
                float dist = camPos.GetDistanceFrom(nodePos);
                if (dist == 0)
                {
                    continue;
                }

                Vertex camTarget = Convertor.CreateVertex(_irrDeviceW.Camera.Target);
                Vertex rotation = (camTarget - camPos).GetHorizontalAngle();

                // Подбираем размер стрелочек, в зависимости от расстояния объекта от камеры
                //
                float scale = dist / 15.0f;
                if (scale < 1)
                {
                    scale = 1;
                }
                Vertex3dW scaleVec = new Vertex3dW(scale, scale, scale);
                _xArrow.SetScale(scaleVec);
                _yArrow.SetScale(scaleVec);
                _zArrow.SetScale(scaleVec);

                if (_irrDeviceW.Selector.SelectionResult.Count == 0)
                {
                    continue;
                }

                // Если был выбран другой объект, то завершаем работу инструмента
                int selectNodeId = _irrDeviceW.Selector.SelectionResult[0].Id;
                if (_editNode.Id != selectNodeId &&
                    _xArrow.Id != selectNodeId &&
                    _yArrow.Id != selectNodeId &&
                    _zArrow.Id != selectNodeId)
                {
                    _editNode.UnHighlight();
                    break;
                }

                // Если стрелка была захвачена, то 
                if (_isGrip && dist > 0)
                {
                    float speedCoeff = (500.0f / dist);

                    if (selectNodeId == _xArrow.Id)
                    {
                        float closeTo270 = Math.Abs(270.0f - rotation.Y);
                        float closeTo90 = Math.Abs(90.0f - rotation.Y);

                        // Угол должен быть отдалён от 90 и 270 градусов для смещения по X
                        if (closeTo270 > 25 && closeTo90 > 25)
                        {
                            if (rotation.Y < 90 || rotation.Y > 270)
                            {
                                nodePos.X += (_mouseOffsetX / speedCoeff);
                            }
                            else
                            {
                                nodePos.X -= (_mouseOffsetX / speedCoeff);
                            }
                        }

                        // Угол должен быть близок к 90 или 270 градусов для смещения по X
                        if (closeTo270 < 60 || closeTo90 < 60)
                        {
                            if (rotation.Y > 180)
                            {
                                nodePos.X += (_mouseOffsetY / speedCoeff);
                            }
                            else
                            {
                                nodePos.X -= (_mouseOffsetY / speedCoeff);
                            }
                        }
                    }
                    if (selectNodeId == _yArrow.Id)
                    {
                        if (_mouseOffsetY != 0)
                        {
                            nodePos.Y -= (_mouseOffsetY / speedCoeff);
                        }
                    }
                    if (selectNodeId == _zArrow.Id)
                    {
                        float closeTo0 = Math.Abs(rotation.Y);
                        float closeTo180 = Math.Abs(180.0f - rotation.Y);
                        float closeTo360 = Math.Abs(360.0f - rotation.Y);

                        float minVal = Math.Min(closeTo0, closeTo180);
                        minVal = Math.Min(minVal, closeTo360);

                        if (minVal > 25)
                        {
                            if (rotation.Y <= 180)
                            {
                                nodePos.Z -= (_mouseOffsetX / speedCoeff);
                            }
                            else
                            {
                                nodePos.Z += (_mouseOffsetX / speedCoeff);
                            }
                        }

                        if (minVal < 25)
                        {
                            if (closeTo180 < 25)
                            {
                                nodePos.Z += (_mouseOffsetY / speedCoeff);
                            }
                            else
                            {
                                nodePos.Z -= (_mouseOffsetY / speedCoeff);
                            }
                        }
                    }

                    Vertex3dW newPos = Convertor.CreateVertex(nodePos);
                    _editNode.SetPosition(newPos);
                    _mouseOffsetX = 0;
                    _mouseOffsetY = 0;
                    _xArrow.SetPosition(newPos);
                    _yArrow.SetPosition(newPos);
                    _zArrow.SetPosition(newPos);

                    if (Changed != null)
                    {
                        Changed(_editNode);
                    }
                }
            }

            _editNode.EnableHalfTransparent(false);
            _irrDeviceW.DeleteSceneNode(_xArrow);
            _irrDeviceW.DeleteSceneNode(_yArrow);
            _irrDeviceW.DeleteSceneNode(_zArrow);
            _editNode = null;
            _xArrow = null;
            _yArrow = null;
            _zArrow = null;

            Deactivate();
            _isActivate = false;
        }

        void PanelMouseDown(object sender, MouseEventArgs e)
        {
            if (_editNode == null)
            {
                return;
            }
            var modifiers = System.Windows.Input.Keyboard.Modifiers;
            bool nothingPressed = (modifiers == 0);
            if (!nothingPressed) return;
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            _lastMousePos = new System.Drawing.Point(int.MinValue, int.MinValue);
            _isGrip = false;
            _mouseOffsetX = 0;
            _mouseOffsetY = 0;
            SceneNodeW node = _irrDeviceW.Selector.GetNodeByScreenCoords(e.Location.X, e.Location.Y, ArrowFilterId);
            if (node != null)
            {
                if (node.Id == _xArrow.Id || node.Id == _yArrow.Id || node.Id == _zArrow.Id)
                {
                    _irrDeviceW.Selector.ClearSelection();
                    _irrDeviceW.Selector.SelectNode(node);
                    _isGrip = true;
                }
            }
        }

        void PanelMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isGrip)
            {
                return;
            }
            var modifiers = System.Windows.Input.Keyboard.Modifiers;
            bool nothingPressed = (modifiers == 0);
            if (!nothingPressed || e.Button != MouseButtons.Left)
            {
                _lastMousePos = new System.Drawing.Point(int.MinValue, int.MinValue);
                _mouseOffsetX = 0;
                _mouseOffsetY = 0;
                return;
            }
            if (_lastMousePos.X == int.MinValue)
            {
                _lastMousePos = new System.Drawing.Point(e.Location.X, e.Location.Y);
                return;
            }
            if (_selectedNode == null)
            {
                if (_irrDeviceW.Selector.SelectionResult.Count > 0)
                    _selectedNode = _irrDeviceW.Selector.SelectionResult[0];
                _restoreSelection = false;
            }
            _mouseOffsetX = e.Location.X - _lastMousePos.X;
            _mouseOffsetY = e.Location.Y - _lastMousePos.Y;
            _lastMousePos = new System.Drawing.Point(e.Location.X, e.Location.Y);
        }

        void PanelMouseUp(object sender, MouseEventArgs e)
        {
            if (!_isGrip || _editNode == null) return;
            if (ChangeComplete != null) ChangeComplete(_editNode);
            _isGrip = false;
            _lastMousePos = new System.Drawing.Point(int.MinValue, int.MinValue);
            _mouseOffsetX = 0;
            _mouseOffsetY = 0;
            _restoreSelection = true;
        }

        /// <summary>
        /// Отключить инструмент
        /// </summary>
        public void Deactivate()
        {
            _irrDevice.PreviewPanel.MouseDown -= PanelMouseDown;
            _irrDevice.PreviewPanel.MouseMove -= PanelMouseMove;
            _irrDevice.PreviewPanel.MouseUp -= PanelMouseUp;
            _irrDevice.PreviewPanel.PreviewKeyDown -= PreviewKeyDown;
            _aborted = true;
        }


        void PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                _aborted = true;
        }

    }
}
