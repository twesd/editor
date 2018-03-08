using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CommonUI;
using CommonUI.UITypeEditors;
using Common;

namespace MainEditors
{
    /// <summary>
    /// Стадия игры в основном хранилище
    /// </summary>
    [Serializable]
    public class StageItem
    {
        /// <summary>
        /// Путь до файла стадии
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Путь до файла стадии")]
        public string Path { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Является ли стадия начальной")]
        public bool IsStartStage { get; set; }

        /// <summary>
        /// Скрипт при завершении стадии
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Скрипт при завершении стадии")]
        [ParserAutocompleteAttribute(ParserAutocompleteAttribute.ParserTypeWords.Units)]
        [Editor(typeof(UITypeEditorScriptFileNameModal), typeof(System.Drawing.Design.UITypeEditor))]
        public string ScriptOnComplete { get; set; }

        public StageItem()
        {
            Path = string.Empty;
            IsStartStage = false;
            ScriptOnComplete = string.Empty;
        }

        public void ToRelativePaths(string root)
        {
            Path = Common.UtilPath.GetRelativePath(Path, root);
        }

        public void ToAbsolutePaths(string root)
        {
            Path = Common.UtilPath.GetAbsolutePath(Path, root);
        }
    }
}
