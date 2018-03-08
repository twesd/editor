using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Common;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Изменить параметр другого юнита
    /// </summary>
    [Serializable]
    public class ExecuteExtParameter : ExecuteBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Параметры")]
        [Editor(typeof(UnitEngine.UITypeEditors.UITypeEditorUnitParameterCollection), typeof(System.Drawing.Design.UITypeEditor))]
        public List<Parameter> Parameters { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Прекратить обработку на первом удачном параметер")]
        public bool BreakOnFirstSuccess { get; set; }
        
        /// <summary>
        /// Выборка моделий
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Выборка моделий")]
        [Editor(typeof(UnitEngine.UITypeEditors.UITypeEditorSelectSceneNodeCollection), typeof(System.Drawing.Design.UITypeEditor))]
        public List<UnitSelectSceneNodeBase> SelectSceneNodes { get; set; }

        public ExecuteExtParameter() 
        {
            Parameters = new List<Parameter>();
            SelectSceneNodes = new List<UnitSelectSceneNodeBase>();
            BreakOnFirstSuccess = true;
        }

        public override string ToString()
        {
            return string.Format("Изменить параметры другого юнита");
        }
    }
}
