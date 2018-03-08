using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aga.Controls.Tree;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace CommonUI
{
    public class TreeViewAdvWorker
    {
        /// <summary>
        /// Может ли объект перетаскиваться
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public delegate bool CanDragItemFunc(TreeNodeBase node);

        /// <summary>
        /// Может ли объект перенистись
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public delegate bool CanDropItemFunc(TreeNodeBase source, TreeNodeBase target, NodePosition nodePos);

        /// <summary>
        /// Событие вставки нового узла
        /// </summary>
        /// <param name="node"></param>
        public delegate bool CanPasteTreeNode(TreeNodeBase source, TreeNodeBase target);

        /// <summary>
        /// Событие вставки нового узла
        /// </summary>
        /// <param name="node"></param>
        public delegate void PasteItemHandler(TreeNodeBase source);

        /// <summary>
        /// Дерево объектов
        /// </summary>
        TreeViewAdv _treeView;

        /// <summary>
        /// Может ли объект перетаскиваться
        /// </summary>
        CanDragItemFunc _canDragItem;

        /// <summary>
        /// Может ли объект перенистись
        /// </summary>
        CanDropItemFunc _canDropItem;

        /// <summary>
        /// Можно ли вставить элемент
        /// </summary>
        CanPasteTreeNode _canPasteTreeNodeHandler;

        PasteItemHandler _pasteItem;

        public TreeViewAdvWorker(TreeViewAdv treeView)
        {
            _treeView = treeView;
        }

        /// <summary>
        /// Установить возможность перетаскивания
        /// </summary>
        public void EnableDragDrop(
            CanDragItemFunc canDragItem,
            CanDropItemFunc canDropItem)
        {
            _canDragItem = canDragItem;
            _canDropItem = canDropItem;

            _treeView.AllowDrop = true;
            _treeView.ItemDrag += new ItemDragEventHandler(ItemDrag);
            _treeView.DragDrop += new DragEventHandler(DragDrop);
            _treeView.DragOver += new DragEventHandler(DragOver);

        }

        /// <summary>
        /// Включить возможность копирования данных
        /// </summary>
        public void EnableCopyPaste(CanPasteTreeNode canPasteTreeNode = null, PasteItemHandler pasteItem = null)
        {
            _canPasteTreeNodeHandler = canPasteTreeNode;
            _pasteItem = pasteItem;
            _treeView.PreviewKeyDown += new PreviewKeyDownEventHandler(CopyPaste_PreviewKeyDown);
        }

        /// <summary>
        /// Поиск узла дерева
        /// </summary>
        /// <param name="control"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public TreeNodeAdv FindTreeNode(object oTag, ReadOnlyCollection<TreeNodeAdv> nodes)
        {
            foreach (TreeNodeAdv node in nodes)
            {
                var nodeData = node.Tag as TreeNodeBase;
                if (nodeData.Tag == oTag)
                    return node;
                TreeNodeAdv searchNode = FindTreeNode(oTag, node.Children);
                if (searchNode != null)
                {
                    return searchNode;
                }
            }
            return null;
        }

        void CopyPaste_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control && _treeView.SelectedNode != null)
            {
                Clipboard.SetData(typeof(TreeNodeBase).FullName, _treeView.SelectedNode.Tag);
            }
            else if (e.KeyCode == Keys.V && e.Control)
            {
                TreeNodeBase item = Clipboard.GetData(typeof(TreeNodeBase).FullName) as TreeNodeBase;
                if (item != null)
                {
                    TreeNodeBase newTreeNode = item.Clone();

                    if (_treeView.SelectedNode != null)
                    {
                       TreeNodeBase target;
                        if (_treeView.SelectedNode.Parent != null)
                        {
                            var parentNode = _treeView.SelectedNode.Parent.Tag as TreeNodeBase;
                            if (parentNode == null)
                            {
                                parentNode = (_treeView.Model as TreeModel).Root;
                            }
                            target = parentNode;
                        }
                        else
                        {
                            target = (_treeView.Model as TreeModel).Root;
                        }

                        if (_canPasteTreeNodeHandler != null)
                        {
                            if (!_canPasteTreeNodeHandler(newTreeNode, target))
                            {
                                return;
                            }
                        }

                        target.Nodes.Insert(_treeView.SelectedNode.Index + 1, newTreeNode);   
                    }
                    else
                    {
                        (_treeView.Model as TreeModel).Nodes.Add(newTreeNode);
                    }

                    if (_pasteItem != null)
                    {
                        _pasteItem(newTreeNode);
                    }
                }
            }
        }

        /// <summary>
        /// Событие начала перетаскивания объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNodeAdv[] nodes = e.Item as TreeNodeAdv[];

            foreach (TreeNodeAdv node in nodes)
            {
                if (_canDragItem != null && !_canDragItem(node.Tag as TreeNodeBase))
                {
                    return;
                }
            }
            _treeView.DoDragDropSelectedNodes(DragDropEffects.Move);
        }

        /// <summary>
        /// Событие когда переносимый объект в пределах дерева
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNodeAdv[])) && _treeView.DropPosition.Node != null)
            {
                TreeNodeAdv[] nodes = e.Data.GetData(typeof(TreeNodeAdv[])) as TreeNodeAdv[];
                TreeNodeAdv parent = _treeView.DropPosition.Node;

                foreach (TreeNodeAdv node in nodes)
                {
                    if (!CheckNodeParent(parent, node))
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }
                    if (_canDropItem != null &&
                        !_canDropItem(node.Tag as TreeNodeBase,
                            parent.Tag as TreeNodeBase,
                            _treeView.DropPosition.Position))
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }
                }

                e.Effect = e.AllowedEffect;
            }
        }

        private bool CheckNodeParent(TreeNodeAdv parent, TreeNodeAdv node)
        {
            while (parent != null)
            {
                if (node == parent)
                    return false;
                else
                    parent = parent.Parent;
            }
            return true;
        }

        /// <summary>
        /// Событие завершения перетаскивания объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DragDrop(object sender, DragEventArgs e)
        {
            if (_treeView.DropPosition.Node == null)
            {
                return;
            }
            TreeNodeBase dropNode = _treeView.DropPosition.Node.Tag as TreeNodeBase;
            TreeNodeAdv[] nodes = (TreeNodeAdv[])e.Data.GetData(typeof(TreeNodeAdv[]));

            _treeView.BeginUpdate();

            if (_treeView.DropPosition.Position == NodePosition.Inside)
            {
                foreach (TreeNodeAdv n in nodes)
                {
                    (n.Tag as TreeNodeBase).Parent = dropNode;
                }
                _treeView.DropPosition.Node.IsExpanded = true;
            }
            else
            {
                TreeNodeBase parent = dropNode.Parent;
                TreeNodeBase nextItem = dropNode;
                if (_treeView.DropPosition.Position == NodePosition.After)
                    nextItem = dropNode.NextNode;

                foreach (TreeNodeAdv node in nodes)
                {
                    (node.Tag as TreeNodeBase).Parent = null;
                }

                int index = -1;
                index = parent.Nodes.IndexOf(nextItem);
                foreach (TreeNodeAdv node in nodes)
                {
                    TreeNodeBase item = node.Tag as TreeNodeBase;
                    if (index == -1)
                        parent.Nodes.Add(item);
                    else
                    {
                        parent.Nodes.Insert(index, item);
                        index++;
                    }
                }
            }

            _treeView.EndUpdate();
        }
    }
}
