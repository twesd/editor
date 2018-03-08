using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitEngine;
using UnitEngine.Behavior;
using StageEngine;
using ControlEngine;
using Common;

namespace StageEngineUI
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
            extraTypes.Add(typeof(ExecuteBase));
            extraTypes.Add(typeof(ControlBase));
            extraTypes.Add(typeof(UnitInstanceBase));
            extraTypes.Add(typeof(XRefData));
            extraTypes.Add(typeof(GroupData));
            return extraTypes.ToArray();
        }
    }
}
