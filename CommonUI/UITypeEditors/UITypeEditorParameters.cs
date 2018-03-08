using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections;
using System.Windows.Forms.Design;
using System.Drawing;
using Common;


namespace CommonUI.UITypeEditors
{
    public class UITypeEditorParameters : UITypeEditor
    {
        public override UITypeEditorEditStyle
          GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context,
                                IServiceProvider provider, object value)
        {
            ControlEditorParameters cntrlEditor = new ControlEditorParameters();
            cntrlEditor.EditItem = value as List<Parameter>;

            if (FormWorker.ShowDialog("Редактор параметров", cntrlEditor) != System.Windows.Forms.DialogResult.OK)
                return value;
            return cntrlEditor.EditItem;
        }
    }
}
