using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitEngine.Events
{
    /// <summary>
    /// Событие окончания действия
    /// </summary>
    [Serializable]
    public class UnitEventActionEnd : UnitEventBase
    {
        public override string ToString()
        {
            return string.Format("Окончание действия");
        }
    }
}
