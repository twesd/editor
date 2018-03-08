using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using ControlEngine;
using Common.Geometry;
using ControlEngineUI.Transient;

namespace ControlEngineUI
{
    class ImageMoveTool
    {
        /// <summary>
        /// Объект перед изменением
        /// </summary>
        public event ChangedHandler ChangeStart;

        /// <summary>
        /// Объект изменён
        /// </summary>
        public event ChangedHandler Changed;

        /// <summary>
        /// Объект изменён (конец изменения положения) 
        /// </summary>
        public event ChangedHandler ChangeComplete;

        /// <summary>
        /// Основной документ для отображения графики 
        /// </summary>
        EditorControlsIrrDocument _irrDocument;

        /// <summary>
        /// Редактируемый контрол
        /// </summary>
        ControlBase _editControl;

        /// <summary>
        /// Захвачен ли контрол
        /// </summary>
        bool _isGrip;

        /// <summary>
        /// Начальная точка клика
        /// </summary>
        System.Drawing.Point _startPoint;

        /// <summary>
        /// Расстояние между началом контрола и кликом
        /// </summary>
        System.Drawing.Point _offset;

        TransientLine _lineXAlign;

        TransientLine _lineYAlign;

        public ImageMoveTool(EditorControlsIrrDocument irrDocument, Panel panel)
        {
            _irrDocument = irrDocument;

            _startPoint = new System.Drawing.Point();
            _offset = new System.Drawing.Point();

            panel.MouseDown += new MouseEventHandler(MouseDown);
            panel.MouseMove += new MouseEventHandler(MouseMove);
            panel.MouseUp += new MouseEventHandler(MouseUp);
            panel.PreviewKeyDown += new PreviewKeyDownEventHandler(PreviewKeyDown);

            _lineXAlign = new TransientLine(new Vertex(), new Vertex(), 0xff00ffff)
            {
                IsVisible = false
            };
            _lineYAlign = new TransientLine(new Vertex(), new Vertex(), 0xff00ffff)
            {
                IsVisible = false
            };
            _irrDocument.TransientManager.Geometries.Add(_lineXAlign);
            _irrDocument.TransientManager.Geometries.Add(_lineYAlign);
        }

        /// <summary>
        /// Обработка нажатия клавиши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (_irrDocument.SelectedControl == null ||
                (_irrDocument.SelectedControl as IControlSizeable == null))
            {
                return;
            }
            float step = 1;
            var cntrlSizeable = _irrDocument.SelectedControl as IControlSizeable;
            if (e.KeyCode == Keys.Left)
            {
                cntrlSizeable.Position.X -= step;
            }
            else if (e.KeyCode == Keys.Right)
            {
                cntrlSizeable.Position.X += step;
            }
            else if (e.KeyCode == Keys.Up)
            {
                cntrlSizeable.Position.Y -= step;
            }
            else if (e.KeyCode == Keys.Down)
            {
                cntrlSizeable.Position.Y += step;
            }
            else
            {
                return;
            }

