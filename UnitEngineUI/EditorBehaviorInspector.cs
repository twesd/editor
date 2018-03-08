using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Serializable;
using UnitEngine;
using UnitEngine.Behavior;
using CommonUI;
using System.Xml.Serialization;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using UnitEngine.Events;
using UnitEngineUI.Events;
using Common;
using TransactionCore;
using Aga.Controls.Tree;
using System.Collections.ObjectModel;
using UnitEngineUI.Properties;
using System.Reflection;
using UnitEngineUI.Controls;

namespace UnitEngineUI
{
    public partial class EditorBehaviorInspector : DockContent
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
        /// Изменение объекта
        /// </summary>
        public event ItemChangedEventHadler ItemDataChanged;

        public UnitAction EditItem
        {
            get
            {
                return _editItem;
            }
        }

        public UnitBehavior ContainerBehavior
        {
            get
            {
                return _mainEditor.UnitBehavior;
            }
        }

        /// <summary>
        /// Класс для работы с деревом объектов
        /// </summary>
        TreeViewAdvWorker _treeViewWorker;

        /// <summary>
        /// Редактируемое действие
        /// </summary>
        UnitAction _editItem;

        /// <summary>
        /// Редактор стадии
        /// </summary>
        EditorBehavior _mainEditor;

        /// <summary>
        /// Свойства узла
        /// </summary>
        EditorBehaviorItemProperty _propertyWindow;

        /// <summary>
        /// Список имён созданных контролов
        /// </summary>
        List<string> _controlsNames;

        /// <summary>
        /// Модель данных дерева
        /// </summary>
        TreeModel _treeModel;

        /// <summary>
        /// Менеджер для undo / redo
        /// </summary>
        TransactionManager _transManager;

        public EditorBehaviorInspector(
            EditorBehavior editor,
            TransactionManager transManager,
            EditorBehaviorItemProperty itemProperty,
            List<string> controlsNames)
        {
            InitializeComponent();

            _mainEditor = editor;
            _propertyWindow = itemProperty;
            _controlsNames = controlsNames;
            _transManager = transManager;

            _treeModel = new TreeModel(transManager);
            _treeView.Model = _treeModel;

            _treeViewWorker = new TreeViewAdvWorker(_treeView);
            //_treeViewWorker.EnableFilter(_textBoxFilter);
            _treeViewWorker.EnableDragDrop(CanDragItem, CanDropItem);
            _treeViewWorker.EnableCopyPaste(CanPaste);

            DataGridViewExtension dataGridExecuteExt = new DataGridViewExtension(_dataGridViewExecute);
            dataGridExecuteExt.EnableDragDropReorder = true;
            dataGridExecuteExt.PasteItem += BehaviorExecutePasted;

            DataGridViewExtension dataGridClauseExt = new DataGridViewExtension(_dataGridViewClauses);
            dataGridClauseExt.EnableDragDropReorder = true;
            dataGridExecuteExt.PasteItem += ClauseEventPasted;

            UpdateItemDesc();
        }

        public void Clear()
        {
            _treeModel.Nodes.Clear();
        }

