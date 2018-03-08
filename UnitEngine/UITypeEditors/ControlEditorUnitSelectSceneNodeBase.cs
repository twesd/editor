using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UnitEngine.UITypeEditors
{
    public partial class ControlEditorUnitSelectSceneNodeBase : UserControl
    {
        /// <summary>
        /// Установить редактируемый объект
        /// </summary>
        public object EditObject
        {
            get
            {
                return _propertyGrid.SelectedObject;
            }
            set
            {
                _propertyGrid.SelectedObject = null;
                UpdateType(value);
                _propertyGrid.SelectedObject = value;
            }
        }

        private void UpdateType(object oEdit)
        {
            if (oEdit is UnitSelectSceneNodeDistance)
            {
                _comboBoxType.SelectedItem = "Расстояние";
            }
            else if (oEdit is UnitSelectSceneNodeTapControl)
            {
                _comboBoxType.SelectedItem = "TapControl";
            }
            else if (oEdit is UnitSelectSceneNodeData)
            {
                _comboBoxType.SelectedItem = "Данные";
            }
            else if (oEdit is UnitSelectSceneNodeId)
            {
                _comboBoxType.SelectedItem = "Индентификатор";
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public ControlEditorUnitSelectSceneNodeBase()
        {
            InitializeComponent();
            _comboBoxType.Items.Add("Расстояние");
            _comboBoxType.Items.Add("TapControl");
            _comboBoxType.Items.Add("Данные");
            _comboBoxType.Items.Add("Индентификатор");
            _comboBoxType.SelectedIndex = 0;
        }

        private void ComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_propertyGrid.SelectedObject == null) return;
            string selectedItem = _comboBoxType.SelectedItem.ToString();
            if (selectedItem == "Расстояние")
            {
                _propertyGrid.SelectedObject = new UnitSelectSceneNodeDistance();
            }
            else if (selectedItem == "TapControl")
            {
                _propertyGrid.SelectedObject = new UnitSelectSceneNodeTapControl();
            }
            else if (selectedItem == "Данные")
            {
                _propertyGrid.SelectedObject = new UnitSelectSceneNodeData();
            }
            else if (selectedItem == "Индентификатор")
            {
                _propertyGrid.SelectedObject = new UnitSelectSceneNodeId();
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
