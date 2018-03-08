using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnitEngine;
using UnitEngine.Behavior;

namespace UnitEngineUI.Behavior
{
    public partial class ControlParticleAffector : UserControl
    {
        public delegate void OnChangeEventHandler(object oEdit);

        public OnChangeEventHandler Changed;

        ExecuteParticleAffector _editItem;

        public ExecuteParticleAffector EditItem
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


        public ControlParticleAffector()
        {
            InitializeComponent();

            _comboBox.Items.Add(new ParticleAffectorFadeOut());
            _comboBox.Items.Add(new ParticleAffectorEmmiterParam());
            _comboBox.Items.Add(new ParticleAffectorAllDirection());
            _comboBox.Items.Add(new ParticleAffectorScale());
            _comboBox.Items.Add(new ParticleAffectorRotation());
        }

        private void SetEditItem(ExecuteParticleAffector executeAffector)
        {
            _editItem = null;
            var startAffector = executeAffector.Affector;
            if (startAffector != null)
            {
                int index = 0;
                foreach (object item in _comboBox.Items)
                {
                    if (item.GetType().Name == startAffector.GetType().Name)
                    {
                        _comboBox.Items[index] = startAffector;
                        break;
                    }
                    index++;
                }
                _comboBox.SelectedItem = startAffector;
            }
            else
            {
                _comboBox.SelectedIndex = 0;
            }
            _editItem = executeAffector;
            _propertyGrid.SelectedObject = _comboBox.SelectedItem;
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_editItem == null) return;
            _propertyGrid.SelectedObject = _comboBox.SelectedItem;
            _editItem.Affector = (ParticleAffectorBase)_comboBox.SelectedItem;            
            if (Changed != null) Changed(_editItem);
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (_editItem == null) return;
            _editItem.Affector = (ParticleAffectorBase)_comboBox.SelectedItem;
            if (Changed != null) Changed(_editItem);
        }
    }
}
