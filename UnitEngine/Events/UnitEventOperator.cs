using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Events
{
    /// <summary>
    /// Группа условий
    /// </summary>
    [Serializable]
    public class UnitEventOperator : UnitEventBase
    {
         /// <summary>
        /// Действия группы
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Оператор")]
        public OperatorType Operator { get; set; }

        public UnitEventOperator()
        {
            Operator = OperatorType.OR;
        }

        public override string ToString()
        {
            return string.Format("[{0}]", Operator);
        }
    }
}
