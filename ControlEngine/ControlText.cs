using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Common.Geometry;
using System.Drawing.Design;

namespace ControlEngine
{
    [Serializable]
    public class ControlText : ControlBase, IControlSizeable
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Текст")]
        public string Text { get; set; }

        /// <summary>
        /// Позиция кнопки
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Позиция")]
        public Vertex Position { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Шрифт")]
        [Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(UITypeEditor))]
        public string FontPath { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Отступ между буквами")]
        public int KerningWidth { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Отступ между строками")]
        public int KerningHeight { get; set; }

        public ControlText()
        {
            Text = string.Empty;
            FontPath = string.Empty;
            Position = new Vertex();
            KerningWidth = 0;
            KerningHeight = 0;
        }

        public override void ToRelativePaths(string root)
        {
            FontPath = Common.UtilPath.GetRelativePath(FontPath, root);
        }

        public override void ToAbsolutePaths(string root)
        {
            FontPath = Common.UtilPath.GetAbsolutePath(FontPath, root);
        }

        public System.Drawing.Size GetSize()
        {
            System.Drawing.Size size = CommonUI.UtilImage.GetImageSize(System.IO.Path.Combine(FontPath, "A.png"));
            if (string.IsNullOrEmpty(Text)) return size;
            string[] strs = Text.Split(new string[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);            
            int maxWidth = strs[0].Length;
            foreach (string str in strs)
            {
                if (str.Length > maxWidth)
                    maxWidth = str.Length;
            }

            size.Width *= maxWidth;
            if (maxWidth > 1)
                size.Width += (maxWidth - 1) * KerningWidth;
            size.Height *= strs.Length;
            if (strs.Length > 1)
                size.Height += (strs.Length - 1) * KerningHeight;
            return size;
        }

        public bool IsPointInside(int x, int y)
        {
            System.Drawing.Size size = GetSize();
            if (size.IsEmpty) return false;
            return (x >= Position.X && x <= (Position.X + size.Width) &&
                y >= Position.Y && y <= (Position.Y + size.Height));
        }
    }
}
