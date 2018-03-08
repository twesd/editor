using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Common;
using Serializable;
using IrrlichtWrap;
using System.Globalization;
using Common.Geometry;
using CommonUI;
using IrrTools;
using ControlEngine;
using UnitEngine;
using StageEngineUI;
using WeifenLuo.WinFormsUI.Docking;
using StageEngineUI.Controls;
using StageEngine;
using UnitEngine.Behavior;
using StageEngineUI.Camera;
using TransactionCore;
using Aga.Controls.Tree;


namespace StageEngineUI
{
    public partial class EditorStage : Form
    {
        /// <summary>
        /// Путь до файла
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// Описание поведений юнитов
        /// </summary>
        public Dictionary<string, UnitBehavior> BehaviorsPackage { get; private set; }

        /// <summary>
        /// Наименование начальной камеры
        /// </summary>
        public string StartCameraName
        {
            get
            {
                return _container.StartCameraName;
            }
        }

        /// <summary>
        /// Изменён для сохранения
        /// </summary>
        bool _needSaveFile = false;

        /// <summary>
        /// Хранилище данных для редактора
        /// </summary>
        ContainerStage _container;

        /// <summary>
        /// Пакеты контролов
        /// </summary>
        ControlPackages _controlPackages;

        /// <summary>
        /// Инспектор юнитов
        /// </summary>
        EditorStageInspector _inspector;

        /// <summary>
        /// Основной документ для отображения графики 
        /// </summary>
        EditorStageIrrDocument _irrDocument;

        /// <summary>
        /// Окно свойств объекта
        /// </summary>
        EditorStageProperty _propertyWindow;

        /// <summary>
        /// Окно для создания юнитов
        /// </summary>
        EditorStageToolboxUnits _unitsWindow;

        /// <summary>
        /// Панель для редактирования моделей
        /// </summary>
        ToolStrip _irrTools;

        /// <summary>
        /// Базовые настройки редактора
        /// </summary>
        CommonEditorSettings _commonSettings;

        /// <summary>
        /// Выбранная камера сцены
        /// </summary>
        UnitInstanceCamera _activateCamera;

        /// <summary>
        /// Менеджер для undo / redo
        /// </summary>
        TransactionManager _transManager;

        public EditorStage()
        {
            InitializeComponent();

            _transManager = new TransactionManager(Helper.GetExtraTypes());
            _transManager.Commit += TransManager_Commit;

            BehaviorsPackage = new Dictionary<string, UnitBehavior>();

            _inspector = new EditorStageInspector(this, _transManager);
            _inspector.ItemCreated += ItemCreated;
            _inspector.ItemDeleted += ItemDeleted;
            _inspector.SelectionChanged += SelectionChanged;

            _irrDocument = new EditorStageIrrDocument(_transManager);

            _propertyWindow = new EditorStageProperty(_transManager);
            _propertyWindow.Changed += new EventHandler(PropertyWindow_Changed);

            _unitsWindow = new EditorStageToolboxUnits(this);

            var utilForm = new CommonUI.FormKeysWorker(this);
            utilForm.KeyFileEvents(NewFile, OpenFile, Save);

            Clear();
            _inspector.Enabled = false;

            if (!LoadUIState(false))
                ShowDefaultWindows();
        }

        /// <summary>
        /// Прочитать файл
        /// </summary>
        public void ReadFile(string path)
        {
            Clear();

            if (!File.Exists(path))
            {
                return;
            }

            FilePath = path;

            // Десерелизуем данные
            //
            XmlSerializer xmlSerelialize = new XmlSerializer(typeof(ContainerStage), Helper.GetExtraTypes());
            using (StreamReader reader = new StreamReader(FilePath, Encoding.UTF8))
            {
                _container = xmlSerelialize.Deserialize(reader) as ContainerStage;
                _container = _container.GetWithAbsolutePaths(FilePath);
            }
            foreach (string behaviorsPath in _container.UnitBehaviorPaths)
            {
                if (BehaviorsPackage.ContainsKey(behaviorsPath)) continue;
                UnitBehavior behavior = UnitBehavior.LoadFromFile(behaviorsPath);
                if (behavior == null)
                {
                    continue;
                }
                behavior.ToAbsolutePaths(behaviorsPath);
                BehaviorsPackage.Add(behaviorsPath, behavior);
            }

            // Копируем значения для ускорения загрузки
            var behaviorInTreeView = new Dictionary<string, UnitBehavior>();
            foreach (var pair in BehaviorsPackage)
                behaviorInTreeView.Add(pair.Key, pair.Value);
            _container.UpdateTreeViewTags(_container.TreeView.Nodes, behaviorInTreeView);
            _inspector.SetContainerTreeView(_container.TreeView);

            _container.ControlPackages.Reload();
            _controlPackages = _container.ControlPackages;

            _irrDocument.LoadModels(_inspector.GetInstances());
            if (_container != null)
            {
                _irrDocument.SetCamera(_container.Camera);
            }

            _unitsWindow.ReloadItems();

            UpdateFormTitle();
            EnableUI();

            _commonSettings.FileOpened(path);
            _commonSettings.FillMenu(_menuFiles, ReadFile);

            _transManager.Clear();
            _needSaveFile = false;
        }

