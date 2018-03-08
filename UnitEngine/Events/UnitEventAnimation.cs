using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Events
{
    /// <summary>
    /// Событие окончания анимации
    /// </summary>
    [Serializable]
    public class UnitEventAnimation : UnitEventBase
    {
        /// <summary>
        /// Расстояние
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Номер кадра относительно начала анимации")]
        public int FrameNr { get; set; }
        
        /// <summary>
        /// Расстояние
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Окончание анимации")]
        public bool OnEnd { get; set; }

        public UnitEventAnimation()
        {
            OnEnd = true;
            FrameNr = 0;
        }

        public override string ToString()
        {
            if (OnEnd)
                return string.Format("Анимация - Окончание");
            else
                return string.Format("Анимация  - Кадр[{0}]", FrameNr);
        }
    }
}
