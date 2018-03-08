using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonUI.UITypeEditors
{
    public partial class ControlEditorScript : UserControl
    {
        public string EditItem
        {
            get 
            {
                return _textBox.Text;
            }
            set
            {
                SetEditItem(value);
            }
        }

        public ControlEditorScript(List<string> autocompleteWords)
        {
            InitializeComponent();
            TextBoxAutoComplete autocomplete = new TextBoxAutoComplete();
            autocomplete.Init(_textBox, autocompleteWords);
        }

        /// <summary>
        /// Назначить новый объект для редактирования
        /// </summary>
        /// <param name="editItem"></param>
        private void SetEditItem(string editItem)
        {
            _textBox.Text = editItem;
        }
    }
}
