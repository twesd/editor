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
using UnitEngine;

namespace UnitEngineUI.Behavior
{
    public partial class ControlMaterial : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        ExecuteMaterial _editItem;

        public ControlMaterial()
        {
            InitializeComponent();
            _comboBoxType.Items.Add(MaterialType.Solid);
            _comboBoxType.Items.Add(MaterialType.Transparent);
            _comboBoxType.Items.Add(MaterialType.AddColor);
            _comboBoxType.Items.Add(MaterialType.VertexAlpha);
            _comboBoxType.SelectedIndex = 0;
        }

        public ExecuteMaterial EditItem
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
        private void SetInstance(ExecuteMaterial editItem)
        {
            _editItem = null;
            // Очищаем контролы
            FormWorker.SetEmptyValueForControl(this);

            if (editItem == null) return;

            // Устанавливаем значения в элементы формы
            //          

            _comboBoxType.SelectedItem = editItem.Type;

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

            _editItem.Type = (MaterialType)_comboBoxType.SelectedItem;
            
            if (Changed != null) Changed(_editItem);
        }
    }
}
