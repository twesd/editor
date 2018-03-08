using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransactionCore
{
    /// <summary>
    /// Состояния транзакции
    /// </summary>
    enum TransactionState
    {
        /// <summary>
        /// В запущенном состоянии
        /// </summary>
        Running,
        /// <summary>
        /// Завершена
        /// </summary>
        Commit,
        /// <summary>
        /// Отмена
        /// </summary>
        Abort
    }
}
