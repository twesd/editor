using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using CommonUI.UITypeEditors;
using CommonUI;
using Common;

namespace ControlEngine
{
    /// <summary>
    /// Элемент действий
    /// </summary>
    [Serializable]
    public class ControlBehavior : ControlBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Скрипт")]
        [ParserAutocompleteAttribute(ParserAutocompleteAttribute.ParserTypeWords.Controls | ParserAutocompleteAttribute.ParserTypeWords.Units)]
        [Editor(typeof(UITypeEditorScriptFileNameModal), typeof(UITypeEditor))]
        public string ScriptFileName { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Имя модуля")]
        public string ModuleName { get; set; }

        public ControlBehavior()
        {
            ScriptFileName = string.Empty;
            ModuleName = string.Empty;
        }

        public override void ToRelativePaths(string root)
        {
            ScriptFileName = Common.UtilPath.GetRelativePath(ScriptFileName, root);
        }

        public override void ToAbsolutePaths(string root)
        {
            ScriptFileName = Common.UtilPath.GetAbsolutePath(ScriptFileName, root);
        }
    }
}
