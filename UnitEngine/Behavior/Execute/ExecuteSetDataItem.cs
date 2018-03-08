using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Описание данных для установки
    /// </summary>
    [Serializable]
    public class ExecuteSetDataItem
    {
        /// <summary>
        /// Наименование параметра данных
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Наименование параметра данных")]
        public string Name { get; set; }

        /// <summary>
        /// Получение данных
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Получение данных")]
        [Editor(typeof(CommonUI.UITypeEditors.UITypeEditorItem), typeof(System.Drawing.Design.UITypeEditor))]
        public DataGetterBase DataGetter { get; set; }

        public ExecuteSetDataItem()
        {
            Name = string.Empty;
            DataGetter = null;
        }

        public override string ToString()
        {
            return string.Format("Имя {0} Данные {1}", Name, DataGetter);
        }
    }
}
