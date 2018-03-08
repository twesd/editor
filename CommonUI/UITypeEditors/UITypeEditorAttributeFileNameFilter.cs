using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUI.UITypeEditors
{
    /// <summary>
    /// Атрибут для фильтра диалогового окна выбора файла
    /// </summary>
    public class UITypeEditorAttributeFileNameFilter : Attribute
    {
        public string Filter { get; private set; }

        public UITypeEditorAttributeFileNameFilter(string filter)
        {
            Filter = filter;
        }
    }
}
