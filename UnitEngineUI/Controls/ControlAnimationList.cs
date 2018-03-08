using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnitEngine;
using CommonUI;
using Aga.Controls.Tree;

namespace UnitEngineUI
{
    public partial class ControlAnimationList : UserControl
    {
        public delegate void OnChangeAnimationHandler(UnitAnimation data);

        public event OnChangeAnimationHandler AnimationChange;

        List<UnitAnimation> _animations;

        TreeModel _treeModel;

        TreeViewAdvWorker _treeViewWorker;

        bool _disableUpdate;

        public List<UnitAnimation> Animations
        {
            get
            {
                return _animations;
            }
            set
            {
                _animations = value;
                RebuildTree(_animations);
            }
        }

        public ControlAnimationList()
        {
            InitializeComponent();

            _treeModel = new TreeModel();
            _treeView.Model = _treeModel;

            _treeViewWorker = new TreeViewAdvWorker(_treeView);
            _treeViewWorker.EnableCopyPaste(CanPasteTreeNode, PasteItemHandler);
            _treeViewWorker.EnableDragDrop(CanDragItem, CanDropItem);

            _nodeTextBox.LabelChanged +=  NodeTextBox_LabelChanged;
        }

        void RebuildTree(List<UnitAnimation> animations)
        {            
            _treeView.BeginUpdate();
            try
            {
                _treeModel.Nodes.Clear();
                foreach (var animation in animations)
                {
                    var node = new TreeNodeAnimation(animation, null);
                    _treeModel.Nodes.Add(node);
                }
            }
            finally
            {
                _treeView.EndUpdate();
            }
        }

        bool CanPasteTreeNode(TreeNodeBase source, TreeNodeBase target)
        {
            return (source.Tag is UnitAnimation);
        }

        void PasteItemHandler(TreeNodeBase source)
        {
            var anim = source.Tag as UnitAnimation;
            ReadAnimationsFromModel();
        }

        void ReadAnimationsFromModel()
        {
            _animations.Clear();
            foreach (var node in _treeView.AllNodes)
            {
                var anim = (node.Tag as TreeNodeBase).Tag as UnitAnimation;
                if (anim != null)
                {
                    _animations.Add(anim);
                }
            }
        }

        /// <summary>
        /// Назначить новый объект для отображения
        /// </summary>
        /// <param name="animItem"></param>
        void SetEditItem(UnitAnimation animItem)
        {
            _disableUpdate = true;

            try
            {

                // Очищаем контролы
                FormWorker.SetEmptyValueForControl(this);

                if (animItem == null)
                {
                    _panelSetting.Enabled = false;
                    return;
                }

                // Устанавливаем значения в элементы формы
                //          
                _nameBox.Text = animItem.Name;
                _nmrStartFrame.Value = animItem.StartFrame;
                _nmrEndFrame.Value = animItem.EndFrame;
                _nmrAnimSpeed.Value = animItem.Speed;
                _checkBoxRepeat.Checked = animItem.Loop;
                _panelSetting.Enabled = true;
            }
            finally
            {
                _disableUpdate = false;
            }
        }

        /// <summary>
        /// Изменение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Control_ItemChanged(object sender, EventArgs e)
        {
            if (_disableUpdate)
            {
                return;
            }

            UnitAnimation editItem = GetEditItem();
            if (editItem == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(_nameBox.Text))
            {
                Common.Report.Warn("Имя анимации не заданно");
                return;
            }

            editItem.Name = _nameBox.Text;
            editItem.StartFrame = Convert.ToInt32(_nmrStartFrame.Value);
            editItem.EndFrame = Convert.ToInt32(_nmrEndFrame.Value);
            editItem.Speed = Convert.ToInt32(_nmrAnimSpeed.Value);
            editItem.Loop = _checkBoxRepeat.Checked;

            if (AnimationChange != null)
            {
                AnimationChange(editItem);
            }

            _treeView.FullUpdate();
        }


        void NodeTextBox_LabelChanged(object sender, Aga.Controls.Tree.NodeControls.LabelEventArgs e)
        {
            UnitAnimation editItem = GetEditItem();
            if (editItem == null)
            {
                return;
            }

            _nameBox.Text = editItem.Name;
        }


        void TreeView_SelectionChanged(object sender, EventArgs e)
        {            
            UnitAnimation editItem = GetEditItem();
            SetEditItem(editItem);

            if (AnimationChange != null)
            {
                AnimationChange(editItem);
            }
        }

        private void TreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                MenuItemDelete(sender, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                UnitAnimation editItem = GetEditItem();
                if (editItem == null)
                {
                    return;
                }

                if (AnimationChange != null)
                {
                    AnimationChange(editItem);
                }
            }
        }

        /// <summary>
        /// Добавление объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemCreateAnim(object sender, EventArgs e)
        {
            var animation = new UnitAnimation("Anim", 0, 0, Common.Constants.AnimationSpeed, false);
            _animations.Add(animation);
            RebuildTree(_animations);

            foreach (var node in _treeView.AllNodes)
            {
                if ((node.Tag as TreeNodeBase).Tag == animation)
                {
                    _treeView.SelectedNode = node;
                    break;
                }
            }

            _nameBox.Focus();
        }

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemDelete(object sender, EventArgs e)
        {
            UnitAnimation editItem = GetEditItem();
            if (editItem == null)
            {
                return;
            }
            _animations.Remove(editItem);
            RebuildTree(_animations);
        }

        /// <summary>
        /// Получить редактируемую анимацию
        /// </summary>
        /// <returns></returns>
        UnitAnimation GetEditItem()
        {
            if (_treeView.SelectedNode == null)
            {
                return null;
            }
            TreeNodeBase dataNode = _treeView.SelectedNode.Tag as TreeNodeBase;
            if (dataNode == null)
            {
                return null;
            }

            UnitAnimation editItem = dataNode.Tag as UnitAnimation;
            return editItem;
        }

        /// <summary>
        /// Может ли объект перетаскиваться
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool CanDragItem(TreeNodeBase node)
        {
            return true;
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
                return false;
            }

            return true;
        }


    }
}
