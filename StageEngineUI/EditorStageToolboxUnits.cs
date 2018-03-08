using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using StageEngineUI.Controls;
using Serializable;
using UnitEngine;
using CommonUI;
using Aga.Controls.Tree;
using System.IO;

namespace StageEngineUI
{
    public partial class EditorStageToolboxUnits : DockContent
    {
        /// <summary>
        /// Редактор стадии
        /// </summary>
        EditorStage _mainEditor;

        /// <summary>
        /// Модель данных дерева
        /// </summary>
        TreeModel _treeModel;

        public EditorStageToolboxUnits(EditorStage editor)
        {
            InitializeComponent();

            _mainEditor = editor;
            _treeModel = new TreeModel(null);
            _treeView.Model = _treeModel;

            _treeView.ItemDrag += TreeView_ItemDrag;
        }


        /// <summary>
        /// Обновить объекты
        /// </summary>
        public void ReloadItems()
        {
            _treeView.BeginUpdate();
            try
            {
                Clear();

                foreach (var item in _mainEditor.BehaviorsPackage)
                {
                    UnitBehavior behavior = item.Value;
                    string filePath = item.Key;

                    TreeNodeToolboxUnit node = new TreeNodeToolboxUnit(
                        filePath,
                        behavior,
                        null);
                    string imagePath = GenImagePath(filePath);
                    if (File.Exists(imagePath))
                    {
                        node.Icon = new Bitmap(imagePath);
                    }

                    _treeModel.Nodes.Add(node);
                }
            }
            finally
            {
                _treeView.EndUpdate();
            }
        }

        void Clear()
        {
            _treeModel.Nodes.Clear();
            _filter.Text = string.Empty;
        }

        /// <summary>
        /// Получить путь до изображения
        /// </summary>
        /// <param name="modelPath"></param>
        /// <returns></returns>
        string GenImagePath(string modelPath)
        {
            return System.IO.Path.ChangeExtension(modelPath, "bpng");
        }


        void TreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var items = e.Item as TreeNodeAdv[];
            if (items == null || items.Length == 0)
            {
                return;
            }
            var item = items[0].Tag as TreeNodeToolboxUnit;
            if (item == null || item.Tag == null)
            {
                return;
            }
            this.DoDragDrop(item, DragDropEffects.Copy);
        }

        void MenuItemCreate_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл поведения (*.behavior)|*.behavior|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Выберите файл поведения";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            if (_mainEditor.BehaviorsPackage.Keys.Contains(dialog.FileName))
            {
                MessageBox.Show("Данный тип уже добавлен");
                return;
            }

            UnitBehavior behaviors = UnitBehavior.LoadFromFile(dialog.FileName);
            if (behaviors == null)
            {
                MessageBox.Show("Описание поведений не загружено");
                return;
            }
            _mainEditor.BehaviorsPackage.Add(dialog.FileName, behaviors);

            ReloadItems();
        }

        void MenuItemDelete_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        void TreeView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteItem();
            }
        }

        void DeleteItem()
        {
            if (_treeView.SelectedNode == null)
            {
                return;
            }

            var dataNode = _treeView.SelectedNode.Tag as TreeNodeBase;
            UnitBehavior behavior = dataNode.Tag as UnitBehavior;
            if (behavior == null)
            {
                return;
            }
            var pair = _mainEditor.BehaviorsPackage.FirstOrDefault(
                x => x.Value == behavior);

            string path = pair.Key;
            if (MessageBox.Show("Удалить юнит?", "Подтверждение", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            _mainEditor.BehaviorsPackage.Remove(path);
            ReloadItems();
        }
    }
}
