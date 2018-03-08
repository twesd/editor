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
    public partial class FormDialog : Form
    {
        public FormDialog()
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
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void FormDialog_Shown(object sender, EventArgs e)
        {
           
        }
    }
}
