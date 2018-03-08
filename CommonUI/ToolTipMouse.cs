using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonUI
{
    /// <summary>
    /// Подсказка перемещающиеся за мышкой
    /// </summary>
    public class ToolTipMouse : IDisposable
    {
        /// <summary>
        /// Подсказка возле мыши
        /// </summary>
        FormToolTip _tooltip;

        /// <summary>
        /// Отступ от указателя мыши
        /// </summary>
        System.Drawing.Point _tooltipOffset;

        Control _panel;
        
        public ToolTipMouse(string message, Control panel)
        {
            _panel = panel;
            _tooltip = new FormToolTip();
            _tooltip.MouseMove += TooltipMouseMove;
            panel.MouseMove += TooltipMouseMove;
            panel.MouseLeave += TooltipMouseLeave;
            _tooltipOffset = new System.Drawing.Point(10, 10);
            _tooltip.SetMessage(message);
        }

        void TooltipMouseLeave(object sender, EventArgs e)
        {
            _tooltip.Hide();
        }


        /// <summary>
        /// Установить позицию подсказки
        /// </summary>
        private void SetToolTipLocation()
        {
            var cursorPos = System.Windows.Forms.Cursor.Position;
            _tooltip.Location = new System.Drawing.Point(
                cursorPos.X + _tooltipOffset.X,
                cursorPos.Y + _tooltipOffset.Y);
        }

        /// <summary>
        /// Обработчик движения мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TooltipMouseMove(object sender, MouseEventArgs e)
        {
            if (!_tooltip.IsDisposed)
            {
                SetToolTipLocation();
                if (!_tooltip.Visible)
                {
                    _tooltip.Show(_panel);
                    _tooltip.UpdateSize();
                }
            }
        }

        /// <summary>
        /// Закрыть указатель
        /// </summary>
        public void Dispose()
        {
            _tooltip.MouseMove += TooltipMouseMove;
            if (!_tooltip.IsDisposed)
                _tooltip.Close();
            Application.DoEvents();
        }

    }
}
