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

namespace UnitEngineUI.Behavior
{
    public partial class ControlAnimation : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        UnitAnimation _editItem;

        public ControlAnimation()
        {
            InitializeComponent();
        }

        public UnitAnimation EditItem
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
        private void SetInstance(UnitAnimation editItem)
        {
            _editItem = null;
            // Очищаем контролы
            FormWorker.SetEmptyValueForControl(this);

            if (editItem == null) return;

            // Устанавливаем значения в элементы формы
            //          
            _nmrStartFrame.Value = editItem.StartFrame;
            _nmrEndFrame.Value = editItem.EndFrame;
            _nmrAnimSpeed.Value = editItem.Speed;
            _checkBoxRepeat.Checked = editItem.Loop;
            _checkBoxEnabled.Checked = editItem.Enabled;

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

            _editItem.StartFrame = Convert.ToInt32(_nmrStartFrame.Value);
            _editItem.EndFrame = Convert.ToInt32(_nmrEndFrame.Value);
            _editItem.Speed = Convert.ToInt32(_nmrAnimSpeed.Value);
            _editItem.Loop = _checkBoxRepeat.Checked;
            _editItem.Enabled = _checkBoxEnabled.Checked;

            if (Changed != null) Changed(_editItem);
        }

        private void СheckBoxEnabled_CheckedChanged(object sender, EventArgs e)
        {
            _panelSetting.Enabled = _checkBoxEnabled.Checked;
            Control_ItemChanged(sender, e);
        }
    }
}
