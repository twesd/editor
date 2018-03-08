using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace UnitEngineUI.Behavior
{
    public partial class EditorBehaviorItemProperty : DockContent
    {
        public TabControl.TabPageCollection TabPages
        {
            get
            {
                return _tabControlProps.TabPages;
            }
        }

        public EditorBehaviorItemProperty()
        {
            InitializeComponent();            
        }

        public void Clear()
        {
            while (_tabControlProps.TabPages.Count > 1)
            {
                _tabControlProps.TabPages.RemoveAt(1);
            }
            _tabControlProps.TabPages[0].Controls.Clear();
        }
    }
}
