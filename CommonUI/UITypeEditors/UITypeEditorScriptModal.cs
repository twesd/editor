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
    /// <summary>
    /// Редактор выражения
    /// </summary>
    public class UITypeEditorScriptModal : UITypeEditor
    {
        public override UITypeEditorEditStyle
           GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context,
                                IServiceProvider provider, object value)
        {

            ControlEditorScript cntrlEditor;

            if (context.PropertyDescriptor.Attributes[typeof(ParserAutocompleteAttribute)] != null)
            {
                var parserAttrs = context.PropertyDescriptor.Attributes[typeof(ParserAutocompleteAttribute)] as ParserAutocompleteAttribute;
                List<string> names = new List<string>();
                if ((parserAttrs.ParserTypes & ParserAutocompleteAttribute.ParserTypeWords.Units) != 0)
                    names.AddRange(ParserFunctionNames.GetUnitsNames());
                if ((parserAttrs.ParserTypes & ParserAutocompleteAttribute.ParserTypeWords.Controls) != 0)
                    names.AddRange(ParserFunctionNames.GetControlsNames());
                if ((parserAttrs.ParserTypes & ParserAutocompleteAttribute.ParserTypeWords.Parameters) != 0)
                    names.AddRange(ParserFunctionNames.GetParametersNames());
                cntrlEditor = new ControlEditorScript(names);
            }
            else
            {
                cntrlEditor = new ControlEditorScript(ParserFunctionNames.GetUnitsNames());
            }

            cntrlEditor.EditItem = value.ToString();
            cntrlEditor.Size = new Size(1024, 480);
            if (FormWorker.ShowDialog("Редактор скрипта", cntrlEditor) != System.Windows.Forms.DialogResult.OK)
                return value;
            return cntrlEditor.EditItem;
        }
    }
}
