using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Serializable;
using Common;

namespace CommonUI
{
    /// <summary>
    /// Форма выбора объекта
    /// </summary>
    public partial class FormSelectObject : Form
    {
        /// <summary>
        /// Результат выбора
        /// </summary>
        public object Result;

        /// <summary>
        /// Класс для работы с деревом объектов
        /// </summary>
        TreeViewAdvWorker _treeViewWorker;

        /// <summary>
        /// Модель данных дерева
        /// </summary>
        TreeModel _treeModel;

        /// <summary>
        /// Форма для выбора объекта
        /// </summary>
        /// <param name="title"></param>
        /// <param name="container"></param>
        public FormSelectObject(string title, ContainerTreeView container)
        {
            InitializeComponent();

            _treeModel = new TreeModel(null);
            _treeView.Model = _treeModel;

            _treeView.BeginUpdate();

            List<TreeNodeBase> nodes =
                TreeViewConvertor.ConvertToNodes(container.Nodes, null, CreateTreeNodeHandler);
            _treeModel.Nodes.Clear();
            foreach (var node in nodes)
            {
                _treeModel.Nodes.Add(node);
            }

            _treeView.EndUpdate();

            var utilForm = new CommonUI.FormKeysWorker(this);
            utilForm.EscEnterEvent(Accept);

            _treeViewWorker = new TreeViewAdvWorker(_treeView);
            //_treeViewWorker.EnableFilter(_textBoxFilter);

            _treeView.FullUpdate();
        }



        /// <summary>
        /// Принять результат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOk_Click(object sender, EventArgs e)
        {
            Accept();
        }

        /// <summary>
        /// Принять результат
        /// </summary>
        private void Accept()
        {
            if (_treeView.SelectedNode == null || _treeView.SelectedNode.Tag is TreeNodeGroup)
            {
                return;
            }
            var dataNode = _treeView.SelectedNode.Tag as TreeNodeBase;
            Result = dataNode.Tag;
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
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
            else
            {
                return new TreeNodeBase(sNode.Text, sNode.Tag, null);
            }
        }

        private void TreeView_NodeMouseDoubleClick(object sender, Aga.Controls.Tree.TreeNodeAdvMouseEventArgs e)
        {
            Accept();
        }        
        
        private void TextBoxFilter_KeyDown(object sender, KeyEventArgs e)
        {
            _treeView.Focus();
        }

        private void TreeView_SelectionChanged(object sender, EventArgs e)
        {
            _buttonOk.Enabled = (!(_treeView.SelectedNode == null || _treeView.SelectedNode.Tag is TreeNodeGroup));
        }


    }
}
