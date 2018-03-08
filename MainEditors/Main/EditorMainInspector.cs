using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using CommonUI;
using Serializable;
using TransactionCore;
using Aga.Controls.Tree;
using System.Collections.ObjectModel;
using Common;

namespace MainEditors.Main
{
    public partial class EditorMainInspector : DockContent
    {
        public delegate void ItemChangedEventHadler(object o);

        /// <summary>
        /// Создание нового объекта
        /// </summary>
        public event ItemChangedEventHadler ItemCreated;

        /// <summary>
        /// Удаление объекта
        /// </summary>
        public event ItemChangedEventHadler ItemDeleted;

        /// <summary>
        /// Изменение объекта
        /// </summary>
        public event ItemChangedEventHadler SelectionChanged;

        /// <summary>
        /// Класс для работы с деревом объектов
        /// </summary>
        TreeViewAdvWorker _treeViewWorker;

        /// <summary>
        /// Редактор стадии
        /// </summary>
        EditorMain _mainEditor;

        /// <summary>
        /// Менеджер для undo / redo
        /// </summary>
        TransactionManager _transManager;

        /// <summary>
        /// Модель данных дерева
        /// </summary>
        TreeModel _treeModel;

        public EditorMainInspector(EditorMain editor, TransactionManager transManager)
        {
            InitializeComponent();

            _mainEditor = editor;
            _transManager = transManager;

            _treeModel = new TreeModel(transManager);
            _treeView.Model = _treeModel;

            _treeViewWorker = new TreeViewAdvWorker(_treeView);
            //_treeViewWorker.EnableFilter(_textBoxFilter);
            _treeViewWorker.EnableDragDrop(CanDragItem, CanDropItem);
            _treeViewWorker.EnableCopyPaste();
        }

        public void Clear()
        {
            _treeModel.Nodes.Clear();
        }

        /// <summary>
        /// Выбранный объект
        /// </summary>
        public object SelectedItem
        {
            get
            {
                if (_treeView.SelectedNode == null)
                {
                    return null;
                }
                var dataNode = _treeView.SelectedNode.Tag as TreeNodeBase;
                return (dataNode.Tag);
            }
            set
            {
                SetSelectionItem(value);
            }
        }

        /// <summary>
        /// Установить дерево
        /// </summary>
        /// <param name="treeView"></param>
        public void SetContainerTreeView(ContainerTreeView container)
        {
            _treeView.BeginUpdate();

            List<TreeNodeBase> nodes =
                TreeViewConvertor.ConvertToNodes(container.Nodes, _transManager, CreateTreeNodeHandler);
            _treeModel.Nodes.Clear();
            foreach (var node in nodes)
            {
                _treeModel.Nodes.Add(node);
            }
            UpdateTreeView();

            _treeView.EndUpdate();
        }

        /// <summary>
        /// Получить данные дерева
        /// </summary>
        /// <returns></returns>
        public ContainerTreeView GetContainerTreeView()
        {
            return TreeViewConvertor.ConvertToContainer(_treeModel.Nodes);
        }

        /// <summary>
        /// Получить полный список объектов
        /// </summary>
        /// <returns></returns>
        public List<StageItem> GetStages()
        {
            return GetTreeViewStages(_treeModel.Nodes);
        }

        /// <summary>
        /// Может ли объект перетаскиваться
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool CanDragItem(TreeNodeBase node)
        {
            return (node.Tag is StageItem || node is TreeNodeGroup);
        }

        /// <summary>
        /// Можно ли перетащить объект 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        bool CanDropItem(TreeNodeBase source, TreeNodeBase target, NodePosition nodePos)
        {
            if (source == null)
            {
                return false;
            }
            if (nodePos == NodePosition.Inside)
            {
                return (target is TreeNodeGroup);
            }

            return true;
        }

        void EditorStageInspector_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        /// <summary>
        /// Получить список объектов
        /// </summary>
        /// <returns></returns>
        List<StageItem> GetTreeViewStages(Collection<TreeNodeBase> nodes)
        {
            List<StageItem> stageInstances = new List<StageItem>();
            foreach (TreeNodeBase node in nodes)
            {
                List<StageItem> childItems = GetTreeViewStages(node.Nodes);
                stageInstances.AddRange(childItems);

                StageItem item = node.Tag as StageItem;
                if (item == null)
                {
                    continue;
                }
                stageInstances.Add(item);
            }
            return stageInstances;
        }

        /// <summary>
        /// Обновление дерева анимации
        /// </summary>
        void UpdateTreeView()
        {
            _treeView.FullUpdate();
        }

        /// <summary>
        /// Выбрать узел по Tag
        /// </summary>
        /// <param name="tag"></param>
        void SetSelectionItem(object tag)
        {
            TreeNodeAdv node = _treeViewWorker.FindTreeNode(tag, _treeView.Root.Children);
            if (node == null)
            {
                return;
            }
            _treeView.SelectedNode = node;
        }
        
        /// <summary>
        /// Событие выбора узла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_SelectionChanged(object sender, EventArgs e)
        {
            _menuItemOpen.Visible = false;
            _menuItemStartStage.Visible = false;

            if (_treeView.SelectedNode != null)
            {
                TreeNodeBase modelNode = _treeView.SelectedNode.Tag as TreeNodeBase;
                if (modelNode == null)
                {
                    return;
                }
                StageItem item = modelNode.Tag as StageItem;
                if (item == null)
                {
                    return;
                }
                _menuItemOpen.Visible = true;
                _menuItemStartStage.Visible = true;
                _menuItemOpen.Tag = item;
            }


            NotifySelectionChanged();
        }
        
