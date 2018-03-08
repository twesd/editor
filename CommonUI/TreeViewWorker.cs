using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Serializable;
using System.Collections;

namespace CommonUI
{
    /// <summary>
    /// Класс для работы с деревом объектов
    /// </summary>
    public class TreeViewWorker
    {
        /// <summary>
        /// Класс для сохранения узла
        /// </summary>
        class SavedTreeNode
        {
            public TreeNode Node;

            /// <summary>
            /// Родитель
            /// </summary>
            public TreeNode Parent;

            /// <summary>
            /// Индекс узла
            /// </summary>
            public int Index;

            public SavedTreeNode(TreeNode node, TreeNode parent, int index)
            {
                Node = node;
                Parent = parent;
                Index = index;
            }
        }

        /// <summary>
        /// Может ли объект перетаскиваться
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public delegate bool CanDragItemFunc(TreeNode node);

        /// <summary>
        /// Может ли объект перенистись
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public delegate bool CanDropItemFunc(TreeNode source, TreeNode target);

        /// <summary>
        /// Перенести в дочернии узлы
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public delegate bool DropToChildsFunc(TreeNode source, TreeNode target);

        /// <summary>
        /// Событие вставки нового узла
        /// </summary>
        /// <param name="node"></param>
        public delegate void PasteTreeNode(TreeNode node);

        /// <summary>
        /// Событие перед вставкой нового узла
        /// </summary>
        /// <param name="node"></param>
        public delegate void BeforePasteTreeNode(TreeNodeCollection parent, TreeNode node);

        /// <summary>
        /// Дерево объектов
        /// </summary>
        TreeView _treeView;

        /// <summary>
        /// Коллекция узлов сохранёных при фильтрации Узел - Родитель
        /// </summary>
        List<SavedTreeNode> _savedTreeNodes = new List<SavedTreeNode>();

        /// <summary>
        /// Может ли объект перетаскиваться
        /// </summary>
        CanDragItemFunc _canDragItem;

        /// <summary>
        /// Может ли объект перенистись
        /// </summary>
        CanDropItemFunc _canDropItem;

        /// <summary>
        /// Перенисти в дочернии узлы
        /// </summary>
        DropToChildsFunc _dropToChilds;

        /// <summary>
        /// Событие вставки нового узла
        /// </summary>
        PasteTreeNode _pasteTreeNode;

        /// <summary>
        /// Событие перед ставкой нового узла
        /// </summary>
        BeforePasteTreeNode _beforePasteTreeNode;

