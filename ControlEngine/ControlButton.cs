using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Geometry;
using System.ComponentModel;
using System.Drawing;
using CommonUI;
using Common;
using CommonUI.UITypeEditors;
using System.Xml.Serialization;

namespace ControlEngine
{
    /// <summary>
    /// Обычная кнопка
    /// </summary>
    [Serializable]
    public class ControlButton : ControlBase, IControlSizeable
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
        [DescriptionAttribute("Путь до текстуры в нормальном состоянии")]
        [UITypeEditorAttributeFileNameFilter("Файл изображения (*.png,*.jpeg,*.jpg)|*.png;*.jpeg;*.jpg|")]
        [Editor(typeof(UITypeEditorFileName), typeof(System.Drawing.Design.UITypeEditor))]
        public string TextureNormal { get; set; }

        /// <summary>
        /// Путь до текстуры в активном состоянии
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Путь до текстуры в активном состоянии")]
        [UITypeEditorAttributeFileNameFilter("Файл изображения (*.png,*.jpeg,*.jpg)|*.png;*.jpeg;*.jpg|")]
        [Editor(typeof(UITypeEditorFileName), typeof(System.Drawing.Design.UITypeEditor))]
        public string TextureActivate { get; set; }

        /// <summary>
        /// Время после которого кнопка переходит в hold сотояние
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Время после которого кнопка переходит в hold сотояние")]
        public int HoldTime { get; set; }

        [CategoryAttribute("Звук")]
        [DescriptionAttribute("Путь до звука")]
        [UITypeEditorAttributeFileNameFilter("Файл звука (*.wav,*.mp3)|*.wav;*.mp3|")]
        [Editor(typeof(UITypeEditorFileName), typeof(System.Drawing.Design.UITypeEditor))]
        public string OnClickSound { get; set; }

        /// <summary>
        /// Событие на нажатие кнопки
        /// </summary>
        [CategoryAttribute("События")]
        [DescriptionAttribute("Событие на нажатие кнопки")]
        [Editor(typeof(CommonUI.UITypeEditors.UITypeEditorScript), typeof(System.Drawing.Design.UITypeEditor))]
        [ParserAutocompleteAttribute(ParserAutocompleteAttribute.ParserTypeWords.Controls | ParserAutocompleteAttribute.ParserTypeWords.Units)]
        public string OnClickDown { get; set; }

        /// <summary>
        /// Событие на нажатие кнопки
        /// </summary>
        [CategoryAttribute("События")]
        [DescriptionAttribute("Событие на нажатие кнопки")]
        [Editor(typeof(CommonUI.UITypeEditors.UITypeEditorScript), typeof(System.Drawing.Design.UITypeEditor))]
        [ParserAutocompleteAttribute(ParserAutocompleteAttribute.ParserTypeWords.Controls | ParserAutocompleteAttribute.ParserTypeWords.Units)]
        public string OnClickUp { get; set; }

        [NonSerialized]
        [XmlIgnore]
        private Size _size;

        public ControlButton() 
        {
            Position = new Vertex();
            HoldTime = 500;
            OnClickDown = string.Empty;
            OnClickUp = string.Empty;
        }

        public ControlButton(string name) : this() 
        {
            Name = name;
        }

        public override void ToRelativePaths(string root)
        {
            TextureNormal = Common.UtilPath.GetRelativePath(TextureNormal, root);
            TextureActivate = Common.UtilPath.GetRelativePath(TextureActivate, root);
            OnClickSound = Common.UtilPath.GetRelativePath(OnClickSound, root);
        }

        public override void ToAbsolutePaths(string root)
        {
            TextureNormal = Common.UtilPath.GetAbsolutePath(TextureNormal, root);
            TextureActivate = Common.UtilPath.GetAbsolutePath(TextureActivate, root);
            OnClickSound = Common.UtilPath.GetAbsolutePath(OnClickSound, root);
        }

        public Size GetSize()
        {
            if (_size.IsEmpty)
            {
                _size = UtilImage.GetImageSize(TextureNormal);
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
