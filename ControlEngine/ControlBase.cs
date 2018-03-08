using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using CommonUI;
using Common;

namespace ControlEngine
{
    [Serializable]
    [XmlInclude(typeof(ControlButton))]
    [XmlInclude(typeof(ControlImage))]    
    [XmlInclude(typeof(ControlTapScene))]
    [XmlInclude(typeof(ControlBehavior))]
    [XmlInclude(typeof(ControlText))]
    [XmlInclude(typeof(ControlCircle))]
    [XmlInclude(typeof(ControlPanel))]
    public class ControlBase : IPathConvertible, ISupportId
    {
        /// <summary>
        /// Индентификатор контрола
        /// </summary>
        [CategoryAttribute("Базовые")]
        [DescriptionAttribute("Индентификатор контрола")]
        [ReadOnlyAttribute(true)]
        public string Id { get; set; }

        /// <summary>
        /// Наименование контрола
        /// </summary>
        [CategoryAttribute("Базовые")]
        [DescriptionAttribute("Наименование контрола")]
        public string Name { get; set; }

        [CategoryAttribute("Базовые")]
        [DescriptionAttribute("Видимость контрола")]
        public bool IsVisible { get; set; }

        [CategoryAttribute("Редактор")]
        [DescriptionAttribute("Видимость контрола в редакторе")]
        public bool IsVisibleInEditor { get; set; }

        /// <summary>
        /// Событие при создании контрола
        /// </summary>
        [CategoryAttribute("События")]
        [DescriptionAttribute("Событие при появлении пакета контрола")]
        [Editor(typeof(CommonUI.UITypeEditors.UITypeEditorScriptFileName), typeof(System.Drawing.Design.UITypeEditor))]
        [ParserAutocompleteAttribute(ParserAutocompleteAttribute.ParserTypeWords.Controls | ParserAutocompleteAttribute.ParserTypeWords.Units)]
        public string OnPackageShow { get; set; }

        public ControlBase() 
        {
            Id = Guid.NewGuid().ToString("B").ToUpper();
            Name = string.Empty;
            OnPackageShow = string.Empty;
            IsVisible = true;
            IsVisibleInEditor = true;
        }


        public virtual void ToRelativePaths(string root) { }

        public virtual void ToAbsolutePaths(string root) { }

        public ControlBase DeepClone()
        {
            return Common.SerializeWorker.Clone(this) as ControlBase;
        }
    }
}
