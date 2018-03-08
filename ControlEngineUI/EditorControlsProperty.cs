using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using TransactionCore;

namespace ControlEngineUI
{
    public partial class EditorControlsProperty : DockContent
    {
        /// <summary>
        /// Событие изменения свойства
        /// </summary>
        public event EventHandler Changed;


        /// <summary>
        /// Отображаемый объект
        /// </summary>
        public object EditItem
        {
            set
            {
                _propertyGrid.SelectedObject = value;
            }
            get
            {
                return _propertyGrid.SelectedObject;
            }
        }

        /// <summary>
        /// Менеджер для undo / redo
        /// </summary>
        TransactionManager _transManager;

        public EditorControlsProperty(TransactionManager transManager)
        {
            InitializeComponent();
            _transManager = transManager;
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            // Заносим изменения в транзакцию
            //
            var eProp = e as PropertyValueChangedEventArgs;
            _transManager.AddChanges(
                EditItem,
                eProp.ChangedItem.PropertyDescriptor.Name,
                eProp.OldValue,
                eProp.ChangedItem.Value);

            if (Changed != null) Changed(s, e);
        }
    }
}
