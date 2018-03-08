using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aga.Controls.Tree;
using System.Collections.ObjectModel;
using TransactionCore;
using Serializable;

namespace CommonUI
{
    /// <summary>
    /// Provides a simple ready to use implementation of <see cref="ITreeModel"/>. Warning: this class is not optimized 
    /// to work with big amount of data. In this case create you own implementation of <c>ITreeModel</c>, and pay attention
    /// on GetChildren and IsLeaf methods.
    /// </summary>
    public class TreeModel : ITreeModel
    {

        public TreeNodeBase Root
        {
            get { return _root; }
        }

        public Collection<TreeNodeBase> Nodes
        {
            get { return _root.Nodes; }
        }

        TransactionManager _transManager;

        TreeNodeBase _root;

        public TreeModel(TransactionManager transManager = null)
        {
            _transManager = transManager;
            _root = new TreeNodeBase(transManager);
            _root.Model = this;
        }

        public TreePath GetPath(TreeNodeBase node)
        {
            if (node == _root)
                return TreePath.Empty;
            else
            {
                Stack<object> stack = new Stack<object>();
                while (node != _root)
                {
                    stack.Push(node);
                    node = node.Parent;
                }
                return new TreePath(stack.ToArray());
            }
        }

        #region ITreeModel Members

        public System.Collections.IEnumerable GetChildren(TreePath treePath)
        {
            TreeNodeBase node = FindNode(treePath);
            if (node != null)
                foreach (TreeNodeBase n in node.Nodes)
                    yield return n;
            else
                yield break;
        }

        public bool IsLeaf(TreePath treePath)
        {
            TreeNodeBase node = FindNode(treePath);
            if (node != null)
                return node.IsLeaf;
            else
                throw new ArgumentException("treePath");
        }

        public event EventHandler<TreeModelEventArgs> NodesChanged;
        internal void OnNodesChanged(TreeModelEventArgs args)
        {
            if (NodesChanged != null)
                NodesChanged(this, args);
        }

        public event EventHandler<TreePathEventArgs> StructureChanged;
        public void OnStructureChanged(TreePathEventArgs args)
        {
            if (StructureChanged != null)
                StructureChanged(this, args);
        }

        public event EventHandler<TreeModelEventArgs> NodesInserted;
        internal void OnNodeInserted(TreeNodeBase parent, int index, TreeNodeBase node)
        {
            if (NodesInserted != null)
            {
                TreeModelEventArgs args = new TreeModelEventArgs(GetPath(parent), new int[] { index }, new object[] { node });
                NodesInserted(this, args);
            }

        }

        public event EventHandler<TreeModelEventArgs> NodesRemoved;
        internal void OnNodeRemoved(TreeNodeBase parent, int index, TreeNodeBase node)
        {
            if (NodesRemoved != null)
            {
                TreeModelEventArgs args = new TreeModelEventArgs(GetPath(parent), new int[] { index }, new object[] { node });
                NodesRemoved(this, args);
            }
        }

        public TreeNodeBase FindNode(TreePath path)
        {
            if (path.IsEmpty())
                return _root;
            else
                return FindNode(_root, path, 0);
        }

        #endregion

 
        /// <summary>
        /// Получить уникальное имя
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetUniqueName(string name)
        {
            string originalName = name;
            int counter = 1;
            for (; ; )
            {
                TreeNodeBase node = FindNode(name, Root.Nodes);
                if (node == null)
                {
                    return name;
                }
                name = originalName + "_" + counter;
                counter++;
            }
        }

        /// <summary>
        /// Поиск узла по имени
        /// </summary>
        /// <param name="name"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public TreeNodeBase FindNode(string name, Collection<TreeNodeBase> nodes)
        {
            if (nodes == null)
            {
                return null;
            }
            foreach (TreeNodeBase node in nodes)
            {
                if (node.Text == name)
                {
                    return node;
                }
                var chNode = FindNode(name, node.Nodes);
                if (chNode != null)
                {
                    return chNode;
                }
            }
            return null;
        }

        private TreeNodeBase FindNode(TreeNodeBase root, TreePath path, int level)
        {
            foreach (TreeNodeBase node in root.Nodes)
                if (node == path.FullPath[level])
                {
                    if (level == path.FullPath.Length - 1)
                        return node;
                    else
                        return FindNode(node, path, level + 1);
                }
            return null;
        }

    }
}
