using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnitEngine.Events;
using CommonUI;

namespace UnitEngineUI.Events
{
    public partial class ControlEventControlTapScene : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Возможные имена контролов для автозаполнения
        /// </summary>
        List<string> _controlsNames;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        UnitEventControlTapScene _editItem;

        public ControlEventControlTapScene(List<string> controlsNames)
        {
            InitializeComponent();
            _controlsNames = controlsNames;
            if (_controlsNames != null && _controlsNames.Count > 0)
                _comboBoxName.Items.AddRange(_controlsNames.ToArray());

            _comboBoxState.Items.Add(UnitEventControlButtonState.Down);
            _comboBoxState.Items.Add(UnitEventControlButtonState.Pressed);
            _comboBoxState.Items.Add(UnitEventControlButtonState.Up);
            _comboBoxState.SelectedIndex = 0;
        }

        public UnitEventControlTapScene EditItem
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
        private void SetInstance(UnitEventControlTapScene editItem)
        {
            _editItem = null;
            // Очищаем контролы
            FormWorker.SetEmptyValueForControl(this);

            if (editItem == null) return;

            // Устанавливаем значения в элементы формы
            //          
            _comboBoxName.Text = editItem.TapSceneName;            
            _comboBoxState.SelectedItem = editItem.State;
            _checkBoxIgnoreNode.Checked = editItem.IgnoreNode;
            _checkBoxIdentNode.Checked = editItem.IdentNode;
            _filterId.Value = (decimal)editItem.FilterId;
            _comboBoxDataName.Text = editItem.DataName;

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

            _editItem.TapSceneName = _comboBoxName.Text;
            _editItem.IgnoreNode = _checkBoxIgnoreNode.Checked;
            _editItem.IdentNode = _checkBoxIdentNode.Checked;
            _editItem.State = (UnitEventControlButtonState)_comboBoxState.SelectedItem;
            _editItem.FilterId = Convert.ToInt32(_filterId.Value);
            _editItem.DataName = _comboBoxDataName.Text;
            if (Changed != null) Changed(_editItem);
        }

        private void CheckBoxIgnoreNode_CheckedChanged(object sender, EventArgs e)
        {
            _checkBoxIdentNode.Enabled = !_checkBoxIgnoreNode.Checked;
            Control_ItemChanged(sender, e);
        }

    }
}
