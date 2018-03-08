using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitEngine.Events;
using Common;

namespace UnitEngine
{
    /// <summary>
    /// Условие для действия
    /// </summary>
    [Serializable]
    public class UnitClause
    {
        /// <summary>
        /// Параметры юнита
        /// </summary>
        public List<Parameter> Parameters = new List<Parameter>();

        /// <summary>
        /// Параметры стадии
        /// </summary>
        public List<Parameter> GlobalParameters = new List<Parameter>();

        /// <summary>
        /// События
        /// </summary>
        public List<UnitEventBase> Events = new List<UnitEventBase>();

        public UnitClause() { }

        public bool IsEmpty()
        {
            return (Events.Count == 0 && Parameters.Count == 0 && GlobalParameters.Count == 0);
        }

        public void ToRelativePaths(string root)
        {
            foreach (UnitEventBase eventItem in Events)
                eventItem.ToRelativePaths(root);
        }

        public void ToAbsolutePaths(string root)
        {
            foreach (UnitEventBase eventItem in Events)
                eventItem.ToAbsolutePaths(root);
        }
    }
}
