using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;

namespace PublishData
{
    [Serializable]
    public class FolderData
    {
        [Category("Основные"), Description("Наименование"), ReadOnly(true)]
        public string Name { get; set; }

        [Browsable(false)]
        public List<FolderData> Childs { get; set; }

        [Category("Настройки"), Description("Настройки")]        
        public FolderSettings Settings { get; set; }

        public FolderData()
        {
            Name = string.Empty;
            Childs = new List<FolderData>();
            Settings = new FolderSettings();
        }

        public FolderData(string name)
        {
            Name = name;
            Childs = new List<FolderData>();
            Settings = new FolderSettings();
        }

        public FolderData Clone()
        {
            XmlSerializer serial = new XmlSerializer(typeof(FolderData));
            using (MemoryStream fileStream = new MemoryStream())
            {
                serial.Serialize(fileStream, this);
                fileStream.Seek(0, SeekOrigin.Begin);
                return serial.Deserialize(fileStream) as FolderData;
            }
        }
    }
}