        /// <summary>
        /// Установить дерево
        /// </summary>
        /// <param name="treeView"></param>
        public void SetContainerTreeView(UnitBehavior behavior)
        {
            _treeView.BeginUpdate();

            List<TreeNodeBase> nodes =
                TreeViewConvertor.ConvertToNodes(behavior.TreeView.Nodes, _transManager, CreateTreeNodeHandler);
            _treeModel.Nodes.Clear();
            foreach (var node in nodes)
            {
                _treeModel.Nodes.Add(node);
            }

            UpdateTreeView();

            _treeView.EndUpdate();

            var menuExecutes = new ContextMenuExecutes(behavior);
            menuExecutes.NewBehaviorExecute += CreateBehaviorExecute;
            _dataGridViewExecute.ContextMenuStrip = menuExecutes;
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
        /// Обновление дерева анимации
        /// </summary>
        public void UpdateTreeView()
        {
            _treeView.FullUpdate();
            UpdateItemDesc();
        }

        /// <summary>
        /// Получить контрол для основных свойств исполнения
        /// </summary>
        /// <param name="execute"></param>
        /// <returns></returns>
        public Control GetControlForExecute(ExecuteBase execute)
        {
            Control mainControl = null;
            if (execute is ExecuteParameter)
            {
                var control = new ControlExecuteParams();
                control.EditItem = execute as ExecuteParameter;
                control.Changed += Changed;
                mainControl = control;
            }
            else if (execute is ExecuteCreateUnit)
            {
                var control = new ControlUnitCreate(_mainEditor.UnitBehavior.ChildsBehaviorsPaths);
                control.EditItem = execute as ExecuteCreateUnit;
                control.Changed += Changed;
                mainControl = control;
            }
            else if (execute is ExecuteDeleteUnit)
            {
                var control = new ControlUnitDelete(_mainEditor.UnitBehavior.ChildsBehaviorsPaths);
                control.EditItem = execute as ExecuteDeleteUnit;
                control.Changed += Changed;
                mainControl = control;
            }
            else if (execute is ExecuteTransform)
            {
                var control = new ControlTransforms();
                control.EditItem = execute as ExecuteTransform;
                control.Changed += Changed;
                mainControl = control;
            }
            else if (execute is ExecuteTextures)
            {
                var control = new ControlTextures();
                control.EditItem = execute as ExecuteTextures;
                control.Changed += Changed;
                mainControl = control;
            }
            else if (execute is ExecuteColor)
            {
                var control = new ControlExecuteColor();
                control.EditItem = execute as ExecuteColor;
                control.Changed += Changed;
                mainControl = control;
            }
            else if (execute is ExecuteMaterial)
            {
                var control = new ControlMaterial();
                control.EditItem = execute as ExecuteMaterial;
                control.Changed += Changed;
                mainControl = control;
            }
            else if (execute is ExecuteGroup)
            {
                var control = new ControlExecuteGroup(this, _mainEditor.UnitBehavior);
                control.EditItem = execute as ExecuteGroup;
                control.Changed += Changed;
                mainControl = control;
            }
            else if (execute is ExecuteSetData)
            {
                var control = new ControlExecuteSetData(_mainEditor);
                control.EditItem = execute as ExecuteSetData;
                control.Changed += Changed;
                mainControl = control;
            }
            else if (execute is ExecuteScript)
            {
                var control = new ControlExecuteScript();
                control.EditItem = execute as ExecuteScript;
                control.Changed += Changed;
                mainControl = control;
            }
            else if (execute is ExecuteParticleEmitter)
            {
                var control = new ControlStdProperties();
                control.EditItem = (execute as ExecuteParticleEmitter).Emitter;
                control.Changed += Changed;
                mainControl = control;
            }
            else if (execute is ExecuteParticleAffector)
            {
                var control = new ControlParticleAffector();
                control.EditItem = execute as ExecuteParticleAffector;
                control.Changed += Changed;
                mainControl = control;
            }
            else
            {
                var control = new ControlStdProperties();
                control.EditItem = execute;
                control.Changed += Changed;
                mainControl = control;
            }

            return mainControl;
        }

        /// <summary>
        /// Получить полный список анимаций
        /// </summary>
        /// <returns></returns>
        List<UnitAction> GetTreeViewTags(TreeNodeCollection nodes)
        {
            List<UnitAction> actions = new List<UnitAction>();
            foreach (TreeNode node in nodes)
            {
                List<UnitAction> childItems = GetTreeViewTags(node.Nodes);
                actions.AddRange(childItems);

                UnitAction behavior = node.Tag as UnitAction;
                if (behavior == null) continue;
                actions.Add(behavior);
            }
            return actions;
        }

        /// <summary>
        /// Создать новый узел анимации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemNewAction_Click(object sender, EventArgs e)
        {
            _textBoxFilter.Text = string.Empty;
            UnitAction action = new UnitAction(_treeModel.GetUniqueName("Action"));
            CreateItem(action);
        }

        /// <summary>
        /// Создать блок действий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemAddBlockAction_Click(object sender, EventArgs e)
        {
            _textBoxFilter.Text = string.Empty;
            UnitAction action = new UnitBlockAction()
            {
                Name = _treeModel.GetUniqueName("Block")
            };
            CreateItem(action);
            // Обновление дерева
            UpdateTreeView();
        }

        /// <summary>
        /// Добавить дочерние действие в блок действий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemAddBlockActionChild_Click(object sender, EventArgs e)
        {
            _textBoxFilter.Text = string.Empty;
            UnitAction action = new UnitAction(_treeModel.GetUniqueName("Action"));
            CreateItem(action);
        }

        /// <summary>
        /// Создать новый узел 
        /// </summary>
        /// <param name="action"></param>
        void CreateItem(UnitAction action)
        {
            _textBoxFilter.Text = string.Empty;

            TreeNodeAdv target = _treeView.Root;
            bool addInside = false;
            if (_treeView.SelectedNode != null)
            {
                TreeNodeBase dataNode = _treeView.SelectedNode.Tag as TreeNodeBase;
                if (dataNode is TreeNodeGroup ||
                    dataNode.Tag is UnitBlockAction)
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
                xrefRootNode = XRefBehaviorWorker.GetXRefNodeRoot(dataTarget);
            }
            // Если объект необходимо добавить во внешнею ссылку, то
            if (xrefRootNode != null)
            {
                // Заносим операцию добавления

                XRefOperationAdd addOp = new XRefOperationAdd(action);
                xrefRootNode.XRefData.Operations.Add(addOp);
                XRefBehaviorWorker.UpdateXref(
                    xrefRootNode,
                    _transManager,
                    CreateTreeNodeHandler);
            }
            else
            {
                var newNode = new TreeNodeAction(action, _transManager);

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

            if (ItemCreated != null) ItemCreated(action);
        }

        /// <summary>
        /// Добавить ссылку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemAddXRef_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл поведения (*.behavior)|*.behavior|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть файл поведения";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            try
            {
                TreeNodeXref xRefTreeViewNode = XRefBehaviorWorker.CreateXreference(
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
        /// Узел изменён
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ItemChanged(object sender, EventArgs e)
        {
            ApplyItemChanges();
        }

        /// <summary>
        /// Может ли объект перетаскиваться
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool CanDragItem(TreeNodeBase node)
        {
            TreeNodeXref xrefRootNode = XRefBehaviorWorker.GetXRefNodeRoot(node);
            if (xrefRootNode != null)
            {
                return false;
            }

            return (node is TreeNodeAction || 
                node is TreeNodeGroup ||
                node is TreeNodeXref);
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

            TreeNodeXref xrefRootNode = XRefBehaviorWorker.GetXRefNodeRoot(target);
            if (xrefRootNode != null)
            {
                return false;
            }

            if (nodePos == NodePosition.Inside)
            {
                if (target is TreeNodeXref)
                {
                    return false;
                }

                return (target is TreeNodeGroup ||
                    target.Tag is UnitBlockAction);
            }

            return true;
        }

        bool CanPaste(TreeNodeBase source, TreeNodeBase target)
        {
            // Запрещаем копировать во внешнею ссылку
            //
            TreeNodeXref xrefRootNode = XRefBehaviorWorker.GetXRefNodeRoot(target);
            if (xrefRootNode != null || target is TreeNodeXref)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Обновление меню для обектов дерева
        /// </summary>
        /// <param name="node"></param>
        void UpdateContextMenuItem(TreeNode node)
        {
            if (node.Tag is UnitBlockAction)
            {
                node.ContextMenuStrip = _contextMenuStripBlockAction;
            }
        }

        /// <summary>
        /// Удалить выбранный узел
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

            TreeNodeXref treeNodeXRef = XRefBehaviorWorker.GetXRefNodeRoot(dataNode);
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

                XRefBehaviorWorker.UpdateXref(
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

        /// <summary>
        /// Удалить узел анимации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Delete_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        void TreeViewItems_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteItem();
            }
        }

        void TreeView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateItemDesc();
            NotifySelectionChanged();
        }

        /// <summary>
        /// Создать группу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CreateGroup_Click(object sender, EventArgs e)
        {
            CreateGroup();
        }

        /// <summary>
        /// Создать группу
        /// </summary>
        void CreateGroup()
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
        /// Принять изменения для узла анимации
        /// </summary>
        void ApplyItemChanges()
        {
            try
            {
                if (_editItem == null)
                {
                    return;
                }

                using (Transaction trans = _transManager.StartTransaction())
                {
                    trans.AddObject(_editItem);

                    _editItem.Name = _textBoxPropName.Text;
                    _editItem.Priority = Convert.ToInt32(_nmrPropPriority.Value);
                    _editItem.NoChangeCurrentAction = _checkBoxNoChangeCurrentAction.Checked;

                    _editItem.Clause.Events = new List<UnitEventBase>();
                    foreach (DataGridViewRow row in _dataGridViewClauses.Rows)
                    {
                        UnitEventBase eventBase = row.Tag as UnitEventBase;
                        if (eventBase == null)
                        {
                            continue;
                        }
                        _editItem.Clause.Events.Add(eventBase);
                    }

                    _editItem.Executes = new List<ExecuteBase>();
                    foreach (DataGridViewRow row in _dataGridViewExecute.Rows)
                    {
                        var execute = row.Tag as ExecuteBase;
                        if (execute == null) continue;
                        _editItem.Executes.Add(execute);
                    }

                    _textBoxPropName.BackColor = Color.FromName("Window");

                    var dublicateNode = _treeModel.FindNode(_textBoxPropName.Text, _treeModel.Root.Nodes);
                    if (dublicateNode != null || string.IsNullOrEmpty(_textBoxPropName.Text))
                    {
                        _textBoxPropName.BackColor = Color.Red;
                    }

                    _dataGridViewClauses.Refresh();
                    _dataGridViewExecute.Refresh();

                    if (ItemDataChanged != null)
                    {
                        ItemDataChanged(_editItem);
                    }
                    UpdateExtraStylesItemDesc(_editItem);

                    // Обработка данных внешней ссылки
                    //
                    TreeNodeAdv nodeAdv = _treeViewWorker.FindTreeNode(_editItem, _treeView.Root.Children);
                    if (nodeAdv == null)
                    {
                        return;
                    }
                    var nodeData = nodeAdv.Tag as TreeNodeBase;
                    TreeNodeXref xrefRootNode = XRefBehaviorWorker.GetXRefNodeRoot(nodeData);
                    // Если объект входит во внешнею ссылку, то
                    if (xrefRootNode != null)
                    {
                        // Если это элемент является оригинальной частью ссылки, 
                        // т.е. не добавлен через операцию добавления
                        if (XRefBehaviorWorker.IsOrigOwnControl(xrefRootNode.XRefData, _editItem))
                        {
                            foreach (PropertyInfo destProperty in _editItem.GetType().GetProperties())
                            {
                                XRefOperationChange opChange = new XRefOperationChange(
                                   _editItem.Id,
                                   destProperty.Name,
                                   destProperty.GetValue(_editItem, new object[] { }));
                                xrefRootNode.XRefData.Operations.Add(opChange);
                            }
                            xrefRootNode.XRefData.RemoveDublicateOperations();

                            if (nodeData is TreeNodeAction)
                            {
                                // Устанавливаем доп. иконку для отображения, что объект изменён
                                //
                                var controlTreeViewNode = nodeData as TreeNodeAction;
                                controlTreeViewNode.IconOverlay = Resources.IconOverlayChange.ToBitmap();
                            }
                        }
                    }

                    trans.Commit();
                }

                _treeView.FullUpdate();
            }
            catch(Exception ex)
            {
                Report.Error(ex);
            }
        }

        /// <summary>
        /// Анимация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LabelAnimation_Click(object sender, EventArgs e)
        {
            if (_editItem == null)
            {
                return;
            }

            ControlAnimation control = new ControlAnimation(_mainEditor.UnitBehavior.Animations);
            control.EditItem = _editItem;
            control.Changed += Changed;
            FormWorker.AddControl(_propertyWindow.TabPages[0], control);

            if (SelectionChanged != null)
            {
                SelectionChanged(_editItem);
            }

            ItemChanged(sender, e);
        }

        /// <summary>
        /// Локальные параметры условия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LabelParams_Click(object sender, EventArgs e)
        {
            _propertyWindow.Clear();
            ControlClauseParams control = new ControlClauseParams();
            control.ShowGlobalParams = false;
            control.EditItem = _editItem.Clause;
            control.Changed += Changed;
            FormWorker.AddControl(_propertyWindow.TabPages[0], control);
        }

        /// <summary>
        /// Глобальные параметры условия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LabelGlobalParams_Click(object sender, EventArgs e)
        {
            ControlClauseParams control = new ControlClauseParams();
            control.ShowGlobalParams = true;
            control.EditItem = _editItem.Clause;
            control.Changed += Changed;
            FormWorker.AddControl(_propertyWindow.TabPages[0], control);
        }

        /// <summary>
        /// Условия отмены поведения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LabelBreak_Click(object sender, EventArgs e)
        {
            ControlClauseBreak control = new ControlClauseBreak();
            control.EditItem = _editItem.Break;
            control.Changed += Changed;
            FormWorker.AddControl(_propertyWindow.TabPages[0], control);
        }

        /// <summary>
        /// Прочитать события условия из формы свойств
        /// </summary>
        /// <returns></returns>
        List<UnitEventBase> ReadClausesEvents()
        {
            List<UnitEventBase> events = new List<UnitEventBase>();
            foreach (DataGridViewRow row in _dataGridViewClauses.Rows)
            {
                if (row.Cells[1].Value == null) continue;
                UnitEventBase eventBase = row.Tag as UnitEventBase;
                if (eventBase == null) continue;
                eventBase.Name = row.Cells[1].Value.ToString();
                events.Add(eventBase);
            }
            return events;
        }

        /// <summary>
        /// Обновление описания выбранного элемента
        /// </summary>
        void UpdateItemDesc()
        {
            _editItem = null;

            // Очищаем описание
            //
            _panelDesc.Enabled = false;
            _textBoxPropName.Text = string.Empty;
            _nmrPropPriority.Value = 0;
            FormWorker.ClearDataGrid(_dataGridViewClauses);
            FormWorker.ClearDataGrid(_dataGridViewExecute);

            _propertyWindow.Clear();

            if (_treeView.SelectedNode == null)
            {
                return;
            }

            var dataNode = _treeView.SelectedNode.Tag as TreeNodeBase;
            if (dataNode == null)
            {
                return;
            }
            UnitAction action = dataNode.Tag as UnitAction;
            if (action == null)
            {
                return;
            }

            // Устанавливаем значения в элементы формы
            //
            _textBoxPropName.Text = action.Name;
            _nmrPropPriority.Value = (decimal)action.Priority;
            _checkBoxNoChangeCurrentAction.Checked = action.NoChangeCurrentAction;
            FillClauses(action);
            FillExecutes(action);

            UpdateExtraStylesItemDesc(action);

            _panelDesc.Enabled = true;
            _editItem = action;
        }

        /// <summary>
        /// Обновить стили свойств
        /// </summary>
        void UpdateExtraStylesItemDesc(UnitAction action)
        {
            if (action == null)
            {
                return;
            }
            
            FontStyle fontStyleAnimation = (!string.IsNullOrEmpty(action.AnimationId)) ? FontStyle.Bold : FontStyle.Regular;
            _labelAnimation.Font = new Font(_labelParams.Font, fontStyleAnimation | FontStyle.Underline);

            FontStyle fontStyleParams = (action.Clause.Parameters.Count > 0) ? FontStyle.Bold : FontStyle.Regular;
            _labelParams.Font = new Font(_labelParams.Font, fontStyleParams | FontStyle.Underline);
            
            FontStyle globalFontStyleParams = (action.Clause.GlobalParameters.Count > 0) ? FontStyle.Bold : FontStyle.Regular;
            _labelGlobalParams.Font = new Font(_labelGlobalParams.Font, globalFontStyleParams | FontStyle.Underline);
            
            FontStyle breakFontStyleParams = (!action.Break.IsEmpty()) ? FontStyle.Bold : FontStyle.Regular;
            _labelBreak.Font = new Font(_labelBreak.Font, breakFontStyleParams | FontStyle.Underline);
        }

        void DataGridViewClauses_MouseClick(object sender, MouseEventArgs e)
        {
            if (_dataGridViewClauses.SelectedRows.Count == 0) return;
            var row = _dataGridViewClauses.SelectedRows[0];
            ShowClauseProperties(row.Tag);
        }

        /// <summary>
        /// Условие изменилось
        /// </summary>
        /// <param name="oClause"></param>
        void ClausePropertyChanged(object oClause)
        {
            if (oClause == null) return;
            if (_dataGridViewClauses.SelectedCells.Count == 0)
                return;
            var row = _dataGridViewClauses.Rows[_dataGridViewClauses.SelectedCells[0].RowIndex];
            row.Tag = oClause;
            ItemChanged(null, null);
        }

        void DataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            ItemChanged(null, null);
        }

        void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ItemChanged(null, null);
        }

        void DataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView datagrid = sender as DataGridView;
            FormWorker.SetNullImageDataGrid(datagrid);
        }

