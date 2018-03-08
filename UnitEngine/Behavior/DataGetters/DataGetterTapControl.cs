using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Получить модель из TapControl
    /// </summary>
    [Serializable]
    public class DataGetterTapControl : DataGetterBase
    {
        /// <summary>
        /// Тип данных
        /// </summary>
        public enum GetterTypeEnum
        {
            /// <summary>
            /// Модель
            /// </summary>
            SceneNode,
            /// <summary>
            /// Позиция клика
            /// </summary>
            Position
        }

        /// <summary>
        /// Наименование контрола
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Наименование контрола")]
        public string TapSceneName { get; set; }

        /// <summary>
        /// Тип данных
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Тип данных")]
        public GetterTypeEnum GetterType { get; set; }

        public DataGetterTapControl()
        {
            TapSceneName = string.Empty;
            GetterType = GetterTypeEnum.SceneNode;
        }

        public override string ToString()
        {
            return string.Format("DataGetterTapControl [{0}][{1}]", TapSceneName, GetterType);
        }
    }
}
