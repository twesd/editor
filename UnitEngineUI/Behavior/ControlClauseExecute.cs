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
using UnitEngine.Events;
using UnitEngineUI.Events;

namespace UnitEngineUI.Behavior
{
    public partial class ControlClauseExecute : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Плавающая панель для свойств
        /// </summary>
        FormFloatPanel _floatPanel;


        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        ExecuteBase _editItem;

        public ControlClauseExecute()
        {
            InitializeComponent();
        }

        public ExecuteBase EditItem
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
        private void SetInstance(ExecuteBase editItem)
        {
            _editItem = null;
            // Очищаем контролы
            FormWorker.ClearDataGrid(_dataGridViewClauses);
            _panelLocalParams.Controls.Clear();
            _panelGlobalParams.Controls.Clear();

            if (editItem == null) return;

            FillClauses(editItem);
            FillParameters(editItem);
            _editItem = editItem;
        }

        /// <summary>
        /// Заполняем параметры
        /// </summary>
        /// <param name="editItem"></param>
        private void FillParameters(ExecuteBase editItem)
        {
            ControlClauseParams control = new ControlClauseParams();
            control.ShowGlobalParams = false;
            control.EditItem = editItem.Clause;
            control.Changed += ChangedData;
            FormWorker.AddControl(_panelLocalParams, control);

            control = new ControlClauseParams();
            control.ShowGlobalParams = true;
            control.EditItem = editItem.Clause;
            control.Changed += ChangedData;
            FormWorker.AddControl(_panelGlobalParams, control);
        }

        void ChangedData(object oEditObj)
        {
            Control_ItemChanged(null, null);
        }

        /// <summary>
        /// Заполнить параметры
        /// </summary>
        private void FillClauses(ExecuteBase executeBase)
        {
            if (executeBase.Clause == null) return;
            // Заполняем события
            //
            foreach (UnitEventBase eventBase in executeBase.Clause.Events)
            {
                int rowIndex = _dataGridViewClauses.Rows.Add();
                var row = _dataGridViewClauses.Rows[rowIndex];
                row.Cells[0].Value = null;
                row.Cells[1].Value = eventBase;
                row.Tag = eventBase;
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

            _editItem.Clause.Events = ReadClauses(_dataGridViewClauses);

            if (Changed != null) Changed(_editItem);
        }

        /// <summary>
        /// Прочитать параметры условия из формы свойств
        /// </summary>
        /// <returns></returns>
        private List<UnitEventBase> ReadClauses(DataGridView datagrid)
        {
            var events = new List<UnitEventBase>();
            foreach (DataGridViewRow row in _dataGridViewClauses.Rows)
            {
                UnitEventBase eventBase = row.Tag as UnitEventBase;
                if (eventBase == null) continue;
                events.Add(eventBase);
            }
            return events;
        }

        private void DataGridViewClauseParameters_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (_editItem == null) return;
            Control_ItemChanged(sender, null);
        }

        private void DataGridViewClauseParameters_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (_editItem == null) return;
            Control_ItemChanged(sender, null);
        }

        private void CloseFloatPanel()
        {
            if (_floatPanel != null && !_floatPanel.IsDisposed)
                _floatPanel.Close();
            _floatPanel = null;
        }

        private void DataGridViewClause_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_editItem == null) return;
            if (e.RowIndex == -1) return;
            UnitEventBase eventBase = _dataGridViewClauses.Rows[e.RowIndex].Tag as UnitEventBase;
            if (eventBase == null) return;

            Control editControl = null;
            if (eventBase is UnitEventTimer)
            {
                var control = new ControlEventTimer();
                control.Changed += PropertyChanged;
                control.EditItem = eventBase as UnitEventTimer;
                editControl = control;
            }
            else
            {
                var control = new ControlStdProperties();               
                control.Changed += PropertyChanged;
                control.EditItem = eventBase;
                editControl = control;
            }

            if (editControl == null)
            {
                CloseFloatPanel();
                return;
            }

            if (_floatPanel == null || _floatPanel.IsDisposed)
            {
                _floatPanel = new FormFloatPanel();
            }
            _floatPanel.Text = "Свойства";
            _floatPanel.MainItem = editControl;
            _floatPanel.StartPosition = FormStartPosition.Manual;
            if (!_floatPanel.Visible)
            {
                var cursorPos = System.Windows.Forms.Cursor.Position;
                _floatPanel.Location = cursorPos - new Size(editControl.Width / 2, 5);
                _floatPanel.Show(FormWorker.GetParentForm(this));
            }
            _floatPanel.Focus();
        }

        void PropertyChanged(object oItem)
        {
            if (Changed != null) Changed(_editItem);
        }


        #region Меню событий

        private void MenuItemTime_Click(object sender, EventArgs e)
        {
            var eventClause = new UnitEventTimer();
            var control = new ControlEventTimer();
            control.EditItem = eventClause;

            if (FormWorker.ShowDialog("Время", control, FormWorker.GetParentForm(this)) != DialogResult.OK)
                return;
            AddClauseToDataGrid(eventClause);
        }


        private void MenuItemClauseDistance_Click(object sender, EventArgs e)
        {
            var eventClause = new UnitEventDistance();
            var control = new ControlStdProperties();
            control.EditItem = eventClause;
            
            if (FormWorker.ShowDialog("Расстояние", control, FormWorker.GetParentForm(this)) != DialogResult.OK)
                return;
            AddClauseToDataGrid(eventClause);
        }


        private void MenuItemAnimation_Click(object sender, EventArgs e)
        {
            AddClauseToDataGrid(new UnitEventAnimation());
        }

        private void MenuItemActionEnd_Click(object sender, EventArgs e)
        {
            AddClauseToDataGrid(new UnitEventActionEnd());
        }

        private void MenuItemOperator_Click(object sender, EventArgs e)
        {
            AddClauseToDataGrid(new UnitEventOperator());
        }

        private void MenuItemChildUnit_Click(object sender, EventArgs e)
        {
            AddClauseToDataGrid(new UnitEventChildUnit());
        }

        private void MenuItemScript_Click(object sender, EventArgs e)
        {
            AddClauseToDataGrid(new UnitEventScript());
        }

        private void AddClauseToDataGrid(UnitEventBase eventBase)
        {
            var row = _dataGridViewClauses.Rows[_dataGridViewClauses.Rows.Add()];
            FormWorker.SelectRow(row);
            row.Tag = eventBase;
            row.Cells[1].Value = eventBase;

            Control_ItemChanged(null, null);
        }

        #endregion




    }
}
