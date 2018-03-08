using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Common
{
    /// <summary>
    /// Параметр
    /// </summary>
    [Serializable]
    public class Parameter
    {
        /// <summary>
        /// Имя параметра
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Имя параметра")]
        public string Name { get; set; }

        /// <summary>
        /// Значение параметра
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Значение параметра")]
        public string Value { get; set; }

        public Parameter(string name, string value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            Name = name;
            Value = value;
        }

        public Parameter() { }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, Value);
        }
    }
}