        #region Общие

        /// <summary>
        /// Показать окно по умолчанию
        /// </summary>
        void ShowDefaultWindows()
        {
            _inspector.Show(_docPanel, DockState.DockLeft);
            _unitsWindow.Show(_docPanel, DockState.DockLeftAutoHide);
            _propertyWindow.Show(_docPanel, DockState.DockRight);
            _irrDocument.Show(_docPanel, DockState.Document);
        }

        /// <summary>
        /// Очистка данных
        /// </summary>
        void Clear()
        {
            _needSaveFile = false;
            _inspector.Clear();
            _irrDocument.Clear();
            FilePath = null;
            _container = new ContainerStage();
            _controlPackages = new ControlPackages();
            BehaviorsPackage.Clear();
            _transManager.Clear();
        }

        /// <summary>
        /// Принять изменения
        /// </summary>
        void Apply()
        {
            Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        bool Save()
        {
            if (FilePath == null)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Файл стадии (*.stage)|*.stage|" +
                             "Все файлы (*.*)|*.*";
                dialog.Title = "Сохранить файл стадии";
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return false;
                FilePath = dialog.FileName;
            }


            // Обновляем данные
            //
            _container.TreeView = _inspector.GetContainerTreeView();
            UpdateActivateCamera();
            _container.UnitBehaviorPaths = new List<string>();
            foreach (var path in BehaviorsPackage.Keys)
                _container.UnitBehaviorPaths.Add(path);
            _container.ControlPackages = _controlPackages;


            // Серелизуем данные для редактора
            //
            ContainerStage containerRelative = _container.GetWithRelativePaths(FilePath);
            XmlSerializer xmlSerelializeDsg = new XmlSerializer(typeof(ContainerStage), Helper.GetExtraTypes());
            using (StreamWriter writer = new StreamWriter(FilePath, false, Encoding.UTF8))
            {
                xmlSerelializeDsg.Serialize(writer, containerRelative);
            }

            // Обновить заголовок окна
            UpdateFormTitle();
            // Обновление отображения дерева объектов
            _inspector.UpdateTreeView();

            // Обновляем список последних открытых файлов
            _commonSettings.FileOpened(FilePath);
            _commonSettings.FillMenu(_menuFiles, ReadFile);

            _needSaveFile = false;

            return true;
        }

        /// <summary>
        /// Обновление данных о текущей камере
        /// </summary>
        void UpdateActivateCamera()
        {
            _container.Camera = _irrDocument.GetContainerCamera();
            if (_activateCamera != null)
            {
                _activateCamera.StartPosition = _container.Camera.Position;
                _activateCamera.StartTarget = _container.Camera.Target;

                UnitInstanceChanged(_activateCamera);
            }
        }

        /// <summary>
        /// Открыть файл
        /// </summary>
        void OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл стадии (*.stage)|*.stage|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть файл стадии";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            ReadFile(dialog.FileName);
        }

        /// <summary>
        /// Обновить заголовок окна
        /// </summary>
        private void UpdateFormTitle()
        {
            this.Text = string.Format("Редактор стадии ({0})", Path.GetFileNameWithoutExtension(FilePath));
        }

        /// <summary>
        /// Создание новой стадии
        /// </summary>
        public void NewFile()
        {
            Clear();
            // Делаем доступным UI
            EnableUI();
        }

