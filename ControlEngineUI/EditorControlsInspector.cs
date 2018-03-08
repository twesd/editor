using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ControlEngine;
using Serializable;
using CommonUI;
using TransactionCore;
using System.Collections.ObjectModel;
using Aga.Controls.Tree;
using Common;
using ControlEngineUI.Properties;

namespace ControlEngineUI
{
    public partial class EditorControlsInspector : DockContent
    {
        public delegate void ItemChangedEventHadler(object o);
                
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
        }
        
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
        /// Редактор контролов
        /// </summary>
        EditorControls _mainEditor;

        /// <summary>
        /// Модель данных дерева
        /// </summary>
        TreeModel _treeModel;

        /// <summary>
        /// Менеджер для undo / redo
        /// </summary>
        TransactionManager _transManager;

        public EditorControlsInspector(EditorControls mainEditor, TransactionManager transManager)
        {
            if (mainEditor == null)
            {
                throw new ArgumentNullException("mainEditor");
            }
            if (transManager == null)
            {
                throw new ArgumentNullException("transManager");
            }

            InitializeComponent();

            _treeModel = new TreeModel(transManager);
            _treeView.Model = _treeModel;


            _mainEditor = mainEditor;
            _transManager = transManager;

            _treeViewWorker = new TreeViewAdvWorker(_treeView);
            //_treeViewWorker.EnableFilter(_textBoxFilter);
            _treeViewWorker.EnableDragDrop(CanDragItem, CanDropItem);
            _treeViewWorker.EnableCopyPaste();
        }

        /// <summary>
        /// Удаление объектов в дереве
        /// </summary>
        public void Clear()
        {
            _treeModel.Nodes.Clear();
        }

        /// <summary>
        /// Установить дерево
        /// </summary>
        /// <param name="container"></param>
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
        /// Получить объекты из дерева
        /// </summary>
        /// <returns></returns>
        public List<ControlBase> GetControls()
        {
            return GetTreeViewControls(_treeModel.Nodes);
        }

        /// <summary>
        /// Получить контрол по позиции
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ControlBase GetControlByPosition(int x, int y)
        {
            ControlBase controlOut = null;
            foreach (ControlBase control in GetControls())
            {
                if (!control.IsVisibleInEditor)
                {
                    continue;
                }

                IControlSizeable cntrlSizeable = control as IControlSizeable;
                if (cntrlSizeable != null)
                {
                    if (cntrlSizeable.IsPointInside(x, y))
                        controlOut = control;
                }
            }
            return controlOut;
        }

        /// <summary>
        /// Установить выборку
        /// </summary>
        /// <param name="control"></param>
        public void SetSelection(ControlBase control)
        {
            TreeNodeAdv node = _treeViewWorker.FindTreeNode(control, _treeView.Root.Children);
            if (node == null) return;
            _treeView.SelectedNode = node;
        }

        /// <summary>
        /// Обновление дерева
        /// </summary>
        public void UpdateTreeView()
        {
            _treeView.FullUpdate();
        }


        /// <summary>
        /// Обработка изменения свойств объекта
        /// </summary>
        /// <param name="oItem"></param>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        public void PropertyChanged(object oItem, string propName, object propValue)
        {
            ControlBase control = oItem as ControlBase;
            if (control == null)
            {
                return;
            }
            TreeNodeAdv nodeAdv = _treeViewWorker.FindTreeNode(control, _treeView.Root.Children);
            if (nodeAdv == null)
            {
                return;
            }

            var nodeData = nodeAdv.Tag as TreeNodeBase;
            TreeNodeXref xrefRootNode = XRefControlsWorker.GetXRefNodeRoot(nodeData);
            // Если объект входит во внешнею ссылку, то
            if (xrefRootNode != null)
            {
                // Если это контрол является оригинальной частью ссылки, 
                // т.е. не добавлен через операцию добавления
                if (XRefControlsWorker.IsOrigOwnControl(xrefRootNode.XRefData, control))
                {
                    using (Transaction trans = _transManager.StartTransaction())
                    {
                        trans.AddObject(xrefRootNode.XRefData);

                        XRefOperationChange opChange = new XRefOperationChange(
                            control.Id,
                            propName,
                            propValue);
                        xrefRootNode.XRefData.Operations.Add(opChange);
                        xrefRootNode.XRefData.RemoveDublicateOperations();

                        if (nodeData is TreeViewNodeControls)
                        {
                            // Устанавливаем доп. иконку для отображения, что объект изменён
                            //
                            var controlTreeViewNode = nodeData as TreeViewNodeControls;
                            controlTreeViewNode.IconOverlay = Resources.IconOverlayChange.ToBitmap();
                        }

                        trans.Commit();
                    }
                }
            }
        }

