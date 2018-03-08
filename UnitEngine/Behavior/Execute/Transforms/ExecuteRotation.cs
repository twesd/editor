using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Common.Geometry;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Установить материю
    /// </summary>
    [Serializable]
    public class ExecuteRotation : ExecuteBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Поворот")]
        public Vertex Rotation { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Установить абсолютное вращение")]
        public bool Absolute { get; set; }

        [CategoryAttribute("ControlCircle")]
        [DescriptionAttribute("Дополнить угол из события ControlCircle")]
        public bool AddAngleFromControlCircle { get; set; }

        [CategoryAttribute("ControlCircle")]
        [DescriptionAttribute("Наименование ControlCircle")]
        public string ControlCircleName { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Скорость установки (угол/сек.). Ноль - установить сразу.")]
        public float Speed { get; set; }

        public ExecuteRotation()
        {
            Rotation = new Vertex();
            Absolute = false;
            AddAngleFromControlCircle = false;
            ControlCircleName = string.Empty;
            Speed = 0;
        }

        public override string ToString()
        {
            return string.Format("Поворот: [{0}]", Rotation.ToString());
        }
    }
}
