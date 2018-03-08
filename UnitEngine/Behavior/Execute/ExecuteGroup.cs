using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Группа действий
    /// </summary>
    [Serializable]
    public class ExecuteGroup : ExecuteBase
    {
        /// <summary>
        /// Действия группы
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Коллекция действий группы")]
        public List<ExecuteBase> Executes {get; set;}

        public ExecuteGroup()
        {
            Executes = new List<ExecuteBase>();
        }

        public override void ToRelativePaths(string root)
        {
            foreach (ExecuteBase exBase in Executes)
                exBase.ToRelativePaths(root);
        }

        public override void ToAbsolutePaths(string root)
        {
            foreach (ExecuteBase exBase in Executes)
                exBase.ToAbsolutePaths(root);
        }

        public override string ToString()
        {
            return string.Format("Группа действий [{0}]", Executes.Count);
        }
    }
}
