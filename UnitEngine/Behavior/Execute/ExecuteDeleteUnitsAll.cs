using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Удалить дочерний юнит
    /// </summary>
    [Serializable]
    public class ExecuteDeleteUnitsAll : ExecuteBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Наименование кости")]
        public string JointName { get; set; }

        /// <summary>
        /// Иницилизация
        /// </summary>
        public ExecuteDeleteUnitsAll()
        {
            JointName = string.Empty;
        }


        public override string ToString()
        {
            return string.Format("Удалить юниты JointName [{0}]", JointName);
        }
    }
}
