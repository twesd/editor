using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.ComponentModel;

namespace StageEngine
{
    /// <summary>
    /// Экземпляр юнита окружающей среды
    /// </summary>
    [Serializable]
    public class UnitInstanceEnv : UnitInstanceBase, IPathConvertible
    {
        /// <summary>
        /// Путь до модели
        /// </summary>
        [CategoryAttribute("Стандартные")]
        [DescriptionAttribute("Путь до файла модели")]
        [Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ModelPath { get; set; }

        /// <summary>
        /// Индентификатор модели
        /// </summary>
        [CategoryAttribute("Стандартные")]
        [DescriptionAttribute("Идентификатор модели")]
        public int NodeId { get; set; }

        public UnitInstanceEnv() 
        {
            ModelPath = string.Empty;
            NodeId = 0;
        }

        public UnitInstanceEnv(string name, string modelPath) :
            base(name)
        {
            ModelPath = modelPath;
        }

        /// <summary>
        /// Перевести пути в абсолютные
        /// </summary>
        /// <param name="root"></param>
        public void ToAbsolutePaths(string root)
        {
            ModelPath = Common.UtilPath.GetAbsolutePath(ModelPath, root);
        }

        /// <summary>
        /// Перевести пути в относительные
        /// </summary>
        /// <param name="root"></param>
        public void ToRelativePaths(string root)
        {
            ModelPath = Common.UtilPath.GetRelativePath(ModelPath, root);            
        }
    }
}
