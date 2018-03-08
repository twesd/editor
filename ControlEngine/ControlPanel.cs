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
    public class ControlPanel : ControlBase, IControlSizeable
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Позиция элемента")]
        public Vertex Position { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Ширина")]
        public int Width { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Высота")]
        public int Height { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Индентификаторы потомков")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public List<string> ChildrenIds { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Горизонтальный скрол")]
        public ControlComponentScroll ScrollHorz { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Вертикальный скрол")]
        public ControlComponentScroll ScrollVert { get; set; }

        public ControlPanel() 
        {
            Position = new Vertex();
            ScrollHorz = new ControlComponentScroll()
            {
                IsVertical = false
            };
            ScrollVert = new ControlComponentScroll()
            {
                IsVertical = true
            };
            Width = 10;
            Height = 10;
            ChildrenIds = new List<string>();
        }

        public System.Drawing.Size GetSize()
        {
            return new System.Drawing.Size(Width, Height);
        }

        public bool IsPointInside(int x, int y)
        {
            Size size = GetSize();
            if (size.IsEmpty) return false;
            return (x >= Position.X && x <= (Position.X + size.Width) &&
                y >= Position.Y && y <= (Position.Y + size.Height));
        }
        
        public override void ToRelativePaths(string root)
        {
            ScrollHorz.ToRelativePaths(root);
            ScrollVert.ToRelativePaths(root);
        }

        public override void ToAbsolutePaths(string root)
        {
            ScrollHorz.ToAbsolutePaths(root);
            ScrollVert.ToAbsolutePaths(root);
        }
    }
}
