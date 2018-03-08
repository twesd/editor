using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using CommonUI.UITypeEditors;
using Common.Geometry;

namespace UnitEngine
{
    [Serializable]
    public class UnitModelBillboard : UnitModelBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Ширина")]
        public float Width { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Высота")]
        public float Height { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Путь до текстуры")]
        [UITypeEditorAttributeFileNameFilter("Файл изображения (*.png,*.jpeg,*.jpg)|*.png;*.jpeg;*.jpg|")]
        [Editor(typeof(UITypeEditorFileName), typeof(System.Drawing.Design.UITypeEditor))]
        public string Texture { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Использовать 32 бита для текстуры")]
        public bool Use32Bit { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Тип материи")]
        public MaterialType MaterialType { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Использовать UpVector")]
        public bool UseUpVector { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Up вектор камеры")]
        public Vertex UpVector { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Использовать ViewVector")]
        public bool UseViewVector { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("ViewVector")]
        public Vertex ViewVector { get; set; }

        public UnitModelBillboard()
        {
            Width = 10;
            Height = 10;
            Texture = string.Empty;
            Use32Bit = false;
            MaterialType = MaterialType.Solid;
            UseUpVector = true;
            UpVector = new Vertex(0, 1, 0);
            UseViewVector = false;
            ViewVector = new Vertex(0, 1, 0);
        }

        public override void ToAbsolutePaths(string root)
        {
            Texture = Common.UtilPath.GetAbsolutePath(Texture, root);
        }

        public override void ToRelativePaths(string root)
        {
            Texture = Common.UtilPath.GetRelativePath(Texture, root);
        }

        public override string ToString()
        {
            return "Billboard";
        }
    }
}
