using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections;
using CommonUI;
using CommonUI.UITypeEditors;
using UnitEngine.Behavior;

namespace UnitEngine.UITypeEditors
{
    /// <summary>
    /// Редактор
    /// </summary>
    public class UITypeEditorActionDescriptions : UITypeEditor
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
            var editItem = value as List<ExtActionDescription>;
            if (editItem == null)
                return value;

            var controlEditor = new ControlEditorListItems(editItem, CreateNewItem, null);
            if (FormWorker.ShowDialog("Редактор", controlEditor) != System.Windows.Forms.DialogResult.OK)
                return value;
            return controlEditor.EditItem.Cast<ExtActionDescription>().ToList();
        }

        object CreateNewItem(string actionName)
        {
            var item = new ExtActionDescription()
            {
                ActionName = "action"
            };
            return item;
        }
    }
}
