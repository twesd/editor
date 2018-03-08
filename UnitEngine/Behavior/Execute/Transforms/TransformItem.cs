using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitEngine
{
    /// <summary>
    /// Точка перемещения
    /// </summary>
    [Serializable]
    public class TransformItem
    {
        /// <summary>
        /// Перемещение по X
        /// </summary>
        public float X = 0;

        /// <summary>
        /// Перемещение по X
        /// </summary>
        public float Y = 0;

        /// <summary>
        /// Перемещение по X
        /// </summary>
        public float Z = 0;

        /// <summary>
        /// Время за которое надо переместиться
        /// </summary>
        public UInt32 Time = 0;

        public TransformItem(float x, float y, float z, UInt32 time)
        {
            X = x;
            Y = y;
            Z = z;
            Time = time;
        }

        /// <summary>
        /// Иницилизация (Для Serializable)
        /// </summary>
        public TransformItem() { }
 
    }
}
