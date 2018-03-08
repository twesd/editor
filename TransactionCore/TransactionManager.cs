using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace TransactionCore
{
    public class TransactionManager
    {
        public delegate void OnCommitEvent();

        /// <summary>
        /// Событие комита
        /// </summary>
        public event OnCommitEvent Commit;

        /// <summary>
        /// Текущая транзакция
        /// </summary>
        public Transaction TopTransaction
        {
            get
            {
                return _stackTransactions.Peek();
            }
        }

        /// <summary>
        /// Доступен ли откат изменений
        /// </summary>
        public bool CanUndo
        {
            get { return _undoStack.Count > 0; }
        }

        /// <summary>
        /// Доступен ли возрат изменений
        /// </summary>
        public bool CanRedo
        {
            get { return _redoStack.Count > 1; }
        }

        public bool EnableTansactions
        {
            get { return _enableTrans; }
        }

        /// <summary>
        /// Стэк изменений для Undo
        /// </summary>
        Stack<TransactionChanges> _undoStack = new Stack<TransactionChanges>();

        /// <summary>
        /// Стэк изменений для Redo
        /// </summary>
        Stack<TransactionChanges> _redoStack = new Stack<TransactionChanges>();

        /// <summary>
        /// Изменения в пределах всех запущенных транзакций
        /// </summary>
        TransactionChanges _allChanges;

        /// <summary>
        /// Сохранено ли последние состояние
        /// </summary>
        bool _lastStateSaved;

        /// <summary>
        /// Режим когда транзакции запрещены
        /// </summary>
        bool _enableTrans = true;

        /// <summary>
        /// Стэк транзакций
        /// </summary>
        Stack<Transaction> _stackTransactions = new Stack<Transaction>();

        /// <summary>
        /// Дополнительные типы для сериализации
        /// </summary>
        Type[] _extraTypes;

        public TransactionManager(Type[] extraTypes)
        {
            _extraTypes = extraTypes;
        }

        /// <summary>
        /// Начать транзакцию
        /// </summary>
        /// <returns></returns>
        public Transaction StartTransaction()
        {
            if (!_enableTrans)
            {
                return null;
            }
            var trans = new Transaction(_extraTypes);
            trans.OnCommit += new TransactionCommitDelegate(TransactionOnCommit);
            trans.OnAbort += new TransactionDelegate(TransactionOnAbort);
            if (_stackTransactions.Count == 0)
            {
                _allChanges = null;
            }
            _stackTransactions.Push(trans);
            return trans;
        }

        /// <summary>
        /// Добавление изменений
        /// </summary>
        /// <param name="oItem"></param>
        /// <param name="propName"></param>
        /// <param name="oldVal"></param>
        /// <param name="newVal"></param>
        public void AddChanges(object oItem, string propName, object oldVal, object newVal)
        {
            PropertyWorker.SetValue(oItem, propName, oldVal);
            
            Transaction trans = StartTransaction();
            trans.AddObject(oItem);
            PropertyWorker.SetValue(oItem, propName, newVal);
            trans.Commit();
        }

        /// <summary>
        /// Событие отмены транзакции
        /// </summary>
        void TransactionOnAbort()
        {
            _stackTransactions.Pop();
        }

        /// <summary>
        /// Событие завершения транзакции
        /// </summary>
        /// <param name="changes"></param>
        void TransactionOnCommit(TransactionChanges changes)
        {
            _stackTransactions.Pop();

            if (_allChanges == null)
            {
                _allChanges = changes;
            }
            else
            {
                _allChanges.Merge(changes);
            }
            
            // Изменения сохраняем только на верхней транзакции
            //
            if (_stackTransactions.Count == 0)
            {
                _undoStack.Push(_allChanges);
                _redoStack.Clear();
                _lastStateSaved = false;

                OnCommit();
            }
        }


        /// <summary>
        /// Откат изменений
        /// </summary>
        public void Undo()
        {
            AssertNoTrans();
            if (CanUndo)
            {
                _enableTrans = false;
                try
                {
                    TransactionChanges transChanges = _undoStack.Pop();

                    if (_redoStack.Count == 0 && !_lastStateSaved)
                    {
                        // Сохраняем текущее состояния объектов
                        TransactionChanges lastState = new TransactionChanges();
                        foreach (object oItem in transChanges.OrigItems)
                        {
                            lastState.AddOriginalItem(oItem, _extraTypes);
                        }
                        _redoStack.Push(lastState);
                        _lastStateSaved = true;
                    }
                    transChanges.SetOriginalValues(_extraTypes);
                    _redoStack.Push(transChanges);
                }
                finally
                {
                    _enableTrans = true;
                }
            }
        }

        /// <summary>
        /// Возрат изменений
        /// </summary>
        public void Redo()
        {
            AssertNoTrans();
            if (CanRedo)
            {
                _enableTrans = false;
                try
                {
                    _undoStack.Push(_redoStack.Pop());
                    TransactionChanges transChanges = _redoStack.Peek();
                    transChanges.SetOriginalValues(_extraTypes);
                    _undoStack.Push(transChanges);
                }
                finally
                {
                    _enableTrans = true;
                }
            }
        }

        /// <summary>
        /// Очистка всех состояний
        /// </summary>
        public void Clear()
        {
            ClearTransactions();
            _undoStack = new Stack<TransactionChanges>();
            _redoStack = new Stack<TransactionChanges>();
        }

        /// <summary>
        /// Очистка всех транзакций
        /// </summary>
        public void ClearTransactions()
        {
            foreach (Transaction trans in _stackTransactions)
            {
                trans.Dispose();
            }
            _stackTransactions.Clear();
        }

        private void OnCommit()
        {
            var handler = Commit;
            if (handler != null)
            {
                handler();
            }
        }

        private void AssertNoTrans()
        {
            if (_stackTransactions.Count > 0)
            {
                throw new Exception("Имеются не завершённые транзакции");
            }
        }

    }
}
