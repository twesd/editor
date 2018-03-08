using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Common;

namespace UnitEngine.Events
{
    /// <summary>
    /// Событие дочернего элемента
    /// </summary>
    [Serializable]
    public class UnitEventChildUnit : UnitEventBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Путь до файла поведений")]
        [CommonUI.UITypeEditors.UITypeEditorAttributeFileNameFilter("Файл поведения (*.behavior)|*.behavior|")]
        [Editor(typeof(CommonUI.UITypeEditors.UITypeEditorFileName), typeof(System.Drawing.Design.UITypeEditor))]
        public string ChildPath { get; set; }
        
        /// <summary>
        /// Параметры
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Параметры")]
        [Editor(typeof(CommonUI.UITypeEditors.UITypeEditorParameters), typeof(System.Drawing.Design.UITypeEditor))]
        public List<Parameter> Parameters { get; set; }

        public UnitEventChildUnit()
        {
            ChildPath = string.Empty;
            Parameters = new List<Parameter>();
        }

        public override void ToRelativePaths(string root)
        {
            ChildPath = Common.UtilPath.GetRelativePath(ChildPath, root);
        }

        public override void ToAbsolutePaths(string root)
        {
            ChildPath = Common.UtilPath.GetAbsolutePath(ChildPath, root);
        }

        public override string ToString()
        {
            return string.Format("Дочерний юнит : [{0}]",
                System.IO.Path.GetFileName(ChildPath));
        }
    }
}
