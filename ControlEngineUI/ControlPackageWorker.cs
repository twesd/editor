using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlEngine;
using System.Xml.Serialization;
using System.IO;
using Common;

namespace ControlEngineUI
{
    class ControlPackageWorker
    {
        /// <summary>
        /// Чтение пакета
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ControlPackage Read(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            // Десерелизуем данные
            //
            XmlSerializer xmlSerelialize = new XmlSerializer(typeof(ControlPackage), Helper.GetExtraTypes());
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                var controlPackage = xmlSerelialize.Deserialize(reader) as ControlPackage;
                controlPackage = controlPackage.GetWithAbsolutePaths(path);
                return controlPackage;
            }
        }


        public static void Save(string path, ControlPackage package)
        {
            // Серелизуем данные для редактора
            //
            ControlPackage containerRelative = package.GetWithRelativePaths(path);
            XmlSerializer xmlSerelializeDsg = new XmlSerializer(typeof(ControlPackage), Helper.GetExtraTypes());
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                xmlSerelializeDsg.Serialize(writer, containerRelative);
            }
        }

    }
}
