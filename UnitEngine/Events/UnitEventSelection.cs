using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Events
{
    /// <summary>
    /// Условие по выборке объектов
    /// </summary>
    [Serializable]
    public class UnitEventSelection : UnitEventBase
    {
        /// <summary>
        /// Количество выбранных объектов
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Количество выбранных объектов")]
        public int Count { get; set; }

        /// <summary>
        /// Выборка моделий
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Выборка моделий")]
        [Editor(typeof(UnitEngine.UITypeEditors.UITypeEditorSelectSceneNodeCollection), typeof(System.Drawing.Design.UITypeEditor))]
        public List<UnitSelectSceneNodeBase> SelectSceneNodes { get; set; }

        public UnitEventSelection()
        {
            Count = 0;
            SelectSceneNodes = new List<UnitSelectSceneNodeBase>();
        }

        public override string ToString()
        {
            return "Условие по выборке объектов";
        }
    }
}
