using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using CommonUI;

namespace MainEditors.Main
{
    public partial class EditorMainProperty : DockContent
    {
        /// <summary>
        /// Событие изменения свойства
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// Редактируемый объект
        /// </summary>
        object _editItem;

        /// <summary>
        /// Редактируемый объект
        /// </summary>
        public object EditItem
        {
            set
            {
                SetEditItem(value);
            }
            get
            {
                return _editItem;
            }
        }

        public EditorMainProperty()
        {
            InitializeComponent();
        }

        void SetEditItem(object editItem)
        {
            _editItem = null;
            _panel.Controls.Clear();
            if (editItem is StageItem)
            {
                ControlEditStageItem cntrl = new ControlEditStageItem();
                cntrl.EditItem = editItem as StageItem;
                cntrl.Dock = DockStyle.Fill;
                cntrl.Changed += ControlItemChanged;
                _panel.Controls.Add(cntrl);
            }
            else
            {
                ControlStdProperties cntrl = new ControlStdProperties();
                cntrl.EditItem = editItem;
                cntrl.Dock = DockStyle.Fill;
                cntrl.Changed += ControlItemChanged;
                _panel.Controls.Add(cntrl);
            }
            _editItem = editItem;
        }

        void ControlItemChanged(object oEdit)
        {
            if (Changed != null) Changed(null, EventArgs.Empty);
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (Changed != null) Changed(s, EventArgs.Empty);
        }
    }
}
