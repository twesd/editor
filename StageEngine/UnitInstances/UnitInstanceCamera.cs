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
    /// Камера сцены
    /// </summary>
    [Serializable]
    public class UnitInstanceCamera : UnitInstanceBase
    {
        /// <summary>
        /// Начальная цель
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Начальная цель")]
        public Vertex StartTarget { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Дальность обзора")]
        public float FarValue { get; set; }

        public CameraBehaviorBase Behavior { get; set; }

        public UnitInstanceCamera()
        {
            // По умолчанию делаем камеру статичной
            Behavior = new CameraBehaviorStatic();
            StartTarget = new Vertex();
            StartPosition = new Vertex(1, 1, 1);
            FarValue = 1000;
        }

    }
}
