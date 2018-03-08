using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Events
{
    /// <summary>
    /// Позиция внутри площади
    /// </summary>
    [Serializable]
    public class UnitEventPositionInsideArea : UnitEventBase
    {
        /// <summary>
        /// Выбирать площадь из экземпляра юнита
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Выбирать площадь из экземпляра юнита")]
        public bool GetAreaFromInstance { get; set; }

        /// <summary>
        /// Выбирать позицию из Tap контрола
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Выбирать позицию из Tap контрола")]
        public bool GetPositionFromTapScene { get; set; }

        /// <summary>
        /// Наименование Tap контрола
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Наименование Tap контрола")]
        public string TapSceneName { get; set; }

        public UnitEventPositionInsideArea()
        {
            GetAreaFromInstance = true;
            GetPositionFromTapScene = true;
        }

        public override string ToString()
        {
            return string.Format("Позиция внутри площади : GetAreaFromInstance[{0}] GetFromTapScene [{1}]", 
                GetAreaFromInstance, GetPositionFromTapScene);
        }
    }
}
