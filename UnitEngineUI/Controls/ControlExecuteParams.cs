using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonUI;
using UnitEngine;
using UnitEngine.Behavior;
using Common;

namespace UnitEngineUI
{
    public partial class ControlExecuteParams : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        ExecuteParameter _editItem;

        public ControlExecuteParams()
        {
            InitializeComponent();
        }

        public ExecuteParameter EditItem
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
        private void SetInstance(ExecuteParameter editItem)
        {
            _editItem = null;
            // Очищаем контролы
            FormWorker.ClearDataGrid(_dataGridViewParameters);

            if (editItem == null) return;

            FillParameters(editItem);
            _editItem = editItem;
        }

        /// <summary>
        /// Заполнить параметры
        /// </summary>
        private void FillParameters(ExecuteParameter executeParam)
        {
            if (executeParam == null) return;
            if (executeParam.Parameters.Count == 0) return;

            _labelTitle.Text = (executeParam.IsGlobal) ?
                "Глобальные параметры : " : "Параметры юнита";

            foreach (var parameter in executeParam.Parameters)
            {
                int rowIndex = _dataGridViewParameters.Rows.Add();
                var row = _dataGridViewParameters.Rows[rowIndex];
                row.Cells[1].Value = parameter.Name;
                row.Cells[2].Value = parameter.Value;
            }
        }


        /// <summary>
        /// Изменение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_ItemChanged(object sender, EventArgs e)
        {
            if (_editItem == null) return;

            _editItem.Parameters = ReadParameters(_dataGridViewParameters);

            if (Changed != null) Changed(_editItem);
        }

        /// <summary>
        /// Прочитать параметры условия из формы свойств
        /// </summary>
        /// <returns></returns>
        private List<Parameter> ReadParameters(DataGridView datagrid)
        {
            List<Parameter> parameters = new List<Parameter>();
            foreach (DataGridViewRow row in datagrid.Rows)
            {
                if (row.Cells[1].Value == null) continue;
                string name = row.Cells[1].Value.ToString();
                string value = (row.Cells[2].Value != null) ?
                    row.Cells[2].Value.ToString() : string.Empty;
                parameters.Add(new Parameter(name, value));
            }
            return parameters;
        }

        private void DataGridViewClauseParameters_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_editItem == null) return;
            Control_ItemChanged(sender, null);
        }

        private void DataGridViewClauseParameters_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (_editItem == null) return;
            Control_ItemChanged(sender, null);
            FormWorker.SetNullImageDataGrid(_dataGridViewParameters);
        }

        private void DataGridViewClauseParameters_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (_editItem == null) return;
            Control_ItemChanged(sender, null);
        }
    }
}
