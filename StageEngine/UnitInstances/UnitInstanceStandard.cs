using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Xml.Serialization;
using System.ComponentModel;

namespace StageEngine
{
    /// <summary>
    /// Экземпляр юнита стандартный
    /// </summary>
    [Serializable]
    public class UnitInstanceStandard : UnitInstanceBase, IPathConvertible
    {
        /// <summary>
        /// Путь до файла поведения
        /// </summary>
        [CategoryAttribute("Стандартные")]
        [DescriptionAttribute("Путь до файла поведения")]
        [Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string BehaviorsPath { get; set; }

        /// <summary>
        /// Поведения юнита (Вспомогательный параметр) тип UnitBehavior
        /// </summary>
        [XmlIgnore]
        public UnitEngine.UnitBehavior Behavior;

        public UnitInstanceStandard() 
        {
            BehaviorsPath = string.Empty;
        }

        public UnitInstanceStandard(string name, string behaviorsPath) :
            base(name)
        {
            BehaviorsPath = behaviorsPath;
        }

        /// <summary>
        /// Преобразовать в относительные пути
        /// </summary>
        /// <param name="root"></param>
        public void ToRelativePaths(string root)
        {
            if (Behavior != null)
            {
                Behavior.ToRelativePaths(root);
            }
            BehaviorsPath = Common.UtilPath.GetRelativePath(BehaviorsPath, root);
        }

        /// <summary>
        /// Преобразовать в абсолютные пути
        /// </summary>
        /// <param name="root"></param>
        public void ToAbsolutePaths(string root)
        {
            if (Behavior != null)
            {
                Behavior.ToAbsolutePaths(root);
            }
            BehaviorsPath = Common.UtilPath.GetAbsolutePath(BehaviorsPath, root);
        }
    }
}
