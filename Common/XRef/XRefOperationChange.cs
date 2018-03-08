using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// Операция удаления во внешней ссылки
    /// </summary>
    [Serializable]
    public class XRefOperationChange : XRefOperation
    {
        /// <summary>
        /// Индентификатор объекта
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Имя свойства
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Значение свойства
        /// </summary>
        public object PropertyValue { get; set; }

        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="id">Индентификатор объекта</param>
        /// <param name="propName">Имя свойства</param>
        /// <param name="propValue">Значение свойства</param>
        public XRefOperationChange(string id, string propName, object propValue)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("id");
            }
            if (string.IsNullOrEmpty(propName))
            {
                throw new ArgumentException("propName");
            }
            if (propValue != null &&
                !propValue.GetType().IsSerializable)
            {
                throw new ArgumentException("Значение должно быть сериализуемо");
            }
            Id = id;
            PropertyName = propName;
            PropertyValue = propValue;
        }

        /// <summary>
        /// Для Serializable
        /// </summary>
        private XRefOperationChange() { }
    }
}
