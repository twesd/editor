using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections;

namespace TransactionCore
{
    /// <summary>
    /// Класс для работы со свойствами свойств
    /// </summary>
    class PropertyWorker
    {
        /// <summary>
        /// Установить свойство объекта
        /// </summary>
        /// <param name="oItem"></param>
        /// <param name="propName"></param>
        /// <param name="val"></param>
        public static void SetValue(object oItem, string propName, object val)
        {
            foreach (PropertyInfo destProperty in oItem.GetType().GetProperties())
            {
                if (destProperty.Name == propName)
                {
                    SetPropertyValue(oItem, destProperty, val);
                    return;
                }
            }
        }

        /// <summary>
        /// Копирование свойств из одного объекта в другой
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void Copy(object source, object destination)
        {
            var destProperties = destination.GetType().GetProperties();

            foreach (var sourceProperty in source.GetType().GetProperties())
            {
                foreach (var destProperty in destProperties)
                {
                    if (destProperty.Name == sourceProperty.Name)
                    {
                        object val = sourceProperty.GetValue(
                            source, new object[] { });
                        SetPropertyValue(destination, destProperty, val);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Установить значение
        /// </summary>
        /// <param name="oItem"></param>
        /// <param name="destProperty"></param>
        /// <param name="newVal"></param>
        private static void SetPropertyValue(object oItem, PropertyInfo destProperty, object newVal)
        {
            if (newVal != null &&
                (!destProperty.PropertyType.IsAssignableFrom(newVal.GetType()) ||
                !destProperty.CanWrite))
            {
                if (newVal is IList &&
                    typeof(IList).IsAssignableFrom(destProperty.PropertyType))
                {
                    IList list = (IList)destProperty.GetValue(oItem, new object[] { });
                    list.Clear();
                    foreach (object listItem in (IList)newVal)
                    {
                        list.Add(listItem);
                    }
                }
                else
                {
                    if (!destProperty.CanWrite)
                    {
                        throw new ArgumentException("Свойство " + destProperty.Name + " не доступно для записи");
                    }
                    else
                    {
                        throw new ArgumentException(
                            "Не верное значение для записи в свойство " + destProperty.Name + " " + newVal.ToString());
                    }
                }
            }
            else
            {
                if (!destProperty.CanWrite)
                {
                    throw new ArgumentException("Свойство " + destProperty.Name + " не доступно для записи");
                }

                destProperty.SetValue(oItem, newVal, new object[] { });
            }
        }

    }
}
