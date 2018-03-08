using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine
{
    /// <summary>
    /// Выборка модели из данных
    /// </summary>
    [Serializable]
    public class UnitSelectSceneNodeData : UnitSelectSceneNodeBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Наименование параметра Data")]
        public string DataName { get; set; }

        public UnitSelectSceneNodeData()
        {
            DataName = string.Empty;
        }

        public override string ToString()
        {
            return string.Format("Выборка модели из данных [{0}]", DataName);
        }
    }
}
