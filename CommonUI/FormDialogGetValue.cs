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
    public partial class FormDialogGetValue : Form
    {
        public string Result { get; set; }

        public string StartText
        {
            set
            {
                _textInput.Text = value;
            }
        }

        public FormDialogGetValue()
        {
            InitializeComponent();
            FormKeysWorker keys = new FormKeysWorker(this);
            keys.EscEnterEvent(null);
        }

        private void _buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void _buttonOk_Click(object sender, EventArgs e)
        {
            Result = _textInput.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void FormDialog_Shown(object sender, EventArgs e)
        {
           
        }
    }
}
