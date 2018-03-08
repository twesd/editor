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

namespace ControlEngine
{
    /// <summary>
    /// Обычная кнопка
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Serializable]
    public class ControlComponentScroll
    {
        [Browsable(false)]
        public bool IsVertical { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Текстура бегунка")]
        [UITypeEditorAttributeFileNameFilter("Файл изображения (*.png,*.jpeg,*.jpg)|*.png;*.jpeg;*.jpg|")]
        [Editor(typeof(UITypeEditorFileName), typeof(System.Drawing.Design.UITypeEditor))]
        public string TextureBody { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Текстура заднего фона")]
        [UITypeEditorAttributeFileNameFilter("Файл изображения (*.png,*.jpeg,*.jpg)|*.png;*.jpeg;*.jpg|")]
        [Editor(typeof(UITypeEditorFileName), typeof(System.Drawing.Design.UITypeEditor))]
        public string TextureBackground { get; set; }


        public ControlComponentScroll() 
        {
           
        }

        public void ToRelativePaths(string root)
        {
            TextureBody = Common.UtilPath.GetRelativePath(TextureBody, root);
            TextureBackground = Common.UtilPath.GetRelativePath(TextureBackground, root);
        }

        public void ToAbsolutePaths(string root)
        {
            TextureBody = Common.UtilPath.GetAbsolutePath(TextureBody, root);
            TextureBackground = Common.UtilPath.GetAbsolutePath(TextureBackground, root);
        }

    }
}
