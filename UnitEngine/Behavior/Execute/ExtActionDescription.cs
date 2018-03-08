using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Описание действия
    /// </summary>
    [Serializable]
    public class ExtActionDescription
    {
        /// <summary>
        /// Наимeнование действия
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Наимeнование действия")]
        public string ActionName { get; set; }

        //[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        //public List<string> WERWER { get; set; }

        public ExtActionDescription()
        {
            ActionName = string.Empty;
        }

        public override string ToString()
        {
            return ActionName;
        }
    }
}
