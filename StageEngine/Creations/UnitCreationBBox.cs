using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using Common.Geometry;

namespace StageEngine
{
    [Serializable]
    public class UnitCreationBBox : UnitCreationBase
    {
        [CategoryAttribute("Фильтр")]
        [DescriptionAttribute("Фильтр индентификатора модели")]
        public int FilterNodeId { get; set; }

        [CategoryAttribute("Фильтр")]
        [DescriptionAttribute("Количество моделий. Если CountNodes = -1, то не учитываем заданный параметр")]
        public int CountNodes { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Граница")]
        public Boundbox Boundbox { get; set; }

        public UnitCreationBBox()
        {
            FilterNodeId = 0;
            Boundbox = new Boundbox();
            CountNodes = 1;
        }

        public override string ToString()
        {
            return string.Format("Граница [{0}]", Boundbox);
        }
    }
}
