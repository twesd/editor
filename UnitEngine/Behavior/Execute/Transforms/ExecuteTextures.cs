using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Аниматор текстур
    /// </summary>
    [Serializable]
    public class ExecuteTextures : ExecuteBase
    {
        /// <summary>
        /// Пути до текстур
        /// </summary>
        public List<string> Paths = new List<string>();
        
        /// <summary>
        /// Интервал между текстурами
        /// </summary>
        public UInt32 TimePerFrame = 100;

        /// <summary>
        /// Повторять
        /// </summary>
        public bool Loop = false;

        /// <summary>
        /// Использовать 32 бита
        /// </summary>
        public bool Use32Bit = false;

        public override void ToRelativePaths(string root)
        {
            Paths = Common.UtilPath.GetRelativePath(Paths, root);
        }

        public override void ToAbsolutePaths(string root)
        {
            Paths = Common.UtilPath.GetAbsolutePath(Paths, root);
        }

        public override string ToString()
        {
            return string.Format("Текстуры ");
        }
    }
}
