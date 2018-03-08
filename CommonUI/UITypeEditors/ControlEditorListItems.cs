using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace CommonUI.UITypeEditors
{
    public partial class ControlEditorListItems : UserControl
    {
        /// <summary>
        /// Операция создания нового объекта
        /// </summary>
        public delegate object NewItemProc(string actionName);

        /// <summary>
        /// Операция создания нового объекта
        /// </summary>
        NewItemProc _newItemProc;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        IEnumerable _editItem;

        /// <summary>
        /// Редактируемы объект
        /// </summary>
        public IEnumerable EditItem
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

        public ControlEditorListItems(IEnumerable editItem, NewItemProc newItemHandler, List<string> actionNames)
        {
            InitializeComponent();
            ClearPropertyGrid();
            _newItemProc = newItemHandler;

            ContextMenuStrip contextMenu = new ContextMenuStrip();

            this.Name = "_contextMenuAddItems";
            this.AutoSize = true;

            ToolStripMenuItem menuItem;

            if (actionNames == null) actionNames = new List<string>() { "Добавить" };

            foreach (string text in actionNames)
            {
                menuItem = new ToolStripMenuItem();
                menuItem.Text = text;
                menuItem.Click += new System.EventHandler(MenuItem_Click);
                contextMenu.Items.Add(menuItem);
            }
            _dataGridItems.ContextMenuStrip = contextMenu;

            SetEditItem(editItem);

            FormKeysWorker keyWorker = new FormKeysWorker(this);
            keyWorker.EscEnterEvent(Apply);
        }

        /// <summary>
        /// Применить изменения
        /// </summary>
        void Apply()
        {
            var form = FormWorker.GetParentForm(this);
            form.DialogResult = DialogResult.OK;                        
        }

        /// <summary>
        /// Назначить новый объект для отображения
        /// </summary>
        /// <param name="editItem"></param>
        private void SetEditItem(IEnumerable editItem)
        {
            _editItem = null;

            // Очищаем контролы
            FormWorker.SetEmptyValueForControl(this);

            if (editItem == null) return;

            foreach (var item in editItem)
            {
                int rowIndex = _dataGridItems.Rows.Add();
                var row = _dataGridItems.Rows[rowIndex];
                row.Cells[0].Value = item;
                row.Tag = item;
            }

            _editItem = editItem;

            if (_dataGridItems.Rows.Count > 0)
            {
                FormWorker.SelectRow(_dataGridItems.Rows[0]);
                ShowItem(_dataGridItems.Rows[0].Tag);
            }
        }

        /// <summary>
        /// Изменение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_ItemChanged(object sender, EventArgs e)
        {
            if(_editItem == null) return;

            _editItem = ReadItems();
            _dataGridItems.Refresh();
       
        }

        /// <summary>
        /// Чтение описаний действий
        /// </summary>
        /// <returns></returns>
        private IEnumerable ReadItems()
        {
            List<object> items = new List<object>();
            foreach (DataGridViewRow row in _dataGridItems.Rows)
            {
                if (row.Tag == null) continue;
                items.Add(row.Tag);
            }
            return items;
        }

        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_editItem == null) return;
            Control_ItemChanged(sender, null);
        }

        private void DataGridView_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (_editItem == null) return;
            Control_ItemChanged(sender, null);
            FormWorker.SetNullImageDataGrid(sender as DataGridView);
        }

        private void DataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (_editItem == null) return;
            Control_ItemChanged(sender, null);
        }

        private void DataGridViewDesc_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ClearPropertyGrid();
            if(e.RowIndex < 0) return;
            var dataGrid = sender as DataGridView;
            var row = dataGrid.Rows[e.RowIndex];            
            ShowItem(row.Tag);
        }

        private void ClearPropertyGrid()
        {
            _propertyGrid.Enabled = false;
            _propertyGrid.SelectedObject = null;
        }

        private void ShowItem(object extActDesc)
        {
            _propertyGrid.Enabled = true;
            _propertyGrid.SelectedObject = extActDesc;
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Control_ItemChanged(s, null);
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            if (_newItemProc == null) return;
            var item = _newItemProc((sender as ToolStripMenuItem).Text);
            if (item == null) return;

            int rowIndex = _dataGridItems.Rows.Add();
            var row = _dataGridItems.Rows[rowIndex];
            row.Tag = item;
            row.Cells[0].Value = item;

            FormWorker.SelectRow(row);
            ShowItem(row.Tag);
        }
    }
}
