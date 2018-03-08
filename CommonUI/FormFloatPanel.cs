using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonUI
{
    /// <summary>
    /// Плавающая панель
    /// </summary>
    public partial class FormFloatPanel : Form
    {
        Control _mainItem;

        public FormFloatPanel()
        {
            InitializeComponent();
            FormKeysWorker util = new FormKeysWorker(this);
            util.EscEnterEvent(Apply);
        }

        public Control MainItem
        {
            set
            {
                SetPanel(value);
            }
        }

        private void SetPanel(Control editItem)
        {
            _mainItem = editItem;
            this.Controls.Clear();
            if (editItem == null) return;
            this.Controls.Add(editItem);
            this.Size = editItem.Size + (new System.Drawing.Size(40, 40));
            editItem.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;            
        }

        void Apply()
        {
            Close();
        }

        private void FormFloatPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_mainItem != null)
                this.Controls.Remove(_mainItem);
        }

    }
}
