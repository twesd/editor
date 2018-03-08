using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IrrlichtWrap
{
    public class IrrCamera
    {
        /// <summary>
        /// Скорость движения камеры
        /// </summary>
        public float Speed { get; set; }

        Panel _panel;

        IrrDevice _irrDevice;

        IrrDeviceW _deviceW;

        /// <summary>
        /// Последняя точка мыши
        /// </summary>
        System.Drawing.Point _lastMousePoint;

        /// <summary>
        /// Последняя локация мышки коректна
        /// </summary>
        bool _lastPointValid = false;


        public IrrCamera(IrrDevice irrDevice)
        {
            _panel = irrDevice.PreviewPanel;
            _irrDevice = irrDevice;
            _deviceW = irrDevice.DeviceW;
            Speed = 1;

            _panel.MouseMove += PanelMouseMove;
            _panel.MouseWheel += PanelMouseWheel;
        }

        void PanelMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta == 0)
            {
                return;
            }
            _deviceW.Camera.WheelCamera(-e.Delta * Speed / 50.0f);
        }

        void PanelMouseMove(object sender, MouseEventArgs e)
        {
            var modifiers = System.Windows.Input.Keyboard.Modifiers;
            bool altPressed = ((modifiers & System.Windows.Input.ModifierKeys.Alt) != 0);
            bool cntrlPressed = ((modifiers & System.Windows.Input.ModifierKeys.Control) != 0);

            _panel.Focus();

            if (e.Button == 0)
            {
                _lastPointValid = false;
                _panel.Cursor = Cursors.Default;
                return;
            }
            if (!_lastPointValid)
            {
                _lastMousePoint = e.Location;
                _lastPointValid = true;
                return;
            }

            // Если зажата левая клавиша
            if (((e.Button & MouseButtons.Left) != 0) && altPressed)
            {
                Vertex3dW vertex = new Vertex3dW(
                    (e.Location.Y - _lastMousePoint.Y) / 2,
                    (e.Location.X - _lastMousePoint.X) / 2,
                    0);
                _deviceW.Camera.RotateCamera(vertex);
                _panel.Cursor = Cursors.NoMove2D;
            }
            else if ((((e.Button & MouseButtons.Middle) != 0) && altPressed)||
                (((e.Button & MouseButtons.Right) != 0) && altPressed))
            {
                Vertex3dW vertex = new Vertex3dW(
                    (float)(e.Location.X - _lastMousePoint.X) / (20.0f / Speed),
                    (float)(e.Location.Y - _lastMousePoint.Y) / (20.0f / Speed),
                    0);
                _deviceW.Camera.MoveCamera(vertex);
                _panel.Cursor = Cursors.SizeAll;
            }

            _lastMousePoint = e.Location;
        }

    }
}
