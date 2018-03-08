using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine
{
    [Serializable]
    public class UnitModelSphere : UnitModelBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Радиус")]
        public float Radius { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Детализация")]
        public int PolyCount { get; set; }

        public UnitModelSphere()
        {
            Radius = 1;
            PolyCount = 8;
        }

        public override string ToString()
        {
            return "Sphere";
        }
    }
}
