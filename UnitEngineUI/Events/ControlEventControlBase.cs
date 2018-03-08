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

namespace UnitEngineUI.Events
{
    public partial class ControlEventControlBase : UserControl
    {
        /// <summary>
        /// Типы контролов
        /// </summary>
        public enum UnitEventControlType
        {
            Button,
            TapScene,
            Circle
        }

        /// <summary>
        /// Редактируемое событие
        /// </summary>
        UnitEventBase _eventControl;

        /// <summary>
        /// Возможные имена контролов для автозаполнения
        /// </summary>
        List<string> _controlsNames;

        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="controlsNames">Возможные имена контролов для автозаполнения</param>
        public ControlEventControlBase(List<string> controlsNames)
        {
            InitializeComponent();

            _controlsNames = controlsNames;

            _comboBoxType.Items.Add(UnitEventControlType.Button);
            _comboBoxType.Items.Add(UnitEventControlType.TapScene);
            _comboBoxType.Items.Add(UnitEventControlType.Circle);            
        }

        /// <summary>
        /// Редактируемое событие
        /// </summary>
        public UnitEventBase EditItem
        {
            get
            {
                return _eventControl;
            }
            set
            {
                SetDataFromEvent(value);
            }
        }

        /// <summary>
        /// Установить редактируемое событие
        /// </summary>
        /// <param name="eventControl"></param>
        private void SetDataFromEvent(UnitEventBase eventControl)
        {
            _panelControlDesc.Controls.Clear();

            _eventControl = eventControl;
            _comboBoxType.SelectedItem = GetControlType(eventControl);
            ShowControlProperties();
        }

        /// <summary>
        /// Получить тип контрола
        /// </summary>
        /// <param name="eventControl"></param>
        /// <returns></returns>
        UnitEventControlType GetControlType(UnitEventBase eventControl)
        {
            if (eventControl is UnitEventControlButton)
                return UnitEventControlType.Button;
            else if (eventControl is UnitEventControlTapScene)
                return UnitEventControlType.TapScene;
            else if (eventControl is UnitEventControlCircle)
                return UnitEventControlType.Circle; 
            else
                throw new NotSupportedException();
        }

        /// <summary>
        /// Изменение данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void СomboBoxType_TextChanged(object sender, EventArgs e)
        {
            if (_eventControl == null) return;
            if (_comboBoxType.SelectedItem == null) return;
            UnitEventControlType controlType = (UnitEventControlType)_comboBoxType.SelectedItem;
            if (controlType == GetControlType(_eventControl)) return;
            if (controlType == UnitEventControlType.Button)
            {
                _eventControl = new UnitEventControlButton("", UnitEventControlButtonState.Up);
            }
            else if (controlType == UnitEventControlType.TapScene)
            {
                _eventControl = new UnitEventControlTapScene();
            }
            else if (controlType == UnitEventControlType.Circle)
            {
                _eventControl = new UnitEventControlCircle();
            }
            else
            {
                throw new NotImplementedException();
            }
            ShowControlProperties();
            if (Changed != null)
                Changed(_eventControl);
        }

        /// <summary>
        /// Показать свойства контрола
        /// </summary>
        private void ShowControlProperties()
        {
            if (_eventControl == null) return;
            
            if (_eventControl is UnitEventControlButton)
            {
                // Событие от кнопки
                var controlEventButton = new ControlEventControlButton(_controlsNames);
                controlEventButton.Changed += ChildChanged;
                controlEventButton.EditItem = _eventControl as UnitEventControlButton;
                FormWorker.AddControl(_panelControlDesc, controlEventButton);
            }
            else if (_eventControl is UnitEventControlTapScene)
            {
                // Событие от клика по сцене
                var controlEventTapScene = new ControlEventControlTapScene(_controlsNames);
                controlEventTapScene.Changed += ChildChanged;
                controlEventTapScene.EditItem = _eventControl as UnitEventControlTapScene;
                FormWorker.AddControl(_panelControlDesc, controlEventTapScene);
            }
            else
            {
                var cntrl = new ControlStdProperties();
                cntrl.Changed += ChildChanged;
                cntrl.EditItem = _eventControl;
                FormWorker.AddControl(_panelControlDesc, cntrl);
            }
        }

        /// <summary>
        /// Событие изменения дочерних объектов
        /// </summary>
        /// <param name="oEvent"></param>
        void ChildChanged(object oEvent)
        {
            if (oEvent == null) return;
            if (Changed != null) Changed(_eventControl);
        }

    }
}
