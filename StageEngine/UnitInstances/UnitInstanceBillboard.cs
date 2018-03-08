using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Common.Geometry;
using UnitEngine;
using CommonUI.UITypeEditors;
using Common;

namespace StageEngine
{
    /// <summary>
    /// Экземпляр юнита окружающей среды
    /// </summary>
    [Serializable]
    public class UnitInstanceBillboard : UnitInstanceBase, IPathConvertible
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

        /// <summary>
        /// Индентификатор модели
        /// </summary>
        [CategoryAttribute("Стандартные")]
        [DescriptionAttribute("Идентификатор модели")]
        public int NodeId { get; set; }

        public UnitInstanceBillboard()
        {
            Texture = string.Empty;
            NodeId = 0;
            ViewVector = new Vertex(0, 1, 0);
            UpVector = new Vertex(0, 1, 0);
            MaterialType = UnitEngine.MaterialType.AddColor;
        }


        /// <summary>
        /// Перевести пути в абсолютные
        /// </summary>
        /// <param name="root"></param>
        public void ToAbsolutePaths(string root)
        {
            Texture = Common.UtilPath.GetAbsolutePath(Texture, root);
        }

        /// <summary>
        /// Перевести пути в относительные
        /// </summary>
        /// <param name="root"></param>
        public void ToRelativePaths(string root)
        {
            Texture = Common.UtilPath.GetRelativePath(Texture, root);
        }
    }
}
