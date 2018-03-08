using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Установить материю
    /// </summary>
    [Serializable]
    public class ExecuteMaterial : ExecuteBase
    {
        public MaterialType Type = MaterialType.Solid;

        public override string ToString()
        {
            return string.Format("Тип материи: " + this.Type);
        }
    }
}
