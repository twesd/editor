using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnitEngine;
using System.Globalization;
using UnitEngine.Behavior;
using Common;

namespace UnitEngineUI.Behavior
{
    /// <summary>
    /// Редактирование трансформации
    /// </summary>
    public partial class ControlExecuteColor : UserControl
    {
        public delegate void OnChangeEventHandler(object oEdit);

        public OnChangeEventHandler Changed;

        ExecuteColor _editItem;

        public ExecuteColor EditItem
        {
            get
            {
                return _editItem;
            }
            set
            {
                SetEditItem(value);
            }
        }


        public ControlExecuteColor()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Применить трансформацию
        /// </summary>
        /// <param name="value"></param>
        private void SetEditItem(ExecuteColor transform)
        {
            _editItem = null;

            ClearControls();

            // Устанавливаем значения в элементы формы
            if (transform == null) return;
            _checkBoxLoop.Checked = transform.Loop;
            foreach (TransformSColorItem item in transform.Items)
            {
                int indexRow = _dataGridViewPoints.Rows.Add();
                DataGridViewRow row = _dataGridViewPoints.Rows[indexRow];
                row.Cells[0].Value = item.Color.A;
                row.Cells[1].Value = item.Color.R;
                row.Cells[2].Value = item.Color.G;
                row.Cells[3].Value = item.Color.B;
                row.Cells[4].Value = item.Time;
            }
            _editItem = transform;
        }


        /// <summary>
        /// Обновить данные трансформации
        /// </summary>
        private void UpdateTransform()
        {
            if (_editItem == null) return;
            _editItem.Loop = _checkBoxLoop.Checked;
            _editItem.Items = GetItemsFromDataGrid();
            if (Changed != null) Changed(_editItem);
        }

        /// <summary>
        /// Получить данные из DataGrid
        /// </summary>
        /// <returns></returns>
        private List<TransformSColorItem> GetItemsFromDataGrid()
        {
            List<TransformSColorItem> outList = new List<TransformSColorItem>();
            foreach (DataGridViewRow row in _dataGridViewPoints.Rows)
            {
                if (row.Cells[0].Value == null &&
                    row.Cells[1].Value == null &&
                    row.Cells[2].Value == null &&
                    row.Cells[3].Value == null)
                    continue;
                SColor color = new SColor();
                color.A = Convert.ToUInt32(row.Cells[0].Value, CultureInfo.InvariantCulture);
                color.R = Convert.ToUInt32(row.Cells[1].Value, CultureInfo.InvariantCulture);
                color.G = Convert.ToUInt32(row.Cells[2].Value, CultureInfo.InvariantCulture);
                color.B = Convert.ToUInt32(row.Cells[3].Value, CultureInfo.InvariantCulture);
                UInt32 time = Convert.ToUInt32(row.Cells[4].Value);
                TransformSColorItem item = new TransformSColorItem(color, time);
                outList.Add(item);
            }
            return outList;
        }

        /// <summary>
        /// Очистить контролы
        /// </summary>
        private void ClearControls()
        {
            _dataGridViewPoints.Rows.Clear();
        }

        private void DataGridViewPoints_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateTransform();
        }

        private void DataGridViewPoints_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            UpdateTransform();
        }

        private void DataGridViewPoints_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            UpdateTransform();
        }

        private void ItemChanged(object sender, EventArgs e)
        {
            UpdateTransform();
        }

    }
}
