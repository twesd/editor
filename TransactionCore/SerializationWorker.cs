using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Collections;

namespace TransactionCore
{
    class SerializationWorker
    {
        static Dictionary<Type, XmlSerializer> _cache = new Dictionary<Type, XmlSerializer>(); 

        public static string Serialize(object oItem, Type[] extraTypes)
        {
            Type itemType = oItem.GetType();
            XmlSerializer serializer = GetSerializer(itemType, extraTypes);
            return Serialize(oItem, serializer);
        }

        public static string Serialize(object oItem, XmlSerializer serializer)
        {
            Type itemType = oItem.GetType();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
            settings.Indent = false;
            settings.OmitXmlDeclaration = false;

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, oItem);
                }
                return textWriter.ToString();
            }
        }

        public static object Deserialize(string xml, Type itemType, Type[] extraTypes)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }

            XmlSerializer serializer = GetSerializer(itemType, extraTypes);

            XmlReaderSettings settings = new XmlReaderSettings();
            using (StringReader textReader = new StringReader(xml))
            {
                using (XmlReader xmlReader = XmlReader.Create(textReader, settings))
                {
                    return serializer.Deserialize(xmlReader);
                }
            }
        }

        private static XmlSerializer GetSerializer(Type itemType, Type[] extraTypes)
        {
            XmlSerializer serializer = null;
            if (_cache.ContainsKey(itemType))
            {
                serializer = _cache[itemType];
            }
            else
            {
                serializer = new XmlSerializer(itemType, extraTypes);
                _cache[itemType] = serializer;
            }
            return serializer;
        }
    }
}
