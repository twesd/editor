using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PublishData.Actions
{
    class DeleteTempFilesAction : IAction
    {
        public void Execute(ProjectData projectData)
        {
            foreach (string destDir in projectData.DestDirs)
            {
                // Заменяем имя у крневой папки, так как она ссылкается на источник
                var rootFolder = projectData.RootFolder.Clone();
                rootFolder.Name = new DirectoryInfo(destDir).Name;

                DeleteTempFiles(
                    Path.GetDirectoryName(destDir),
                    new List<FolderData> { rootFolder });
            }
        }

        private static void DeleteTempFiles(string root, List<FolderData> folderData)
        {
            if (folderData == null)
            {
                return;
            }
            foreach (FolderData fData in folderData)
            {
                string dirName = Path.Combine(root, fData.Name);

                if (fData.Settings.DeleteBackFiles)
                {
                    DirectoryInfo dir = new DirectoryInfo(dirName);
                    FileInfo[] files = dir.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        string temppath = Path.Combine(dirName, file.Name);
                        string exst = Path.GetExtension(file.Name).ToLower();
                        if (exst == ".bpng" ||
                            exst.StartsWith(".bak") ||
                            exst.StartsWith(".bk"))
                        {
                            File.Delete(temppath);
                        }
                    }
                }

                DeleteTempFiles(dirName, fData.Childs);
            }
        }
    }
}
