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
    public partial class ControlEditorParameters : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        List<Parameter> _editItem;

        public ControlEditorParameters()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Отображать глобальные параметры 
        /// </summary>
        public bool ShowGlobalParams;

        public List<Parameter> EditItem
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
        private void SetInstance(List<Parameter> editItem)
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
        private void FillParameters(List<Parameter> parameters)
        {
            if (parameters == null) return;

            foreach (var parameter in parameters)
            {
                int rowIndex = _dataGridViewClauseParameters.Rows.Add();
                var row = _dataGridViewClauseParameters.Rows[rowIndex];
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

            _editItem = ReadParameters(_dataGridViewClauseParameters);

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
            FormWorker.SetNullImageDataGrid(_dataGridViewClauseParameters);
        }

        private void DataGridViewClauseParameters_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (_editItem == null) return;
            Control_ItemChanged(sender, null);
        }
    }
}
