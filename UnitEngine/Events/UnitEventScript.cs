using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Common;
using CommonUI.UITypeEditors;

namespace UnitEngine.Events
{
    /// <summary>
    /// Событие в виде скрипта (Result = true/false)
    /// </summary>
    [Serializable]
    public class UnitEventScript : UnitEventBase
    {
        /// <summary>
        /// Скрипт
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Скрипт")]
        [ParserAutocompleteAttribute(ParserAutocompleteAttribute.ParserTypeWords.Units)]
        [Editor(typeof(UITypeEditorScriptFileNameModal), typeof(System.Drawing.Design.UITypeEditor))]
        public string ScriptFileName { get; set; }

        public UnitEventScript()
        {
            ScriptFileName = string.Empty;
        }

        public override void ToRelativePaths(string root)
        {
            ScriptFileName = Common.UtilPath.GetRelativePath(ScriptFileName, root);
        }

        public override void ToAbsolutePaths(string root)
        {
            ScriptFileName = Common.UtilPath.GetAbsolutePath(ScriptFileName, root);
        }

        public override string ToString()
        {
            return string.Format("Скрипт");
        }
    }
}
