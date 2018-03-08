using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Установить связь с параметрами модели
    /// </summary>
    [Serializable]
    public class ExecuteMappingTransform : ExecuteBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Использовать модель данного поведения")]
        public bool UseThisBehavior { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Путь до дочернего поведения")]
        [Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string BehaviorChildPath { get; set; }

        [CategoryAttribute("Параметры модели")]
        [DescriptionAttribute("Масштаб по X")]
        public string ScaleX { get; set; }

        [CategoryAttribute("Параметры модели")]
        [DescriptionAttribute("Масштаб по Y")]
        public string ScaleY { get; set; }

        [CategoryAttribute("Параметры модели")]
        [DescriptionAttribute("Масштаб по Z")]
        public string ScaleZ { get; set; }

        [CategoryAttribute("Параметры модели")]
        [DescriptionAttribute("Позиция по X")]
        public string PositionX { get; set; }

        [CategoryAttribute("Параметры модели")]
        [DescriptionAttribute("Позиция по Y")]
        public string PositionY { get; set; }

        [CategoryAttribute("Параметры модели")]
        [DescriptionAttribute("Позиция по Z")]
        public string PositionZ { get; set; }

        [CategoryAttribute("Параметры модели")]
        [DescriptionAttribute("Поворот по X")]
        public string RotationX { get; set; }

        [CategoryAttribute("Параметры модели")]
        [DescriptionAttribute("Поворот по Y")]
        public string RotationY { get; set; }

        [CategoryAttribute("Параметры модели")]
        [DescriptionAttribute("Поворот по Z")]
        public string RotationZ { get; set; }
        
        public ExecuteMappingTransform()
        {
            UseThisBehavior = true;
        }

        public override void ToRelativePaths(string root)
        {
            BehaviorChildPath = Common.UtilPath.GetRelativePath(BehaviorChildPath, root);
        }

        public override void ToAbsolutePaths(string root)
        {
            BehaviorChildPath = Common.UtilPath.GetAbsolutePath(BehaviorChildPath, root);
        }

        
        public override string ToString()
        {
            return string.Format("Cвязь с параметрами модели");
        }
    }
}
