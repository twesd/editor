using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnitEngine.Behavior;
using CommonUI;
using Common;
using System.IO;

namespace UnitEngineUI.Behavior
{
    public partial class ControlExecuteScript : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        ExecuteScript _editItem;

        public ControlExecuteScript()
        {
            InitializeComponent();

            TextBoxAutoComplete autocomplete = new TextBoxAutoComplete();
            autocomplete.Init(_textBoxScript, ParserFunctionNames.GetUnitsNames());

            _btnSave.Enabled = false;
        }

        public ExecuteScript EditItem
        {
            get
            {
                return _editItem;
            }
            set
            {
                SetInstance(value);
            }
        }

        /// <summary>
        /// Назначить новый объект для отображения
        /// </summary>
        /// <param name="editItem"></param>
        private void SetInstance(ExecuteScript editItem)
        {
            _editItem = null;

            // Очищаем контролы
            FormWorker.SetEmptyValueForControl(this);

            if (editItem == null) return;

            _textBoxFileName.Text = editItem.ScriptFileName;
            _textBoxScript.Text = UtilFile.ReadTextFile(editItem.ScriptFileName);
            _btnSave.Enabled = false;

            _editItem = editItem;
        }

        private void TextBoxScript_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (_editItem == null) return;

            _btnSave.Enabled = true;
        }

        private void _btnSave_Click(object sender, EventArgs e)
        {
            UtilFile.WriteTextFile(_editItem.ScriptFileName, _textBoxScript.Text);
            _btnSave.Enabled = false;
        }

        private void _textBoxFileName_TextChanged(object sender, EventArgs e)
        {
            if (_editItem == null) return;
            _btnSave.Enabled = false;
            _editItem.ScriptFileName = _textBoxFileName.Text;
            _textBoxScript.Text = UtilFile.ReadTextFile(_textBoxFileName.Text);
            if (Changed != null) Changed(_editItem);
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
