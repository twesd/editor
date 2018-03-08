using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Serializable;
using CommonUI;
using UnitEngine;
using Common;

namespace UnitEngineUI.Behavior
{
    public partial class ControlClauseParams : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        UnitClause _editItem;

        public ControlClauseParams()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Отображать глобальные параметры 
        /// </summary>
        public bool ShowGlobalParams;

        public UnitClause EditItem
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
        private void SetInstance(UnitClause editItem)
        {
            _editItem = null;
            // Очищаем контролы
            FormWorker.ClearDataGrid(_dataGridViewClauseParameters);

            if (editItem == null) return;

            FillParameters(editItem);
            _editItem = editItem;
        }

        /// <summary>
        /// Заполнить параметры
        /// </summary>
        private void FillParameters(UnitClause clause)
        {
            if (clause == null) return;

            _labelTitle.Text = (ShowGlobalParams) ?
                "Глобальные параметры : " : "Параметры юнита";

            List<Parameter> unitParams;
            if (ShowGlobalParams)
                unitParams = clause.GlobalParameters;
            else
                unitParams = clause.Parameters;
            if (unitParams != null)
            {
                foreach (var parameter in unitParams)
                {
                    int rowIndex = _dataGridViewClauseParameters.Rows.Add();
                    var row = _dataGridViewClauseParameters.Rows[rowIndex];
                    row.Cells[1].Value = parameter.Name;
                    row.Cells[2].Value = parameter.Value;
                }
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
            
            if (ShowGlobalParams)
            {
                _editItem.GlobalParameters = 
                    ReadClauseParameters(_dataGridViewClauseParameters);
            }
            else
            {
                _editItem.Parameters = 
                    ReadClauseParameters(_dataGridViewClauseParameters);
            }

            if (Changed != null) Changed(_editItem);
        }

        /// <summary>
        /// Прочитать параметры условия из формы свойств
        /// </summary>
        /// <returns></returns>
        private List<Parameter> ReadClauseParameters(DataGridView datagrid)
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
            FormWorker.SetNullImageDataGrid(_dataGridViewClauseParameters);
        }

        private void DataGridViewClauseParameters_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (_editItem == null) return;
            Control_ItemChanged(sender, null);
        }
    }
}