            if (Changed != null)
            {
                Changed(cntrlSizeable);
            }
        }

        void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _editControl = null;
            IControlSizeable cntrlSizeable = _irrDocument.SelectedControl as IControlSizeable;

            if (cntrlSizeable == null ||
                !cntrlSizeable.IsPointInside(e.X, e.Y))
            {
                return;
            }

            _editControl = _irrDocument.SelectedControl;

            _startPoint.X = e.X;
            _startPoint.Y = e.Y;

            
            _offset.X = (int)cntrlSizeable.Position.X - _startPoint.X;
            _offset.Y = (int)cntrlSizeable.Position.Y - _startPoint.Y;
        }

        void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left ||
                _editControl == null)
            {
                return;
            }

            if (!_isGrip)
            {
                if ((Math.Abs(_startPoint.X - e.X) > 5) ||
                    Math.Abs(_startPoint.Y - e.Y) > 5)
                {
                    ChangeStart(_editControl);
                    _isGrip = true;
                }
                return;
            }

            var modifiers = System.Windows.Input.Keyboard.Modifiers;
            bool shiftPressed = ((modifiers & System.Windows.Input.ModifierKeys.Shift) != 0);

            IControlSizeable cntrlSizeable = _editControl as IControlSizeable;
            if (cntrlSizeable != null)
            {
                Vertex notAlignPos = new Vertex(e.X + _offset.X, e.Y + _offset.Y, 0);

                Vertex alignPoint = GetAlignPoint(notAlignPos);
                if (alignPoint != null && shiftPressed)
                {
                    cntrlSizeable.Position = alignPoint;
                }
                else
                {
                    cntrlSizeable.Position = notAlignPos;

                    // Отключаем переходную графику
                    _lineXAlign.IsVisible = false;
                    _lineYAlign.IsVisible = false;
                }
                if (Changed != null)
                {
                    Changed(_editControl);
                }
            }
        }

        /// <summary>
        /// Получить точку выравнивания
        /// </summary>
        /// <param name="newPos">Новая позиция без выравнивания</param>
        /// <returns></returns>
        private Vertex GetAlignPoint(Vertex notAlignPos)
        {
            float minLimit = 10;
            float gMinDiffX = float.MaxValue;
            float gMinDiffY = float.MaxValue;
            Vertex alignPoint = null;

            List<ControlBase> controls = _irrDocument.Inspector.GetControls();
            foreach (ControlBase control in controls)
            {
                if (control == _editControl)
                {
                    continue;
                }
                IControlSizeable cntrlSizeable = control as IControlSizeable;
                if (cntrlSizeable == null)
                {
                    continue;
                }
                var size = cntrlSizeable.GetSize();

                // Получаем отступ от границ контрола

                float xDiff1 = Math.Abs(cntrlSizeable.Position.X - notAlignPos.X);
                float yDiff1 = Math.Abs(cntrlSizeable.Position.Y - notAlignPos.Y);

                float xDiff2 = Math.Abs(cntrlSizeable.Position.X + size.Width - notAlignPos.X);
                float yDiff2 = Math.Abs(cntrlSizeable.Position.Y - size.Height - notAlignPos.Y);

                // Получаем отступ слишком большой, то
                if (xDiff1 > minLimit && yDiff1 > minLimit &&
                    xDiff2 > minLimit && yDiff2 > minLimit)
                {
                    // пропускаем
                    continue;
                }

                // Вычисляем X и Y привязки
                //
                float destX;
                float minDiffX;
                if (xDiff1 < xDiff2)
                {
                    minDiffX = xDiff1;
                    destX = cntrlSizeable.Position.X;
                }
                else
                {
                    minDiffX = xDiff2;
                    destX = cntrlSizeable.Position.X + size.Width;
                }

                float destY;
                float minDiffY;
                if (yDiff1 < yDiff2)
                {
                    minDiffY = yDiff1;
                    destY = cntrlSizeable.Position.Y;
                }
                else
                {
                    minDiffY = yDiff2;
                    destY = cntrlSizeable.Position.Y - size.Height;
                }

                if (alignPoint == null)
                {
                    alignPoint = new Vertex(notAlignPos.X, notAlignPos.Y, 0);
                }

                if (minDiffX < gMinDiffX)
                {
                    gMinDiffX = minDiffX;
                    if (minDiffX < minLimit)
                    {
                        alignPoint.X = destX;

                        // Обновляем переходную графику
                        //
                        _lineXAlign.Start.X = destX;
                        _lineXAlign.Start.Y = cntrlSizeable.Position.Y - 300;
                        _lineXAlign.End.X = destX;
                        _lineXAlign.End.Y = cntrlSizeable.Position.Y + 300;
                        _lineXAlign.IsVisible = true;
                    }
                    else
                    {
                        alignPoint.X = notAlignPos.X;

                        // Отключаем переходную графику для Х
                        _lineXAlign.IsVisible = false;
                    }
                }

                if (minDiffY < gMinDiffY)
                {
                    gMinDiffY = minDiffY;
                    if (minDiffY < minLimit)
                    {
                        alignPoint.Y  = destY;

                        // Обновляем переходную графику
                        //
                        _lineYAlign.Start.X = cntrlSizeable.Position.X - 300;
                        _lineYAlign.Start.Y = destY;
                        _lineYAlign.End.X = cntrlSizeable.Position.X + 300;
                        _lineYAlign.End.Y = destY;
                        _lineYAlign.IsVisible = true;
                    }
                    else
                    {
                        alignPoint.Y = notAlignPos.Y;

                        // Отключаем переходную графику для Y
                        _lineYAlign.IsVisible = false;
                    }
                }

                
            }

            return alignPoint;
        }

        void MouseUp(object sender, MouseEventArgs e)
        {
            if (!_isGrip)
            {
                return;
            }
            object oItem = _editControl;

            _isGrip = false;
            _editControl = null;
            _lineXAlign.IsVisible = false;
            _lineYAlign.IsVisible = false;
            
            if (ChangeComplete != null)
            {
                ChangeComplete(oItem);
            }
        }

    }
}
