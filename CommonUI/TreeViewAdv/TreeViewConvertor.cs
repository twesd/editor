using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serializable;
using TransactionCore;
using System.Collections.ObjectModel;
using Common;

namespace CommonUI
{
    public class TreeViewConvertor
    {
        public delegate TreeNodeBase CreateTreeNodeHandler(SerializableTreeNode sNode, out bool handleChilds);

        /// <summary>
        /// Получить контэйнер для сериализации данных
        /// </summary>
        /// <returns></returns>
        public static ContainerTreeView ConvertToContainer(Collection<TreeNodeBase> nodes)
        {
            ContainerTreeView container = new ContainerTreeView();
            foreach (TreeNodeBase node in nodes)
            {
                var sNode = new SerializableTreeNode(node.Text, node.Tag, node.GetType().ToString());
                sNode.Tag = node.Tag;
                container.Nodes.Add(sNode);
                FillSerializableNodes(node.Nodes, sNode);
            }
            return container;
        }


        /// <summary>
        /// Конвертация данных из ContainerTreeView в лист TreeViewModelNode
        /// </summary>
        /// <param name="container"></param>
        public static List<TreeNodeBase> ConvertToNodes(
            List<SerializableTreeNode> sNodes,
            TransactionManager transManager,
            CreateTreeNodeHandler createHandler = null)
        {
            var nodes = new List<TreeNodeBase>();
            foreach (SerializableTreeNode sNode in sNodes)
            {
                TreeNodeBase node;
                bool handleChilds = true; // Необходимо ли обрабатывать дочерние узлы
                if (createHandler != null)
                {                    
                    node = createHandler(sNode, out handleChilds);                    
                }
                else
                {
                    node = new TreeNodeBase(transManager);
                }
                node.Text = sNode.Text;
                node.Tag = sNode.Tag;
                nodes.Add(node);

                if (!handleChilds)
                {
                    continue;
                }

                FillTreeNode(sNode.Nodes, node, transManager, createHandler);
            }
            return nodes;
        }


        private static void FillTreeNode(
            List<SerializableTreeNode> nodes,
            TreeNodeBase rootNode,
            TransactionManager transManager,
            CreateTreeNodeHandler createHandler)
        {
            foreach (SerializableTreeNode sNode in nodes)
            {
                TreeNodeBase node;
                bool handleChilds = true; // Необходимо ли обрабатывать дочерние узлы
                if (createHandler != null)
                {
                    node = createHandler(sNode, out handleChilds);
                }
                else
                {
                    node = new TreeNodeBase(transManager);
                }
                node.Text = sNode.Text;
                node.Tag = sNode.Tag;
                rootNode.Nodes.Add(node);

                if (!handleChilds)
                {
                    continue;
                }

                FillTreeNode(sNode.Nodes, node, transManager, createHandler);
            }
        }


        private static void FillSerializableNodes(Collection<TreeNodeBase> nodes, SerializableTreeNode rootNode)
        {
            foreach (TreeNodeBase node in nodes)
            {
                var sNode = new SerializableTreeNode(node.Text, node.Tag, node.GetType().ToString());
                sNode.Tag = node.Tag;
                rootNode.Nodes.Add(sNode);
                FillSerializableNodes(node.Nodes, sNode);
            }
        }
    }
}
