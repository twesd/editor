using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CommonUI
{
    public class UtilImage
    {
        /// <summary>
        /// Сохранить содержимое контрола в файл
        /// </summary>
        /// <param name="control"></param>
        /// <param name="filename"></param>
        public static void CaptureFromScreenToFile(Control control, string filename)
        {
            Bitmap bitmap = new Bitmap(control.Width, control.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            var rect = control.RectangleToScreen(control.ClientRectangle);
            graphics.CopyFromScreen(rect.Location, Point.Empty, control.Size);
            //control.DrawToBitmap
            //control.CreateGraphics();
            bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
            bitmap.Dispose();
        }

        /// <summary>
        /// Получение BitmapImage из Bitmap 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapImage GetBitmapImage(Bitmap bitmap)
        {
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = stream;
            bmp.EndInit();

            return bmp;
        }

        /// <summary>
        /// Получение BitmapImage из Icon 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static BitmapImage GetBitmapImage(Icon image)
        {
            MemoryStream stream = new MemoryStream();
            image.Save(stream);
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = stream;
            bmp.EndInit();

            return bmp;
        }

        /// <summary>
        /// Получить размер изображения
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Size GetImageSize(string filename)
        {
            Size size = new Size(0, 0);
            if (!File.Exists(filename))
                return size;
            Image image = Bitmap.FromFile(filename);
            return image.Size;
        }
    }
}
