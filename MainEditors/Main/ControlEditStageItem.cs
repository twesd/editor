using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonUI;
using Common;

namespace MainEditors.Main
{
    public partial class ControlEditStageItem : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;
        
        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        StageItem _editItem;

        public ControlEditStageItem()
        {
            InitializeComponent();

            TextBoxAutoComplete autocomplete = new TextBoxAutoComplete();
            autocomplete.Init(_scriptAfterComplete, ParserFunctionNames.GetUnitsNames());
        }

        public StageItem EditItem
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
        private void SetInstance(StageItem editItem)
        {
            _editItem = null;

            // Очищаем контролы
            FormWorker.SetEmptyValueForControl(this);

            if (editItem == null) return;

            _stagePath.Text = editItem.Path;
            _scriptAfterComplete.Text = editItem.ScriptOnComplete;

            _editItem = editItem;
        }

        private void TextBoxScript_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (_editItem == null) return;

            ReadData();

            if (Changed != null) Changed(_editItem);
        }

        /// <summary>
        /// Изменение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_ItemChanged(object sender, EventArgs e)
        {
            if (_editItem == null) return;

            ReadData();

            if (Changed != null) Changed(_editItem);
        }

        private void ReadData()
        {
            if (_editItem == null) return;
            _editItem.Path = _stagePath.Text;
            _editItem.ScriptOnComplete = _scriptAfterComplete.Text;
        }

        private void SelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл стадии (*.stage)|*.stage|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть файл стадии";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            _stagePath.Text = dialog.FileName;
        }       
    }
}
