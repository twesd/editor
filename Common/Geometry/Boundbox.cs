using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Common.ObjectConvertors;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Common.Geometry
{
    [TypeConverter(typeof(ObjectConverterBoundbox))]
    [Serializable]
    public class Boundbox
    {
        [Category("Основные")]
        [Description("Минимальная точка границы")]
        public Vertex MinPoint { get; set; }

        [Category("Основные")]
        [Description("Максимальная точка границы")]
        public Vertex MaxPoint { get; set; }

        public int Width
        {
            get
            {
                return Convert.ToInt32(MaxPoint.X - MinPoint.X);
            }
        }

        public int Height
        {
            get
            {
                return Convert.ToInt32(MaxPoint.Y - MinPoint.Y);
            }
        }

        public Boundbox()
        {
            MinPoint = new Vertex();
            MaxPoint = new Vertex();
        }

        public Boundbox(Vertex minPoint, Vertex maxPoint)
        {
            MinPoint = minPoint;
            MaxPoint = maxPoint;
        }

        public override string ToString()
        {
            return string.Format("({0}) ({1})", MinPoint.ToString(), MaxPoint.ToString());
        }

        public static Boundbox Parse(string value)
        {
            Boundbox boundBox = new Boundbox();
            
            value = value.Trim();
            
            int separateIndex = value.IndexOf(')');
            string part1 = value.Substring(1, separateIndex - 1);

            value = value.Substring(separateIndex+1, value.Length - separateIndex - 1).Trim();
            string part2 = value.Substring(1, value.Length - 2);

            boundBox.MinPoint = Vertex.Parse(part1);
            boundBox.MaxPoint = Vertex.Parse(part2);
            return boundBox;
        }
    }
}
