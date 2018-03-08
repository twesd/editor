using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Geometry;

namespace Serializable
{
    [Serializable]
    public class ContainerCamera
    {
        /// <summary>
        /// Позиция камеры
        /// </summary>
        public Vertex Position = new Vertex(1, 1, 1);

        /// <summary>
        /// Цель камеры
        /// </summary>
        public Vertex Target = new Vertex(0, 0, 0);

        /// <summary>
        /// Клонирование объекта
        /// </summary>
        /// <returns></returns>
        public ContainerCamera DeepClone()
        {
            return Common.SerializeWorker.Clone(this) as ContainerCamera;
        }
    }
}
