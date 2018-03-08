using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitEngine
{
    [Serializable]
    public class UnitModelEmpty : UnitModelBase
    {
        public override string ToString()
        {
            return "Empty";
        }
    }
}
