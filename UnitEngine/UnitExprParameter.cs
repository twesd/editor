using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CommonUI;
using CommonUI.UITypeEditors;
using Common;

namespace UnitEngine
{
    /// <summary>
    /// Условие изменения параметра
    /// </summary>
    [Serializable]
    public class UnitExprParameter
    {
        /// <summary>
        /// Имя параметра
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Имя параметра")]
        public string Name { get; set; }

        /// <summary>
        /// Значение параметра
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Скрипт")]
        [ParserAutocompleteAttribute(ParserAutocompleteAttribute.ParserTypeWords.Parameters | ParserAutocompleteAttribute.ParserTypeWords.Units)]
        [Editor(typeof(UITypeEditorScriptFileNameModal), typeof(System.Drawing.Design.UITypeEditor))]
        public string ScriptFileName { get; set; }

        public UnitExprParameter() 
        {
            Name = string.Empty;
            ScriptFileName = string.Empty;
        }

        public void ToRelativePaths(string root)
        {
            ScriptFileName = UtilPath.GetRelativePath(ScriptFileName, root);
        }

        public void ToAbsolutePaths(string root)
        {
            ScriptFileName = UtilPath.GetAbsolutePath(ScriptFileName, root);
        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}
