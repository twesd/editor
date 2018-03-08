using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace StageEngine
{
    [Serializable]
    public class UnitCreationDistance : UnitCreationBase
    {
        [CategoryAttribute("Фильтр")]
        [DescriptionAttribute("Фильтр индентификатора модели")]
        public int FilterNodeId { get; set; }

        [CategoryAttribute("Фильтр")]
        [DescriptionAttribute("Количество моделий. Если CountNodes = -1, то не учитываем заданный параметр")]
        public int CountNodes { get; set; }

        [CategoryAttribute("Фильтр")]
        [DescriptionAttribute("Тип сравнения")]
        public Common.CompareType CompareType { get; set; }

        [CategoryAttribute("Фильтр")]
        [DescriptionAttribute("Учитывать размеры юнитов")]
        public bool UseUnitSize { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Дистанция")]
        public float Distance { get; set; }

        public UnitCreationDistance()
        {
            FilterNodeId = 0;
            Distance = 1;
            CountNodes = 1;
            CompareType = Common.CompareType.Less;
            UseUnitSize = false;
        }

        public override string ToString()
        {
            return string.Format("Дистанция [{0}]({1})", Distance, CompareType);
        }
    }
}
