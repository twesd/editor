using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Common.ObjectConvertors;
using System.Globalization;

namespace Common.Geometry
{
    [TypeConverter(typeof(ObjectConverterVertex))]
    [Serializable]
    public class Vertex
    {
        //! 64bit constant for converting from degrees to radians (formally known as GRAD_PI2)
        const double DEGTORAD64 = Math.PI / 180.0;

        //! 64bit constant for converting from radians to degrees
        const double RADTODEG64 = 180.0 / Math.PI;

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        /// <summary>
        /// Конструктор для серелизации
        /// </summary>
        public Vertex() 
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Vertex(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Возвращает длину текущего вектора
        /// </summary>
        public float Length
        {
            get
            {
                return Convert.ToSingle(Math.Sqrt(X * X + Y * Y + Z * Z));
            }
        }

        public float GetDistanceFrom(Vertex other)
        {
            return Convert.ToSingle(
                Math.Sqrt(Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2) + Math.Pow(other.Z - Z, 2)));
        }

        /// <summary>
        /// Оператор умножения
        /// </summary>
        /// <param name="a"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vertex operator *(Vertex a, float value)
        {
            Vertex res = new Vertex();
            res.X = a.X * value;
            res.Y = a.Y * value;
            res.Z = a.Z * value;
            return res;
        }

        /// <summary>
        /// Оператор умножения
        /// </summary>
        /// <param name="value"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Vertex operator *(float value, Vertex a)
        {
            return a * value;
        }

        /// <summary>
        /// Оператор вычетания
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vertex operator -(Vertex a, Vertex b)
        {
            Vertex res = new Vertex();
            res.X = a.X - b.X;
            res.Y = a.Y - b.Y;
            res.Z = a.Z - b.Z;
            return res;
        }

        /// <summary>
        /// Оператор сложения
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vertex operator +(Vertex a, Vertex b)
        {
            Vertex res = new Vertex();
            res.X = a.X + b.X;
            res.Y = a.Y + b.Y;
            res.Z = a.Z + b.Z;
            return res;
        }

        /// <summary>
        /// Оператор деления
        /// </summary>
        /// <param name="a"></param>
        /// <param name="digit"></param>
        /// <returns></returns>
        public static Vertex operator /(Vertex a, float digit)
        {
            Vertex res = new Vertex();
            res.X = a.X / digit;
            res.Y = a.Y / digit;
            res.Z = a.Z / digit;
            return res;
        }

        /// <summary>
        /// Сравнение двух точек. True - если точки равны.
        /// </summary>
        public bool Compare(Vertex b)
        {
            return Vertex.Compare(this, b);
        }

        /// <summary>
        /// Сравнение двух точек по координатам. True - если точки равны.
        /// </summary>
        public static bool Compare(Vertex a, Vertex b)
        {
            if (object.ReferenceEquals(a, b)) return true;
            if (object.ReferenceEquals(a, null)) return false;
            if (object.ReferenceEquals(b, null)) return false;
            return (a.X == b.X && a.Y == b.Y && a.Z == b.Z);
        }

        public Vertex Clone()
        {
            return this.MemberwiseClone() as Vertex;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", X, Y, Z);
        }

        public static Vertex Parse(string value)
        {
            value = value.Replace(",", ".");
            string[] parts = value.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new Common.Geometry.Vertex(
                float.Parse(parts[0], CultureInfo.InvariantCulture),
                float.Parse(parts[1], CultureInfo.InvariantCulture),
                float.Parse(parts[2], CultureInfo.InvariantCulture)
                );
        }

        /// <summary>
        /// Get the rotations that would make a (0,0,1) direction vector point in the same direction 
        /// as this direction vector.
        /// Example code:
		/// Where target and seeker are of type ISceneNode*
        /// const vector3df toTarget(target->getAbsolutePosition() - seeker->getAbsolutePosition());
		/// const vector3df requiredRotation = toTarget.getHorizontalAngle();
		/// seeker->setRotation(requiredRotation); 
        /// </summary>
        /// <returns></returns>
        public Vertex GetHorizontalAngle()
        {
            Vertex angle = new Vertex();

            angle.Y = (float)(Math.Atan2(X, Z) * RADTODEG64);

            if (angle.Y < 0.0f)
                angle.Y += 360.0f;
            if (angle.Y >= 360.0f)
                angle.Y -= 360.0f;

            double z1 = Math.Sqrt(X * X + Z * Z);

            angle.X = (float)(Math.Atan2(z1, (double)Y) * RADTODEG64 - 90.0);

            if (angle.X < 0.0f)
            {
                angle.X += 360.0f;
            }
            if (angle.X >= 360.0f)
            {
                angle.X -= 360.0f;
            }

            return angle;
        }
    }
}
