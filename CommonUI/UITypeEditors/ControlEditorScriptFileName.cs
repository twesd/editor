using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;

namespace CommonUI.UITypeEditors
{
    public partial class ControlEditorScriptFileName : UserControl
    {
        public string EditItem
        {
            get 
            {
                return _textBoxFileName.Text;
            }
            set
            {
                SetEditItem(value);
            }
        }

        public ControlEditorScriptFileName(List<string> autocompleteWords)
        {
            InitializeComponent();
            TextBoxAutoComplete autocomplete = new TextBoxAutoComplete();
            autocomplete.Init(_textBoxScript, autocompleteWords);
        }

        /// <summary>
        /// Назначить новый объект для редактирования
        /// </summary>
        /// <param name="editItem"></param>
        private void SetEditItem(string editItem)
        {
            _textBoxFileName.Text = editItem;
            _textBoxScript.Text = UtilFile.ReadTextFile(editItem);
            _btnSave.Enabled = false;
        }


        private void TextBoxScript_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            _btnSave.Enabled = true;
        }

        private void _btnSave_Click(object sender, EventArgs e)
        {
            UtilFile.WriteTextFile(_textBoxFileName.Text, _textBoxScript.Text);
            _btnSave.Enabled = false;
        }

        private void _textBoxFileName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_textBoxFileName.Text)) return;
            _btnSave.Enabled = false;
            _textBoxScript.Text = UtilFile.ReadTextFile(_textBoxFileName.Text);
        }

        private void _btnSelect_Click(object sender, EventArgs e)
        {
            string fileName = UtilFile.OpenDialog(".sc");
            if (!string.IsNullOrEmpty(fileName))
                _textBoxFileName.Text = fileName;
            _btnSave.Enabled = false;
        }       
    }
}