        private void TreeView_NodeMouseClick(object sender, TreeNodeAdvMouseEventArgs e)
        {
           
        }

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeAdvMouseEventArgs e)
        {
            TreeNodeBase modelNode = e.Node.Tag as TreeNodeBase;
            if (modelNode == null)
            {
                return;
            }
            StageItem stageItem = modelNode.Tag as StageItem;
            if (stageItem == null)
            {
                return;
            }

            _mainEditor.OpenStage(stageItem.Path);
        }

        void TreeViewItems_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteItem();
            }
        }
        
        /// <summary>
        /// Создать выбранный узел анимации
        /// </summary>
        void DeleteItem()
        {
            if (_treeView.SelectedNode == null)
            {
                return;
            }

            _textBoxFilter.Text = string.Empty;

            var dataNode = _treeView.SelectedNode.Tag as TreeNodeBase;
            object oItem = dataNode.Tag;

            dataNode.Parent.Nodes.Remove(dataNode);

            if (ItemDeleted != null)
            {
                ItemDeleted(oItem);
            }

        }

        /// <summary>
        /// Создать новый объект
        /// </summary>
        void CreateItem(StageItem stageItem)
        {
            _textBoxFilter.Text = string.Empty;

            bool addInside = false;
            TreeNodeAdv target = _treeView.Root;
            if (_treeView.SelectedNode != null)
            {
                if (_treeView.SelectedNode.Tag is TreeNodeGroup)
                {
                    target = _treeView.SelectedNode;
                    addInside = true;
                }
                else if (_treeView.SelectedNode.Parent != null &&
                    _treeView.SelectedNode.Parent.Tag is TreeNodeGroup)
                {
                    target = _treeView.SelectedNode.Parent;
                    addInside = true;
                }
                else
                {
                    target = _treeView.SelectedNode;
                }
            }

            TreeModel model = _treeView.Model as TreeModel;

            TreeNodeBase dataTarget = target.Tag as TreeNodeBase;
            if (dataTarget == null)
            {
                dataTarget = _treeModel.Root;
            }

            var newNode = new TreeViewNodeStage(stageItem, _transManager);

            if (addInside)
            {
                dataTarget.Nodes.Add(newNode);
            }
            else
            {
                var parent = dataTarget.Parent;
                if (parent == null)
                {
                    dataTarget.Nodes.Add(newNode);
                }
                else
                {
                    int index = parent.Nodes.IndexOf(dataTarget) + 1;
                    parent.Nodes.Insert(index, newNode);
                }
            }

            var nodeAdv = _treeView.FindNodeByTag(newNode);
            _treeView.SelectedNode = nodeAdv;

            UpdateTreeView();

            if (ItemCreated != null)
            {
                ItemCreated(stageItem);
            }
        }

        /// <summary>
        /// Создать группу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CreateGroup(object sender, EventArgs e)
        {
            _textBoxFilter.Text = string.Empty;

            CommonUI.TreeNodeGroup newNode = new CommonUI.TreeNodeGroup(new GroupData());
            newNode.Text = _treeModel.GetUniqueName("Group");

            TreeModel model = _treeView.Model as TreeModel;

            model.Root.Nodes.Add(newNode);
            _treeView.SelectedNode = _treeView.FindNodeByTag(newNode);

            UpdateTreeView();
        }

        void Refresh_Click(object sender, EventArgs e)
        {
            UpdateTreeView();
        }

        /// <summary>
        /// Удалить узел
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemDelete_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        void MenuItemOpenStage(object sender, EventArgs e)
        {
            StageItem item = _menuItemOpen.Tag as StageItem;
            if (item != null)
            {
                _mainEditor.OpenStage(item.Path);
            }
        }

        /// <summary>
        /// Добавить стадию
        /// </summary>
        /// <param name="path"></param>
        void MenuItemCreateStageItem(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл стадии (*.stage)|*.stage|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть файл стадии";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            StageItem stageItem = new StageItem();
            stageItem.Path = dialog.FileName;
            CreateItem(stageItem);
        }

        /// <summary>
        /// Установить начальную стадию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemStartStage(object sender, EventArgs e)
        {
            StageItem item = _menuItemOpen.Tag as StageItem;
            if (item != null)
            {
                foreach (StageItem stageItem in GetTreeViewStages(_treeModel.Nodes))
                {
                    stageItem.IsStartStage = false;
                }
                item.IsStartStage = true;
            }
            UpdateTreeView();
        }

        /// <summary>
        /// Функция создания узла дерева
        /// </summary>
        /// <param name="sNode"></param>
        /// <returns></returns>
        TreeNodeBase CreateTreeNodeHandler(SerializableTreeNode sNode, out bool handleChilds)
        {
            handleChilds = true;
            if (sNode.Type == typeof(TreeNodeGroup).ToString())
            {
                return new TreeNodeGroup(sNode.Tag as GroupData);
            }
            else
            {
                return new TreeViewNodeStage(sNode, _transManager);
            }
        }

        void NotifySelectionChanged()
        {
            if (SelectionChanged != null)
            {
                if (_treeView.SelectedNode != null)
                {
                    var dataNode = _treeView.SelectedNode.Tag as TreeNodeBase;
                    if (dataNode != null)
                    {
                        SelectionChanged(dataNode.Tag);
                    }
                    else
                    {
                        SelectionChanged(null);
                    }
                }
                else
                {
                    SelectionChanged(null);
                }
            }
        }

    }
}
