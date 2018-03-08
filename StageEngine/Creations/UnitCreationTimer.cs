using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace StageEngine
{
    /// <summary>
    /// Cоздание юнита по времени
    /// </summary>
    [Serializable]
    public class UnitCreationTimer : UnitCreationBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Начальное время создания (м.сек)")]
        public UInt32 StartTime { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Конечное время создания (м.сек) или ноль, если бесконечно")]
        public UInt32 EndTime { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Интервал (м.сек), если не надо повторять")]
        public UInt32 Interval { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Обновить StartTime и EndTime в соответствии с текущем времени при первой проверки")]
        public bool UpdateTimesOnFirstCheck { get; set; }

        public UnitCreationTimer() 
        {
            StartTime = 0;
            EndTime = 0;
            Interval = 0;
            UpdateTimesOnFirstCheck = false;
        }

        public override string ToString()
        {
            return string.Format("Время: [{0},{1}], Интервал: {2}", StartTime, EndTime, Interval);
        }
    }
}
