using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PublishData.Actions
{
    class CopyAction : IAction
    {
        public void Execute(ProjectData projectData)
        {
            foreach (string destDir in projectData.DestDirs)
            {
                if (Directory.Exists(destDir))
                {
                    Directory.Delete(destDir, true);
                }
                DirectoryCopy(projectData.RootDir, destDir, true);
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    if (subdir.Name.StartsWith("."))
                    {
                        continue;
                    }
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