        void Changed(object oEditObj)
        {
            ItemChanged(null, null);
        }

        void PasteTreeNode(TreeNode node)
        {
            if (node.Tag != null && ItemCreated != null)
            {
                ItemCreated(node.Tag);
            }
        }

        void BehaviorExecutePasted(object oItem)
        {
            ExecuteBase execute = oItem as ExecuteBase;
            CreateBehaviorExecute(execute);
        }

        void CreateBehaviorExecute(ExecuteBase execute)
        {
            if (execute == null)
            {
                return;
            }
            var row = _dataGridViewExecute.Rows[_dataGridViewExecute.Rows.Add()];
            FormWorker.SelectRow(row);
            row.Tag = execute;
            row.Cells[1].Value = execute;

            ItemChanged(null, null);
            ShowExecuteProperties(execute);
        }

        /// <summary>
        /// Отобразить свойства действия
        /// </summary>
        /// <param name="execute"></param>
        void ShowExecuteProperties(ExecuteBase execute)
        {
            _propertyWindow.Clear();

            if (execute == null) return;

            // Показываем условия
            //
            if (execute.Clause != null)
            {
                string tabName = "Условия";
                if (!execute.Clause.IsEmpty())
                    tabName += "*";
                var tabPagePropsClause = new TabPage(tabName);
                _propertyWindow.TabPages.Add(tabPagePropsClause);

                var control = new ControlClauseExecute();
                control.EditItem = execute as ExecuteBase;
                control.Changed += Changed;
                FormWorker.AddControl(tabPagePropsClause, control);
            }

            // Показываем основные свойства
            //
            FormWorker.AddControl(_propertyWindow.TabPages[0], GetControlForExecute(execute));
        }

