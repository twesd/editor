using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlEngineUI;
using ControlEngine;
using Serializable;
using CommonUI;
using StageEngineUI;
using MainEditors.Main;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using Common;
using System.Xml.Serialization;
using TransactionCore;
using System.Configuration;
using System.Diagnostics;

namespace MainEditors
{
    public partial class EditorMain : Form
    {
        /// <summary>
        /// Основное хранилище данных
        /// </summary>
        ContainerMain _container;

        /// <summary>
        /// Пакеты контролов
        /// </summary>
        ControlPackages _cacheControlPackages;

        /// <summary>
        /// Инспектор юнитов
        /// </summary>
        EditorMainInspector _inspector;

        /// <summary>
        /// Окно свойств объекта
        /// </summary>
        EditorMainProperty _propertyWindow;

        /// <summary>
        /// Путь до файла
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// Изменён для сохранения
        /// </summary>
        bool _needSaveFile = false;

        /// <summary>
        /// Базовые настройки редактора
        /// </summary>
        CommonEditorSettings _commonSettings;

        /// <summary>
        /// Менеджер для undo / redo
        /// </summary>
        TransactionManager _transManager;

        public EditorMain()
        {
            InitializeComponent();

            _transManager = new TransactionManager(Helper.GetExtraTypes());

            var utilForm = new CommonUI.FormKeysWorker(this);
            utilForm.KeyFileEvents(NewFile, OpenFile, Save);

            _inspector = new EditorMainInspector(this, _transManager);
            _inspector.ItemCreated += ItemCreated;
            _inspector.ItemDeleted += ItemDeleted;
            _inspector.SelectionChanged += SelectionChanged;

            _propertyWindow = new EditorMainProperty();
            _propertyWindow.Changed += new EventHandler(PropertyWindow_Changed);

            Clear();
            _inspector.Enabled = false;

            if (!LoadUIState(false))
            {
                ShowDefaultWindows();
            }
        }

        /// <summary>
        /// Прочитать файл
        /// </summary>
        public void ReadFile(string path)
        {
            Clear();

            FilePath = path;

            // Десерелизуем данные
            //
            XmlSerializer xmlSerelialize = new XmlSerializer(typeof(ContainerMain), Helper.GetExtraTypes());
            using (StreamReader reader = new StreamReader(FilePath, Encoding.UTF8))
            {
                _container = xmlSerelialize.Deserialize(reader) as ContainerMain;
                _container = _container.GetWithAbsolutePaths(FilePath);
            }
            _inspector.SetContainerTreeView(_container.TreeView);

            UpdateTitle();
            EnableUI();

            _commonSettings.FileOpened(path);
            _commonSettings.FillMenu(_menuFiles, ReadFile);

            _transManager.Clear();
            _needSaveFile = false;
        }

        /// <summary>
        /// Открытие редактора стадии
        /// </summary>
        /// <param name="path"></param>
        public void OpenStage(string path)
        {
            string editorPath = GetEditorAppPath("StageEngineApp");
            if (!File.Exists(editorPath))
            {
                Report.Warn("Директория до редактора задана неверно " + editorPath);
                return;
            }

            if (!File.Exists(path))
            {
                path = string.Empty;
            }

            System.Diagnostics.Process.Start(editorPath, path);
        }

        #region Общие

