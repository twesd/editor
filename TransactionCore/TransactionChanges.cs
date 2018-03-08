using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace TransactionCore
{
    /// <summary>
    /// Изменения произведённые в транзакции
    /// </summary>
    class TransactionChanges
    {
        /// <summary>
        /// Оригинальные объекты
        /// </summary>
        public List<object> OrigItems
        {
            get
            {
                return _items.Keys.ToList();
            }
        }

        Dictionary<object, object> _items = new Dictionary<object, object>();

        public void Merge(TransactionChanges topChanges)
        {
            foreach (var pair in topChanges._items)
            {
                _items[pair.Key] = pair.Value;
            }
        }

        /// <summary>
        /// Добавить данные
        /// </summary>
        /// <param name="oItem"></param>
        public void AddOriginalItem(object oItem, Type[] extraTypes)
        {
            if (_items.ContainsKey(oItem))
                return;
            if (oItem is IList)
            {
                // Клонирование списка листа
                var list = oItem as IList;
                ArrayList destList = new ArrayList(list);
                _items.Add(oItem, destList);
            }
            else if (oItem.GetType().IsSerializable)
            {
                // Полное клонирование, возрат к предыдущим свойствам гарантирован
                var oClone = SerializationWorker.Serialize(oItem, extraTypes);
                _items.Add(oItem, oClone);
            }            
            else
            {
                // Не полное клонирование
                _items.Add(oItem, CloneObject(oItem));
            }
        }

        /// <summary>
        /// Установить начальные данные для объекта
        /// </summary>
        public void SetOriginalValues(Type[] extraTypes)
        {
            foreach (var itemPair in _items)
            {
                var target = itemPair.Key;
                if (target is IList)
                {
                    // Копируем коллекцию
                    var targetList = target as IList;
                    ArrayList source = itemPair.Value as ArrayList;
                    targetList.Clear();
                    foreach (object item in source)
                    {
                        targetList.Add(item);
                    }
                }
                else if (target.GetType().IsSerializable)
                {
                    var source = SerializationWorker.Deserialize((string)itemPair.Value, target.GetType(), extraTypes);
                    // Копируем свойства объекта
                    PropertyWorker.Copy(source, target);
                }
                else
                {
                    var source = itemPair.Value;
                    PropertyWorker.Copy(source, target);
                }
            }
        }

        private object CloneObject(object o)
        {
            Type t = o.GetType();
            PropertyInfo[] properties = t.GetProperties();

            Object p = t.InvokeMember("", System.Reflection.BindingFlags.CreateInstance,
                null, o, null);

            foreach (PropertyInfo pi in properties)
            {
                if (pi.CanWrite)
                {
                    pi.SetValue(p, pi.GetValue(o, null), null);
                }
            }

            return p;
        }
    }
}
