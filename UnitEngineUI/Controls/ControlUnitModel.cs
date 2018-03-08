using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnitEngine;

namespace UnitEngineUI
{
    public partial class ControlUnitModel : UserControl
    {
        public UnitModelBase Result
        {
            get
            {
                return _comboBox.SelectedItem as UnitModelBase;
            }
        }

        public ControlUnitModel(UnitModelBase startModel)
        {
            InitializeComponent();

            _comboBox.Items.Add(new UnitModelAnim());
            _comboBox.Items.Add(new UnitModelBillboard());
            _comboBox.Items.Add(new UnitModelSphere());
            _comboBox.Items.Add(new UnitModelEmpty());
            _comboBox.Items.Add(new UnitModelParticleSystem());
            _comboBox.Items.Add(new UnitModelVolumeLight());

            if (startModel != null)
            {
                int index = 0;
                foreach (UnitModelBase item in _comboBox.Items)
                {
                    if (item.GetType().Name == startModel.GetType().Name)
                    {
                        _comboBox.Items[index] = startModel;
                        break;
                    }
                    index++;
                }
                _comboBox.SelectedItem = startModel;
            }
            else
            {
                _comboBox.SelectedIndex = 0;
            }
        }

        private void _comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _propertyGrid.SelectedObject = _comboBox.SelectedItem;
        }
    }
}
