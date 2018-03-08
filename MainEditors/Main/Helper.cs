using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainEditors
{
    class Helper
    {
        public static Type[] GetExtraTypes()
        {
            List<Type> extraTypes = new List<Type>();
            extraTypes.Add(typeof(StageItem));
            return extraTypes.ToArray();
        }
    }
}
