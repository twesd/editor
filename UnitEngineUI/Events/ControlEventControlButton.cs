using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnitEngine.Events;

namespace UnitEngineUI.Events
{
    /// <summary>
    /// Представление события от кнопки
    /// </summary>
    public partial class ControlEventControlButton : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Обрабатываемое событие
        /// </summary>
        UnitEventControlButton _eventButton;

        /// <summary>
        /// Возможные имена контролов для автозаполнения
        /// </summary>
        List<string> _controlsNames;

        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="controlsNames">Возможные имена контролов для автозаполнения</param>
        public ControlEventControlButton(List<string> controlsNames)
        {
            InitializeComponent();
            _controlsNames = controlsNames;

            _comboBoxState.Items.Add(UnitEventControlButtonState.Down);            
            _comboBoxState.Items.Add(UnitEventControlButtonState.Pressed);
            _comboBoxState.Items.Add(UnitEventControlButtonState.Hold);
            _comboBoxState.Items.Add(UnitEventControlButtonState.Up);

            if (_controlsNames != null && _controlsNames.Count > 0)
                _comboBoxName.Items.AddRange(_controlsNames.ToArray());
        }

        public UnitEventControlButton EditItem
        {
            get
            {
                return _eventButton;
            }
            set
            {
                SetEventButton(value);
            }
        }

        /// <summary>
        /// Назначить новое событие для отображения
        /// </summary>
        /// <param name="eventButton"></param>
        private void SetEventButton(UnitEventControlButton eventButton)
        {
            _eventButton = null;
            // Очищаем контролы
            _comboBoxName.Text = string.Empty;
            if (eventButton == null) return;

            _comboBoxState.SelectedItem = eventButton.State;
            _comboBoxName.Text = eventButton.ButtonName;

            _eventButton = eventButton;
        }

        /// <summary>
        /// Изменение события
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (_comboBoxState.SelectedItem == null) return;
            if (_eventButton == null) return;
            _eventButton.ButtonName = _comboBoxName.Text;
            if(_comboBoxState.Items.Count > 0)
                _eventButton.State = (UnitEventControlButtonState)_comboBoxState.SelectedItem;
            if (Changed != null) Changed(_eventButton);           
        }

    }
}
