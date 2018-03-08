using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StageEngineUI.Controls
{
    public partial class ControlPathsEditing : UserControl
    {
        public List<string> Result
        {
            get
            {
                return GetPaths();
            }
        }

        /// <summary>
        /// Расширение файлов
        /// </summary>
        string _extensions;

        /// <summary>
        /// Получить пути из DataGrid
        /// </summary>
        /// <returns></returns>
        private List<string> GetPaths()
        {
            var paths = new List<string>();
            foreach (DataGridViewRow row in _dataGridView.Rows)
            {
                string path = row.Cells[0].Value as string;
                if (string.IsNullOrEmpty(path)) continue;
                paths.Add(path);
            }
            return paths;
        }

        public ControlPathsEditing(List<string> paths, string extensions)
        {
            InitializeComponent();
            _extensions = extensions;
            foreach (string path in paths)
            {
                int index = _dataGridView.Rows.Add();
                var row = _dataGridView.Rows[index];
                row.Cells[0].Value = path;
            }
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

        /// <summary>
        /// Добавить текстуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddTextures_Click(object sender, EventArgs e)
        {
            _dataGridView.EndEdit();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл модели " + _extensions +
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

        private void DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _dataGridView.EndEdit();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл модели " + _extensions +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть файл модели";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            if (GetCountRowsFilled() >= _dataGridView.Rows.Count - 1)
            {
                _dataGridView.Rows.Add();
            }

            var row = _dataGridView.Rows[e.RowIndex];
            row.Cells[0].Value = dialog.FileName;
        }
    }
}
