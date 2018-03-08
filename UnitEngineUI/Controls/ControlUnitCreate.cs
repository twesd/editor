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
using Common;

namespace UnitEngineUI
{
    public partial class ControlUnitCreate : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Пути до предпологаемых поведений
        /// </summary>
        List<string> _childsBehaviorsPaths;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        ExecuteCreateUnit _editItem;

        public ControlUnitCreate(List<string> childsBehaviorsPaths)
        {
            InitializeComponent();
            _childsBehaviorsPaths = childsBehaviorsPaths;

            _creationType.Items.Add(ExecuteCreateUnit.CreationTypeEnum.Child);
            _creationType.Items.Add(ExecuteCreateUnit.CreationTypeEnum.External);
            _creationType.SelectedIndex = 0;

        }

        public ExecuteCreateUnit EditItem
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
        private void SetInstance(ExecuteCreateUnit editItem)
        {
            _editItem = null;
            // Очищаем контролы
            FormWorker.SetEmptyValueForControl(this);

            if (editItem == null) return;
            
            _textBoxBehaviorsPath.Text = editItem.BehaviorsPath;

            _checkBoxAllowSeveralInst.Checked = editItem.AllowSeveralInstances;
            
            _x.Value = (decimal)editItem.Position.X;
            _y.Value = (decimal)editItem.Position.Y;
            _z.Value = (decimal)editItem.Position.Z;

            _rotX.Value = (decimal)editItem.Rotation.X;
            _rotY.Value = (decimal)editItem.Rotation.Y;
            _rotZ.Value = (decimal)editItem.Rotation.Z;

            _creationType.SelectedItem = editItem.CreationType;

            _scriptFileName.Text = editItem.StartScriptFileName;

            _getPosFromTapScene.Checked = editItem.GetPositionFromTapScene;
            _tapSceneName.Text = editItem.TapSceneName;
            _jointName.Text = editItem.JointName;

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

            _editItem.AllowSeveralInstances = _checkBoxAllowSeveralInst.Checked;

            _editItem.Position.X = Convert.ToSingle(_x.Value);
            _editItem.Position.Y = Convert.ToSingle(_y.Value);
            _editItem.Position.Z = Convert.ToSingle(_z.Value);

            _editItem.Rotation.X = Convert.ToSingle(_rotX.Value);
            _editItem.Rotation.Y = Convert.ToSingle(_rotY.Value);
            _editItem.Rotation.Z = Convert.ToSingle(_rotZ.Value);

            _editItem.CreationType = (ExecuteCreateUnit.CreationTypeEnum)_creationType.SelectedItem;

            _editItem.StartScriptFileName = _scriptFileName.Text;

            _editItem.GetPositionFromTapScene = _getPosFromTapScene.Checked;
            _editItem.TapSceneName = _tapSceneName.Text.Trim();
            _editItem.JointName = _jointName.Text.Trim();

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

        private void GetPosFromTapScene_CheckedChanged(object sender, EventArgs e)
        {
            _tapSceneName.Enabled = _getPosFromTapScene.Checked;
            _x.Enabled = _y.Enabled = _z.Enabled = !_getPosFromTapScene.Checked;
            Control_ItemChanged(sender, e);
        }

        private void StartScript_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            Control_ItemChanged(sender, null);
        }

        private void _btnSelect_Click(object sender, EventArgs e)
        {
            var cntrlEditor = new CommonUI.UITypeEditors.ControlEditorScriptFileName(ParserFunctionNames.GetUnitsNames());
            cntrlEditor.EditItem = _scriptFileName.Text;
            cntrlEditor.Size = new Size(1024, 480);
            if (FormWorker.ShowDialog("Редактор скрипта", cntrlEditor) != System.Windows.Forms.DialogResult.OK)
                return;
            _scriptFileName.Text = cntrlEditor.EditItem;
        }

        private void _buttonDeleteScript_Click(object sender, EventArgs e)
        {
            _scriptFileName.Text = string.Empty;
        }
    }
}
