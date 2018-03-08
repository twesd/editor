using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Common.Geometry;
using System.Drawing;
using CommonUI;
using System.Xml.Serialization;

namespace ControlEngine
{
    /// <summary>
    /// Обычное изображение
    /// </summary>
    [Serializable]
    public class ControlImage : ControlBase, IControlSizeable
    {
        /// <summary>
        /// Позиция кнопки
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Позиция кнопки")]
        public Vertex Position { get; set; }

        /// <summary>
        /// Путь до текстуры в нормальном состоянии
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Путь до текстуры")]
        [Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Texture { get; set; }

        [NonSerialized]
        [XmlIgnore]
        private Size _size;

        public ControlImage()
        {
            Position = new Vertex();
        }

        public override void ToRelativePaths(string root)
        {
            Texture = Common.UtilPath.GetRelativePath(Texture, root);
        }

        public override void ToAbsolutePaths(string root)
        {
            Texture = Common.UtilPath.GetAbsolutePath(Texture, root);
        }

        public Size GetSize()
        {
            if (_size.IsEmpty)
            {
                _size = UtilImage.GetImageSize(Texture);
            }
            return _size;
        }

        public bool IsPointInside(int x, int y)
        {
            Size size = GetSize();
            if (size.IsEmpty) return false;
            return (x >= Position.X && x <= (Position.X + size.Width) &&
                y >= Position.Y && y <= (Position.Y + size.Height));
        }
    }
}
