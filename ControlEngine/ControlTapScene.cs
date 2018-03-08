using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Geometry;
using System.ComponentModel;

namespace ControlEngine
{
    /// <summary>
    /// Контрол для клика по сцене
    /// </summary>
    [Serializable]
    public class ControlTapScene : ControlBase
    {
        /// <summary>
        /// Минимальная точка области обработки
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Минимальная точка области обработки")]
        public Vertex MinPoint { get; set; }

        /// <summary>
        /// Максимальная точка области обработки
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Максимальная точка области обработки")]
        public Vertex MaxPoint { get; set; }

        /// <summary>
        /// Позволить выбирать объекты
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Позволить выбирать объекты")]
        public bool PickSceneNode { get; set; }

        /// <summary>
        /// Фильтр для моделий
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Фильтр для моделий")]
        public int FilterNodeId { get; set; }

        public ControlTapScene() 
        {
            MinPoint = new Vertex(0, 0, 0);
            MaxPoint = new Vertex(480, 320, 0);
            PickSceneNode = true;
            FilterNodeId = -1;
        }

    }
}
