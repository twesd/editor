using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using Common.ObjectConvertors;

namespace Common.Geometry
{
    [TypeConverter(typeof(ObjectConverterDimension))]
    [Serializable]
    public class Dimension
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public Dimension()
        {
        }

        public Dimension(int width, int height)
        {
            Width = width;
            Height = height;
        }
        
        public override string ToString()
        {
            return string.Format("{0} {1}", Width, Height);
        }

        public static object Parse(string value)
        {
            value = value.Replace(",", ".");
            string[] parts = value.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new Dimension(
                int.Parse(parts[0]),
                int.Parse(parts[1]));
        }
    }
}
