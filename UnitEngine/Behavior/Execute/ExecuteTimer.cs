using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Изменить параметры таймера
    /// </summary>
    [Serializable]
    public class ExecuteTimer : ExecuteBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Наименование таймера")]
        public string TimerName { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Использовать таймер доступный во всей стадии")]
        public bool UseStageTimer { get; set; }

        [CategoryAttribute("Состояние таймера")]
        [DescriptionAttribute("Запустить тайме")]
        public bool StartTimer { get; set; }

        [CategoryAttribute("Состояние таймера")]
        [DescriptionAttribute("Остановить таймер")]
        public bool StopTimer { get; set; }

        [CategoryAttribute("Установка времени")]
        [DescriptionAttribute("Установить время")]
        public bool SetTime { get; set; }

        [CategoryAttribute("Установка времени")]
        [DescriptionAttribute("Время. Имеет значение если SetTime = true ")]
        public UInt32 Time { get; set; }

        public ExecuteTimer()
        {
            TimerName = string.Empty;
            UseStageTimer = false;
            StartTimer = false;
            StopTimer = false;
            SetTime = true;
            Time = 0;
        }

        public override string ToString()
        {
            return string.Format("Таймер [{0}]", TimerName);
        }
    }
}
