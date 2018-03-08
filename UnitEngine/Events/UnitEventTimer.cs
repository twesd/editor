using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Events
{
    /// <summary>
    /// Событие времени
    /// </summary>
    [Serializable]
    public class UnitEventTimer : UnitEventBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Интервал времени")]
        public UInt32 Interval { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Повторять действие")]
        public bool Loop { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Наименование таймера, может быть пустое значение")]
        public string TimerName { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Использовать таймер доступный во всей стадии")]
        public bool UseStageTimer { get; set; }

        public UnitEventTimer()
        {
            Interval = 1000;
            Loop = false;
            TimerName = string.Empty;
            UseStageTimer = false;
        }

        public override string ToString()
        {
            return string.Format("Время : [{0}] Повторять [{1}] Таймер : [{2}]", Interval, Loop, TimerName);
        }
    }
}
