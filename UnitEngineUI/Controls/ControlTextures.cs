using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnitEngine.Behavior;
using CommonUI;

namespace UnitEngineUI
{
    public partial class ControlTextures : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        ExecuteTextures _editItem;

        public ControlTextures()
        {
            InitializeComponent();
        }

        public ExecuteTextures EditItem
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
        private void SetInstance(ExecuteTextures editItem)
        {
            _editItem = null;
            // Очищаем контролы
            FormWorker.SetEmptyValueForControl(this);

            if (editItem == null) return;

            // Устанавливаем значения в элементы формы
            //          

            _timePerFrame.Value = (decimal)editItem.TimePerFrame;
            _checkBoxLoop.Checked = editItem.Loop;
            _use32bits.Checked = editItem.Use32Bit;
            foreach (string path in editItem.Paths)
            {
                _dataGridView.Rows.Add(new object[] { path });
            }

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

            _editItem.TimePerFrame = Convert.ToUInt32(_timePerFrame.Value);
            _editItem.Loop = _checkBoxLoop.Checked;
            _editItem.Use32Bit = _use32bits.Checked;
            _editItem.Paths = new List<string>();
            foreach (DataGridViewRow row in _dataGridView.Rows)
            {
                string path = row.Cells[0].Value as string;
                if (string.IsNullOrEmpty(path)) continue;
                _editItem.Paths.Add(path);
            }

            if (Changed != null) Changed(_editItem);
        }

        private void DataGridView_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            Control_ItemChanged(sender, null);
        }

        private void DataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            Control_ItemChanged(sender, null);
        }

        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Control_ItemChanged(sender, null);
        }

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _dataGridView.EndEdit();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл изображения (*.png,*.jpeg,*.jpg)|*.png;*.jpeg;*.jpg|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть Файл изображения";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            if (GetCountRowsFilled() >= _dataGridView.Rows.Count - 1)
            {
                _dataGridView.Rows.Add();
            }

            var row = _dataGridView.Rows[e.RowIndex];
            row.Cells[0].Value = dialog.FileName;
        }

        /// <summary>
        /// Количество заполненых полей
        /// </summary>
        /// <returns></returns>
        private int GetCountRowsFilled()
        {
            int res = 0;
            foreach (DataGridViewRow row in _dataGridView.Rows)
            {
                string path = row.Cells[0].Value as string;
                if (string.IsNullOrEmpty(path)) continue;
                res++;
            }
            return res;
        }

        private void ButtonUp_Click(object sender, EventArgs e)
        {
            if (_dataGridView.SelectedRows.Count == 0) return;
            if (_dataGridView.Rows.Count < 2) return;
            var curRow = _dataGridView.SelectedRows[0];
            if (curRow.Index == 0) return;
            int index = curRow.Index;
            _dataGridView.Rows.RemoveAt(index);
            _dataGridView.Rows.Insert(index - 1, curRow);
        }

        private void ButtonDown_Click(object sender, EventArgs e)
        {
            if (_dataGridView.SelectedRows.Count == 0) return;
            if (_dataGridView.Rows.Count < 2) return;
            var curRow = _dataGridView.SelectedRows[0];
            if (curRow.Index == _dataGridView.Rows.Count - 1) return;
            int index = curRow.Index;
            _dataGridView.Rows.RemoveAt(index);
            _dataGridView.Rows.Insert(index + 1);
        }

        private void MenuItemAddTextures_Click(object sender, EventArgs e)
        {

            _dataGridView.EndEdit();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл изображения (*.png,*.jpeg,*.jpg)|*.png;*.jpeg;*.jpg|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть Файл изображения";
            dialog.Multiselect = true;
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            foreach (string fileName in dialog.FileNames)
            {
                int index = _dataGridView.Rows.Add();
                var row = _dataGridView.Rows[index];
                row.Cells[0].Value = fileName;
            }
        }

        private void ControlTextures_Load(object sender, EventArgs e)
        {

        }
    }
}
