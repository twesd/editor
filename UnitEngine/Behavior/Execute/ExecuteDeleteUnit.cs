using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Удалить дочерний юнит
    /// </summary>
    [Serializable]
    public class ExecuteDeleteUnit : ExecuteBase
    {
        /// <summary>
        /// Путь до файла поведений
        /// </summary>
        public string BehaviorsPath;


        /// <summary>
        /// Иницилизация
        /// </summary>
        public ExecuteDeleteUnit(string behaviorPath)
        {
            BehaviorsPath = behaviorPath;
        }

        /// <summary>
        /// Для Serializable
        /// </summary>
        private ExecuteDeleteUnit()
        {
        }

        public override void ToRelativePaths(string root)
        {
            BehaviorsPath = Common.UtilPath.GetRelativePath(BehaviorsPath, root);
        }

        public override void ToAbsolutePaths(string root)
        {
            BehaviorsPath = Common.UtilPath.GetAbsolutePath(BehaviorsPath, root);
        }

        public override string ToString()
        {
            return string.Format("Удалить юнит {0}", 
                System.IO.Path.GetFileNameWithoutExtension(BehaviorsPath));
        }
    }
}
