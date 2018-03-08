using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common
{
    /// <summary>
    /// Класс для серелизации / десерилизации объектов
    /// </summary>
    public class SerializeWorker
    {
        /// <summary>
        /// Десерилизация из масива байт
        /// </summary>
        public static object ByteArrayToObject(byte[] byteArray)
        {
            object outObj = null;
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArray);
            outObj = formatter.Deserialize(ms);
            return outObj;
        }

        /// <summary>
        /// Возращает объект в виде byte[]
        /// </summary>        
        /// <returns>Byte Array</returns>
        public static byte[] ObjectToByteArray(object entity)
        {
            if (entity == null) return null;

            System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _BinaryFormatter
                        = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            _BinaryFormatter.Serialize(_MemoryStream, entity);

            byte[] array = _MemoryStream.ToArray();
            return array;
        }

        /// <summary>
        /// Возращает объект в виде string
        /// </summary>        
        /// <returns>Byte Array</returns>
        public static string ObjectToString(object entity)
        {
            byte[] arrayBytes = ObjectToByteArray(entity);
            if (arrayBytes == null) return null;
            return System.Convert.ToBase64String(arrayBytes);
        }

        /// <summary>
        /// Десерилизация из строки
        /// </summary>
        public static object StringToObject(string stringObject)
        {
            if (string.IsNullOrEmpty(stringObject)) return null;
            byte[] byteArray = System.Convert.FromBase64String(stringObject);
            return ByteArrayToObject(byteArray);
        }

        /// <summary>
        /// Клонирование объекта через серелизацию / десерилизацию
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static object Clone(object source)
        {
            if (!source.GetType().IsSerializable)
            {
                throw new ArgumentException("Тип должен быть серелизуемый", "source");
            }

            if (Object.ReferenceEquals(source, null))
            {
                return null;
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(stream);
            }
        }

    }
}
