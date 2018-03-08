using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Действия для собственного удаления
    /// </summary>
    [Serializable]
    public class ExecuteDeleteSelf : ExecuteBase
    {
        public override string ToString()
        {
            return string.Format("Удалить себя");
        }
    }
}
