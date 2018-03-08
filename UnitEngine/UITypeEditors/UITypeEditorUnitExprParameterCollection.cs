using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using CommonUI;
using CommonUI.UITypeEditors;

namespace UnitEngine.UITypeEditors
{
    public class UITypeEditorUnitExprParameterCollection : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(
            ITypeDescriptorContext context,
            IServiceProvider provider,
            object value)
        {
            var editItem = value as List<UnitExprParameter>;
            if (editItem == null)
                return value;

            var controlEditor = new ControlEditorListItems(editItem, CreateNewItem, null);
            if (FormWorker.ShowDialog("Редактор", controlEditor) != System.Windows.Forms.DialogResult.OK)
                return value;
            return controlEditor.EditItem.Cast<UnitExprParameter>().ToList();
        }

        object CreateNewItem(string actionName)
        {
            return new UnitExprParameter();
        }
    }
}
