using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Получить модель по индентификатору
    /// </summary>
    [Serializable]
    public class DataGetterId : DataGetterBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Фильтр индентификатора модели")]
        public int FilterNodeId { get; set; }

        public DataGetterId()
        {
            FilterNodeId = 0;
        }

        public override string ToString()
        {
            return string.Format("DataGetterId [{0}]", FilterNodeId);
        }
    }
}
