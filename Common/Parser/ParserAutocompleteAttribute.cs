using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// Атрибут слов для автозаполнения
    /// </summary>
    public class ParserAutocompleteAttribute : Attribute
    {
        public enum ParserTypeWords
        {
            Units = 1,            
            Controls = 2,
            Parameters = 4
        }

        public ParserTypeWords ParserTypes { get; private set; }

        public ParserAutocompleteAttribute(ParserTypeWords parserTypes)
        {
            ParserTypes = parserTypes;
        }
    }
}
