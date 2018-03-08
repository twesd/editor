using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnitEngine.Events;
using UnitEngine;
using CommonUI;
using StageEngine;

namespace StageEngineUI.Controls
{
    public partial class ControlEditingCameraBase : UserControl
    {
        /// <summary>
        /// Редактируемый объект
        /// </summary>
        UnitInstanceCamera _editItem;


        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Иницилизация
        /// </summary>
        public ControlEditingCameraBase()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Редактируемое событие
        /// </summary>
        public UnitInstanceCamera EditItem
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

        /// <summary>
        /// Установить редактируемое поведение
        /// </summary>
        /// <param name="editItem"></param>
        private void SetEditItem(UnitInstanceCamera editItem)
        {
            _panelControlDesc.Controls.Clear();

            _editItem = editItem;

            if (!(editItem.Behavior is CameraBehaviorStatic))
                _comboBoxType.Items.Add(new CameraBehaviorStatic());
            if (!(editItem.Behavior is CameraBehaviorFollowToNode))
                _comboBoxType.Items.Add(new CameraBehaviorFollowToNode());

            _comboBoxType.Items.Add(editItem.Behavior);
            _comboBoxType.SelectedItem = editItem.Behavior;

            ShowControlProperties();
        }

        /// <summary>
        /// Изменение данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void СomboBoxType_TextChanged(object sender, EventArgs e)
        {
            if (_editItem == null) return;
            if (_comboBoxType.SelectedItem == null) return;
            _editItem.Behavior = _comboBoxType.SelectedItem as CameraBehaviorBase;
            ShowControlProperties();
            if (Changed != null)
                Changed(_editItem);
        }

        /// <summary>
        /// Показать свойства контрола
        /// </summary>
        private void ShowControlProperties()
        {
            if (_editItem == null) return;

            _panelControlDesc.Controls.Clear();

            var cntrl = new ControlStdProperties();
            cntrl.Changed += ChildChanged;
            cntrl.EditItem = _editItem.Behavior;
            FormWorker.AddControl(_panelControlDesc, cntrl);

            _propertyGrid.SelectedObject = _editItem;

        }

        /// <summary>
        /// Событие изменения дочерних объектов
        /// </summary>
        /// <param name="oEvent"></param>
        void ChildChanged(object oEvent)
        {
            if (oEvent == null) return;
            if (Changed != null) Changed(_editItem);
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (Changed != null) Changed(_editItem);
        }

    }
}
