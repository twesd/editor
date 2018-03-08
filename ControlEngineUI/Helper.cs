using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonUI;
using ControlEngine;
using Common;

namespace ControlEngineUI
{
    class Helper
    {
        /// <summary>
        /// Получение индентификатора объекта
        /// </summary>
        /// <param name="oItem"></param>
        /// <returns></returns>
        public static string GetItemId(object oItem)
        {
            if (oItem is ISupportId)
            {
                return (oItem as ISupportId).Id;
            }
            return string.Empty;
        }

        public static Type[] GetExtraTypes()
        {
            List<Type> extraTypes = new List<Type>();
            extraTypes.Add(typeof(ControlBase));
            extraTypes.Add(typeof(XRefData));
            extraTypes.Add(typeof(GroupData));
            return extraTypes.ToArray();
        }
    }
}
