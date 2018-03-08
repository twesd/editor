using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CommonUI.UITypeEditors;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Действия для воспроизведения звука
    /// </summary>
    [Serializable]
    public class ExecuteSound : ExecuteBase
    {
        /// <summary>
        /// Путь до звукового файла
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Путь до звукового файла")]
        [UITypeEditorAttributeFileNameFilter("Файл изображения (*.wav,*.mp3)|*.wav;*.mp3|")]
        [Editor(typeof(UITypeEditorFileName), typeof(System.Drawing.Design.UITypeEditor))]
        public string Filename { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Повторять звук")]
        public bool Loop { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Формат звука")]
        public SoundFormat Format { get; set; }

        public override void ToRelativePaths(string root)
        {
            Filename = Common.UtilPath.GetRelativePath(Filename, root);
        }

        public override void ToAbsolutePaths(string root)
        {
            Filename = Common.UtilPath.GetAbsolutePath(Filename, root);
        }

        public override string ToString()
        {
            return string.Format("Звук {0}", System.IO.Path.GetFileName(Filename));
        }
    }
}
