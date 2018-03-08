﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using UnitEngine;
using System.Xml.Serialization;
using Common;
using Serializable;
using IrrlichtWrap;
using System.Globalization;
using Common.Geometry;
using UnitEngine.Events;
using UnitEngineUI.Events;
using CommonUI;
using IrrTools;
using UnitEngineUI.Behavior;
using UnitEngine.Behavior;
using WeifenLuo.WinFormsUI.Docking;
using CommonUI.UITypeEditors;

namespace UnitEngineUI
{
    public partial class EditorBehavior : Form
    {
        /// <summary>
        /// Хранилище данных
        /// </summary>
        public UnitBehavior ContainerBehavior
        {
            get
            {
                return _unitBehavior;
            }
        }

        /// <summary>
        /// Путь до файла
        /// </summary>
        string _path;

        /// <summary>
        /// Хранилище данных для редактора
        /// </summary>
        UnitBehavior _unitBehavior;

        /// <summary>
        /// Инспектор юнитов
        /// </summary>
        EditorBehaviorInspector _inspector;

        /// <summary>
        /// Основной документ для отображения графики 
        /// </summary>
        EditorBehaviorIrrDocument _irrDocument;

        /// <summary>
        /// Свойства объекта
        /// </summary>
        EditorBehaviorItemProperty _propertyWindow;

        /// <summary>
        /// Панель для редактирования моделей
        /// </summary>
        ToolStrip _irrTools;

        /// <summary>
        /// Необходимо ли сохранить файл
        /// </summary>
        bool _needSaveFile;

        /// <summary>
        /// Список имён созданных контролов
        /// </summary>
        List<string> _controlsNames;

        /// <summary>
        /// Базовые настройки редактора
        /// </summary>
        CommonEditorSettings _commonSettings;

        /// <summary>
        /// Буффер для undo / redo
        /// </summary>
        UndoBuffer _undoRedoBuffer;
                
        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="controlsNames">Список имён созданных контролов</param>
        public EditorBehavior(List<string> controlsNames)
        {
            InitializeComponent();

            _undoRedoBuffer = new UndoBuffer();

            var utilForm = new CommonUI.FormKeysWorker(this);
            utilForm.KeyFileEvents(NewFile, OpenFile, Save);

            _controlsNames = controlsNames;

            _propertyWindow = new EditorBehaviorItemProperty();

            _inspector = new EditorBehaviorInspector(this, _propertyWindow, controlsNames);
            _inspector.ItemCreated += ItemCreated;
            _inspector.ItemDeleted += ItemDeleted;
            _inspector.ItemDataChanged += ItemDataChanged;
            _inspector.SelectionChanged += SelectionChanged;

            _irrDocument = new EditorBehaviorIrrDocument();

            Clear();

            if (!LoadUIState(false))
                ShowDefaultWindows();

            _inspector.Enabled = false;
            _propertyWindow.Enabled = false;

        }

        /// <summary>
        /// Показать окно по умолчанию
        /// </summary>
        private void ShowDefaultWindows()
        {
            _inspector.Show(_docPanel, DockState.DockLeft);
            _propertyWindow.Show(_docPanel, DockState.DockRight);
            _irrDocument.Show(_docPanel, DockState.Document);
        }

