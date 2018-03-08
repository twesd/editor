using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonUI
{
    public class PanelIrr : Panel
    {
        public PanelIrr()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // nothing
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // nothing
        }
    }
}