        /// <summary>
        /// Сохраняет положение окон
        /// </summary>
        void SaveUIState()
        {
            try
            {
                _commonSettings.Save(Path.Combine(UtilPath.AssemblyDirectory, "EditorMainUI.CommonUI.xml"));

                _dockPanel.SaveAsXml(
                    Path.Combine(UtilPath.AssemblyDirectory, "EditorMainUI.xml"));
                var utilForm = new FormKeysWorker(this);
                FormWorker.SaveState(
                    Path.Combine(UtilPath.AssemblyDirectory, "EditorMainUIMain.xml"),
                    this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static CommonEditorSettings GetCommonSettings()
        {
            return CommonEditorSettings.Open(Path.Combine(UtilPath.AssemblyDirectory, "EditorMainUI.CommonUI.xml"));
        }

        /// <summary>
        /// Загрузить положение окон
        /// </summary>
        bool LoadUIState(bool onlyMain)
        {
            try
            {
                _commonSettings = GetCommonSettings();
                _commonSettings.FillMenu(_menuFiles, ReadFile);

                if (!onlyMain)
                {
                    string path = Path.Combine(UtilPath.AssemblyDirectory, "EditorMainUI.xml");
                    if (!File.Exists(path)) return false;
                    _dockPanel.LoadFromXml(path, GetContentFromPersistString);
                }

                FormWorker.LoadState(
                    Path.Combine(UtilPath.AssemblyDirectory, "EditorMainUIMain.xml"),
                    this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(EditorMainInspector).ToString())
                return _inspector;
            else if (persistString == typeof(EditorMainProperty).ToString())
                return _propertyWindow;
            return null;
        }

        /// <summary>
        /// Показать окно по умолчанию
        /// </summary>
        void ShowDefaultWindows()
        {
            _inspector.Show(_dockPanel, DockState.DockLeft);
            _propertyWindow.Show(_dockPanel, DockState.DockRight);
        }

        void Clear()
        {
            _needSaveFile = false;
            _container = new ContainerMain();
            _cacheControlPackages = _container.CacheControlPackages;
            _transManager.Clear();

            if (_inspector != null) _inspector.Clear();
        }

        /// <summary>
        /// Открыть файл
        /// </summary>
        void OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл игры (*.game)|*.game|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть файл игры";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            ReadFile(dialog.FileName);
        }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        bool Save()
        {
            if (FilePath == null)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Файл игры (*.game)|*.game|" +
                             "Все файлы (*.*)|*.*";
                dialog.Title = "Сохранить файл игры";
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return false;
                FilePath = dialog.FileName;
            }

            // Обновляем данные
            //
            _container.TreeView = _inspector.GetContainerTreeView();

            // Серелизуем данные для редактора
            //
            ContainerMain containerRelative = _container.GetWithRelativePaths(FilePath);
            XmlSerializer xmlSerelializeDsg = new XmlSerializer(typeof(ContainerMain), Helper.GetExtraTypes());
            using (StreamWriter writer = new StreamWriter(FilePath, false, Encoding.UTF8))
            {
                xmlSerelializeDsg.Serialize(writer, containerRelative);
            }

            _needSaveFile = false;
            UpdateTitle();

            return true;
        }

        /// <summary>
        /// Обновить заголовок окна
        /// </summary>
        void UpdateTitle()
        {
            this.Text = string.Format("Редактор игры ({0})", Path.GetFileNameWithoutExtension(FilePath));
        }

        /// <summary>
        /// Создание новой стадии
        /// </summary>
        void NewFile()
        {
            Clear();
            // Делаем доступным UI
            EnableUI();
        }

        /// <summary>
        /// Сделать доступным UI
        /// </summary>
        void EnableUI()
        {
            _inspector.Enabled = true;
        }

        void EditorMain_FormClosing(object sender, FormClosingEventArgs e)
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

            // Сохраняем положение окон и др.
            SaveUIState();
        }

        string GetEditorAppPath(string configItemName)
        {
            string editorPath = ConfigurationManager.AppSettings[configItemName];
            if (!File.Exists(editorPath))
            {
                editorPath = UtilPath.GetAbsolutePath(editorPath, UtilPath.AssemblyDirectory);
                if (!File.Exists(editorPath))
                {
                    return null;
                }
            }
            return editorPath;
        }
        

        #endregion

        #region Основное меню

        /// <summary>
        /// Добавить контролы в кэш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemAddControlPackageToCache_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл контролов (*.controls)|*.controls|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть файл контролов";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            _cacheControlPackages.Add(dialog.FileName);
        }

