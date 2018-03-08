using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Следовать за моделью
    /// </summary>
    [Serializable]
    public class ExecuteMoveToSceneNode : ExecuteBase
    {
        /// <summary>
        /// Наименование контрола TapScene
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Алгоритм выборки объекта")]
        [Editor(typeof(UnitEngine.UITypeEditors.UITypeEditorUnitSelectSceneNodeBase), typeof(System.Drawing.Design.UITypeEditor))]
        public UnitSelectSceneNodeBase SelectSceneNode { get; set; }

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
        public float ObstacleFilterId { get; set; }

        public ExecuteMoveToSceneNode()
        {
            SelectSceneNode = new UnitSelectSceneNodeTapControl();
            Speed = 1;
            TargetDist = 1;
            ObstacleFilterId = -1;
        }

        public override string ToString()
        {
            return string.Format("Следовать за моделью");
        }
    }
}
