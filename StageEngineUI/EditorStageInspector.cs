using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Serializable;
using CommonUI;
using StageEngine;
using UnitEngine;
using TransactionCore;
using Aga.Controls.Tree;
using System.Collections.ObjectModel;
using Common;

namespace StageEngineUI
{
    public partial class EditorStageInspector : DockContent
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
        EditorStage _mainEditor;

        /// <summary>
        /// Модель данных дерева
        /// </summary>
        TreeModel _treeModel;

        /// <summary>
        /// Менеджер для undo / redo
        /// </summary>
        TransactionManager _transManager;

        public EditorStageInspector(EditorStage editor, TransactionManager transManager)
        {
            if (editor == null)
            {
                throw new ArgumentNullException("mainEditor");
            }
            if (transManager == null)
            {
                throw new ArgumentNullException("transManager");
            }

            InitializeComponent();

            _mainEditor = editor;
            _transManager = transManager;

            _treeModel = new TreeModel(transManager);
            _treeView.Model = _treeModel;

            _treeViewWorker = new TreeViewAdvWorker(_treeView);
            //_treeViewWorker.EnableFilter(_textBoxFilter);
            _treeViewWorker.EnableDragDrop(CanDragItem, CanDropItem);
            _treeViewWorker.EnableCopyPaste(null, PasteItem);
        }

        public void Clear()
        {
            _treeModel.Nodes.Clear();
        }

