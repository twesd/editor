using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using UnitEngine.Behavior;
using CommonUI;
using Serializable;
using UnitEngine;

namespace UnitEngineUI.Behavior
{
    class ContextMenuExecutes : ContextMenuStrip
    {
        public delegate void NewItemHadler(ExecuteBase execute);

        public event NewItemHadler NewBehaviorExecute;

        UnitBehavior _container;

        public ContextMenuExecutes(UnitBehavior container)
        {
            _container = container;

            this.Name = "_contextMenuExecuteProps";
            this.AutoSize = true;

            ToolStripMenuItem menuItem;

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Выполнить скрипт";
            menuItem.Click += new System.EventHandler(MenuItemExecuteScript_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Локальные параметры";
            menuItem.Click += new System.EventHandler(MenuItemExecuteSetParam_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Глобальные параметры";
            menuItem.Click += new System.EventHandler(MenuItemExecuteSetGlobalParam_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Трансформации";
            menuItem.Click += new System.EventHandler(MenuItemExecuteTransform_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Изменить цвет";
            menuItem.Click += new System.EventHandler(MenuItemExecuteColor_Click);
            this.Items.Add(menuItem); 

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Создать юнит";
            menuItem.Click += new System.EventHandler(MenuItemExecuteCreateUnit_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Удалить юнит";
            menuItem.Click += new System.EventHandler(MenuItemExecuteDeleteUnit_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Текстуры";
            menuItem.Click += new System.EventHandler(MenuItemTextures_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Тип материи";
            menuItem.Click += new System.EventHandler(MenuItemMaterial_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Поворот";
            menuItem.Click += new System.EventHandler(MenuItemExecuteRotation_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Изменить параметры других юнитов";
            menuItem.Click += new System.EventHandler(MenuItemExecuteExtParameter_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Внешнее действие";
            menuItem.Click += new System.EventHandler(MenuItemExecuteExtAction_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Переместиться в точку";
            menuItem.Click += new System.EventHandler(MenuItemExecuteMoveToPoint_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Следовать за моделью";
            menuItem.Click += new System.EventHandler(MenuItemExecuteMoveToSceneNode_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Удалить себя";
            menuItem.Click += new System.EventHandler(MenuItemExecuteDeleteSelf_Click);
            this.Items.Add(menuItem);
            
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Звук";
            menuItem.Click += new System.EventHandler(MenuItemExecuteSound_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "След. действие";
            menuItem.Click += new System.EventHandler(MenuItemExecuteAddNextAction_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Группа";
            menuItem.Click += new System.EventHandler(MenuItemExecuteGroup_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Изменить Id модели";
            menuItem.Click += new System.EventHandler(MenuItemExecuteChangeSceneNodeId_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Связь с параметрами модели";
            menuItem.Click += new System.EventHandler(MenuItemExecuteMappingTransform_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Установить данные";
            menuItem.Click += new System.EventHandler(MenuItemExecuteSetData_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Система частиц - Emmiter";
            menuItem.Click += new System.EventHandler(MenuItemExecuteParticleEmitter_Click);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Система частиц - Affector";
            menuItem.Click += new System.EventHandler(MenuItemExecuteParticleAffector_Click);
            this.Items.Add(menuItem);
        }

        private void MenuItemExecuteSetParam_Click(object sender, EventArgs e)
        {
            ExecuteParameter execute = new ExecuteParameter(false);
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteSetGlobalParam_Click(object sender, EventArgs e)
        {
            ExecuteParameter execute = new ExecuteParameter(true);
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteCreateUnit_Click(object sender, EventArgs e)
        {
            if (_container == null || _container.ChildsBehaviorsPaths.Count == 0)
            {
                MessageBox.Show("Дочерние юниты отсутствуют");
                return;
            }

            ContainerTreeView contTreeView = new ContainerTreeView();
            foreach (string path in _container.ChildsBehaviorsPaths)
            {
                contTreeView.Nodes.Add(new SerializableTreeNode()
                {
                    Text = System.IO.Path.GetFileNameWithoutExtension(path),
                    Tag = path
                });
            }
            FormSelectObject selectForm = new FormSelectObject(
                "Выберите поведение", contTreeView);
            if (selectForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            string resPath = selectForm.Result as string;

            var execute = new ExecuteCreateUnit();
            execute.BehaviorsPath = resPath;
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteDeleteUnit_Click(object sender, EventArgs e)
        {
            if (_container == null || _container.ChildsBehaviorsPaths.Count == 0)
            {
                MessageBox.Show("Дочерние юниты отсутствуют");
                return;
            }

            ContainerTreeView contTreeView = new ContainerTreeView();
            foreach (string path in _container.ChildsBehaviorsPaths)
            {
                contTreeView.Nodes.Add(new SerializableTreeNode()
                {
                    Text = System.IO.Path.GetFileNameWithoutExtension(path),
                    Tag = path
                });
            }
            FormSelectObject selectForm = new FormSelectObject(
                "Выберите поведение", contTreeView);
            if (selectForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            string resPath = selectForm.Result as string;
            var execute = new ExecuteDeleteUnit(resPath);
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteParticleEmitter_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteParticleEmitter();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteParticleAffector_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteParticleAffector();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteTransform_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteTransform();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteColor_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteColor();
            NewBehaviorExecute(execute);
        }

        private void MenuItemTextures_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteTextures();
            NewBehaviorExecute(execute);
        }

        private void MenuItemMaterial_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteMaterial();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteMoveToPoint_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteMoveToPoint();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteRotation_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteRotation();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteSound_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteSound();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteMoveToSceneNode_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteMoveToSceneNode();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteExtAction_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteExtAction();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteDeleteSelf_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteDeleteSelf();
            NewBehaviorExecute(execute);
        }

        /// <summary>
        /// Добавить группу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExecuteGroup_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteGroup();
            NewBehaviorExecute(execute);
        }

        /// <summary>
        /// Добавить связь с параметрами модели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExecuteMappingTransform_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteMappingTransform();
            NewBehaviorExecute(execute);
        }

        /// <summary>
        /// Добавить след. действие
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExecuteAddNextAction_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteAddNextAction();
            NewBehaviorExecute(execute);
        }

        /// <summary>
        /// Изменить параметры других юнитов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExecuteExtParameter_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteExtParameter();
            NewBehaviorExecute(execute);
        }

        /// <summary>
        /// Изменить Id модели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExecuteChangeSceneNodeId_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteChangeSceneNodeId();
            NewBehaviorExecute(execute);
        }

        /// <summary>
        /// Установить данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExecuteSetData_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteSetData();
            NewBehaviorExecute(execute);
        }

        /// <summary>
        /// Выполнить скрипт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExecuteScript_Click(object sender, EventArgs e)
        {
            var execute = new ExecuteScript();
            NewBehaviorExecute(execute);
        }
    }
}
