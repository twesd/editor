using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace PublishData
{
    [Serializable]
    public class ProjectData
    {
        public string RootDir { get; set; }

        public List<string> DestDirs { get; set; }

        public FolderData RootFolder { get; set; }

        public ProjectData()
        {
            RootFolder = new FolderData();
            DestDirs = new List<string>();
        }

        public ProjectData Clone()
        {
            XmlSerializer serial = new XmlSerializer(typeof(ProjectData));
            using (MemoryStream fileStream = new MemoryStream())
            {
                serial.Serialize(fileStream, this);
                fileStream.Seek(0, SeekOrigin.Begin);
                return serial.Deserialize(fileStream) as ProjectData;
            }
        }

        public void ToRelativePaths()
        {
            // TODO !
            RootDir = RootDir;
        }

        public void ToAbsolutePaths()
        {
            RootDir = RootDir;
        }
    }
}
