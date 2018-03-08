using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonUI;
using UnitEngine.Events;

namespace UnitEngineUI.Events
{
    public partial class ControlEventTimer : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        UnitEventTimer _editItem;

        public ControlEventTimer()
        {
            InitializeComponent();
        }

        public UnitEventTimer EditItem
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
        private void SetInstance(UnitEventTimer editItem)
        {
            _editItem = null;

            // Очищаем контролы
            FormWorker.SetEmptyValueForControl(this);

            if (editItem == null) return;
            _interval.Value = (decimal)editItem.Interval;
            _checkBoxLoop.Checked = editItem.Loop;

            _editItem = editItem;
        }

        /// <summary>
        /// Изменение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_ItemChanged(object sender, EventArgs e)
        {
            if(_editItem == null) return;

            _editItem.Interval = Convert.ToUInt32(_interval.Value);
            _editItem.Loop = _checkBoxLoop.Checked;

            if (Changed != null) Changed(_editItem);
        }
    }
}
