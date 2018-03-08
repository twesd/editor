using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonUI
{
    public partial class ControlStdProperties : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        /// <summary>
        /// Объект изменился
        /// </summary>
        public event OnChangeEventHandler Changed;

        public event PropertyValueChangedEventHandler PropertyValueChanged;
        
        public ControlStdProperties()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Установить редактируемый объект
        /// </summary>
        public object EditItem
        {
            get
            {
                return _propertyGrid.SelectedObject;
            }
            set
            {
                _propertyGrid.SelectedObject = value;
            }
        }


        /// <summary>
        /// Свойство объекта изменилось
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (PropertyValueChanged != null)
            {
                PropertyValueChanged(s, e);
            }
            if (Changed != null)
            {
                Changed(_propertyGrid.SelectedObject);
            }
        }
    }
}
