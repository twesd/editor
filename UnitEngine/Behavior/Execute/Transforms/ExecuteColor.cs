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
    public class ExecuteColor : ExecuteBase
    {
        /// <summary>
        /// Повторять изменения
        /// </summary>
        public bool Loop { get; set; }

        /// <summary>
        /// Набор параметров изменения
        /// </summary>
        public List<TransformSColorItem> Items { get; set; }

        /// <summary>
        /// Иницилизация
        /// </summary>
        public ExecuteColor() 
        {
            Loop = false;
            Items = new List<TransformSColorItem>();
        }

        /// <summary>
        /// Deep clone
        /// </summary>
        /// <returns></returns>
        public TransformSColorItem Clone()
        {
            return SerializeWorker.Clone(this) as TransformSColorItem;
        }

        public override string ToString()
        {
            return "Изменение цвета";
        }
    }
}
