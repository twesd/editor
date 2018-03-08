using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CommonUI.UITypeEditors;
using CommonUI;
using System.Drawing.Design;
using Common;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Выполнить скрипт
    /// </summary>
    [Serializable]
    public class ExecuteScript : ExecuteBase
    {
        /// <summary>
        /// Скрипт
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Скрипт")]
        [UITypeEditorAttributeFileNameFilter("Файл скрипта (*.sc)|*.sc|")]
        [Editor(typeof(UITypeEditorFileName), typeof(System.Drawing.Design.UITypeEditor))]
        public string ScriptFileName { get; set; }

        public ExecuteScript()
        {
            ScriptFileName = string.Empty;
        }

        public override void ToRelativePaths(string root)
        {
            ScriptFileName = UtilPath.GetRelativePath(ScriptFileName, root);
        }

        public override void ToAbsolutePaths(string root)
        {
            ScriptFileName = UtilPath.GetAbsolutePath(ScriptFileName, root);
        }

        public override string ToString()
        {
            return string.Format("Cкрипт");
        }
    }
}