        /// <summary>
        /// Удалить контролы из кэша
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemDeleteControlPackageToCache_Click(object sender, EventArgs e)
        {
            if (_cacheControlPackages == null || _cacheControlPackages.Items.Count == 0)
            {
                MessageBox.Show("Пакеты отсутствуют");
                return;
            }

            ContainerTreeView contTreeView = new ContainerTreeView();
            foreach (string path in _cacheControlPackages.Items.Keys)
            {
                contTreeView.Nodes.Add(new SerializableTreeNode(
                    System.IO.Path.GetFileNameWithoutExtension(path),
                    path,
                    string.Empty));
            }
            FormSelectObject selectForm = new FormSelectObject(
                "Выберите тип", contTreeView);
            if (selectForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            _cacheControlPackages.Remove(selectForm.Result as string);
        }

        void ContextMenuStripEditors_Opening(object sender, CancelEventArgs e)
        {
            _contextMenuStripEditors.Items.Clear();

            ToolStrip ts = _contextMenuStripEditors.SourceControl as ToolStrip;
            ToolStripItem clickedItem = null;
            if (ts != null)
            {
                Point pos = ts.PointToClient(MousePosition);
                foreach (ToolStripItem item in ts.Items)
                    if (item.Bounds.Contains(pos)) clickedItem = item;
            }
            if (clickedItem == null)
            {
                e.Cancel = true;
                return;
            }

            ToolStripMenuItem menuItem;

            if (clickedItem == _toolStripButtonStage)
            {
                CommonEditorSettings settings = StageEngineUI.EditorStage.GetCommonSettings(                   
                   Path.GetDirectoryName(GetEditorAppPath("StageEngineApp")));
                if (settings != null)
                {
                    foreach (string filename in settings.RecentFiles)
                    {
                        menuItem = new ToolStripMenuItem();
                        menuItem.Text = filename;
                        menuItem.Click += new EventHandler(MenuItemOpenStage);
                        _contextMenuStripEditors.Items.Add(menuItem);
                    }
                }
            }
            else if (clickedItem == _toolStripButtonBehavior)
            {
                CommonEditorSettings settings = UnitEngineUI.EditorBehavior.GetCommonSettings(
                    Path.GetDirectoryName(GetEditorAppPath("UnitEngineApp")));
                if (settings != null)
                {
                    foreach (string filename in settings.RecentFiles)
                    {
                        menuItem = new ToolStripMenuItem();
                        menuItem.Text = filename;
                        menuItem.Click += new EventHandler(MenuItemOpenBehaviors);
                        _contextMenuStripEditors.Items.Add(menuItem);
                    }
                }
            }
            else if (clickedItem == _toolStripButtonEditorControls)
            {
                CommonEditorSettings settings = ControlEngineUI.EditorControls.GetCommonSettings(                   
                   Path.GetDirectoryName(GetEditorAppPath("ControlEngineApp")));
                if (settings != null)
                {
                    foreach (string filename in settings.RecentFiles)
                    {
                        menuItem = new ToolStripMenuItem();
                        menuItem.Text = filename;
                        menuItem.Click += new EventHandler(MenuItemOpenControls);
                        _contextMenuStripEditors.Items.Add(menuItem);
                    }
                }
            }
            if (_contextMenuStripEditors.Items.Count == 0)
            {
                menuItem = new ToolStripMenuItem();
                menuItem.Text = "Файлы отсутствуют";
                menuItem.Enabled = false;
                _contextMenuStripEditors.Items.Add(menuItem);
            }
        }

        /// <summary>
        /// Открыть редактор стадий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemOpenStage(object sender, EventArgs e)
        {
            string path = string.Empty;
            if (sender is ToolStripMenuItem)
            {
                path = (sender as ToolStripMenuItem).Text;
                if (!File.Exists(path))
                {
                    path = string.Empty;
                }
            }

            OpenStage(path);
        }

        /// <summary>
        /// Открыть редактор поведения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemOpenBehaviors(object sender, EventArgs e)
        {
            string editorPath = GetEditorAppPath("UnitEngineApp");
            if (!File.Exists(editorPath))
            {
                Report.Warn("Директория до редактора задана неверно " + editorPath);
                return;
            }

            string path = string.Empty;
            if (sender is ToolStripMenuItem)
            {
                path = (sender as ToolStripMenuItem).Text;
                if (!File.Exists(path))
                {
                    path = string.Empty;
                }
            }

            System.Diagnostics.Process.Start(editorPath, path);
        }

        /// <summary>
        /// Открыть редактор контролов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemOpenControls(object sender, EventArgs e)
        {
            string editorPath = GetEditorAppPath("ControlEngineApp");
            if (!File.Exists(editorPath))
            {
                Report.Warn("Директория до редактора задана неверно " + editorPath);
                return;
            }

            string path = string.Empty;
            if (sender is ToolStripMenuItem)
            {
                path = (sender as ToolStripMenuItem).Text;
                if (!File.Exists(path))
                {
                    path = string.Empty;
                }
            }

            System.Diagnostics.Process.Start(editorPath, path);
        }

        void MenuItemStartClientEngine(object sender, EventArgs e)
        {
            StartClientEngine("960_640");
        }

        private void MenuItemClientEngine960_640(object sender, EventArgs e)
        {
            StartClientEngine("960_640");
        }

        private void MenuItemClientEngine480_320(object sender, EventArgs e)
        {
            StartClientEngine("480_320");
        }

        private void MenuItemClientEngine1136_640(object sender, EventArgs e)
        {
            StartClientEngine("1136_640");
        }

        /// <summary>
        /// Создание нового описания анимаций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemNewFile(object sender, EventArgs e)
        {
            NewFile();
        }

        void MenuItemOpenFile(object sender, EventArgs e)
        {
            OpenFile();
        }

        /// <summary>
        /// Сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemSave(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Сохранить как
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemSaveAs(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Файл игры (*.game)|*.game|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Сохранить файл игры";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            FilePath = dialog.FileName;
            Save();
        }

        void StartClientEngine(string displayMode)
        {
            string path = GetEditorAppPath("ClientEngine");
            if (!File.Exists(path))
            {
                Report.Warn("Директория до редактора задана неверно " + path);
                return;
            }
            if (string.IsNullOrEmpty(FilePath))
            {
                Report.Warn("Отсутствуют открытые файлы игры");
                return;
            }
            System.Diagnostics.Process.Start(path,
                string.Format("\"{0}\" \"{1}\"",
                    Path.GetDirectoryName(FilePath),
                    displayMode));
        }

        /// <summary>
        /// Показать окна по умолчанию
        /// </summary>
        void RestoreWindowsMenuItem(object sender, EventArgs e)
        {
            ShowDefaultWindows();
        }

        #endregion

        #region Редактирование объекта

        void SelectionChanged(object item)
        {
            UpdateItemDesc();
        }

        void ItemDeleted(object item)
        {
            _needSaveFile = true;
        }

        void ItemCreated(object item)
        {
            _needSaveFile = true;
        }

        /// <summary>
        /// Обновление описания выбранного элемента
        /// </summary>
        void UpdateItemDesc()
        {
            _propertyWindow.EditItem = _inspector.SelectedItem;
        }

        void PropertyWindow_Changed(object sender, EventArgs e)
        {
            StageItem instance = _propertyWindow.EditItem as StageItem;
            _needSaveFile = true;
        }

        #endregion


    }
}
