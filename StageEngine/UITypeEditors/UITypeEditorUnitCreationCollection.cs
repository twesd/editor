using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections;
using CommonUI;
using CommonUI.UITypeEditors;

namespace StageEngine.UITypeEditors
{
    public class UITypeEditorUnitCreationCollection : UITypeEditor
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
            var editItem = value as List<UnitCreationBase>;
            if (editItem == null)
                return value;

            var controlEditor = new ControlEditorListItems(editItem, CreateNewItem,
                new List<string>() { "Время", "Расстояние", "Глобальные параметры", "Граница" });
            if (FormWorker.ShowDialog("Редактор", controlEditor) != System.Windows.Forms.DialogResult.OK)
                return value;
            return controlEditor.EditItem.Cast<UnitCreationBase>().ToList();
        }

        object CreateNewItem(string actionName)
        {
            if (actionName == "Время")
            {
                return new UnitCreationTimer();
            }
            else if (actionName == "Расстояние")
            {
                return new UnitCreationDistance();
            }
            else if (actionName == "Граница")
            {
                return new UnitCreationBBox();
            }
            else if (actionName == "Глобальные параметры")
            {
                return new UnitCreationGlobalParameters();
            }
            throw new NotSupportedException();
        }
    }
}
