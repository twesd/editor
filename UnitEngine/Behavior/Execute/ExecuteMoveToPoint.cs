using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Geometry;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Перемещение к заданной точки
    /// </summary>
    [Serializable]
    public class ExecuteMoveToPoint : ExecuteBase
    {
        /// <summary>
        /// Получить позицию из события TapScene 
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Получить позицию из события TapScene")]
        public bool GetPositionFromTapControl { get; set; }

        /// <summary>
        /// Наименование TapScene
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Наименование TapScene")]
        public string TapSceneName { get; set; }

        /// <summary>
        /// Позиция цели
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Позиция цели")]
        public Vertex TargetPosition { get; set; }

        /// <summary>
        /// Скорость перемещения (едениц в секунду)
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Скорость перемещения (едениц в секунду)")]
        public float Speed { get; set; }

        /// <summary>
        /// Дистанция на которую надо подойти
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Дистанция на которую надо подойти")]
        public float TargetDist { get; set; }

        /// <summary>
        /// Фильтр для моделий препятствий
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Фильтр для моделий препятствий")]
        public int ObstacleFilterId { get; set; }

        public ExecuteMoveToPoint()
        {
            GetPositionFromTapControl = false;
            TapSceneName = string.Empty;
            TargetPosition = new Vertex();
            Speed = 1;
            TargetDist = 1;
            ObstacleFilterId = -1;
        }

        public override string ToString()
        {
            return string.Format("Переместиться в точку");
        }
    }
}
