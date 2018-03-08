using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public interface IPathConvertible
    {
        /// <summary>
        /// Преобразовать пути в относительные
        /// </summary>
        /// <param name="root"></param>
        void ToRelativePaths(string root);

        /// <summary>
        /// Преобразовать пути в абсолютные
        /// </summary>
        /// <param name="root"></param>
        void ToAbsolutePaths(string root);
    }
}
