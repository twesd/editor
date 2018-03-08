using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
//using CommonUI.UITypeEditors;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Установить действие другому юниту
    /// </summary>
    [Serializable]
    public class ExecuteExtAction : ExecuteBase
    {
        /// <summary>
        /// Описание внешних действий
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Описание действий")]
        [Editor(typeof(UnitEngine.UITypeEditors.UITypeEditorActionDescriptions), typeof(System.Drawing.Design.UITypeEditor))]
        public List<ExtActionDescription> ExtActionDescriptions { get; set; }

        /// <summary>
        /// Выборка моделий
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Выборка моделий")]
        [Editor(typeof(UnitEngine.UITypeEditors.UITypeEditorSelectSceneNodeCollection), typeof(System.Drawing.Design.UITypeEditor))]
        public List<UnitSelectSceneNodeBase> SelectSceneNodes { get; set; }

        public ExecuteExtAction()
        {
            SelectSceneNodes = new List<UnitSelectSceneNodeBase>();
            ExtActionDescriptions = new List<ExtActionDescription>();
        }

        public override string ToString()
        {
            return string.Format("Установить внешнее действие");
        }
    }
}
