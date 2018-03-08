using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Установить данные
    /// </summary>
    [Serializable]
    public class ExecuteSetData : ExecuteBase
    {
        /// <summary>
        /// Данные для установки
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Данные для установки")]
        public List<ExecuteSetDataItem> DataItems;

        public ExecuteSetData()
        {
            DataItems = new List<ExecuteSetDataItem>();
        }

        public override string ToString()
        {
            return string.Format("Установить данные");
        }
    }
}
