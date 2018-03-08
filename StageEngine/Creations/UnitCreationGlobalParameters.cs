using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace StageEngine
{
    [Serializable]
    public class UnitCreationGlobalParameters : UnitCreationBase
    {      
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Параметры")]
        [Editor(typeof(UnitEngine.UITypeEditors.UITypeEditorUnitParameterCollection), typeof(System.Drawing.Design.UITypeEditor))]
        public List<Common.Parameter> Parameters { get; set; }

        public UnitCreationGlobalParameters()
        {
            Parameters = new List<Common.Parameter>();
        }

        public override string ToString()
        {
            return string.Format("Глобальные параметры ");
        }
    }
}
