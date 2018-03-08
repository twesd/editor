using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnitEngine;
using Serializable;
using StageEngine;

namespace StageEngineUI.Creations
{
    public partial class ControlUnitCreationTimer : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        UnitCreationTimer _editItem;

        public ControlUnitCreationTimer()
        {
            InitializeComponent();
        }

        public UnitCreationTimer EditItem
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
        private void SetInstance(UnitCreationTimer editItem)
        {
            _editItem = null;
            // Очищаем контролы
            _startTime.Value = 0;
            _endTime.Value = 0;
            _interval.Value = 0;

            if (editItem == null) return;

            _startTime.Value = (decimal)editItem.StartTime;
            _endTime.Value = (decimal)editItem.EndTime;
            _interval.Value = (decimal)editItem.Interval;

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
            _editItem.StartTime = Convert.ToUInt32(_startTime.Value);
            _editItem.EndTime = Convert.ToUInt32(_endTime.Value);
            _editItem.Interval = Convert.ToUInt32(_interval.Value);
            if (Changed != null) Changed(_editItem);
        }
    }
}
