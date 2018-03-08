using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace CommonUI.UITypeEditors
{
    /// <summary>
    /// Редактор имени файла
    /// </summary>
    public class UITypeEditorFileName : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context,
                                IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes =
                provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (wfes != null)
            {
                string filter = "Все файлы (*.*)|*.*";
                if (context.PropertyDescriptor.Attributes[typeof(UITypeEditorAttributeFileNameFilter)] != null)
                {
                    var filterAttr = context.PropertyDescriptor.Attributes[typeof(UITypeEditorAttributeFileNameFilter)] as UITypeEditorAttributeFileNameFilter;
                    filter = filterAttr.Filter + filter;
                }

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = filter;
                dialog.Title = "Открыть файл";
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return value;

                return dialog.FileName;
            }
            return value;
        }
    }
}
