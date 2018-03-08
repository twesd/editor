using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.Geometry;
using System.ComponentModel;

namespace UnitEngine
{
    /// <summary>
    /// Точка перемещения
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ParticleScaleItem
    {
        public Dimension TargetScale { get; set; }

        /// <summary>
        /// Время за которое надо увеличить масштаб до TargetScale
        /// </summary>
        public UInt32 Time { get; set; }

        public ParticleScaleItem(Dimension scale, UInt32 time)
        {
            TargetScale = scale;
            Time = time;
        }

        /// <summary>
        /// Иницилизация (Для Serializable)
        /// </summary>
        public ParticleScaleItem() 
        {
            TargetScale = new Dimension();
            Time = 0;
        }
 
    }
}
