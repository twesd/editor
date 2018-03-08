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
    public partial class ControlExecuteSetData : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        ExecuteSetData _editItem;

        /// <summary>
        /// Редактор поведения
        /// </summary>
        EditorBehavior _editorBehavior;

        public ControlExecuteSetData(EditorBehavior editorBehavior)
        {
            InitializeComponent();

            _editorBehavior = editorBehavior;
        }

        public ExecuteSetData EditItem
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
        private void SetInstance(ExecuteSetData editItem)
        {
            _editItem = null;

            // Очищаем контролы
            FormWorker.SetEmptyValueForControl(this);

            if (editItem == null) return;

            foreach (var exc in editItem.DataItems)
            {
                int rowIndex = _dataGridItems.Rows.Add();
                var row = _dataGridItems.Rows[rowIndex];
                row.Cells[0].Value = exc;
                row.Tag = exc;
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

            _editItem.DataItems = ReadItems();
            _dataGridItems.Refresh();

            if (Changed != null) Changed(_editItem);
        }

        /// <summary>
        /// Чтение описаний действий
        /// </summary>
        /// <returns></returns>
        private List<ExecuteSetDataItem> ReadItems()
        {
            List<ExecuteSetDataItem> items = new List<ExecuteSetDataItem>();
            foreach (DataGridViewRow row in _dataGridItems.Rows)
            {
                if (row.Tag == null) continue;
                var extDesc = row.Tag as ExecuteSetDataItem;
                if (extDesc == null) continue;
                items.Add(extDesc);
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
            _propertyGrid.SelectedObject = null;
            if (e.RowIndex < 0) return;
            var dataGrid = sender as DataGridView;
            var row = dataGrid.Rows[e.RowIndex];
            ShowItemProperties(row.Tag as ExecuteSetDataItem);
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Control_ItemChanged(s, null);
        }

        /// <summary>
        /// Выбрать по событию клика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemTapScene_Click(object sender, EventArgs e)
        {
            var dataItem = new ExecuteSetDataItem()
            {
                Name = "TapControl",
                DataGetter = new DataGetterTapControl()
            };
            NewDataItem(dataItem);
        }

        /// <summary>
        /// Выборка по индентификатору
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemId_Click(object sender, EventArgs e)
        {
            var dataItem = new ExecuteSetDataItem()
            {
                Name = "NodeId",
                DataGetter = new DataGetterId()
            };
            NewDataItem(dataItem);
        }

        /// <summary>
        /// Выборка по расстоянию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDist_Click(object sender, EventArgs e)
        {
            var dataItem = new ExecuteSetDataItem()
            {
                Name = "Distance",
                DataGetter = new DataGetterDistance()
            };
            NewDataItem(dataItem);
        }


        private void NewDataItem(ExecuteSetDataItem item)
        {
            if (item.DataGetter == null) throw new ArgumentNullException();
            var row = _dataGridItems.Rows[_dataGridItems.Rows.Add()];
            FormWorker.SelectRow(row);
            row.Tag = item;
            row.Cells[0].Value = item;

            Control_ItemChanged(null, null);
            ShowItemProperties(item);
        }


        /// <summary>
        /// Отобразить свойства действия
        /// </summary>
        /// <param name="execute"></param>
        private void ShowItemProperties(ExecuteSetDataItem execute)
        {
            _propertyGrid.SelectedObject = null;

            if (execute == null) return;

            _propertyGrid.SelectedObject = execute;
        }



    }
}
