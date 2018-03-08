using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace UnitEngine
{
    /// <summary>
    /// Базовый класс выбора модели
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(UnitSelectSceneNodeDistance))]
    [XmlInclude(typeof(UnitSelectSceneNodeTapControl))]
    [XmlInclude(typeof(UnitSelectSceneNodeId))]
    [XmlInclude(typeof(UnitSelectSceneNodeData))]
    public class UnitSelectSceneNodeBase
    {
        [CategoryAttribute("Базовые")]
        [DescriptionAttribute("Фильтр индентификатора модели")]
        public int FilterNodeId { get; set; }

        [CategoryAttribute("Базовые")]
        [DescriptionAttribute("Выбирать дочерние элементы")]
        public bool SelectChilds { get; set; }

        public UnitSelectSceneNodeBase()
        {
            FilterNodeId = -1;
            SelectChilds = false;
        }
    }
}
