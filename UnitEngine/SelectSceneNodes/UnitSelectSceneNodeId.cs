using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine
{
    /// <summary>
    /// Выбор по индентификатору моделей
    /// </summary>
    [Serializable]
    public class UnitSelectSceneNodeId : UnitSelectSceneNodeBase
    {
        public UnitSelectSceneNodeId()
        {
            FilterNodeId = 0;
        }

        public override string ToString()
        {
            return string.Format("Выборка по FilterNodeId [{0}]", FilterNodeId);
        }
    }
}
