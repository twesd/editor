using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace UnitEngine
{
    /// <summary>
    /// Точка перемещения
    /// </summary>
    [Serializable]
    public class TransformSColorItem
    {
        public SColor Color;

        /// <summary>
        /// Время за которое надо переместиться
        /// </summary>
        public UInt32 Time = 0;

        public TransformSColorItem(SColor color, UInt32 time)
        {
            Color = color;
            Time = time;
        }

        /// <summary>
        /// Иницилизация (Для Serializable)
        /// </summary>
        public TransformSColorItem() { }
 
    }
}
