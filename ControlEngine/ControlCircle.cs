
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Geometry;
using System.ComponentModel;
using System.Drawing;
using CommonUI;
using Common;
using System.Xml.Serialization;

namespace ControlEngine
{
    /// <summary>
    /// Обычная кнопка
    /// </summary>
    [Serializable]
    public class ControlCircle : ControlBase, IControlSizeable
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
        [DescriptionAttribute("Путь до текстуры заднего плана")]
        [Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string TextureBackground { get; set; }

        /// <summary>
        /// Путь до текстуры в активном состоянии
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Путь до текстуры в центре")]
        [Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string TextureCenter { get; set; }

        [NonSerialized]
        [XmlIgnore]
        private Size _size;

        public ControlCircle() 
        {
            Position = new Vertex();
        }

        public ControlCircle(string name)
            : this() 
        {
            Name = name;
        }

        public override void ToRelativePaths(string root)
        {
            TextureBackground = Common.UtilPath.GetRelativePath(TextureBackground, root);
            TextureCenter = Common.UtilPath.GetRelativePath(TextureCenter, root);
        }

        public override void ToAbsolutePaths(string root)
        {
            TextureBackground = Common.UtilPath.GetAbsolutePath(TextureBackground, root);
            TextureCenter = Common.UtilPath.GetAbsolutePath(TextureCenter, root);
        }

        public Size GetCenterSize()
        {
            if (_size.IsEmpty)
            {
                _size = UtilImage.GetImageSize(TextureCenter);
            }
            return _size;
        }

        public Size GetSize()
        {
            return UtilImage.GetImageSize(TextureBackground);
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
