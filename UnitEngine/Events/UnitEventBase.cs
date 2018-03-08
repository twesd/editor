using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace UnitEngine.Events
{
    /// <summary>
    /// Описание события
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(UnitEventControlButton))]
    [XmlInclude(typeof(UnitEventControlTapScene))]
    [XmlInclude(typeof(UnitEventDistance))]
    [XmlInclude(typeof(UnitEventTimer))]
    [XmlInclude(typeof(UnitEventActionEnd))]
    [XmlInclude(typeof(UnitEventScript))]
    [XmlInclude(typeof(UnitEventAnimation))]
    [XmlInclude(typeof(UnitEventPositionInsideArea))]
    [XmlInclude(typeof(UnitEventSelection))]
    [XmlInclude(typeof(UnitEventOperator))]
    [XmlInclude(typeof(UnitEventChildUnit))]
    [XmlInclude(typeof(UnitEventControlCircle))]        
    public class UnitEventBase
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// Иницилизация (Для Serializable)
        /// </summary>
        public UnitEventBase() { }

        public virtual void ToRelativePaths(string root) { }

        public virtual void ToAbsolutePaths(string root) { }
    }
}
