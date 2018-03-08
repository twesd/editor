using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PublishData.Actions;

namespace PublishData
{
    class ProcessWorker
    {
        static public void Execute(ProjectData projectData)
        {
            if (!Check(projectData))
            {
                return;
            }

            List<IAction> actions = new List<IAction>();
            actions.Add(new CopyAction());
            actions.Add(new DeleteTempFilesAction());
            actions.Add(new RotationImageAction());

            foreach (IAction action in actions)
            {
                action.Execute(projectData);
            }

        }


       
        
        private static bool Check(ProjectData projectData)
        {
            if (string.IsNullOrEmpty(projectData.RootDir))
            {
                MessageBox.Show("Корневая директория не задана");
                return false;
            }

            if (projectData.RootFolder.Childs.Count == 0)
            {
                MessageBox.Show("Исходные директории не заданы");
                return false;
            }

            if (projectData.DestDirs.Count == 0)
            {
                MessageBox.Show("Выходные директории не заданы");
                return false;
            }
            return true;
        }

        
    }
}
