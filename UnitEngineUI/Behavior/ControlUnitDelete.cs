using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnitEngine.Behavior;
using CommonUI;
using Serializable;

namespace UnitEngineUI.Behavior
{
    public partial class ControlUnitDelete : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        List<string> _childsBehaviorsPaths;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        ExecuteDeleteUnit _editItem;

        public ControlUnitDelete(List<string> childsBehaviorsPaths)
        {
            InitializeComponent();
            _childsBehaviorsPaths = childsBehaviorsPaths;
        }

        public ExecuteDeleteUnit EditItem
        {
            get
            {
                return _editItem;
            }
            set
            {
                SetInstance(value);
            }
        }

        /// <summary>
        /// Назначить новый объект для отображения
        /// </summary>
        /// <param name="editItem"></param>
        private void SetInstance(ExecuteDeleteUnit editItem)
        {
            _editItem = null;
            // Очищаем контролы
            FormWorker.SetEmptyValueForControl(this);

            if (editItem == null) return;

            _textBoxBehaviorsPath.Text = editItem.BehaviorsPath;

            _editItem = editItem;
        }


        /// <summary>
        /// Изменение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_ItemChanged(object sender, EventArgs e)
        {
            if (_editItem == null) return;

            _editItem.BehaviorsPath = _textBoxBehaviorsPath.Text;

            if (Changed != null) Changed(_editItem);
        }

        private void BtnSelectBehaviors_Click(object sender, EventArgs e)
        {
            if (_childsBehaviorsPaths == null || _childsBehaviorsPaths.Count == 0)
            {
                MessageBox.Show("Дочерние юниты отсутствуют");
                return;
            }

            ContainerTreeView contTreeView = new ContainerTreeView();
            foreach (string path in _childsBehaviorsPaths)
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
            _textBoxBehaviorsPath.Text = resPath;
        }

    }
}
