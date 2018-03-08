using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Изменить Id модели
    /// </summary>
    [Serializable]
    public class ExecuteChangeSceneNodeId : ExecuteBase
    {
        public int NodeId { get; set; }

        public ExecuteChangeSceneNodeId()
        {
            NodeId = -1;
        }

        public override string ToString()
        {
            return "Изменить Id модели";
        }
    }
}
