using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Описание изменения объекта 
    /// </summary>
    [Serializable]
    public class ExecuteTransform : ExecuteBase
    {
        /// <summary>
        /// Наименование изменения
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип изменения
        /// </summary>
        public TransformType Type = TransformType.LINE;

        /// <summary>
        /// Повторять изменения
        /// </summary>
        public bool Loop { get; set; }

        /// <summary>
        /// Набор параметров изменения
        /// </summary>
        public List<TransformItem> Items = new List<TransformItem>();

        /// <summary>
        /// Фильтр для моделий препятствий
        /// </summary>
        public int ObstacleFilterId { get; set; }

        /// <summary>
        /// Иницилизация (Для Serializable)
        /// </summary>
        public ExecuteTransform() 
        {
            Name = string.Empty;
            Loop = false;
            ObstacleFilterId = 0;
        }

        /// <summary>
        /// Deep clone
        /// </summary>
        /// <returns></returns>
        public ExecuteTransform Clone()
        {
            return SerializeWorker.Clone(this) as ExecuteTransform;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name)) return Type.ToString();
            return Name;
        }
    }
}
