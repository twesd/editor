using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitEngine
{
    /// <summary>
    /// Описание эффекта
    /// </summary>
    [Serializable]
    public class UnitEffect
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name;

        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="name">Наименование эффекта</param>
        public UnitEffect(string name) 
        {
            Name = name;
        }

        /// <summary>
        /// Иницилизация (Для Serializable)
        /// </summary>
        public UnitEffect() { }
    }
}
