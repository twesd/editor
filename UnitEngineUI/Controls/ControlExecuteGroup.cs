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
using Serializable;
using UnitEngine;

namespace UnitEngineUI
{
    public partial class ControlExecuteGroup : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        ExecuteGroup _editItem;

        /// <summary>
        /// Редактор поведения
        /// </summary>
        EditorBehaviorInspector _inspector;

        public ControlExecuteGroup(EditorBehaviorInspector editorBehavior, UnitBehavior container)
        {
            InitializeComponent();
            
            _inspector = editorBehavior;

            ContextMenuExecutes contextMenu = new ContextMenuExecutes(container);
            contextMenu.NewBehaviorExecute += new ContextMenuExecutes.NewItemHadler(NewBehaviorExecute);
            _dataGridItems.ContextMenuStrip = contextMenu;
        }

        public ExecuteGroup EditItem
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
        private void SetInstance(ExecuteGroup editItem)
        {
            _editItem = null;

            // Очищаем контролы
            FormWorker.SetEmptyValueForControl(this);

            if (editItem == null) return;

            foreach (var exc in editItem.Executes)
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
            if(_editItem == null) return;

            _editItem.Executes = ReadExecutesBase();
            _dataGridItems.Refresh();

            if (Changed != null) Changed(_editItem);
        }

        /// <summary>
        /// Чтение описаний действий
        /// </summary>
        /// <returns></returns>
        private List<ExecuteBase> ReadExecutesBase()
        {
            List<ExecuteBase> items = new List<ExecuteBase>();
            foreach (DataGridViewRow row in _dataGridItems.Rows)
            {
                if (row.Tag == null) continue;
                var extDesc = row.Tag as ExecuteBase;
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
            _panelProps.Controls.Clear();
            if(e.RowIndex < 0) return;
            var dataGrid = sender as DataGridView;
            var row = dataGrid.Rows[e.RowIndex];
            ShowExecuteProperties(row.Tag as ExecuteBase);
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Control_ItemChanged(s, null);
        }

        private void NewBehaviorExecute(ExecuteBase execute)
        {
            var row = _dataGridItems.Rows[_dataGridItems.Rows.Add()];
            FormWorker.SelectRow(row);
            row.Tag = execute;
            row.Cells[0].Value = execute;

            Control_ItemChanged(null, null);
            ShowExecuteProperties(execute);
        }

        /// <summary>
        /// Отобразить свойства действия
        /// </summary>
        /// <param name="execute"></param>
        private void ShowExecuteProperties(ExecuteBase execute)
        {
            _panelProps.Controls.Clear();

            if (execute == null) return;
            
            // Показываем основные свойства
            //            
            FormWorker.AddControl(_panelProps, _inspector.GetControlForExecute(execute));
        }

    }
}
