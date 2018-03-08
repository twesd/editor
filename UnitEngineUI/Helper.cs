using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitEngine;
using UnitEngine.Behavior;
using Common;

namespace UnitEngineUI
{
    class Helper
    {
        public static Type[] GetExtraTypes()
        {
            List<Type> extraTypes = new List<Type>();
            extraTypes.Add(typeof(UnitAnimation));
            extraTypes.Add(typeof(UnitSound));
            extraTypes.Add(typeof(UnitEffect));
            extraTypes.Add(typeof(UnitAction));
            extraTypes.Add(typeof(ExecuteTransform));
            extraTypes.Add(typeof(XRefData));
            extraTypes.Add(typeof(GroupData));
            return extraTypes.ToArray();
        }

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
    }
}
