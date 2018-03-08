using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// Объект поддерживает уникальный индентификатор
    /// </summary>
    public interface ISupportId
    {
        /// <summary>
        /// Индентификатор объекта
        /// </summary>
        string Id { get; }
    }
}
