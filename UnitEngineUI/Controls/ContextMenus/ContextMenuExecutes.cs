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

namespace UnitEngineUI
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
            menuItem.Click += new System.EventHandler(MenuItemExecuteScript);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Локальные параметры";
            menuItem.Click += new System.EventHandler(MenuItemExecuteSetParam);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Глобальные параметры";
            menuItem.Click += new System.EventHandler(MenuItemExecuteSetGlobalParam);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Трансформации";
            menuItem.Click += new System.EventHandler(MenuItemExecuteTransform);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Изменить цвет";
            menuItem.Click += new System.EventHandler(MenuItemExecuteColor);
            this.Items.Add(menuItem); 

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Создать юнит";
            menuItem.Click += new System.EventHandler(MenuItemExecuteCreateUnit);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Удалить юнит";
            menuItem.Click += new System.EventHandler(MenuItemExecuteDeleteUnit);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Удалить юниты";
            menuItem.Click += new System.EventHandler(MenuItemExecuteDeleteUnitsAll);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Текстуры";
            menuItem.Click += new System.EventHandler(MenuItemTextures);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Тип материи";
            menuItem.Click += new System.EventHandler(MenuItemMaterial);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Поворот";
            menuItem.Click += new System.EventHandler(MenuItemExecuteRotation);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Изменить параметры других юнитов";
            menuItem.Click += new System.EventHandler(MenuItemExecuteExtParameter);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Внешнее действие";
            menuItem.Click += new System.EventHandler(MenuItemExecuteExtAction);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Переместиться в точку";
            menuItem.Click += new System.EventHandler(MenuItemExecuteMoveToPoint);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Следовать за моделью";
            menuItem.Click += new System.EventHandler(MenuItemExecuteMoveToSceneNode);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Удалить себя";
            menuItem.Click += new System.EventHandler(MenuItemExecuteDeleteSelf);
            this.Items.Add(menuItem);
            
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Звук";
            menuItem.Click += new System.EventHandler(MenuItemExecuteSound);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "След. действие";
            menuItem.Click += new System.EventHandler(MenuItemExecuteAddNextAction);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Группа";
            menuItem.Click += new System.EventHandler(MenuItemExecuteGroup);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Изменить Id модели";
            menuItem.Click += new System.EventHandler(MenuItemExecuteChangeSceneNodeId);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Связь с параметрами модели";
            menuItem.Click += new System.EventHandler(MenuItemExecuteMappingTransform);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Таймер";
            menuItem.Click += new System.EventHandler(MenuItemExecuteTimer);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Установить данные";
            menuItem.Click += new System.EventHandler(MenuItemExecuteSetData);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Система частиц - Emmiter";
            menuItem.Click += new System.EventHandler(MenuItemExecuteParticleEmitter);
            this.Items.Add(menuItem);

            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Система частиц - Affector";
            menuItem.Click += new System.EventHandler(MenuItemExecuteParticleAffector);
            this.Items.Add(menuItem);
        }

        void MenuItemExecuteSetParam(object sender, EventArgs e)
        {
            ExecuteParameter execute = new ExecuteParameter(false);
            NewBehaviorExecute(execute);
        }

        void MenuItemExecuteSetGlobalParam(object sender, EventArgs e)
        {
            ExecuteParameter execute = new ExecuteParameter(true);
            NewBehaviorExecute(execute);
        }

        void MenuItemExecuteCreateUnit(object sender, EventArgs e)
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

        void MenuItemExecuteDeleteUnit(object sender, EventArgs e)
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

        void MenuItemExecuteDeleteUnitsAll(object sender, EventArgs e)
        {
            var execute = new ExecuteDeleteUnitsAll();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteParticleEmitter(object sender, EventArgs e)
        {
            var execute = new ExecuteParticleEmitter();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteParticleAffector(object sender, EventArgs e)
        {
            var execute = new ExecuteParticleAffector();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteTransform(object sender, EventArgs e)
        {
            var execute = new ExecuteTransform();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteColor(object sender, EventArgs e)
        {
            var execute = new ExecuteColor();
            NewBehaviorExecute(execute);
        }

        private void MenuItemTextures(object sender, EventArgs e)
        {
            var execute = new ExecuteTextures();
            NewBehaviorExecute(execute);
        }

        private void MenuItemMaterial(object sender, EventArgs e)
        {
            var execute = new ExecuteMaterial();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteMoveToPoint(object sender, EventArgs e)
        {
            var execute = new ExecuteMoveToPoint();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteRotation(object sender, EventArgs e)
        {
            var execute = new ExecuteRotation();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteSound(object sender, EventArgs e)
        {
            var execute = new ExecuteSound();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteMoveToSceneNode(object sender, EventArgs e)
        {
            var execute = new ExecuteMoveToSceneNode();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteExtAction(object sender, EventArgs e)
        {
            var execute = new ExecuteExtAction();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteDeleteSelf(object sender, EventArgs e)
        {
            var execute = new ExecuteDeleteSelf();
            NewBehaviorExecute(execute);
        }

        private void MenuItemExecuteTimer(object sender, EventArgs e)
        {
            var execute = new ExecuteTimer();
            NewBehaviorExecute(execute);
        }

        /// <summary>
        /// Добавить группу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExecuteGroup(object sender, EventArgs e)
        {
            var execute = new ExecuteGroup();
            NewBehaviorExecute(execute);
        }

        /// <summary>
        /// Добавить связь с параметрами модели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExecuteMappingTransform(object sender, EventArgs e)
        {
            var execute = new ExecuteMappingTransform();
            NewBehaviorExecute(execute);
        }

        /// <summary>
        /// Добавить след. действие
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExecuteAddNextAction(object sender, EventArgs e)
        {
            var execute = new ExecuteAddNextAction();
            NewBehaviorExecute(execute);
        }

        /// <summary>
        /// Изменить параметры других юнитов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExecuteExtParameter(object sender, EventArgs e)
        {
            var execute = new ExecuteExtParameter();
            NewBehaviorExecute(execute);
        }

        /// <summary>
        /// Изменить Id модели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExecuteChangeSceneNodeId(object sender, EventArgs e)
        {
            var execute = new ExecuteChangeSceneNodeId();
            NewBehaviorExecute(execute);
        }

        /// <summary>
        /// Установить данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExecuteSetData(object sender, EventArgs e)
        {
            var execute = new ExecuteSetData();
            NewBehaviorExecute(execute);
        }

        /// <summary>
        /// Выполнить скрипт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExecuteScript(object sender, EventArgs e)
        {
            var execute = new ExecuteScript();
            NewBehaviorExecute(execute);
        }
    }
}
