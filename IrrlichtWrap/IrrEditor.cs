using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonUI;

namespace IrrlichtWrap
{
    public class IrrEditor
    {
        IrrDevice _irrDevice;

        IrrDeviceW _deviceW;

        /// <summary>
        /// Флаг отмены операции
        /// </summary>
        bool _aborted;

        /// <summary>
        /// Точка клика мыши
        /// </summary>
        int[] _mousePickPoint;


        public IrrEditor(IrrDevice irrDevice)
        {
            if (irrDevice == null) throw new NullReferenceException("irrDevice");
            _irrDevice = irrDevice;
            _deviceW = irrDevice.DeviceW;
            Panel panel = _irrDevice.PreviewPanel;
            panel.MouseClick += new MouseEventHandler(PanelMouseClick);
            panel.PreviewKeyDown += PreviewKeyDown;
        }

        void PanelMouseClick(object sender, MouseEventArgs e)
        {
            var modifiers = System.Windows.Input.Keyboard.Modifiers;
            bool nothingPressed = (modifiers == 0);

            if (e.Button == MouseButtons.Left && nothingPressed)
            {
                _mousePickPoint = new int[] { e.X, e.Y };
            }
        }

        void PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                _aborted = true;
        }

        /// <summary>
        /// Выбрать точку
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Vertex3dW PickPoint(float planeHeight = 0, string message = null)
        {
            try
            {
                _irrDevice.Selector.EnableUserSelection = false;
                if (message == null) message = "Выбирете точку:";
                _aborted = false;
                _mousePickPoint = null;
                Panel panel = _irrDevice.PreviewPanel;
                using (ToolTipMouse mouseTooltip = new ToolTipMouse(message, panel))
                {
                    while (true)
                    {
                        Application.DoEvents();
                        if (_aborted) return null;
                        if (panel.IsDisposed) return null;
                        if (_mousePickPoint != null)
                        {
                            var resPoint = _deviceW.ScreenCoordToPosition3d(
                                _mousePickPoint[0], _mousePickPoint[1], planeHeight);
                            return resPoint;
                        }
                    }
                }
            }
            finally
            {
                _irrDevice.Selector.EnableUserSelection = true;
            }
        }

    }
}