        void DataGridViewExecute_SelectionChanged(object sender, EventArgs e)
        {
            if (_dataGridViewExecute.SelectedRows.Count == 0) return;
            var row = _dataGridViewExecute.SelectedRows[0];
            ShowExecuteProperties(row.Tag as ExecuteBase);
        }

        void DataGridViewExecute_MouseClick(object sender, MouseEventArgs e)
        {
            if (_dataGridViewExecute.SelectedRows.Count == 0) return;
            var row = _dataGridViewExecute.SelectedRows[0];
            ShowExecuteProperties(row.Tag as ExecuteBase);
        }

        #region Свойства объекта

        void FillExecutes(UnitAction behavior)
        {
            if (behavior.Executes.Count == 0) return;
            // Заполняем действия
            //
            foreach (ExecuteBase execute in behavior.Executes)
            {
                int rowIndex = _dataGridViewExecute.Rows.Add();
                var row = _dataGridViewExecute.Rows[rowIndex];
                row.Cells[0].Value = null;
                row.Cells[1].Value = execute;
                row.Tag = execute;
            }
        }

        /// <summary>
        /// Заполнить условия
        /// </summary>
        void FillClauses(UnitAction behavior)
        {
            if (behavior.Clause == null) return;
            // Заполняем события
            //
            foreach (UnitEventBase eventBase in behavior.Clause.Events)
            {
                int rowIndex = _dataGridViewClauses.Rows.Add();
                var row = _dataGridViewClauses.Rows[rowIndex];
                row.Cells[0].Value = null;
                row.Cells[1].Value = eventBase;
                row.Tag = eventBase;
            }
        }