        /// <summary>
        /// Сохраняет положение окон
        /// </summary>
        private void SaveUIState()
        {
            try
            {
                _commonSettings.Save(
                    Path.Combine(UtilPath.AssemblyDirectory, "EditorBehavior.CommonUI.xml")
                    );

                _docPanel.SaveAsXml(
                    Path.Combine(UtilPath.AssemblyDirectory, "EditorBehaviorUI.xml"));

                FormWorker.SaveState(
                    Path.Combine(UtilPath.AssemblyDirectory, "EditorBehaviorUIMain.xml"),
                    this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static CommonEditorSettings GetCommonSettings()
        {
            return CommonEditorSettings.Open(
                Path.Combine(UtilPath.AssemblyDirectory, "EditorBehavior.CommonUI.xml"));
        }

        /// <summary>
        /// Загрузить положение окон
        /// </summary>
        private bool LoadUIState(bool onlyMain)
        {
            try
            {
                _commonSettings = GetCommonSettings();
                _commonSettings.FillMenu(_menuFiles, ReadFile);

                if (!onlyMain)
                {
                    string path = Path.Combine(UtilPath.AssemblyDirectory, "EditorBehaviorUI.xml");
                    if (!File.Exists(path)) return false;
                    _docPanel.LoadFromXml(path, GetContentFromPersistString);
                }

                FormWorker.LoadState(
                    Path.Combine(UtilPath.AssemblyDirectory, "EditorBehaviorUIMain.xml"),
                    this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(EditorBehaviorInspector).ToString())
                return _inspector;
            else if (persistString == typeof(EditorBehaviorIrrDocument).ToString())
                return _irrDocument;
            else if (persistString == typeof(EditorBehaviorItemProperty).ToString())
                return _propertyWindow;
            return null;
        }

        #region Общие

        /// <summary>
        /// Очистка данных
        /// </summary>
        private void Clear()
        {
            _path = null;
            _unitBehavior = new UnitBehavior();
            _irrDocument.Clear();
            _inspector.Clear();
            _undoRedoBuffer.Flush();
        }

        /// <summary>
        /// Принять изменения
        /// </summary>
        private void SaveAndClose()
        {
            Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        private bool Save()
        {

            if (_path == null)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Файл поведения (*.behavior)|*.behavior|" +
                             "Все файлы (*.*)|*.*";
                dialog.Title = "Сохранить файл поведения";
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return false;
                _path = dialog.FileName;
            }

            // Обновляем данные
            //
            _unitBehavior.TreeView = _inspector.GetContainerTreeView();
            _unitBehavior.Camera = _irrDocument.GetContainerCamera();

            UnitBehavior containerRelative = _unitBehavior.GetWithRelativePaths(_path);
            // Серелизуем данные для редактора
            //
            XmlSerializer xmlSerelializeDsg = new XmlSerializer(typeof(UnitBehavior), GetExtraTypes());
            using (StreamWriter writer = new StreamWriter(_path, false, Encoding.UTF8))
            {
                xmlSerelializeDsg.Serialize(writer, containerRelative);
            }

            // Сохраняем изображение
            string imgPath = System.IO.Path.ChangeExtension(_path, "bpng");
            _irrDocument.CaptureFromScreenToFile(imgPath);

            // Обновляем заголовок формы
            UpdateFormTitle(_path);

            _needSaveFile = false;

            return true;
        }

        private static Type[] GetExtraTypes()
        {
            List<Type> extraTypes = new List<Type>();
            extraTypes.Add(typeof(UnitAnimation));
            extraTypes.Add(typeof(UnitSound));
            extraTypes.Add(typeof(UnitEffect));
            extraTypes.Add(typeof(UnitAction));
            extraTypes.Add(typeof(ExecuteTransform));
            return extraTypes.ToArray();
        }

        /// <summary>
        /// Открыть файл
        /// </summary>
        private void OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл поведения (*.behavior)|*.behavior|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть файл поведения";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            ReadFile(dialog.FileName);
        }

        /// <summary>
        /// Прочитать файл
        /// </summary>
        public void ReadFile(string path)
        {
            Clear();

            _path = path;

            // Десерелизуем данные
            _unitBehavior = ReadBehaviorFromFile(path);
            // Обновление внешних сслылок
            XrefBehaviorWorker.UpdateXReferences(_unitBehavior);

            _inspector.SetTreeView(_unitBehavior);
            _irrDocument.LoadModels(_unitBehavior);
            if (_unitBehavior != null)
                _irrDocument.UpdateCamera(_unitBehavior.Camera);

            UpdateFormTitle(path);
            EnableUI();

            // Обновляем список недавно открытых файлов
            _commonSettings.FileOpened(path);
            _commonSettings.FillMenu(_menuFiles, ReadFile);

            // Запоминаем первоначальное состояние
            _undoRedoBuffer.Do(GetEditorState());
        }

        private void UpdateFormTitle(string path)
        {
            this.Text = string.Format("Редактор поведения ({0})", Path.GetFileNameWithoutExtension(path));
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        /// <summary>
        /// Создание нового описания анимаций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        /// <summary>
        /// Создание нового описания анимаций
        /// </summary>
        public void NewFile()
        {
            Clear();
            // Делаем доступным UI
            EnableUI();
            // Устанавливаем путой контейнер
            _inspector.SetTreeView(_unitBehavior);
            _irrDocument.Clear();
        }

        /// <summary>
        /// Сделать доступным UI
        /// </summary>
        private void EnableUI()
        {
            _inspector.Enabled = true;
            _propertyWindow.Enabled = true;
            _menuItemModels.Enabled = true;
            _saveToolStripMenuItem.Enabled = true;
            _menuItemParameters.Enabled = true;
            _menuItemSaveAs.Enabled = true;
            _menuItemChilds.Enabled = true;
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_needSaveFile)
            {
                var resDlg = MessageBox.Show("Сохранить изменения ?", "Сохранение изменений", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (resDlg == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                if (resDlg == DialogResult.Yes)
                {
                    if (!Save())
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            _irrDocument.Destroy();

            // Сохраняем положение окон и др.
            SaveUIState();
        }

        private void Editor_Shown(object sender, EventArgs e)
        {
            LoadUIState(true);

            if (_irrTools == null)
            {
                var cntrl = new ControlPreviewTools(_irrDocument.Device);
                cntrl.NodeChanged += NodeSaveChange;
                _irrTools = cntrl.ExtractToolStrip();
                _irrTools.Dock = DockStyle.None;
                //_toolStripContainer.TopToolStripPanel.Join(_irrTools, 1);
                _toolStripContainer.TopToolStripPanel.Join(_irrTools, _toolStripFile.Width + 5, 1);
            }
        }

        /// <summary>
        /// Обработчик изменения модели
        /// </summary>
        /// <param name="node"></param>
        void NodeSaveChange(SceneNodeW node)
        {
            _irrDocument.NodeSaveChange(node, _unitBehavior);
        }

        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                _irrDocument.UpdateModel(_inspector.EditItem, _unitBehavior);
            
            // Обработка операций отмены
            //
            if (e.KeyCode == Keys.Z && e.Control)
            {
                if (_undoRedoBuffer.CanUndo)
                    SetEditorState(_undoRedoBuffer.Undo());
            }
            else if (e.KeyCode == Keys.Y && e.Control)
            {
                if (_undoRedoBuffer.CanRedo)
                    SetEditorState(_undoRedoBuffer.Redo());
            }
        }



        /// <summary>
        /// Десерилизация поведения из файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private UnitBehavior ReadBehaviorFromFile(string path)
        {
            UnitBehavior container;
            XmlSerializer xmlSerelialize = new XmlSerializer(typeof(UnitBehavior), GetExtraTypes());
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                container = xmlSerelialize.Deserialize(reader) as UnitBehavior;
                container = container.GetWithAbsolutePaths(path);
            }
            return container;
        }

        #endregion

        /// <summary>
        /// Сохранить изменения для файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonApply_Click(object sender, EventArgs e)
        {
            Save();
        }

        #region Меню

        /// <summary>
        /// Добавить дополнительную модель
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddEnvModel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл модели (*.x)|*.X|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Выберите файл модели";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            ContainerNode node = new ContainerNode(0, dialog.FileName, new Vertex(), new Vertex());
            _unitBehavior.EnviromentModels.Add(node);
            _irrDocument.LoadModels(_unitBehavior);
        }

        /// <summary>
        /// Удалить дополнительную модель
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDeleteEnvModel_Click(object sender, EventArgs e)
        {
            if (_unitBehavior == null || _unitBehavior.EnviromentModels.Count == 0)
            {
                MessageBox.Show("Дополнительные модели отсутствуют");
                return;
            }

            ContainerTreeView contTreeView = new ContainerTreeView();
            foreach (ContainerNode envNode in _unitBehavior.EnviromentModels)
            {
                contTreeView.Nodes.Add(new SerializableTreeNode()
                {
                    Text = envNode.Path,
                    Tag = envNode
                });
            }
            FormSelectObject selectForm = new FormSelectObject(
                "Выберите модель", contTreeView);
            if (selectForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            _unitBehavior.EnviromentModels.Remove(selectForm.Result as ContainerNode);

            _irrDocument.LoadModels(_unitBehavior);
        }

        /// <summary>
        /// Сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Сохранить как
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Файл поведения (*.behavior)|*.behavior|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Сохранить файл поведения";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            _path = dialog.FileName;
            Save();
        }

        private void MenuItemSetFileMainModel_Click(object sender, EventArgs e)
        {
            ControlUnitModel control = new ControlUnitModel(_unitBehavior.UnitModel);
            if (FormWorker.ShowDialog("Выберите модель", control, this) != System.Windows.Forms.DialogResult.OK)
                return;

            _unitBehavior.UnitModel = control.Result;

            _irrDocument.LoadModels(_unitBehavior);
        }

        private void MenuItemInfoParameters_Click(object sender, EventArgs e)
        {
            if (_unitBehavior == null) return;
            var control = new ControlStdProperties();
            control.Size = new System.Drawing.Size(480, 320);
            control.EditItem = _unitBehavior.DeepClone();
            if (FormWorker.ShowDialog("Изменить параметры", control, this) == System.Windows.Forms.DialogResult.OK)
            {
                _unitBehavior = control.EditItem as UnitBehavior;
            }
        }

        /// <summary>
        /// Добавить дочерний юнит
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemNewChild_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл поведения (*.behavior)|*.behavior|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Выберите файл поведения";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            _unitBehavior.ChildsBehaviorsPaths.Add(dialog.FileName);
        }

        private void MenuItemDeleteChild_Click(object sender, EventArgs e)
        {
            if (_unitBehavior == null || _unitBehavior.ChildsBehaviorsPaths.Count == 0)
            {
                MessageBox.Show("Дочерние юниты отсутствуют");
                return;
            }

            ContainerTreeView contTreeView = new ContainerTreeView();
            foreach (string path in _unitBehavior.ChildsBehaviorsPaths)
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
            _unitBehavior.ChildsBehaviorsPaths.Remove(selectForm.Result as string);

            // TODO : Добавить удаление зависимых элементов
        }

        /// <summary>
        /// Редактор скрипта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemScript_Click(object sender, EventArgs e)
        {
            ControlEditorScriptFileName cntrlEditor;
            List<string> names = new List<string>();
            names.AddRange(ParserFunctionNames.GetUnitsNames());
            cntrlEditor = new ControlEditorScriptFileName(names);
            cntrlEditor.EditItem = _unitBehavior.ScriptFileName;
            cntrlEditor.Size = new Size(1024, 480);
            if (FormWorker.ShowDialog("Редактор скрипта", cntrlEditor) != System.Windows.Forms.DialogResult.OK)
                return;
            _unitBehavior.ScriptFileName = cntrlEditor.EditItem;
        }

        /// <summary>
        /// Редактор модуля
        /// </summary>
        private void MenuItemModule_Click(object sender, EventArgs e)
        {
            FormDialogGetValue frmGetVal = new FormDialogGetValue();
            frmGetVal.StartText = _unitBehavior.ModuleName;
            if (frmGetVal.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            _unitBehavior.ModuleName = frmGetVal.Result;
        }

        #endregion

        #region Undo - Redo
                
        protected object GetEditorState()
        {
            // Обновляем данные
            //
            _unitBehavior.TreeView = _inspector.GetContainerTreeView();
            _unitBehavior.Camera = _irrDocument.GetContainerCamera();

            return _unitBehavior.DeepClone();
        }

        protected void SetEditorState(object state)
        {
            if (state == null) return;
            _unitBehavior = (state as UnitBehavior).DeepClone();
            if (_unitBehavior == null) throw new ArgumentNullException();

            _irrDocument.Clear();
            _inspector.Clear();

            _inspector.SetTreeView(_unitBehavior);
            _irrDocument.LoadModels(_unitBehavior);
            if (_unitBehavior != null)
                _irrDocument.UpdateCamera(_unitBehavior.Camera);
        }

        #endregion

        #region Редактирование объекта


        void SelectionChanged(object item)
        {
            UpdateItemDesc(item as UnitAction);
        }

        void ItemDataChanged(object item)
        {
            _needSaveFile = true;
            UpdateItemDesc(item as UnitAction);

            _undoRedoBuffer.Do(GetEditorState());
        }

        void ItemDeleted(object item)
        {
            _needSaveFile = true;
            _undoRedoBuffer.Do(GetEditorState());
        }

        void ItemCreated(object item)
        {
            _needSaveFile = true;
            _undoRedoBuffer.Do(GetEditorState());
        }

        /// <summary>
        /// Обновление описания выбранного элемента
        /// </summary>
        void UpdateItemDesc(UnitAction action)
        {
            if (action == null) return;
            _irrDocument.UpdateModel(action, _unitBehavior);
        }

        #endregion

    }
}
