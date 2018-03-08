using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Geometry;

namespace Serializable
{
    /// <summary>
    /// Хранилище для модели сцены
    /// </summary>
    [Serializable]
    public class ContainerNode
    {
        /// <summary>
        /// Индентификатор объекта
        /// </summary>
        public int Id = 0;

        /// <summary>
        /// Путь до модели
        /// </summary>
        public string Path;

        /// <summary>
        /// Позиция модели
        /// </summary>
        public Vertex Position = new Vertex();

        /// <summary>
        /// Поворот модели
        /// </summary>
        public Vertex Rotation = new Vertex();

        public ContainerNode(int id, string path, Vertex position, Vertex rotation)
        {
            if (position == null || rotation == null)
                throw new ArgumentNullException();

            Id = id;
            Path = path;
            Position = position;
            Rotation = rotation;
        }

        public ContainerNode() { }

        /// <summary>
        /// Клонирование объекта
        /// </summary>
        /// <returns></returns>
        public ContainerNode Clone()
        {
            return this.MemberwiseClone() as ContainerNode;
        }

        public void ToRelativePaths(string root)
        {
            Path = Common.UtilPath.GetRelativePath(Path, root);
        }

        public void ToAbsolutePaths(string root)
        {
            Path = Common.UtilPath.GetAbsolutePath(Path, root);
        }
    }
}