        /// <summary>
        /// Добавить параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ParamtersAdd_Click(object sender, EventArgs e)
        {
            var dataGrid = _contextMenuParameters.SourceControl as DataGridView;
            int rowIndex = dataGrid.Rows.Add();
            var row = dataGrid.Rows[rowIndex];
            row.Cells[1].Value = "Unnamed";
            row.Cells[2].Value = "";
            ItemChanged(null, null);
            FormWorker.SelectRow(row);
        }

        /// <summary>
        /// Добавить условие Событие - Контрол
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EventControlAdd_Click(object sender, EventArgs e)
        {
            // По умолчанию создаём кнопку
            //
            UnitEventControlButton unitEvent = new UnitEventControlButton();
            unitEvent.Name = "ControlEvent";
            CreateClauseEvent(unitEvent);
        }

        /// <summary>
        /// Добавить условие расстояние
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EventDistanceAdd_Click(object sender, EventArgs e)
        {
            UnitEventDistance unitEvent = new UnitEventDistance();
            unitEvent.Name = "Distance";
            CreateClauseEvent(unitEvent);
        }

        /// <summary>
        /// Позиция внутри площади
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EventPositionInsideArea_Click(object sender, EventArgs e)
        {
            var unitEvent = new UnitEventPositionInsideArea();
            unitEvent.Name = "PosInsideArea";
            CreateClauseEvent(unitEvent);
        }

