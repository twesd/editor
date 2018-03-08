using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Common.Geometry;
using System.Xml.Serialization;

namespace StageEngine
{
    /// <summary>
    /// Невидимый объект
    /// </summary>
    [Serializable]
    public class UnitInstanceEmpty : UnitInstanceBase
    {

        [Category("Основные")]
        [Description("Размер")]
        public Vertex Scale { get; set; }

        [Category("Основные")]
        [Description("Индентификатор модели")]
        public int NodeId { get; set; }

        public UnitInstanceEmpty()
        {
            Name = "Empty";
            Scale = new Vertex(1,1,1);
            NodeId = 0;
        }

    }
}
