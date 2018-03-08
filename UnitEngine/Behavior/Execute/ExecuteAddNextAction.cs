using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Добавить след. действие в список действий
    /// </summary>
    [Serializable]
    public class ExecuteAddNextAction : ExecuteBase
    {
        /// <summary>
        /// Наимeнование действия
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Наимeнование следующего действия")]
        public string ActionName { get; set; }

        public ExecuteAddNextAction()
        {
            ActionName = string.Empty;
        }

        public override string ToString()
        {
            return string.Format("След. действие [{0}]", ActionName);
        }
    }
}
