using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections;
using CommonUI;
using CommonUI.UITypeEditors;

namespace UnitEngine.UITypeEditors
{
    public class UITypeEditorSelectSceneNodeCollection : UITypeEditor
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
            var editItem = value as List<UnitSelectSceneNodeBase>;
            if (editItem == null)
                return value;

            var controlEditor = new ControlEditorListItems(editItem, CreateNewItem,
                new List<string>() { "Расстояние", "TapControl", "Данные", "Индентификатор" });
            if (FormWorker.ShowDialog("Редактор", controlEditor) != System.Windows.Forms.DialogResult.OK)
                return value;
            return controlEditor.EditItem.Cast<UnitSelectSceneNodeBase>().ToList();
        }

        object CreateNewItem(string actionName)
        {
            if (actionName == "Расстояние")
            {
                return new UnitSelectSceneNodeDistance();
            }
            if (actionName == "TapControl")
            {
                return new UnitSelectSceneNodeTapControl();
            }
            if (actionName == "Данные")
            {
                return new UnitSelectSceneNodeData();
            }
            if (actionName == "Индентификатор")
            {
                return new UnitSelectSceneNodeId();
            }
            return null;
        }
    }
}