        /// <summary>
        /// Сделать доступным UI
        /// </summary>
        private void EnableUI()
        {
            _saveToolStripMenuItem.Enabled = true;
            _menuItemSaveAs.Enabled = true;
            _inspector.Enabled = true;
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
        /// Принять все изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOk_Click(object sender, EventArgs e)
        {
            Close();
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

            // Сохраняем положение окон
            SaveUIState();
        }

        /// <summary>
        /// Сохраняет положение окон
        /// </summary>
        private void SaveUIState()
        {
            try
            {
                _commonSettings.Save(Path.Combine(UtilPath.AssemblyDirectory, "EditorStageUI.CommonUI.xml"));

                _docPanel.SaveAsXml(
                    Path.Combine(UtilPath.AssemblyDirectory, "EditorStageUI.xml"));
                var utilForm = new FormKeysWorker(this);
                FormWorker.SaveState(
                    Path.Combine(UtilPath.AssemblyDirectory, "EditorStageUIMain.xml"),
                    this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static CommonEditorSettings GetCommonSettings(string root)
        {
            return CommonEditorSettings.Open(
                Path.Combine(root, "EditorStageUI.CommonUI.xml"));
        }

        /// <summary>
        /// Загрузить положение окон
        /// </summary>
        private bool LoadUIState(bool onlyMain)
        {
            try
            {
                _commonSettings = GetCommonSettings(UtilPath.AssemblyDirectory);
                _commonSettings.FillMenu(_menuFiles, ReadFile);

                if (!onlyMain)
                {
                    string path = Path.Combine(UtilPath.AssemblyDirectory, "EditorStageUI.xml");
                    if (!File.Exists(path)) return false;
                    _docPanel.LoadFromXml(path, GetContentFromPersistString);
                }

                FormWorker.LoadState(
                    Path.Combine(UtilPath.AssemblyDirectory, "EditorStageUIMain.xml"),
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
            if (persistString == typeof(EditorStageInspector).ToString())
                return _inspector;
            else if (persistString == typeof(EditorStageIrrDocument).ToString())
                return _irrDocument;
            else if (persistString == typeof(EditorStageProperty).ToString())
                return _propertyWindow;
            else if (persistString == typeof(EditorStageToolboxUnits).ToString())
                return _unitsWindow;
            return null;
        }

        private void EditorStage_Shown(object sender, EventArgs e)
        {
            if (_irrTools == null)
            {
                var cntrl = new ControlPreviewTools(_irrDocument.Device);
                cntrl.NodeChanged += IrrNodeChanged;
                _irrTools = cntrl.ExtractToolStrip();
                _irrTools.Dock = DockStyle.None;
                _toolStripContainer.TopToolStripPanel.Join(_irrTools, _toolStripFile.Width + 5, 1);

                _irrDocument.PanelIrrView.MouseUp += new MouseEventHandler(PanelIrrView_MouseUp);
            }
        }

        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                UpdateItemDesc();
        }

        #endregion

        #region Основное меню

        /// <summary>
        /// Создание нового описания анимаций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemNewFile_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        void MenuItemOpenFile_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        /// <summary>
        /// Сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Сохранить как
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Файл контролов (*.stage)|*.stage|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Сохранить файл контролов";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            FilePath = dialog.FileName;
            Save();
        }


        /// <summary>
        /// Редактор контролов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemControlManager_Click(object sender, EventArgs e)
        {
            ControlManager manager = new ControlManager(_controlPackages);
            if (manager.ShowDialog(this) != DialogResult.OK) return;
            if (manager.Result != null)
                _controlPackages = manager.Result;
            _needSaveFile = true;
        }

        /// <summary>
        /// Редактирования начальных глобальных параметров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemStartParameters_Click(object sender, EventArgs e)
        {
            var controlEditor = new CommonUI.UITypeEditors.ControlEditorParameters();
            controlEditor.EditItem = _container.StartGlobalParameters;
            if (FormWorker.ShowDialog("Редактор", controlEditor) != System.Windows.Forms.DialogResult.OK)
                return;
            _container.StartGlobalParameters = controlEditor.EditItem;
            _needSaveFile = true;
        }

        /// <summary>
        /// Список кэшированных моделий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemCacheModelPaths_Click(object sender, EventArgs e)
        {
            ControlPathsEditing pathsEditing = new ControlPathsEditing(_container.CacheModelPaths, "(*.x)|*.x|");
            if (FormWorker.ShowDialog("Пути до моделий", pathsEditing, this) != System.Windows.Forms.DialogResult.OK)
                return;
            _container.CacheModelPaths = pathsEditing.Result;
            _needSaveFile = true;
        }

        /// <summary>
        /// Список кэшированных текстур
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemCacheTexturePaths_Click(object sender, EventArgs e)
        {
            ControlPathsEditing pathsEditing = new ControlPathsEditing(
                _container.CacheTexturePaths, "(*.png,*.jpeg,*.jpg)|*.png;*.jpeg;*.jpg|");
            if (FormWorker.ShowDialog("Пути до текстур", pathsEditing, this) != System.Windows.Forms.DialogResult.OK)
                return;
            _container.CacheTexturePaths = pathsEditing.Result;
            _needSaveFile = true;
        }

        /// <summary>
        /// Список кэшированных путей до XmlFiles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemCacheXmlPaths_Click(object sender, EventArgs e)
        {
            ControlPathsEditing pathsEditing = new ControlPathsEditing(_container.CacheXmlFiles, "(*.behavior)|*.behavior|");
            if (FormWorker.ShowDialog("Пути до файлов", pathsEditing, this) != System.Windows.Forms.DialogResult.OK)
                return;
            _container.CacheXmlFiles = pathsEditing.Result;
            _needSaveFile = true;
        }

        /// <summary>
        /// Список кэшированных путей до XmlFiles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemCacheScripts_Click(object sender, EventArgs e)
        {
            ControlPathsEditing pathsEditing = new ControlPathsEditing(_container.CacheScripts, "(*.sc)|*.sc|");
            if (FormWorker.ShowDialog("Пути до скриптов", pathsEditing, this) != System.Windows.Forms.DialogResult.OK)
                return;
            _container.CacheScripts = pathsEditing.Result;
            _needSaveFile = true;
        }

        /// <summary>
        /// Автоматическое исправление ошибок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemFixErrors_Click(object sender, EventArgs e)
        {
            StageValidator stageValidator = new StageValidator();
            List<Common.Message> msgs = stageValidator.FixErrors(_container);
            MessageBox.Show("Исправлено " + msgs.Count + " ошибок");
            _needSaveFile = true;
        }

        #endregion

        #region Редактирование объекта

        void SelectionChanged(object item)
        {
            UpdateItemDesc();
        }

        void ItemDeleted(object item)
        {
            UnitInstanceBase instance = item as UnitInstanceBase;
            if (instance == null)
            {
                return;
            }
            _irrDocument.DeleteSceneNode(instance);
            
            _needSaveFile = true;
        }

        void ItemCreated(object item)
        {
            _irrDocument.LoadModel(item as UnitInstanceBase);
            _needSaveFile = true;
        }

        void PropertyWindow_Changed(object sender, EventArgs e)
        {
            var instance = _propertyWindow.EditItem as UnitInstanceBase;
            UnitInstanceChanged(instance);
        }

        /// <summary>
        /// Обработка изменения объекта стадии
        /// </summary>
        /// <param name="instance"></param>
        void UnitInstanceChanged(UnitInstanceBase instance)
        {
            if (instance != null)
            {
                SceneNodeW sceneNode = _irrDocument.GetSceneNodeW(instance);
                if (sceneNode != null)
                {
                    NodeWorkerEx nodeWorker = new NodeWorkerEx(sceneNode);
                    nodeWorker.Apply(instance);
                }

                _needSaveFile = true;
            }
        }

        /// <summary>
        /// Обновление описания выбранного элемента
        /// </summary>
        void UpdateItemDesc()
        {
            if (_inspector.SelectedItem is UnitInstanceBase)
            {
                _irrDocument.SelectSceneNode(_inspector.SelectedItem as UnitInstanceBase);
            }
            _propertyWindow.EditItem = _inspector.SelectedItem;
        }

        #endregion

        void DocPanel_DragEnter(object sender, DragEventArgs e)
        {
            var item = e.Data.GetData(typeof(TreeNodeToolboxUnit)) as TreeNodeToolboxUnit;
            if (item == null)
            {
                return;
            }
            e.Effect = DragDropEffects.Copy;
        }

        void DocPanel_DragDrop(object sender, DragEventArgs e)
        {
            var item = e.Data.GetData(typeof(TreeNodeToolboxUnit)) as TreeNodeToolboxUnit;
            if (item == null)
            {
                return;
            }

            var cursorPos = System.Windows.Forms.Cursor.Position;
            Vertex3dW position = _irrDocument.GetPosition(cursorPos);
            var instance = _inspector.CreateInstanceStandard(item.FilePath);
            SceneNodeW sceneNode = _irrDocument.GetSceneNodeW(instance);
            if (sceneNode != null)
            {
                sceneNode.SetPosition(position);
                NodeWorkerEx nodeWorker = new NodeWorkerEx(sceneNode);
                nodeWorker.Update(instance);
            }
        }

        /// <summary>
        /// Обработчик изменения модели
        /// </summary>
        /// <param name="nodeW"></param>
        void IrrNodeChanged(SceneNodeW nodeW)
        {
            UnitInstanceBase instance = _inspector.GetInstance(nodeW);
            if (instance == null)
            {
                return;
            }

            using (Transaction trans = _transManager.StartTransaction())
            {
                trans.AddObject(instance);

                NodeWorkerEx nodeWorker = new NodeWorkerEx(nodeW);
                nodeWorker.Update(instance);

                trans.Commit();
            }
            _propertyWindow.EditItem = instance;

            _needSaveFile = true;
        }

        /// <summary>
        /// Клик по панели отображения графики
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PanelIrrView_MouseUp(object sender, MouseEventArgs e)
        {
            if (_irrDocument.Device == null) return;
            if (_irrDocument.Device.Selector.SelectionCount > 0)
            {
                var instance = _inspector.GetInstance(
                    _irrDocument.Device.Selector.SelectionResult[0]);
                _inspector.SelectedItem = instance;
            }
        }

        void FullUpdate()
        {
            _irrDocument.UpdateModels(_inspector.GetInstances());
            _unitsWindow.ReloadItems();
        }

        void EditorStage_KeyDown(object sender, KeyEventArgs e)
        {
            // Обработка операций undo / redo
            //
            if (e.KeyCode == Keys.Z && e.Control)
            {
                if (_transManager.CanUndo)
                {
                    _transManager.Undo();
                    FullUpdate();
                }
            }
            else if (e.KeyCode == Keys.Y && e.Control)
            {
                if (_transManager.CanRedo)
                {
                    _transManager.Redo();
                    FullUpdate();
                }
            }
        }

        /// <summary>
        /// Событие комита транзакции
        /// </summary>
        void TransManager_Commit()
        {
            _needSaveFile = true;
        }

        #region КАМЕРА

        /// <summary>
        /// Выбор перспективы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemCameraPerspective_Click(object sender, EventArgs e)
        {
            if (_activateCamera != null)
            {
                // Восстанавливаем видимость  пред. камеры
                SceneNodeW sceneNode = _irrDocument.GetSceneNodeW(_activateCamera);
                if (sceneNode != null) sceneNode.Visible = true;
            }
            UpdateActivateCamera();
            _activateCamera = null;
        }

        /// <summary>
        /// Выбор камеры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSelectCamera_Click(object sender, EventArgs e)
        {
            if (_activateCamera != null)
            {
                // Восстанавливаем видимость  пред. камеры
                SceneNodeW sceneNode = _irrDocument.GetSceneNodeW(_activateCamera);
                if (sceneNode != null) sceneNode.Visible = true;
            }
            _activateCamera = StageCameraWorker.SelectCamera(
                _inspector.GetContainerTreeView());
            if (_activateCamera != null)
            {
                ContainerCamera container = new ContainerCamera()
                {
                    Position = _activateCamera.StartPosition.Clone(),
                    Target = _activateCamera.StartTarget.Clone()
                };
                _irrDocument.SetCamera(container);
                SceneNodeW sceneNode = _irrDocument.GetSceneNodeW(_activateCamera);
                if (sceneNode != null) sceneNode.Visible = false;
            }
        }

        /// <summary>
        /// Выбрать начальную камеру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSelectStartCamera_Click(object sender, EventArgs e)
        {
            UnitInstanceCamera camera = StageCameraWorker.SelectCamera(
                            _inspector.GetContainerTreeView());
            if (camera != null)
            {
                _container.StartCameraName = camera.Name;
            }
        }

        #endregion


    }
}