        /// <summary>
        /// Условие по выборки объектов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EventSelection_Click(object sender, EventArgs e)
        {
            var unitEvent = new UnitEventSelection();
            unitEvent.Name = "Selection";
            CreateClauseEvent(unitEvent);
        }

        /// <summary>
        /// Оператор в условиях
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EventOperator_Click(object sender, EventArgs e)
        {
            var unitEvent = new UnitEventOperator();
            unitEvent.Name = "Operator";
            CreateClauseEvent(unitEvent);
        }

        /// <summary>
        /// Дочерний юнит в условиях
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EventChildUnit_Click(object sender, EventArgs e)
        {
            var unitEvent = new UnitEventChildUnit();
            unitEvent.Name = "ChildUnit";
            CreateClauseEvent(unitEvent);
        }

        /// <summary>
        /// Добавить условие по времени
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EventTime_Click(object sender, EventArgs e)
        {
            var unitEvent = new UnitEventTimer();
            unitEvent.Name = "Time";
            CreateClauseEvent(unitEvent);
        }

        /// <summary>
        /// Добавить условие скрипт (Result = true/false)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EventScript_Click(object sender, EventArgs e)
        {
            UnitEventScript unitEvent = new UnitEventScript();
            unitEvent.Name = "Script";
            CreateClauseEvent(unitEvent);
        }

