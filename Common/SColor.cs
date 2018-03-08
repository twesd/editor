using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using Common.ObjectConvertors;

namespace Common
{
    [Serializable]
    [TypeConverter(typeof(ObjectConverterSColor))]
    public class SColor
    {
        public UInt32 R { get; set; }

        public UInt32 G { get; set; }

        public UInt32 B { get; set; }

        public UInt32 A { get; set; }

        public SColor()
        {
        }

        public SColor(UInt32 a, UInt32 r, UInt32 g, UInt32 b)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", A, R, G, B);
        }

        public static object Parse(string value)
        {
            value = value.Replace(",", ".");
            string[] parts = value.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new SColor(
                UInt32.Parse(parts[0]),
                UInt32.Parse(parts[1]),
                UInt32.Parse(parts[2]),
                UInt32.Parse(parts[3]));
        }
    }
}
