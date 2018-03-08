using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace PublishData
{
    class SerializeWorker
    {
        public static T Load<T>(string filename)
        {
            XmlSerializer serial = new XmlSerializer(typeof(T));
            using (FileStream fileStream = new FileStream(filename, FileMode.Open))
            {
                return (T)(serial.Deserialize(fileStream));
            }
        }

        public static void Save<T>(string filename, object data)
        {
            XmlSerializer serial = new XmlSerializer(typeof(T));
            using (FileStream fileStream = new FileStream(filename, FileMode.Create))
            {
                serial.Serialize(fileStream, data);
            } 
        }
    
    }
}
