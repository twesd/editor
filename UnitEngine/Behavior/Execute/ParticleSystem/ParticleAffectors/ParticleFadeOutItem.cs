using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.ComponentModel;

namespace UnitEngine
{
    /// <summary>
    /// Точка перемещения
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ParticleFadeOutItem
    {
        public SColor Color { get; set; }

        /// <summary>
        /// Время за которое надо переместиться
        /// </summary>
        public UInt32 Time { get; set; }

        public ParticleFadeOutItem(SColor color, UInt32 time)
        {
            Color = color;
            Time = time;
        }

        /// <summary>
        /// Иницилизация (Для Serializable)
        /// </summary>
        public ParticleFadeOutItem() 
        {
            Color = new SColor();
            Time = 0;
        }
 
    }
}
