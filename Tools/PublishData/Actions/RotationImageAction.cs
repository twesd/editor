using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace PublishData.Actions
{
    class RotationImageAction : IAction
    {
        public void Execute(ProjectData projectData)
        {
            foreach (string destDir in projectData.DestDirs)
            {
                // Заменяем имя у крневой папки, так как она ссылкается на источник
                var rootFolder = projectData.RootFolder.Clone();
                rootFolder.Name = new DirectoryInfo(destDir).Name;

                RotateImages(
                    Path.GetDirectoryName(destDir),
                    new List<FolderData> { rootFolder });
            }
        }

        private void RotateImages(string root, List<FolderData> folderData)
        {
            if (folderData == null)
            {
                return;
            }
            foreach (FolderData fData in folderData)
            {
                string dirName = Path.Combine(root, fData.Name);

                if (fData.Settings.RotateImages)
                {
                    DirectoryInfo dir = new DirectoryInfo(dirName);
                    FileInfo[] files = dir.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        string temppath = Path.Combine(dirName, file.Name);
                        string exst = Path.GetExtension(file.Name).ToLower();
                        if (exst == ".jpg" ||
                            exst.StartsWith(".jpeg") ||
                            exst.StartsWith(".png") ||
                            exst.StartsWith(".bmp"))
                        {
                            RotateImage(temppath);
                        }
                    }
                }

                RotateImages(dirName, fData.Childs);
            }
        }

        /// <summary>
        /// Перевернуть изображение
        /// </summary>
        /// <param name="temppath"></param>
        private void RotateImage(string temppath)
        {
            var img = Bitmap.FromFile(temppath);
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            img.Save(temppath);

            /*
            Bitmap sourceBitmap = ....;
            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(returnBitmap);
            //move rotation point to center of image
            g.TranslateTransform((float)sourceBitmap.Width / 2, (float)sourceBitmap.Height / 2);
            //rotate
            g.RotateTransform(90);
            //move image back
            g.TranslateTransform(-(float)sourceBitmap.Width / 2, -(float)sourceBitmap.Height / 2);
            //draw passed in image onto graphics object
            g.DrawImage(sourceBitmap, new Point(0, 0));
            //returnBitmap.Save(temppath, System.Drawing.Imaging.ImageFormat.Png);
            returnBitmap.Save(temppath);*/
        }
    }
}