        public TreeViewWorker(TreeView treeView)
        {
            _treeView = treeView;
        }

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
                TreeNode node = FindNode(name, _treeView.Nodes);
                if (node == null)
                    return name;
                name = originalName + "_" + counter;
                counter++;
            }
        }

        /// <summary>
        /// Найти узел по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TreeNode FindNode(string name, List<TreeNode> ignoreNodes = null)
        {
            return FindNode(name, _treeView.Nodes, ignoreNodes);
        }

        /// <summary>
        /// Найти узел по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TreeNode FindNode(string name, TreeNodeCollection nodes, List<TreeNode> ignoreNodes = null)
        {
            foreach (TreeNode node in nodes)
            {
                if (string.Compare(node.Text, name, true) == 0)
                {
                    if (ignoreNodes == null || !ignoreNodes.Contains(node))
                        return node;
                }
                var targetNode = FindNode(name, node.Nodes, ignoreNodes);
                if (targetNode != null)
                    return targetNode;
            }
            return null;       
        }

        /// <summary>
        /// Установить фильтр для дерева
        /// </summary>
        /// <param name="textBox"></param>
        public void EnableFilter(TextBox textBox)
        {
            textBox.TextChanged += Filter_TextChanged;
        }

        /// <summary>
        /// Редактирование наименования узла
        /// </summary>
        public void EnableTreeNodeGroupEdit()
        {
            _treeView.LabelEdit = true;
            _treeView.BeforeLabelEdit += BeforeLabelEdit;
        }

        /// <summary>
        /// Установить возможность перетаскивания
        /// </summary>
        public void EnableDragDrop(
            CanDragItemFunc canDragItem, 
            CanDropItemFunc canDropItem, 
            DropToChildsFunc dropToChild)
        {
            _canDragItem = canDragItem;
            _canDropItem = canDropItem;
            _dropToChilds = dropToChild;

            _treeView.AllowDrop = true;
            _treeView.ItemDrag += new ItemDragEventHandler(ItemDrag);
            _treeView.DragDrop += new DragEventHandler(DragDrop);
            _treeView.DragOver += new DragEventHandler(DragOver);

        }


        void BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (!(e.Node is TreeNodeGroup))
                e.CancelEdit = true;            
        }

        /// <summary>
        /// Событие когда переносимый объект в пределах дерева
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DragOver(object sender, DragEventArgs e)
        {
            TreeNode hoverNode = GetHoveringNode(e.X, e.Y);
            TreeNode draggingNode = GetDraggingNode(e);
            if (hoverNode == null ||
                draggingNode == null ||
                hoverNode == draggingNode ||
                draggingNode.Nodes.Contains(hoverNode))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            if (_canDropItem != null && !_canDropItem(draggingNode, hoverNode))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            e.Effect = DragDropEffects.Move;
            _treeView.SelectedNode = hoverNode;
        }

        /// <summary>
        /// Получить перетаскиваемый объект
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private static TreeNode GetDraggingNode(DragEventArgs e)
        {
            TreeNode outNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            if(outNode == null)
                outNode = e.Data.GetData(typeof(TreeNodeGroup)) as TreeNode;
            return outNode;
        }

        /// <summary>
        /// Событие завершения переноса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                TreeNode hoverNode = GetHoveringNode(e.X, e.Y);
                TreeNode draggingNode = GetDraggingNode(e);
                if (hoverNode != null && draggingNode != null)
                {
                    draggingNode.Remove();
                    if (_dropToChilds != null && !_dropToChilds(draggingNode, hoverNode))
                    {
                        TreeNodeCollection nodes = (hoverNode.Parent == null) ?
                            _treeView.Nodes : hoverNode.Parent.Nodes;

                        nodes.Insert(hoverNode.Index + 1, draggingNode);                        
                    }
                    else
                    {                        
                        hoverNode.Nodes.Add(draggingNode);
                    }
                }
            }
        }

        /// <summary>
        /// Событие начала перетаскивания объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (_canDragItem != null && !_canDragItem(e.Item as TreeNode)) return;
            _treeView.DoDragDrop(e.Item, DragDropEffects.All);
        }

        /// <summary>
        /// Получить узел под мышью
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private TreeNode GetHoveringNode(int x, int y)
        {
            Point point = _treeView.PointToClient(new Point(x, y));
            TreeViewHitTestInfo hitInfo = _treeView.HitTest(point);
            return hitInfo.Node;
        }


        /// <summary>
        /// Изменение значения фильтра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Filter_TextChanged(object sender, EventArgs e)
        {
            Restore();
            TextBox textBox = sender as TextBox;
            if (textBox == null) return;
            if (string.IsNullOrEmpty(textBox.Text))
                return;
            FilterTreeView(textBox.Text, _treeView.Nodes);
        }

        /// <summary>
        /// Клонировать узлы
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        List<TreeNode> CloneTreeNodes(List<TreeNode> nodes)
        {
            List<TreeNode> outNodes = new List<TreeNode>();
            foreach (TreeNode node in nodes)
                outNodes.Add(node.Clone() as TreeNode);  
            return outNodes;
        }

        /// <summary>
        /// Клонировать узлы
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        List<TreeNode> CloneTreeNodes(TreeNodeCollection nodes)
        {
            List<TreeNode> outNodes = new List<TreeNode>();
            foreach (TreeNode node in nodes)
                outNodes.Add(node.Clone() as TreeNode);
            return outNodes;
        }

        /// <summary>
        /// Восстановить первоначальное состояние дерева c учётом изменений
        /// </summary>
        void Restore()
        {
            if (_savedTreeNodes.Count == 0) 
                return;

            _treeView.BeginUpdate();

            // Добавляем удалёные узлы обрабтно в дерево
            //
            foreach (SavedTreeNode saveTreeNode in _savedTreeNodes)
            {                
                if (saveTreeNode.Parent == null)
                {
                    _treeView.Nodes.Add(saveTreeNode.Node);
                }
                else
                {
                    saveTreeNode.Parent.Nodes.Add(saveTreeNode.Node);
                }                                
            }
            // Пытаемся восстановить их положение
            //
            foreach (SavedTreeNode saveTreeNode in _savedTreeNodes)
            {
                if (saveTreeNode.Index == saveTreeNode.Node.Index)
                    continue;

                TreeNodeCollection nodes = (saveTreeNode.Parent == null) ? 
                    _treeView.Nodes : saveTreeNode.Parent.Nodes;

                if (saveTreeNode.Index > nodes.Count || saveTreeNode.Index < 0)
                    continue;
                saveTreeNode.Node.Remove();
                nodes.Insert(saveTreeNode.Index, saveTreeNode.Node);
            }
            _savedTreeNodes.Clear();

            _treeView.EndUpdate();
        }

        /// <summary>
        /// Фильтрация дерева
        /// </summary>
        /// <param name="text"></param>
        /// <param name="nodes"></param>
        void FilterTreeView(string text, TreeNodeCollection nodes)
        {
            List<TreeNode> removeNodes = new List<TreeNode>();
            foreach (TreeNode node in nodes)
            {
                if (!node.Text.ToUpper().Contains(text.ToUpper()) &&
                    !NodesContainsText(text, node.Nodes))
                {
                    _savedTreeNodes.Add(new SavedTreeNode(node, node.Parent, node.Index));

                    removeNodes.Add(node);                    
                }
                else
                {
                    node.Expand();
                    FilterTreeView(text, node.Nodes);
                }
            }
            // Удаляем узлы
            foreach (TreeNode node in removeNodes)
            {                
                node.Remove();
            }
        }

        /// <summary>
        /// Содержат ли элементы заданный текст
        /// </summary>
        /// <param name="text"></param>
        /// <param name="treeNodeCollection"></param>
        /// <returns></returns>
        private bool NodesContainsText(string text, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text.ToUpper().Contains(text.ToUpper()) ||
                    NodesContainsText(text, node.Nodes))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Получить коллекцию узлов для TreeView
        /// </summary>
        /// <returns></returns>
        public static List<TreeNode> GetTreeNodes(ContainerTreeView treeviewContainer)
        {
            return GetTreeNodes(treeviewContainer.Nodes);
        }

        public static List<TreeNode> GetTreeNodes(List<SerializableTreeNode> nodes)
        {
            List<TreeNode> outNodes = new List<TreeNode>();
            foreach (SerializableTreeNode serializeNode in nodes)
            {
                TreeNode node;
                if (serializeNode.Type == "CommonUI.TreeNodeGroup")
                    node = new CommonUI.TreeNodeGroup();
                else
                    node = new TreeNode();
                node.Text = serializeNode.Text;
                node.Tag = serializeNode.Tag;
                node.Nodes.AddRange(GetTreeNodes(serializeNode.Nodes).ToArray());
                outNodes.Add(node);
            }
            return outNodes;
        }

        /// <summary>
        /// Включить возможность копирования данных
        /// </summary>
        public void EnableCopyPaste(PasteTreeNode pasteTreeNode = null, BeforePasteTreeNode beforePasteTreeNode = null)
        {
            _treeView.PreviewKeyDown += new PreviewKeyDownEventHandler(CopyPaste_PreviewKeyDown);
            _pasteTreeNode = pasteTreeNode;
            _beforePasteTreeNode = beforePasteTreeNode;
        }

        void CopyPaste_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control && _treeView.SelectedNode != null)
            {                
                Clipboard.SetData(typeof(TreeNode).FullName, _treeView.SelectedNode);
            }
            else if (e.KeyCode == Keys.V && e.Control)
            {
                TreeNode item = Clipboard.GetData(typeof(TreeNode).FullName) as TreeNode;
                if (item != null)
                {
                    TreeNode newTreeNode = item.Clone() as TreeNode;


                    if (_treeView.SelectedNode != null)
                    {
                        TreeNodeCollection nodes = (_treeView.SelectedNode.Parent == null) ?
                            _treeView.Nodes : _treeView.SelectedNode.Parent.Nodes;


                        if (_beforePasteTreeNode != null)
                        {
                            _beforePasteTreeNode(nodes, newTreeNode);
                        }

                        nodes.Insert(_treeView.SelectedNode.Index + 1, newTreeNode);
                    }
                    else
                    {
                        if (_beforePasteTreeNode != null)
                        {
                            _beforePasteTreeNode(_treeView.Nodes, newTreeNode);
                        }

                        _treeView.Nodes.Add(newTreeNode);
                    }


                    if (_pasteTreeNode != null)
                    {
                        _pasteTreeNode(newTreeNode);
                    }
                }
            }
        }
    }
}
