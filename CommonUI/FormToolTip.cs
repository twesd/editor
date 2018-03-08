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
    /// Форма в виде подсказки
    /// </summary>
    public partial class FormToolTip : Form
    {
        public FormToolTip()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Установить сообщение
        /// </summary>
        /// <param name="message"></param>
        public void SetMessage(string message)
        {
            _lblMessage.Text = message;
            UpdateSize();
        }


        public void UpdateSize()
        {
            this.Width = _lblMessage.Width + 10;
            this.Height = _lblMessage.Height + 5;
        }
    }
}
