using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using StageEngine;
using CommonUI;
using StageEngineUI.Controls;
using TransactionCore;

namespace StageEngineUI
{
    public partial class EditorStageProperty : DockContent
    {
        /// <summary>
        /// Событие изменения свойства
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// Редактируемый объект
        /// </summary>
        object _editItem;

        /// <summary>
        /// Редактируемый объект
        /// </summary>
        public object EditItem
        {
            set
            {
                SetEditItem(value);  
            }
            get
            {
                return GetEditItem();
            }
        }
        
        /// <summary>
        /// Менеджер для undo / redo
        /// </summary>
        TransactionManager _transManager;

        public EditorStageProperty(TransactionManager transManager)
        {
            InitializeComponent();
            _transManager = transManager;
        }

        private object GetEditItem()
        {
            return _editItem;
        }

        private void SetEditItem(object editItem)
        {
            _editItem = editItem;
            _panelContent.Controls.Clear();

            if (editItem is UnitInstanceCamera)
            {
                var cntrl = new ControlEditingCameraBase();
                cntrl.EditItem = editItem as UnitInstanceCamera;
                cntrl.Changed += OnChange;
                FormWorker.AddControl(_panelContent, cntrl);
            }
            else
            {
                var cntrl = new ControlStdProperties();
                cntrl.EditItem = editItem;
                cntrl.PropertyValueChanged += new PropertyValueChangedEventHandler(PropertyValueChanged);
                cntrl.Changed += OnChange;
                FormWorker.AddControl(_panelContent, cntrl);
            }
        }

        void PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            // Заносим изменения в транзакцию
            //
            var eProp = e as PropertyValueChangedEventArgs;
            _transManager.AddChanges(
                EditItem,
                eProp.ChangedItem.PropertyDescriptor.Name,
                eProp.OldValue,
                eProp.ChangedItem.Value);
        }

        private void OnChange(object oItem)
        {
            if (Changed != null)
            {
                Changed(oItem, EventArgs.Empty);
            }
        }
    }
}
