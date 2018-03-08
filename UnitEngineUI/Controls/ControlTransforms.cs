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

namespace UnitEngineUI
{
    /// <summary>
    /// Редактирование трансформации
    /// </summary>
    public partial class ControlTransforms : UserControl
    {
        public delegate void OnChangeEventHandler(object oEdit);

        public OnChangeEventHandler Changed;

        ExecuteTransform _editItem;

        public ExecuteTransform EditItem
        {
            get
            {
                return _editItem;
            }
            set
            {
                ApplyTransform(value);
            }
        }


        public ControlTransforms()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Применить трансформацию
        /// </summary>
        /// <param name="value"></param>
        private void ApplyTransform(ExecuteTransform transform)
        {
            _editItem = null;

            ClearControls();

            // Устанавливаем значения в элементы формы
            SetTransform(transform);
            _editItem = transform;
        }

        /// <summary>
        /// Устанавливаем значения в элементы формы
        /// </summary>
        /// <param name="transform"></param>
        private void SetTransform(ExecuteTransform transform)
        {
            if (transform == null) return;
            _textBoxName.Text = transform.Name;
            _comboBoxType.Text = transform.Type.ToString();
            _checkBoxLoop.Checked = transform.Loop;
            _obstacleFilterId.Value = (decimal)transform.ObstacleFilterId;
            foreach (TransformItem point in transform.Items)
            {
                int indexRow = _dataGridViewPoints.Rows.Add();
                DataGridViewRow row = _dataGridViewPoints.Rows[indexRow];
                row.Cells[0].Value = point.X;
                row.Cells[1].Value = point.Y;
                row.Cells[2].Value = point.Z;
                row.Cells[3].Value = point.Time;
            }
        }

        /// <summary>
        /// Обновить данные трансформации
        /// </summary>
        private void UpdateTransform()
        {
            if (_editItem == null) return;
            _editItem.Name = _textBoxName.Text;
            _editItem.Type = (TransformType)_comboBoxType.SelectedItem;
            _editItem.Loop = _checkBoxLoop.Checked;
            _editItem.Items = GetItemsFromDataGrid();
            _editItem.ObstacleFilterId = Convert.ToInt32(_obstacleFilterId.Value);
            if (Changed != null) Changed(_editItem);
        }

        /// <summary>
        /// Получить данные из DataGrid
        /// </summary>
        /// <returns></returns>
        private List<TransformItem> GetItemsFromDataGrid()
        {
            List<TransformItem> outList = new List<TransformItem>();
            foreach (DataGridViewRow row in _dataGridViewPoints.Rows)
            {
                if (row.Cells[0].Value == null &&
                    row.Cells[1].Value == null &&
                    row.Cells[2].Value == null &&
                    row.Cells[3].Value == null)
                    continue;
                float x = Convert.ToSingle(row.Cells[0].Value, CultureInfo.InvariantCulture);
                float y = Convert.ToSingle(row.Cells[1].Value, CultureInfo.InvariantCulture);
                float z = Convert.ToSingle(row.Cells[2].Value, CultureInfo.InvariantCulture);
                UInt32 time = Convert.ToUInt32(row.Cells[3].Value);
                TransformItem item = new TransformItem(x, y, z, time);
                outList.Add(item);
            }
            return outList;
        }
        /// <summary>
        /// Очистить контролы
        /// </summary>
        private void ClearControls()
        {
            _textBoxName.Text = string.Empty;
            _comboBoxType.Items.Clear();
            _comboBoxType.Items.Add(TransformType.LINE);
            _comboBoxType.Items.Add(TransformType.ROTATE);
            _comboBoxType.Items.Add(TransformType.SCALE);
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