        private void EditorStageInspector_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
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
                if (dataNode == null)
                {
                    return null;
                }
                return dataNode.Tag;
            }
            set
            {
                SetSelectionItem(value);
            }
        }

        /// <summary>
        ///  Получить модель по редактируемому юниту
        /// </summary>
        /// <param name="_editItem"></param>
        /// <returns></returns>
        public UnitInstanceBase GetInstance(SceneNodeW nodeW)
        {
            if (nodeW == null)
            {
                return null;
            }
            foreach (UnitInstanceBase instance in GetInstances())
            {
                if (nodeW.Id == instance.EditorModelId)
                    return instance;
            }
            return null;
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
        /// Получить полный список объектов
        /// </summary>
        /// <returns></returns>
        public List<UnitInstanceBase> GetInstances()
        {
            return GetTreeViewInstances(_treeModel.Nodes);
        }

        /// <summary>
        /// Обновление отображения дерева объектов
        /// </summary>
        public void UpdateTreeView()
        {
            _treeView.FullUpdate();
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
        /// Добавить стандартный экземпляр юнита
        /// </summary>
        /// <param name="behaviorPath"></param>
        public UnitInstanceStandard CreateInstanceStandard(string behaviorPath)
        {
            UnitInstanceStandard instance = new UnitInstanceStandard(
               _treeModel.GetUniqueName(System.IO.Path.GetFileNameWithoutExtension(behaviorPath)),
               behaviorPath);
            instance.Behavior = UnitBehavior.LoadFromFile(behaviorPath);
            instance.Behavior.ToAbsolutePaths(behaviorPath);
            CreateItem(instance);
            return instance;
        }

        /// <summary>
        /// Добавить билбоард
        /// </summary>
        /// <param name="behaviorPath"></param>
        public UnitInstanceBillboard CreateInstanceBillboard(string texturePath)
        {
            UnitInstanceBillboard instance = new UnitInstanceBillboard()
            {
                Name = _treeModel.GetUniqueName("Billboard"),
                Texture = texturePath,
                Width = 10,
                Height = 10
            };

            CreateItem(instance);
            return instance;
        }

        /// <summary>
        /// Добавить камеру
        /// </summary>
        /// <param name="behaviorPath"></param>
        public UnitInstanceCamera CreateInstanceCamera()
        {
            UnitInstanceCamera instance = new UnitInstanceCamera()
            {
                Name = _treeModel.GetUniqueName(
                    System.IO.Path.GetFileNameWithoutExtension("Camera"))
            };
            CreateItem(instance);
            return instance;
        }

        /// <summary>
        /// Получить список объектов
        /// </summary>
        /// <returns></returns>
        List<UnitInstanceBase> GetTreeViewInstances(Collection<TreeNodeBase> nodes)
        {
            List<UnitInstanceBase> stageInstances = new List<UnitInstanceBase>();
            foreach (TreeNodeBase node in nodes)
            {
                List<UnitInstanceBase> childItems = GetTreeViewInstances(node.Nodes);
                stageInstances.AddRange(childItems);

                UnitInstanceBase unitInstance = node.Tag as UnitInstanceBase;
                if (unitInstance == null) continue;
                stageInstances.Add(unitInstance);
            }
            return stageInstances;
        }


        /// <summary>
        /// Может ли объект перетаскиваться
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool CanDragItem(TreeNodeBase node)
        {
            return (node.Tag is UnitInstanceBase || node is TreeNodeGroup);
        }

        /// <summary>
        ///  Можно ли перетащить объект 
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

        /// <summary>
        /// Обработка вставки объекта Copy-Paste
        /// </summary>
        /// <param name="source"></param>
        void PasteItem(TreeNodeBase source)
        {
            foreach (TreeNodeBase child in source.Nodes)
            {
                PasteItem(child);
            }

            var instance = source.Tag as UnitInstanceBase;
            if (instance == null)
            {
                return;
            }

            instance.Name = _treeModel.GetUniqueName(instance.Name);
            if (ItemCreated != null)
            {
                ItemCreated(instance);                
            }
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

        void TreeView_SelectionChanged(object sender, EventArgs e)
        {
            NotifySelectionChanged();
        }

        void TreeView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteItem();
            }
        }

        /// <summary>
        /// Оповещение об изменении выбранных объектов
        /// </summary>
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
                return new TreeNodeInstance(sNode, _transManager);
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

            _treeView.FullUpdate();
        }

        /// <summary>
        /// Удалить узел анимации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        /// <summary>
        /// Создать новый юнит
        /// </summary>
        void CreateItem(UnitInstanceBase instance)
        {
            _textBoxFilter.Text = string.Empty;

            // Создаём по умолчанию создание сразу
            if (instance.Creations.Count == 0)
            {
                instance.Creations.Add(new UnitCreationTimer());
            }

            TreeNodeAdv target = _treeView.Root;
            bool addInside = false;
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

            var newNode = new TreeNodeInstance(instance, _transManager);

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
                ItemCreated(instance);
            }
        }

        /// <summary>
        /// Создать группу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateGroup_Click(object sender, EventArgs e)
        {
            CreateGroup();
        }

        /// <summary>
        /// Создать группу
        /// </summary>
        private void CreateGroup()
        {
            _textBoxFilter.Text = string.Empty;

            CommonUI.TreeNodeGroup newNode = new CommonUI.TreeNodeGroup(new GroupData());
            newNode.Text = _treeModel.GetUniqueName("Group");

            TreeModel model = _treeView.Model as TreeModel;

            model.Root.Nodes.Add(newNode);
            _treeView.SelectedNode = _treeView.FindNodeByTag(newNode);

            UpdateTreeView();
        }

        void CreateInstanceEnv_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл модели (*.x)|*.X|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Выберите файл модели";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            
            UnitInstanceEnv unitEnv = new UnitInstanceEnv(
                _treeModel.GetUniqueName(_treeModel.GetUniqueName(
                    System.IO.Path.GetFileNameWithoutExtension(dialog.FileName))),
                dialog.FileName);
            CreateItem(unitEnv);
        }

        /// <summary>
        /// Создать новый стандартный экземпляр юнита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateInstanceStandard_Click(object sender, EventArgs e)
        {
            string selectedPath = GetPathToBehavior();
            if (string.IsNullOrEmpty(selectedPath))
                return;
            CreateInstanceStandard(selectedPath);
        }

        private void CreateInstanceBillboard_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл изображения (*.png,*.jpeg,*.jpg)|*.png;*.jpeg;*.jpg|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть Файл изображения";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            CreateInstanceBillboard(dialog.FileName);
        }

        /// <summary>
        /// Создать новый пустой юнита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateInstanceEmpty(object sender, EventArgs e)
        {
            UnitInstanceEmpty instance = new UnitInstanceEmpty()
            {
                Name = _treeModel.GetUniqueName("Empty")
            };
            CreateItem(instance);
        }

        /// <summary>
        /// Создать камеру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateInstanceCamera_Click(object sender, EventArgs e)
        {
            CreateInstanceCamera();
        }

        string GetPathToBehavior()
        {
            if (string.IsNullOrEmpty(_mainEditor.FilePath))
            {
                MessageBox.Show("Проект не сохранён");
                return null;
            }
            if (_mainEditor.BehaviorsPackage.Count == 0)
            {
                MessageBox.Show("Типы отсутствуют");
                return null;
            }

            // Генерация дерева для формы выбора тип юнита
            //
            ContainerTreeView contTreeView = new ContainerTreeView();
            foreach (var pair in _mainEditor.BehaviorsPackage)
            {
                contTreeView.Nodes.Add(new SerializableTreeNode()
                {
                    Text = System.IO.Path.GetFileNameWithoutExtension(pair.Key),
                    Tag = pair.Key
                });
            }
            // Отображение формы выбора тип юнита
            FormSelectObject selectForm = new FormSelectObject(
                "Выберите тип", contTreeView);
            if (selectForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return null;
            return (selectForm.Result as string);
        }

        void Refresh_Click(object sender, EventArgs e)
        {
            UpdateTreeView();
        }


    }
}
