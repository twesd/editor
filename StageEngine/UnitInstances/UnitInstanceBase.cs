using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Geometry;
using Common;
using System.Xml.Serialization;
using System.ComponentModel;

namespace StageEngine
{
    /// <summary>
    /// Экземпляр юнита
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(UnitInstanceStandard))]
    [XmlInclude(typeof(UnitInstanceEnv))]
    [XmlInclude(typeof(UnitInstanceCamera))]
    [XmlInclude(typeof(UnitInstanceEmpty))]
    [XmlInclude(typeof(UnitInstanceBillboard))]    
    public class UnitInstanceBase
    {
        /// <summary>
        /// Имя
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Имя юнита")]
        public string Name { get; set; }

        /// <summary>
        /// Индентификатор модели для редактора
        /// </summary>
        public int EditorModelId = 0;
        
        /// <summary>
        /// Начальная позиция
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Начальная позиция")]
        public Vertex StartPosition { get; set; }

        /// <summary>
        /// Начальный поворот
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Начальный поворот")]
        public Vertex StartRotation { get; set; }

        /// <summary>
        /// Описание создания юнита
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Описание создания юнита")]
        [Editor(typeof(StageEngine.UITypeEditors.UITypeEditorUnitCreationCollection), typeof(System.Drawing.Design.UITypeEditor))]
        public List<UnitCreationBase> Creations { get; set; }
        
        public UnitInstanceBase() 
        {
            Name = string.Empty;
            StartPosition = new Vertex();
            StartRotation = new Vertex();
            Creations = new List<UnitCreationBase>();
        }

        public UnitInstanceBase(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        /// Полное клонирование
        /// </summary>
        /// <returns></returns>
        public UnitInstanceBase DeepClone()
        {
            return SerializeWorker.Clone(this) as UnitInstanceBase;
        }
    }
}
