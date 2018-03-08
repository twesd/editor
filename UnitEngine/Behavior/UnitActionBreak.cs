using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Common;

namespace UnitEngine
{
    /// <summary>
    /// Условия отмены текущего действия
    /// </summary>
    [Serializable]
    public class UnitActionBreak
    {
        /// <summary>
        /// Типы аниматоров
        /// </summary>
        public enum AnimatorType
        {
            None,
            Any,
            MoveToPoint,
            MoveToSceneNode
        }

        /// <summary>
        /// Отмена, если после завершения анимации
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Отмена, если после завершения анимации")]
        public bool AnimationEnd { get; set; }

        /// <summary>
        /// Отмена, если начальные условия не выполняются
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Отмена, если начальные условия не выполняются")]
        public bool StartClauseNotApproved { get; set; }

        /// <summary>
        /// Отмена, если начальные условия выполняются
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Отмена, если начальные условия выполняются")]
        public bool StartClauseApproved { get; set; }

        /// <summary>
        /// Отмена, если поведение должно завершится после выполнения
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Отмена, если поведение должно завершится после выполнения")]
        public bool IsExecuteOnly { get; set; }

        /// <summary>
        /// Аниматор закончил работу
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Аниматор закончил работу")]
        public AnimatorType AnimatorEnd { get; set; }
        
        /// <summary>
        /// Выражение
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Имя файла скрипта")]
        public string ScriptFileName { get; set; }

        public UnitActionBreak() 
        {
            AnimationEnd = false;
            StartClauseNotApproved = false;
            StartClauseApproved = false;
            IsExecuteOnly = false;
            AnimatorEnd = AnimatorType.None;
            ScriptFileName = string.Empty;
        }

        /// <summary>
        /// True - если не задана отмена
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return (!(AnimationEnd || AnimatorEnd != AnimatorType.None ||
                StartClauseApproved || StartClauseNotApproved ||
                IsExecuteOnly || (!string.IsNullOrEmpty(ScriptFileName.Trim()))));
        }

        public void ToRelativePaths(string root)
        {
            ScriptFileName = UtilPath.GetRelativePath(ScriptFileName, root);
        }

        public void ToAbsolutePaths(string root)
        {
            ScriptFileName = UtilPath.GetAbsolutePath(ScriptFileName, root);
        }
    }
}
