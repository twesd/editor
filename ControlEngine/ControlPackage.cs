using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Xml.Serialization;
using ControlEngine;
using System.IO;
using Serializable;

namespace ControlEngine
{
    /// <summary>
    /// Хранилище для редактора контролов
    /// </summary>
    [Serializable]
    public class ControlPackage
    {
        /// <summary>
        /// Модели окружающей среды
        /// </summary>
        public List<ContainerNode> EnviromentModels { get; set; }

        /// <summary>
        /// Дерево объектов
        /// </summary>
        public ContainerTreeView TreeView { get; set; }

        /// <summary>
        /// Описание положения камеры
        /// </summary>
        public ContainerCamera Camera { get; set; }

        public ControlPackage()
        {
            EnviromentModels = new List<ContainerNode>();
        }

        public ControlPackage DeepClone()
        {
            return SerializeWorker.Clone(this) as ControlPackage;
        }

        /// <summary>
        /// Получить данные с абсолютными путями
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public ControlPackage GetWithAbsolutePaths(string root)
        {
            // Получаем абсолютные пути
            //
            ControlPackage container = this.DeepClone();

            List<ContainerNode> envNodes = new List<ContainerNode>();
            foreach (ContainerNode envNode in container.EnviromentModels)
                envNode.ToAbsolutePaths(root);
            ConvertTreeItemToAbsolutePath(container.TreeView.Nodes, root);

            return container;
        }

        /// <summary>
        /// Получить данные с относительными путями
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public ControlPackage GetWithRelativePaths(string root)
        {
            // Получаем относительные пути
            //
            ControlPackage container = this.DeepClone();

            List<ContainerNode> envNodes = new List<ContainerNode>();
            foreach (ContainerNode envNode in container.EnviromentModels)
                envNode.ToRelativePaths(root);

            ConvertTreeItemToRelativePath(container.TreeView.Nodes, root);
            return container;
        }

        /// <summary>
        /// Преобразовать пути в относительные
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="root"></param>
        private void ConvertTreeItemToRelativePath(List<SerializableTreeNode> nodes, string root)
        {
            foreach (var node in nodes)
            {
                var item = (node.Tag as IPathConvertible);
                if (item != null)
                {
                    item.ToRelativePaths(root);
                }
                if (node.Nodes.Count > 0)
                {
                    ConvertTreeItemToRelativePath(node.Nodes, root);
                }
            }
        }

        /// <summary>
        /// Преобразовать пути в абсолютные
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="root"></param>
        private void ConvertTreeItemToAbsolutePath(List<SerializableTreeNode> nodes, string root)
        {
            foreach (var node in nodes)
            {
                var item = (node.Tag as IPathConvertible);
                if (item != null)
                {    
                    item.ToAbsolutePaths(root);
                }
                if (node.Nodes.Count > 0)
                {
                    ConvertTreeItemToAbsolutePath(node.Nodes, root);
                }
            }
        }

        /// <summary>
        /// Загрузить пакет из заданнного файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ControlPackage LoadFromFile(string path)
        {
            XmlSerializer xmlSerelialize = new XmlSerializer(typeof(ControlPackage), GetExtraTypes());
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                return xmlSerelialize.Deserialize(reader) as ControlPackage;
            }
        }

        private static Type[] GetExtraTypes()
        {
            List<Type> extraTypes = new List<Type>();
            extraTypes.Add(typeof(ControlBase));
            extraTypes.Add(typeof(GroupData));
            extraTypes.Add(typeof(XRefData));
            return extraTypes.ToArray();
        }
    }
}
