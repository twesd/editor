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
    public class UITypeEditorScriptFileName : UITypeEditor
    {
        public override UITypeEditorEditStyle
           GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context,
                                IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes =
                provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (wfes != null)
            {
                ControlEditorScriptFileName cntrlEditor;

                if (context.PropertyDescriptor.Attributes[typeof(ParserAutocompleteAttribute)] != null)
                {
                    var parserAttrs = context.PropertyDescriptor.Attributes[typeof(ParserAutocompleteAttribute)] as ParserAutocompleteAttribute;
                    List<string> names = new List<string>();
                    if ((parserAttrs.ParserTypes & ParserAutocompleteAttribute.ParserTypeWords.Units) != 0)
                        names.AddRange(ParserFunctionNames.GetUnitsNames());
                    if ((parserAttrs.ParserTypes & ParserAutocompleteAttribute.ParserTypeWords.Controls) != 0)
                        names.AddRange(ParserFunctionNames.GetControlsNames());
                    cntrlEditor = new ControlEditorScriptFileName(names);
                }
                else
                {
                    cntrlEditor = new ControlEditorScriptFileName(ParserFunctionNames.GetUnitsNames());
                }

                cntrlEditor.EditItem = value.ToString();

                wfes.DropDownControl(cntrlEditor);
                value = cntrlEditor.EditItem;
            }
            return value;
        }
    }
}
