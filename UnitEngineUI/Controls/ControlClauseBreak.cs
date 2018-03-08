using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnitEngine;
using CommonUI;
using Common;
using CommonUI.UITypeEditors;

namespace UnitEngineUI
{
    public partial class ControlClauseBreak : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        UnitActionBreak _editItem;

        public ControlClauseBreak()
        {
            InitializeComponent();

            _comboBoxAnimatorEnd.Items.Add(UnitEngine.UnitActionBreak.AnimatorType.None);
            _comboBoxAnimatorEnd.Items.Add(UnitEngine.UnitActionBreak.AnimatorType.Any);
            _comboBoxAnimatorEnd.Items.Add(UnitEngine.UnitActionBreak.AnimatorType.MoveToPoint);
            _comboBoxAnimatorEnd.Items.Add(UnitEngine.UnitActionBreak.AnimatorType.MoveToSceneNode);

        }

        public UnitActionBreak EditItem
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
        private void SetInstance(UnitActionBreak editItem)
        {
            _editItem = null;
            // Очищаем контролы
            FormWorker.SetEmptyValueForControl(this);

            if (editItem == null) return;

            _checkBoxAnimation.Checked = editItem.AnimationEnd;
            _checkBoxStartClauseNotApprove.Checked = editItem.StartClauseNotApproved;
            _checkBoxStartClauseApprove.Checked = editItem.StartClauseApproved;
            _checkBoxExecuteOnly.Checked = editItem.IsExecuteOnly;
            _comboBoxAnimatorEnd.SelectedItem = editItem.AnimatorEnd;
            _scriptFileName.Text = editItem.ScriptFileName;

            _editItem = editItem;
        }


        /// <summary>
        /// Изменение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_ItemChanged(object sender, EventArgs e)
        {
            if (_editItem == null) return;

            _editItem.AnimationEnd = _checkBoxAnimation.Checked;
            _editItem.StartClauseNotApproved = _checkBoxStartClauseNotApprove.Checked;
            _editItem.StartClauseApproved = _checkBoxStartClauseApprove.Checked;
            _editItem.IsExecuteOnly = _checkBoxExecuteOnly.Checked;
            _editItem.AnimatorEnd = (UnitEngine.UnitActionBreak.AnimatorType)_comboBoxAnimatorEnd.SelectedItem;
            _editItem.ScriptFileName = _scriptFileName.Text.Trim();

            if (Changed != null) Changed(_editItem);
        }

        private void _textBoxFileName_TextChanged(object sender, EventArgs e)
        {
            Control_ItemChanged(null, null);
        }

        private void _buttonDeleteScript_Click(object sender, EventArgs e)
        {
            _scriptFileName.Text = string.Empty;
        }

        private void _btnSelect_Click(object sender, EventArgs e)
        {
            var cntrlEditor = new ControlEditorScriptFileName(ParserFunctionNames.GetUnitsNames());
            cntrlEditor.EditItem = _scriptFileName.Text;
            cntrlEditor.Size = new Size(1024, 480);
            if (FormWorker.ShowDialog("Редактор скрипта", cntrlEditor) != System.Windows.Forms.DialogResult.OK)
                return;
            _scriptFileName.Text = cntrlEditor.EditItem;
        }

    }
}
