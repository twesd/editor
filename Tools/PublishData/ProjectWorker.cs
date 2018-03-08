using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace PublishData
{
    class ProjectWorker
    {
        public static void Save(string filename, ProjectData prjData)
        {
            prjData = prjData.Clone();
            prjData.ToRelativePaths();
            SerializeWorker.Save<ProjectData>(filename, prjData);
        }

        public static ProjectData Load(string filename)
        {
            ProjectData prjData = SerializeWorker.Load<ProjectData>(filename);
            prjData.ToAbsolutePaths();
            return prjData;
        }

    }
}
