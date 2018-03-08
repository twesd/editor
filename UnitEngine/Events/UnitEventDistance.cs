using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Common;

namespace UnitEngine.Events
{
    /// <summary>
    /// Событие расстояния
    /// </summary>
    [Serializable]
    public class UnitEventDistance : UnitEventBase
    {
        /// <summary>
        /// Расстояние
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Расстояние")]
        public float Distance { get; set; }

        /// <summary>
        /// Тип сравнения
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Тип сравнения")]
        public CompareType CompareType { get; set; }

        /// <summary>
        /// Фильтр моделей
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Фильтр моделей")]
        public int FilterNodeId { get; set; }
        
        /// <summary>
        /// Учитывать размеры юнитов
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Учитывать размеры юнитов")]
        public bool UseUnitSize { get; set; }

        /// <summary>
        /// Получить модель из данных
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Получить модель из данных")]
        public string DataName { get; set; }

        public UnitEventDistance()
        {
            Distance = 1.0f;
            CompareType = CompareType.Less;
            FilterNodeId = -1;
            DataName = string.Empty;
            UseUnitSize = true;
        }

        public override string ToString()
        {
            return string.Format("Расстояние : [{0}] Тип [{1}] FilterId [{2}] Данные [{3}]",
                Distance, CompareType, FilterNodeId, DataName);
        }
    }
}
