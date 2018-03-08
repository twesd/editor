using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Events
{
    /// <summary>
    /// Описание события от клика по сцене
    /// </summary>
    [Serializable]
    public class UnitEventControlCircle : UnitEventBase
    {
        /// <summary>
        /// Наименование контрола
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Наименование контрола")]
        public string ControlName { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Состояния")]
        public List<UnitEventControlCircleState> States { get; set; }

        public UnitEventControlCircle()
        {
            ControlName = string.Empty;
            States = new List<UnitEventControlCircleState>();
        }

        public override string ToString()
        {
            return string.Format("ControlCircle [{0}]", ControlName);
        }
    }
}