        /// <summary>
        /// Может ли объект перетаскиваться
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool CanDragItem(TreeNodeBase node)
        {
            return (node.Tag is ControlBase || node is TreeNodeGroup);
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
        /// Получить полный список анимаций
        /// </summary>
        /// <returns></returns>
        List<ControlBase> GetTreeViewControls(Collection<TreeNodeBase> nodes)
        {
            List<ControlBase> controls = new List<ControlBase>();
            foreach (TreeNodeBase node in nodes)
            {
                List<ControlBase> childItems = GetTreeViewControls(node.Nodes);
                controls.AddRange(childItems);

                ControlBase control = node.Tag as ControlBase;
                if (control == null)
                {
                    continue;
                }
                controls.Add(control);
            }
            return controls;
        }

        /// <summary>
        /// Функция создания узла дерева
        /// </summary>
        /// <param name="sNode"></param>
        /// <returns></returns>
        private TreeNodeBase CreateTreeNodeHandler(SerializableTreeNode sNode, out bool handleChilds)
        {
            handleChilds = true;
            if (sNode.Type == typeof(TreeNodeGroup).ToString())
            {
                return new TreeNodeGroup(sNode.Tag as GroupData);
            }
            else if (sNode.Type == typeof(TreeNodeXref).ToString())
            {
                var treeNodeXref = new TreeNodeXref(sNode.Tag as XRefData);
                XRefControlsWorker.UpdateXref(
                    treeNodeXref,
                    _transManager,
                    CreateTreeNodeHandler);
                handleChilds = false;
                return treeNodeXref;
            }
            else
            {
                return new TreeViewNodeControls(sNode, _transManager);
            }
        }

        /// <summary>
        /// Создать новый контрол 
        /// </summary>
        private void CreateItem(ControlBase control)
        {
            _textBoxFilter.Text = string.Empty;            

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

            TreeNodeXref xrefRootNode = null;
            if (dataTarget is TreeNodeXref)
            {
                xrefRootNode = dataTarget as TreeNodeXref;
            }
            else
            {
                xrefRootNode = XRefControlsWorker.GetXRefNodeRoot(dataTarget);
            }
            // Если объект необходимо добавить во внешнею ссылку, то
            if (xrefRootNode != null)
            {
                // Заносим операцию добавления

                XRefOperationAdd addOp = new XRefOperationAdd(control);
                xrefRootNode.XRefData.Operations.Add(addOp);
                XRefControlsWorker.UpdateXref(
                    xrefRootNode,
                    _transManager,
                    CreateTreeNodeHandler);
            }
            else
            {
                var newNode = new TreeViewNodeControls(control, _transManager);

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
            }

            UpdateTreeView();

            if (ItemCreated != null) ItemCreated(control);
        }

        /// <summary>
        /// Оповещение об изменении выбранных объектов
        /// </summary>
        private void NotifySelectionChanged()
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
        /// Удалить узел
        /// </summary>
        private void DeleteItem()
        {
            if (_treeView.SelectedNode == null)
            {
                return;
            }

            _textBoxFilter.Text = string.Empty;

            var dataNode = _treeView.SelectedNode.Tag as TreeNodeBase;
            object oItem = dataNode.Tag;

            TreeNodeXref treeNodeXRef = XRefControlsWorker.GetXRefNodeRoot(dataNode);
            // Если это объект внешней ссылки, то ...
            if (treeNodeXRef != null)
            {
                string id = Helper.GetItemId(dataNode.Tag);
                if (string.IsNullOrEmpty(id))
                {
                    return;
                }
                var delOp = new XRefOperationDelete(id);
                treeNodeXRef.XRefData.Operations.Add(delOp);

                XRefControlsWorker.UpdateXref(
                   treeNodeXRef,
                   _transManager,
                   CreateTreeNodeHandler);
            }
            else
            {
                dataNode.Parent.Nodes.Remove(dataNode);
            }

            if (ItemDeleted != null)
            {
                ItemDeleted(oItem);
            }
        }

        private void TreeView_DragDrop(object sender, DragEventArgs e)
        {
            NotifySelectionChanged();
        }

        private void TreeView_SelectionChanged(object sender, EventArgs e)
        {            
            NotifySelectionChanged();
        }

        /// <summary>
        /// Обработка нажатия клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeViewItems_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteItem();
            }
        }

        /// <summary>
        /// Удалить узел
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDelete(object sender, EventArgs e)
        {
            DeleteItem();
        }

        /// <summary>
        /// Создать группу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemCreateGroup(object sender, EventArgs e)
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

        /// <summary>
        /// Создать новую кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemCreateButton(object sender, EventArgs e)
        {
            ControlButton control = new ControlButton(
                _treeModel.GetUniqueName("Button"));
            CreateItem(control);
        }

        /// <summary>
        /// Создать новый круговой элемент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemCreateCircle(object sender, EventArgs e)
        {
            ControlCircle control = new ControlCircle(
                _treeModel.GetUniqueName("Circle"));
            CreateItem(control);
        }

        /// <summary>
        /// Создать панель
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemCreatePanel(object sender, EventArgs e)
        {
            var control = new ControlPanel()
            {
                Name = _treeModel.GetUniqueName("Panel")
            };
            CreateItem(control);
        }

        /// <summary>
        /// Создать изображение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemCreateImage(object sender, EventArgs e)
        {
            ControlImage control = new ControlImage()
            {
                Name = _treeModel.GetUniqueName("Image")
            };
            CreateItem(control);
        }

        /// <summary>
        /// Создать контрол TapScene
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemCreateTapScene(object sender, EventArgs e)
        {
            ControlTapScene control = new ControlTapScene()
            {
                Name = _treeModel.GetUniqueName("Tap")
            };
            CreateItem(control);
        }

        private void MenuItemCreateText(object sender, EventArgs e)
        {
            ControlText control = new ControlText()
            {
                Name = _treeModel.GetUniqueName("Text")
            };
            CreateItem(control);
        }
        
        /// <summary>
        /// Добавить ссылку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemCreateXRef(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл контролов (*.controls)|*.controls|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть файл контролов";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            try
            {
                TreeNodeXref xRefTreeViewNode =
                    XRefControlsWorker.CreateXreference(
                    dialog.FileName,
                    _transManager,
                    CreateTreeNodeHandler);
                _treeModel.Nodes.Add(xRefTreeViewNode);
                UpdateTreeView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// Создать элемент поведения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemCreateControlBehavior(object sender, EventArgs e)
        {
            ControlBehavior control = new ControlBehavior();
            control.Name = _treeModel.GetUniqueName("Behavior");
            CreateItem(control);
        }

        /// <summary>
        /// Обработчик закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorControlsInspector_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

    }
}
