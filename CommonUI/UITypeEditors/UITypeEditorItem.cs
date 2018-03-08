using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections;

namespace CommonUI.UITypeEditors
{
    /// <summary>
    /// Редактор объекта
    /// </summary>
    public class UITypeEditorItem : UITypeEditor
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
            var controlEditor = new ControlStdProperties();
            controlEditor.EditItem = value;
            if (FormWorker.ShowDialog("Редактор объекта", controlEditor) != System.Windows.Forms.DialogResult.OK)
                return value;
            return controlEditor.EditItem;
        }
    }
}
