using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Common;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Получить модель по индентификатору
    /// </summary>
    [Serializable]
    public class DataGetterDistance : DataGetterBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Фильтр индентификатора модели")]
        public int FilterNodeId { get; set; }


        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Дистанция")]
        public float Distance { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Тип сравнения")]
        public CompareType CompareType { get; set; }

        /// <summary>
        /// Учитывать размеры юнитов
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Учитывать размеры юнитов")]
        public bool UseUnitSize { get; set; }

        public DataGetterDistance()
        {
            FilterNodeId = 0;
            Distance = 1;
            CompareType = CompareType.Less;
            UseUnitSize = false;
        }

        public override string ToString()
        {
            return string.Format("Дистанция [{0}]({1})", Distance, CompareType);
        }
    }
}
