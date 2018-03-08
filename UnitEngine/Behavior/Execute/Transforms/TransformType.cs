using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitEngine
{
    /// <summary>
    /// Типы передвижения
    /// </summary>
    [Serializable]
    public enum TransformType
    {
        /// <summary>
        /// Линейное перемещение
        /// </summary>
        LINE = 0,
        /// <summary>
        /// Линейное вращение
        /// </summary>
        ROTATE = 1,
        /// <summary>
        /// Линейное скалирование
        /// </summary>
        SCALE = 2
    }
}
