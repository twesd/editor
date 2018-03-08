using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonUI;
using ControlEngine;
using Serializable;
using Aga.Controls.Tree;
using StageEngineUI.Properties;


namespace StageEngineUI
{
    public partial class ControlManager : Form
    {
        public ControlPackages Result { get; set; }

        /// <summary>
        /// Класс для работы с деревом объектов
        /// </summary>
        TreeViewAdvWorker _treeViewWorker;

        /// <summary>
        /// Пакет контролов
        /// </summary>
        ControlPackages _controlPackages;
        
        /// <summary>
        /// Модель данных дерева
        /// </summary>
        TreeModel _treeModel;

        public ControlManager(ControlPackages packages)
        {
            InitializeComponent();
            
            _treeModel = new TreeModel(null);
            _treeView.Model = _treeModel;

            _controlPackages = packages.DeepClone();

            FormKeysWorker keysWorker = new FormKeysWorker(this);
            keysWorker.EscEnterEvent(null);

            _treeViewWorker = new TreeViewAdvWorker(_treeView);
            //_treeViewWorker.EnableFilter(_textBoxFilter);
            _treeViewWorker.EnableDragDrop(CanDragItem, CanDropItem);
            _treeViewWorker.EnableCopyPaste();

            FillTreeNodes();
            UpdateTreeView();
        }

        private void FillTreeNodes()
        {
            _treeModel.Nodes.Clear();
            foreach (var pair in _controlPackages.Items)
            {
                var node = new TreeNodePackage(
                        System.IO.Path.GetFileNameWithoutExtension(pair.Key),
                        pair.Value, null);
                if (_controlPackages.DefaultPath == pair.Key)
                {
                    node.IconOverlay = Resources.IconOverlaySet.ToBitmap();
                }
                _treeModel.Nodes.Add(node);
            }        
        }

        /// <summary>
        /// Может ли объект перетаскиваться
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool CanDragItem(TreeNodeBase node)
        {
            return (node.Tag is ControlPackage || node is TreeNodeGroup);
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
            _treeView.FullUpdate();
            UpdateItemDesc();
        }
        
        /// <summary>
        /// Обновление описания выбранного элемента
        /// </summary>
        private void UpdateItemDesc()
        {
            FormWorker.SetEmptyValueForControl(_panelProps);
            if (_treeView.SelectedNode == null) return;
            ControlPackage controlPackage = _treeView.SelectedNode.Tag as ControlPackage;
            if (controlPackage == null) return;
            var pair = _controlPackages.Items.Where(x => x.Value == controlPackage).FirstOrDefault();
            _packageFilename.Text = pair.Key;
        }

        /// <summary>
        /// Добавить новый пакет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateItem(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл контролов (*.controls)|*.controls|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть файл контролов";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            if (!_controlPackages.Add(dialog.FileName))
            {
                FormWorker.ShowErrorBox("Элемент не добавлен");
                return;
            }
            var node = new TreeNodePackage(
                        System.IO.Path.GetFileNameWithoutExtension(dialog.FileName),
                        _controlPackages.Items[dialog.FileName], null);
            _treeModel.Root.Nodes.Add(node);
            UpdateTreeView();
            UpdateItemDesc();
            ItemChanged();
        }

        private void ItemChanged()
        {
            
        }

        private void DeleteItem(object sender, EventArgs e)
        {
            if (_treeView.SelectedNode == null)
            {
                return;
            }

            var dataNode = _treeView.SelectedNode.Tag as TreeNodeBase;
            if (dataNode == null)
            {
                return;
            }

            ControlPackage controlPackage = dataNode.Tag as ControlPackage;
            if (controlPackage == null)
            {
                return;
            }
            var pair = _controlPackages.Items.Where(x => x.Value == controlPackage).FirstOrDefault();
            _controlPackages.Remove(pair.Key);
            
            FillTreeNodes();
            UpdateTreeView();
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            Accept();
        }

        /// <summary>
        /// Принять результат
        /// </summary>
        private void Accept()
        {
            Result = _controlPackages;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonNewItem_Click(object sender, EventArgs e)
        {
            CreateItem(sender, e);
        }

        private void ButtonDeleteItem_Click(object sender, EventArgs e)
        {
            DeleteItem(sender, e);
        }

        private void MenuItemSetByDefault(object sender, EventArgs e)
        {
            if (_treeView.SelectedNode == null)
            {
                return;
            }
            var dataNode = _treeView.SelectedNode.Tag as TreeNodeBase;
            if (dataNode == null)
            {
                return;
            }

            ControlPackage controlPackage = dataNode.Tag as ControlPackage;
            if (controlPackage == null)
            {
                return;
            }
            var pair = _controlPackages.Items.Where(x => x.Value == controlPackage).FirstOrDefault();
            _controlPackages.DefaultPath = pair.Key;
            FillTreeNodes();
        }

        private void TreeViewItems_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteItem(sender, null);
            }
        }

        private void TreeView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateItemDesc();
        }

        private void ControlManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteItem(sender, e);
            }
        }
        
    }
}
