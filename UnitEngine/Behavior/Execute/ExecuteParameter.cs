using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Установить параметер
    /// </summary>
    [Serializable]
    public class ExecuteParameter : ExecuteBase
    {
        /// <summary>
        /// Является ли параметр глобальным
        /// </summary>
        public bool IsGlobal;

        public List<Parameter> Parameters = new List<Parameter>();

        public ExecuteParameter() { }

        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="isGlobal">Является ли параметры глобальными</param>
        public ExecuteParameter(bool isGlobal)
        {
            IsGlobal = isGlobal;
        }

        public override string ToString()
        {
            if(IsGlobal)
                return string.Format("Изменить глобальные параметры ");
            else
                return string.Format("Изменить локальные параметры");
        }
    }
}