        /// <summary>
        /// Событие вставки условия
        /// </summary>
        /// <param name="oItem"></param>
        void ClauseEventPasted(object oItem)
        {
            CreateClauseEvent(oItem as UnitEventBase);
        }

        /// <summary>
        /// Добавить условие
        /// </summary>
        /// <param name="unitEvent"></param>
        void CreateClauseEvent(UnitEventBase unitEvent)
        {
            if (unitEvent == null)
            {
                return;
            }
            int rowIndex = _dataGridViewClauses.Rows.Add();
            var row = _dataGridViewClauses.Rows[rowIndex];
            FormWorker.SelectRow(row);
            row.Tag = unitEvent;
            row.Cells[1].Value = unitEvent;

            ItemChanged(null, null);
            ShowClauseProperties(unitEvent);
        }

        /// <summary>
        /// Показать свойства объекта условия
        /// </summary>
        void ShowClauseProperties(object oClause)
        {
            _propertyWindow.Clear();
            Control containerControl = _propertyWindow.TabPages[0];

            if (oClause == null) return;
            UnitEventBase eventBase = oClause as UnitEventBase;
            if (eventBase != null)
            {
                if (eventBase is UnitEventControlButton ||
                    eventBase is UnitEventControlTapScene)
                {
                    ControlEventControlBase controlEventControl = new ControlEventControlBase(_controlsNames);
                    controlEventControl.Changed += ClausePropertyChanged;
                    controlEventControl.EditItem = eventBase;
                    FormWorker.AddControl(containerControl, controlEventControl);
                }
                else
                {
                    ControlStdProperties controlEventControl = new ControlStdProperties();
                    controlEventControl.Changed += ClausePropertyChanged;
                    controlEventControl.EditItem = eventBase;
                    FormWorker.AddControl(containerControl, controlEventControl);

                }
            }
        }

        #endregion

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
            else if (sNode.Type == typeof(TreeNodeXref).ToString())
            {
                var treeNodeXref = new TreeNodeXref(sNode.Tag as XRefData);
                XRefBehaviorWorker.UpdateXref(
                    treeNodeXref,
                    _transManager,
                    CreateTreeNodeHandler);
                handleChilds = false;
                return treeNodeXref;
            }
            else
            {
                return new TreeNodeAction(sNode, _transManager);
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
    }
}
