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

namespace UnitEngineUI.Behavior
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
                return _mainEditor.ContainerBehavior;
            }
        }

        /// <summary>
        /// Класс для работы с деревом объектов
        /// </summary>
        TreeViewWorker _treeViewWorker;

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

        public EditorBehaviorInspector(
            EditorBehavior editor,
            EditorBehaviorItemProperty itemProperty,
            List<string> controlsNames)
        {
            InitializeComponent();

            _mainEditor = editor;
            _propertyWindow = itemProperty;
            _controlsNames = controlsNames;

            _treeViewWorker = new TreeViewWorker(_treeViewItems);
            _treeViewWorker.EnableFilter(_textBoxFilter);
            _treeViewWorker.EnableTreeNodeGroupEdit();
            _treeViewWorker.EnableDragDrop(CanDragItem, CanDropItem, DropToChilds);
            _treeViewWorker.EnableCopyPaste(PasteTreeNode);

            DataGridViewExtension dataGridExecuteExt = new DataGridViewExtension(_dataGridViewExecute);
            dataGridExecuteExt.EnableDragDropReorder = true;

            DataGridViewExtension dataGridClauseExt = new DataGridViewExtension(_dataGridViewClauses);
            dataGridClauseExt.EnableDragDropReorder = true;

            UpdateItemDesc();
        }

        public void Clear()
        {
            _treeViewItems.Nodes.Clear();
        }

        #region Дерево объектов

        /// <summary>
        /// Установить дерево
        /// </summary>
        /// <param name="treeView"></param>
        public void SetTreeView(UnitBehavior behavior)
        {
            if (behavior.TreeView != null)
                _treeViewItems.Nodes.AddRange(TreeViewWorker.GetTreeNodes(behavior.TreeView).ToArray());
            UpdateTreeView();
            UpdateItemDesc();

            var menuExecutes = new ContextMenuExecutes(behavior);
            menuExecutes.NewBehaviorExecute += NewBehaviorExecute;
            _dataGridViewExecute.ContextMenuStrip = menuExecutes;
        }

        /// <summary>
        /// Получить данные дерева
        /// </summary>
        /// <returns></returns>
        public ContainerTreeView GetContainerTreeView()
        {
            if (!string.IsNullOrEmpty(_textBoxFilter.Text))
                _textBoxFilter.Text = string.Empty;

            ContainerTreeView containerDsg = new ContainerTreeView(_treeViewItems);
            return containerDsg;
        }

        /// <summary>
        /// Получить полный список анимаций
        /// </summary>
        /// <returns></returns>
        private List<UnitAction> GetTreeViewTags(TreeNodeCollection nodes)
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
        /// Получить объект по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UnitAction GetItem(string name)
        {
            var node = GetItem(name, _treeViewItems.Nodes);
            if (node == null) return null;
            return node.Tag as UnitAction;
        }

        /// <summary>
        /// Найти узел по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TreeNode GetItem(string name, TreeNodeCollection nodes, List<TreeNode> ignoreNodes = null)
        {
            foreach (TreeNode node in nodes)
            {
                UnitAction action = node.Tag as UnitAction;
                if (action != null)
                {                    
                    if (string.Compare(action.Name, name, true) == 0)
                    {
                        if (ignoreNodes == null || !ignoreNodes.Contains(node))
                            return node;
                    }
                }
                var targetNode = GetItem(name, node.Nodes, ignoreNodes);
                if (targetNode != null)
                    return targetNode;
            }
            return null;
        }

        /// <summary>
        /// Создать новый узел анимации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemNewAction_Click(object sender, EventArgs e)
        {
            _textBoxFilter.Text = string.Empty;
            UnitAction action = new UnitAction(_treeViewWorker.GetUniqueName("Action"));
            NewAction(action);
        }

        /// <summary>
        /// Создать блок действий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddBlockAction_Click(object sender, EventArgs e)
        {
            _textBoxFilter.Text = string.Empty;
            UnitAction action = new UnitBlockAction()
            {
                Name = _treeViewWorker.GetUniqueName("Block")
            };
            NewAction(action);
            // Обновление дерева
            UpdateTreeView();
        }

        /// <summary>
        /// Добавить дочерние действие в блок действий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddBlockActionChild_Click(object sender, EventArgs e)
        {
            _textBoxFilter.Text = string.Empty;
            UnitAction action = new UnitAction(_treeViewWorker.GetUniqueName("Action"));
            NewAction(action);
        }

        /// <summary>
        /// Создать новый узел 
        /// </summary>
        /// <param name="action"></param>
        private void NewAction(UnitAction action)
        {
            _textBoxFilter.Text = string.Empty;

            TreeNode newNode = new TreeNode();
            newNode.Tag = action;

            TreeNodeCollection target = _treeViewItems.Nodes;
            if (_treeViewItems.SelectedNode != null)
            {
                if (_treeViewItems.SelectedNode.Tag is UnitBlockAction)
                    target = _treeViewItems.SelectedNode.Nodes;
                else if (_treeViewItems.SelectedNode is TreeNodeGroup)
                    target = _treeViewItems.SelectedNode.Nodes;
                else if (_treeViewItems.SelectedNode.Parent is TreeNodeGroup)
                    target = _treeViewItems.SelectedNode.Parent.Nodes;
            }

            target.Add(newNode);
            _treeViewItems.SelectedNode = newNode;

            UpdateTreeView();

            if (ItemCreated != null) ItemCreated(action);
        }

        /// <summary>
        /// Добавить ссылку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddXRef_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл поведения (*.behavior)|*.behavior|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть файл поведения";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            try
            {
                _treeViewItems.Nodes.Add(
                    XrefBehaviorWorker.NewXreference(
                    _mainEditor.ContainerBehavior,
                    dialog.FileName));
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
        private void ItemChanged(object sender, EventArgs e)
        {
            //_buttonApplyItem.Enabled = true;
            ApplyChangeItem();
        }

        /// <summary>
        /// Может ли объект перетаскиваться
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool CanDragItem(TreeNode node)
        {
            if (node.Tag is UnitAction)
            {
                UnitAction action = node.Tag as UnitAction;
                // Объекты внешней ссылки переносить нельзя
                if (!string.IsNullOrEmpty(action.LinkPath))
                    return false;
            }
            return (node.Tag is UnitAction || node is TreeNodeGroup);
        }

        /// <summary>
        /// Может ли объект перетаскиваться
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool DropToChilds(TreeNode source, TreeNode target)
        {
            if (source is TreeNodeGroup) return false;
            if (source.Tag is UnitAction && target.Tag is UnitAction) return false;
            return (source.Tag is UnitAction && target is TreeNodeGroup);
        }

        /// <summary>
        /// Может ли объект перенистись
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool CanDropItem(TreeNode source, TreeNode target)
        {
            return true;
        }

        /// <summary>
        /// Обновление дерева анимации
        /// </summary>
        private void UpdateTreeView()
        {
            UpdateTreeViewNodes(_treeViewItems.Nodes);
        }

        private void UpdateTreeViewNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.ImageKey = GetImageKey(node);
                node.SelectedImageKey = GetImageKey(node);
                UpdateContextMenuItem(node);
                UpdateTreeViewNodes(node.Nodes);
                UnitAction action = node.Tag as UnitAction;
                if (action == null) continue;
                node.Text = string.Format("{0} [{1}]", action.Name, action.Priority);
            }
        }

        /// <summary>
        /// Обновление меню для обектов дерева
        /// </summary>
        /// <param name="node"></param>
        private void UpdateContextMenuItem(TreeNode node)
        {
            if (node.Tag is UnitBlockAction)
            {
                node.ContextMenuStrip = _contextMenuStripBlockAction;
            }
        }

        private string GetImageKey(TreeNode node)
        {
            if (node is TreeNodeGroup)
            {
                if (node.Tag is XRefData)
                    return "xref.ico";
                return "folder.ico";
            }
            if (node.Tag is UnitBlockAction) return "block.ico";
            if (node.Tag is UnitAction) return "item.ico";
            return string.Empty;
        }

        /// <summary>
        /// Удалить выбранный узел
        /// </summary>
        private void DeleteItem()
        {
            if (_treeViewItems.SelectedNode == null) return;

            _textBoxFilter.Text = string.Empty;

            XRefData xRefData = _treeViewItems.SelectedNode.Tag as XRefData;
            // Если удаляется внешняя ссылка, то ...
            if (xRefData != null)
            {
                _mainEditor.ContainerBehavior.XRefPaths.Remove(xRefData.FileName);
            }

            object oItem = _treeViewItems.SelectedNode.Tag;
            _treeViewItems.SelectedNode.Remove();

            if (ItemDeleted != null) ItemDeleted(oItem);
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

        private void TreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            var tag = (_treeViewItems.SelectedNode != null) ?
                _treeViewItems.SelectedNode.Tag : null;
            if (SelectionChanged != null) SelectionChanged(tag);
        }

        private void TreeViewItems_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteItem();
            }
        }

        /// <summary>
        /// Событие выбора узла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeViewItems_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var tag = (_treeViewItems.SelectedNode != null) ?
                _treeViewItems.SelectedNode.Tag : null;
            if (SelectionChanged != null) SelectionChanged(tag);
            UpdateItemDesc();
        }

        /// <summary>
        /// Создать группу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateGroup_Click(object sender, EventArgs e)
        {
            NewGroup();
        }

        /// <summary>
        /// Создать группу
        /// </summary>
        private void NewGroup()
        {
            _textBoxFilter.Text = string.Empty;

            CommonUI.TreeNodeGroup newNode = new CommonUI.TreeNodeGroup();
            newNode.Text = _treeViewWorker.GetUniqueName("Группа");
            _treeViewItems.Nodes.Add(newNode);
            _treeViewItems.SelectedNode = newNode;

            UpdateTreeView();

            newNode.BeginEdit();
        }

        #endregion

        /// <summary>
        /// Принять изменения для узла анимации
        /// </summary>
        private void ApplyChangeItem()
        {
            if (_treeViewItems.SelectedNode == null || _editItem == null)
                return;

            UnitAction action = _treeViewItems.SelectedNode.Tag as UnitAction;
            if (action == null) return;
            if (_editItem == null) return;

            _editItem.Name = _textBoxPropName.Text;
            _editItem.Priority = Convert.ToInt32(_nmrPropPriority.Value);
            _editItem.NoChangeCurrentAction = _checkBoxNoChangeCurrentAction.Checked;

            _editItem.Clause.Events = new List<UnitEventBase>();
            foreach (DataGridViewRow row in _dataGridViewClauses.Rows)
            {
                UnitEventBase eventBase = row.Tag as UnitEventBase;
                if (eventBase == null) continue;
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
            var dublicateNode = GetItem(_textBoxPropName.Text, _treeViewItems.Nodes,
                new List<TreeNode>() { _treeViewItems.SelectedNode });
            if (dublicateNode != null || string.IsNullOrEmpty(_textBoxPropName.Text))
            {
                _textBoxPropName.BackColor = Color.Red;
            }

            _treeViewItems.SelectedNode.Tag = _editItem;
            _treeViewItems.SelectedNode.Text = string.Format("{0} [{1}]", _editItem.Name, _editItem.Priority);

            _dataGridViewClauses.Refresh();
            _dataGridViewExecute.Refresh();

            if (ItemDataChanged != null) ItemDataChanged(_editItem);

            UpdateExtraStylesItemDesc(_editItem);            
        }

        /// <summary>
        /// Анимация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelAnimation_Click(object sender, EventArgs e)
        {
            if (_editItem == null) return;

            ControlAnimation control = new ControlAnimation();
            control.EditItem = _editItem.Animation;
            control.Changed += Changed;
            FormWorker.AddControl(_propertyWindow.TabPages[0], control);

            if (SelectionChanged != null) SelectionChanged(_editItem);

            ItemChanged(sender, e);

        }

        /// <summary>
        /// Локальные параметры условия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelParams_Click(object sender, EventArgs e)
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
        private void LabelGlobalParams_Click(object sender, EventArgs e)
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
        private void LabelBreak_Click(object sender, EventArgs e)
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
        private List<UnitEventBase> ReadClausesEvents()
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
        private void UpdateItemDesc()
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

            if (_treeViewItems.SelectedNode == null) return;
            UnitAction action = _treeViewItems.SelectedNode.Tag as UnitAction;
            if (action == null) return;


            // Устанавливаем значения в элементы формы
            //
            _textBoxPropName.Text = action.Name;
            _nmrPropPriority.Value = (decimal)action.Priority;
            _checkBoxNoChangeCurrentAction.Checked = action.NoChangeCurrentAction;
            FillClauses(action);
            FillExecutes(action);

            UpdateExtraStylesItemDesc(action);

            if (SelectionChanged != null) SelectionChanged(action);

            _panelDesc.Enabled = true;

            _editItem = action.DeepClone();
        }

        /// <summary>
        /// Обновить стили свойств
        /// </summary>
        private void UpdateExtraStylesItemDesc(UnitAction action)
        {
            if (action == null) return;
            FontStyle fontStyleAnimation = (action.Animation.Enabled) ? FontStyle.Bold : FontStyle.Regular;
            _labelAnimation.Font = new Font(_labelParams.Font, fontStyleAnimation | FontStyle.Underline);
            FontStyle fontStyleParams = (action.Clause.Parameters.Count > 0) ? FontStyle.Bold : FontStyle.Regular;
            _labelParams.Font = new Font(_labelParams.Font, fontStyleParams | FontStyle.Underline);
            FontStyle globalFontStyleParams = (action.Clause.GlobalParameters.Count > 0) ? FontStyle.Bold : FontStyle.Regular;
            _labelGlobalParams.Font = new Font(_labelGlobalParams.Font, globalFontStyleParams | FontStyle.Underline);
            FontStyle breakFontStyleParams = (!action.Break.IsEmpty()) ? FontStyle.Bold : FontStyle.Regular;
            _labelBreak.Font = new Font(_labelBreak.Font, breakFontStyleParams | FontStyle.Underline);
        }

        private void DataGridViewClauses_MouseClick(object sender, MouseEventArgs e)
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

        private void DataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            ItemChanged(null, null);
        }

        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ItemChanged(null, null);
        }

        private void DataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView datagrid = sender as DataGridView;
            FormWorker.SetNullImageDataGrid(datagrid);
        }
        
        /// <summary>
        /// Принять изменения для узла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonApplyItem_Click(object sender, EventArgs e)
        {
            ApplyChangeItem();
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

        #region Действия

        private void NewBehaviorExecute(ExecuteBase execute)
        {
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
        private void ShowExecuteProperties(ExecuteBase execute)
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
                var control = new ControlUnitCreate(_mainEditor.ContainerBehavior.ChildsBehaviorsPaths);
                control.EditItem = execute as ExecuteCreateUnit;
                control.Changed += Changed;
                mainControl = control;
            }
            else if (execute is ExecuteDeleteUnit)
            {
                var control = new ControlUnitDelete(_mainEditor.ContainerBehavior.ChildsBehaviorsPaths);
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
                var control = new ControlExecuteGroup(this, _mainEditor.ContainerBehavior);
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

        private void DataGridViewExecute_SelectionChanged(object sender, EventArgs e)
        {
            if (_dataGridViewExecute.SelectedRows.Count == 0) return;
            var row = _dataGridViewExecute.SelectedRows[0];
            ShowExecuteProperties(row.Tag as ExecuteBase);
        }

        private void DataGridViewExecute_MouseClick(object sender, MouseEventArgs e)
        {
            if (_dataGridViewExecute.SelectedRows.Count == 0) return;
            var row = _dataGridViewExecute.SelectedRows[0];
            ShowExecuteProperties(row.Tag as ExecuteBase);
        }

        #endregion

        #region Свойства объекта


        private void FillExecutes(UnitAction behavior)
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
        private void FillClauses(UnitAction behavior)
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
        private void ParamtersAdd_Click(object sender, EventArgs e)
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
        private void EventControlAdd_Click(object sender, EventArgs e)
        {
            // По умолчанию создаём кнопку
            //
            UnitEventControlButton unitEvent = new UnitEventControlButton();
            unitEvent.Name = "ControlEvent";
            AddNewClauseEvent(unitEvent);
        }

        /// <summary>
        /// Добавить условие расстояние
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventDistanceAdd_Click(object sender, EventArgs e)
        {
            UnitEventDistance unitEvent = new UnitEventDistance();
            unitEvent.Name = "Distance";
            AddNewClauseEvent(unitEvent);
        }

        /// <summary>
        /// Позиция внутри площади
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventPositionInsideArea_Click(object sender, EventArgs e)
        {
            var unitEvent = new UnitEventPositionInsideArea();
            unitEvent.Name = "PosInsideArea";
            AddNewClauseEvent(unitEvent);
        }

        /// <summary>
        /// Условие по выборки объектов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventSelection_Click(object sender, EventArgs e)
        {
            var unitEvent = new UnitEventSelection();
            unitEvent.Name = "Selection";
            AddNewClauseEvent(unitEvent);
        }

        /// <summary>
        /// Оператор в условиях
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventOperator_Click(object sender, EventArgs e)
        {
            var unitEvent = new UnitEventOperator();
            unitEvent.Name = "Operator";
            AddNewClauseEvent(unitEvent);
        }

        /// <summary>
        /// Дочерний юнит в условиях
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventChildUnit_Click(object sender, EventArgs e)
        {
            var unitEvent = new UnitEventChildUnit();
            unitEvent.Name = "ChildUnit";
            AddNewClauseEvent(unitEvent);
        }

        /// <summary>
        /// Добавить условие по времени
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventTime_Click(object sender, EventArgs e)
        {
            var unitEvent = new UnitEventTimer();
            unitEvent.Name = "Time";
            AddNewClauseEvent(unitEvent);
        }

        /// <summary>
        /// Добавить условие скрипт (Result = true/false)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventScript_Click(object sender, EventArgs e)
        {
            UnitEventScript unitEvent = new UnitEventScript();
            unitEvent.Name = "Script";
            AddNewClauseEvent(unitEvent);
        }

        /// <summary>
        /// Добавить условие
        /// </summary>
        /// <param name="unitEvent"></param>
        private void AddNewClauseEvent(UnitEventBase unitEvent)
        {
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
        private void ShowClauseProperties(object oClause)
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


    }
}
