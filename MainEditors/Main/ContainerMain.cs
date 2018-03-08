using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serializable;
using Common;
using ControlEngine;

namespace MainEditors
{
    /// <summary>
    /// Основное хранилище игры
    /// </summary>
    [Serializable]
    public class ContainerMain
    {
        /// <summary>
        /// Дерево объектов
        /// </summary>
        public ContainerTreeView TreeView { get; set; }

        /// <summary>
        /// Пакеты контролов (Вспомогательный элемент)
        /// </summary>
        public ControlPackages CacheControlPackages { get; set; }

        public ContainerMain()
        {
            CacheControlPackages = new ControlPackages();
        }

        public ContainerMain DeepClone()
        {
            return SerializeWorker.Clone(this) as ContainerMain;
        }

        /// <summary>
        /// Получить контэйнер с абсолютными путями
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public ContainerMain GetWithAbsolutePaths(string root)
        {
            // Получаем абсолютные пути
            //
            ContainerMain container = this.DeepClone();
            container.ConvertTreeItemToAbsolutePath(container.TreeView.Nodes, root);
           
            return container;
        }
        

        /// <summary>
        /// Получить контэйнер с относительными путями
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public ContainerMain GetWithRelativePaths(string root)
        {
            // Получаем относительные пути
            //
            ContainerMain container = this.DeepClone();

            container.ConvertTreeItemToRelativePath(container.TreeView.Nodes, root);
           
            
            return container;
        }

        private void ConvertTreeItemToRelativePath(List<SerializableTreeNode> nodes, string root)
        {
            foreach (var node in nodes)
            {
                if (node.Tag is StageItem)
                {
                    var instance = node.Tag as StageItem;
                    instance.ToRelativePaths(root);
                }
                else if (node.Type == typeof(CommonUI.TreeNodeGroup).ToString())
                {
                    // nothing
                }
                else
                {
                    throw new NotSupportedException();
                }
                if (node.Nodes.Count > 0)
                    ConvertTreeItemToRelativePath(node.Nodes, root);
            }            
        }

        private void ConvertTreeItemToAbsolutePath(List<SerializableTreeNode> nodes, string root)
        {
            foreach (var node in nodes)
            {
                if (node.Tag is StageItem)
                {
                    var instance = node.Tag as StageItem;
                    instance.ToAbsolutePaths(root);
                }
                else if (node.Type == typeof(CommonUI.TreeNodeGroup).ToString())
                {
                    // nothing
                }
                else
                {
                    throw new NotSupportedException();
                }
                if (node.Nodes.Count > 0)
                    ConvertTreeItemToAbsolutePath(node.Nodes, root);

            }            
        }

    }
}
