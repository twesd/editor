using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransactionCore
{
    /// <summary>
    ///  Событие завершения транзакции
    /// </summary>
    /// <param name="changes"></param>
    internal delegate void TransactionCommitDelegate(TransactionChanges changes);

    /// <summary>
    ///  Событие транзакции
    /// </summary>
    internal delegate void TransactionDelegate();

    /// <summary>
    /// Транзакция
    /// </summary>
    public class Transaction : IDisposable
    {
        /// <summary>
        /// Событие завершения транзакции
        /// </summary>
        internal event TransactionCommitDelegate OnCommit;

        /// <summary>
        /// Событие отмены транзакции
        /// </summary>
        internal event TransactionDelegate OnAbort;

        /// <summary>
        /// Состояние транзакции
        /// </summary>
        TransactionState _state;

        /// <summary>
        /// Изменения
        /// </summary>
        TransactionChanges _transChanges;

        /// <summary>
        /// Дополнительные типы для сериализации
        /// </summary>
        Type[] _extraTypes;

        bool _isDisposed = false;

        internal Transaction(Type[] extraTypes)
        {
            _state = TransactionState.Running;
            _transChanges = new TransactionChanges();
            _extraTypes = extraTypes;
        }

        /// <summary>
        /// Деиницилизация компонента
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed) return;
            if (_state == TransactionState.Running)
            {
                Abort();
            }
            _isDisposed = true;
        }

        /// <summary>
        /// Добавить объект в транзакцию
        /// </summary>
        /// <param name="oItem"></param>
        /// <returns></returns>
        public void AddObject(object oItem)
        {
            _transChanges.AddOriginalItem(oItem, _extraTypes);
        }

        /// <summary>
        /// Завершить транзакцию
        /// </summary>
        public void Commit()
        {
            if (_state != TransactionState.Running)
                throw new Exception("Неверное состояние транзакции");
            _state = TransactionState.Commit;
            if (OnCommit != null)
                OnCommit(_transChanges);
        }

        /// <summary>
        /// Отмена транзакции
        /// </summary>
        public void Abort()
        {
            if (_state != TransactionState.Running)
                throw new Exception("Неверное состояние транзакции");

            _transChanges.SetOriginalValues(_extraTypes);

            _state = TransactionState.Abort;
            if (OnAbort != null)
                OnAbort();
        }


    }
}
