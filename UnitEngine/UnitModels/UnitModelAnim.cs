using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using CommonUI.UITypeEditors;

namespace UnitEngine
{
    [Serializable]
    public class UnitModelAnim : UnitModelBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Путь до файла модели")]
        [UITypeEditorAttributeFileNameFilter("Файл скрипта (*.x)|*.x|")]
        [Editor(typeof(UITypeEditorFileName), typeof(System.Drawing.Design.UITypeEditor))]
        public string ModelPath { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Использовать 32 бита")]
        public bool Use32Bit { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Использовать отсечение")]
        public bool Culling { get; set; }

        public UnitModelAnim()
        {
            ModelPath = string.Empty;
            Use32Bit = false;
            Culling = true;
        }

        public override void ToAbsolutePaths(string root)
        {
            ModelPath = Common.UtilPath.GetAbsolutePath(ModelPath, root);
        }

        public override void ToRelativePaths(string root)
        {
            ModelPath = Common.UtilPath.GetRelativePath(ModelPath, root);
        }

        public override string ToString()
        {
            return "Animation SceneNode";
        }
    }
}
