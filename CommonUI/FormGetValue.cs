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
    public partial class FormGetValue : Form
    {
        public string Result;

        public FormGetValue()
        {
            InitializeComponent();
            FormKeysWorker util = new FormKeysWorker(this);
            util.EscEnterEvent(Accept);
        }

        private void _buttonOk_Click(object sender, EventArgs e)
        {
            Accept();
        }

        private void Accept()
        {
            Result = _textBox.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
