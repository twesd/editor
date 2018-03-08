using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Common
{
    [Serializable]
    public class XRefOperationAdd : XRefOperation
    {
        /// <summary>
        /// Объект который был добавлен
        /// </summary>
        public object Item
        {
            get
            {
                return _oItem;
            }
            set
            {
                var item = value;
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
                if (!item.GetType().IsSerializable)
                {
                    throw new Exception("Объект должен быть сериализуем");
                }
                _oItem = item;
            }
        }

        object _oItem;

        public XRefOperationAdd(object item)
        {
            Item = item;
        }

        /// <summary>
        /// Для Serializable
        /// </summary>
        private XRefOperationAdd() { }
    }
}
