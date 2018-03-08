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
using WeifenLuo.WinFormsUI.Docking;
using TransactionCore;


namespace ControlEngineUI
{
    public partial class EditorControls : Form
    {
        /// <summary>
        /// Никогда не закрывать форму, только прятать
        /// </summary>
        public bool NeverClose = false;

        /// <summary>
        /// Путь до файла
        /// </summary>
        string _path;

        /// <summary>
        /// Хранилище данных для редактора
        /// </summary>
        ControlPackage _controlPackage;

        /// <summary>
        /// Основной документ для отображения графики 
        /// </summary>
        EditorControlsIrrDocument _irrDocument;

        /// <summary>
        /// Инспектор объектов
        /// </summary>
        EditorControlsInspector _inspector;

        /// <summary>
        /// Окно свойств объекта
        /// </summary>
        EditorControlsProperty _propertyWindow;

        /// <summary>
        /// Базовые настройки редактора
        /// </summary>
        CommonEditorSettings _commonSettings;

        /// <summary>
        /// Обнаружены ли изменения
        /// </summary>
        bool _needSaveFile;

        /// <summary>
        /// Менеджер для undo / redo
        /// </summary>
        TransactionManager _transManager;
        
        public EditorControls()
        {
            InitializeComponent();

            _transManager = new TransactionManager(Helper.GetExtraTypes());
            _transManager.Commit += TransManager_Commit;

            _inspector = new EditorControlsInspector(this, _transManager);
            _inspector.ItemCreated += ItemCreated;
            _inspector.ItemDeleted += ItemDeleted;
            _inspector.SelectionChanged += SelectionChanged;

            _irrDocument = new EditorControlsIrrDocument(_inspector, _transManager);
            _irrDocument.Changed += new ChangedHandler(IrrDocument_Changed);

            _propertyWindow = new EditorControlsProperty(_transManager);
            _propertyWindow.Changed += new EventHandler(PropertyWindow_Changed);

            var utilForm = new CommonUI.FormKeysWorker(this);
            utilForm.KeyFileEvents(NewFile, OpenFile, Save);
            
            Clear();
            _inspector.Enabled = false;
            _propertyWindow.Enabled = false;
            _irrDocument.Enabled = false;

            if (!LoadUIState(false))
            {
                ShowDefaultWindows();
            }            
        }

        #region Общие

        /// <summary>
        /// Очистка данных
        /// </summary>
        private void Clear()
        {
            _path = null;
            _controlPackage = new ControlPackage();
            _inspector.Clear();
            _propertyWindow.EditItem = null;
            _transManager.Clear();
            XRefControlsWorker.ClearCache();
        }

        /// <summary>
        /// Принять изменения
        /// </summary>
        private void Apply()
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
                dialog.Filter = "Файл контролов (*.controls)|*.controls|" +
                             "Все файлы (*.*)|*.*";
                dialog.Title = "Сохранить файл контролов";
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return false;
                _path = dialog.FileName;
            }

            // Обновляем данные
            //
            _controlPackage.TreeView = _inspector.GetContainerTreeView();
            _controlPackage.Camera = _irrDocument.GetContainerCamera();

            ControlPackageWorker.Save(_path, _controlPackage);
            _needSaveFile = false;

            UpdateTitle(_path);
            _commonSettings.FileOpened(_path);
            _commonSettings.FillMenu(_menuFiles, ReadFile);

            _inspector.UpdateTreeView();
            return true;
        }

        /// <summary>
        /// Открыть файл
        /// </summary>
        private void OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл контролов (*.controls)|*.controls|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть файл контролов";
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

            if (!File.Exists(path))
            {
                return;
            }

            _path = path;

            _controlPackage = ControlPackageWorker.Read(_path);
            _inspector.SetContainerTreeView(_controlPackage.TreeView);

            _irrDocument.LoadModels(_controlPackage.EnviromentModels);
            if (_controlPackage.Camera != null)
                _irrDocument.UpdateCamera(_controlPackage.Camera);
            _irrDocument.UpdateIrrView();

            UpdateTitle(path);
            EnableUI();

            _commonSettings.FileOpened(path);
            _commonSettings.FillMenu(_menuFiles, ReadFile);


            _transManager.Clear();
            _needSaveFile = false;
        }

        /// <summary>
        /// Обновить заголовок окна
        /// </summary>
        /// <param name="path"></param>
        private void UpdateTitle(string path)
        {
            this.Text = string.Format("Редактор контролов ({0})", Path.GetFileNameWithoutExtension(path));
        }

        
        /// <summary>
        /// Показать окна по умолчанию
        /// </summary>
        private void MenuItemWindowDefaultLocations_Click(object sender, EventArgs e)
        {
            ShowDefaultWindows();
        }

        /// <summary>
        /// Показать окна по умолчанию
        /// </summary>
        private void ShowDefaultWindows()
        {
            _inspector.Show(_dockPanel, DockState.DockLeft);
            _propertyWindow.Show(_dockPanel, DockState.DockRight);
            _irrDocument.Show(_dockPanel, DockState.Document);
        }

        /// <summary>
        /// Сохраняет положение окон
        /// </summary>
        private void SaveUIState()
        {
            try
            {
                _commonSettings.Save(Path.Combine(UtilPath.AssemblyDirectory, "EditorControls.CommonUI.xml"));

                _dockPanel.SaveAsXml(
                    Path.Combine(UtilPath.AssemblyDirectory, "EditorControlsUI.xml"));
                
                FormWorker.SaveState(
                    Path.Combine(UtilPath.AssemblyDirectory, "EditorControlsUIMain.xml"),
                    this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Получить общие настройки
        /// </summary>
        /// <returns></returns>
        public static CommonEditorSettings GetCommonSettings(string root)
        {
            return CommonEditorSettings.Open(
                    Path.Combine(root, "EditorControls.CommonUI.xml"));
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
                    string path = Path.Combine(UtilPath.AssemblyDirectory, "EditorControlsUI.xml");
                    if (!File.Exists(path)) return false;
                    _dockPanel.LoadFromXml(path, GetContentFromPersistString);
                }

                FormWorker.LoadState(
                    Path.Combine(UtilPath.AssemblyDirectory, "EditorControlsUIMain.xml"),
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
            if (persistString == typeof(EditorControlsInspector).ToString())
                return _inspector;
            else if (persistString == typeof(EditorControlsIrrDocument).ToString())
                return _irrDocument;
            else if (persistString == typeof(EditorControlsProperty).ToString())
                return _propertyWindow;
            return null;
        }

        /// <summary>
        /// Событие комита транзакции
        /// </summary>
        void TransManager_Commit()
        {
            _needSaveFile = true;
        }
        
        private void MenuItemOpenFile_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        /// <summary>
        /// Создание нового описания анимаций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemNewFile_Click(object sender, EventArgs e)
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
        }

        /// <summary>
        /// Сделать доступным UI
        /// </summary>
        private void EnableUI()
        {
            _inspector.Enabled = true;
            _propertyWindow.Enabled = true;
            _irrDocument.Enabled = true;
                       
            _menuItemModels.Enabled = true;
            _saveToolStripMenuItem.Enabled = true;            
            _menuItemSaveAs.Enabled = true;
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

            if (NeverClose)
            {
                e.Cancel = true;
                Hide();
            }
            else
            {
                _irrDocument.Destroy();
            }

            // Сохраняем положение окон
            SaveUIState();
        }
        
        /// <summary>
        /// Обработка нажатия клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                UpdateState();
            }

            // Обработка операций undo / redo
            //
            if (e.KeyCode == Keys.Z && e.Control)
            {
                if (_transManager.CanUndo)
                {
                    _transManager.Undo();
                }
                UpdateState();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Y && e.Control)
            {
                if (_transManager.CanRedo)
                {
                   _transManager.Redo();
                }
                UpdateState();
                e.Handled = true;
            }
        }
        
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
            _controlPackage.EnviromentModels.Add(node);
            _irrDocument.LoadModels(_controlPackage.EnviromentModels);
        }

        /// <summary>
        /// Удалить дополнительную модель
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDeleteEnvModel_Click(object sender, EventArgs e)
        {
            if (_controlPackage == null || _controlPackage.EnviromentModels.Count == 0)
            {
                MessageBox.Show("Дополнительные модели отсутствуют");
                return;
            }

            ContainerTreeView contTreeView = new ContainerTreeView();
            foreach (ContainerNode envNode in _controlPackage.EnviromentModels)
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
            _controlPackage.EnviromentModels.Remove(selectForm.Result as ContainerNode);

            _irrDocument.LoadModels(_controlPackage.EnviromentModels);
        }

        /// <summary>
        /// Сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSaveFile_Click(object sender, EventArgs e)
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
            dialog.Filter = "Файл контролов (*.controls)|*.controls|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Сохранить файл контролов";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            _path = dialog.FileName;
            Save();
        }

        #endregion

        #region Редактирование объекта

        void SelectionChanged(object item)
        {
            UpdateState();
        }

        void ItemDeleted(object item)
        {
            _irrDocument.UpdateIrrView();
            _needSaveFile = true;
        }

        void ItemCreated(object item)
        {
            _irrDocument.UpdateIrrView();
            _needSaveFile = true;
        }
        
        /// <summary>
        /// Изменение свойства объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PropertyWindow_Changed(object sender, EventArgs e)
        {   
            if (_propertyWindow.EditItem as ControlBase != null)
            {
                UpdateState();
            }
            _needSaveFile = true;

            // Оповещаем инспектор об изменении объекта
            //
            var eProp = e as PropertyValueChangedEventArgs;
            _inspector.PropertyChanged(
                _propertyWindow.EditItem,
                eProp.ChangedItem.PropertyDescriptor.Name,
                eProp.ChangedItem.Value);

            if(_propertyWindow.EditItem is ControlButton)
            {
                // Пытаем подставить второе значение для текстуры
                var cntrlBtn = _propertyWindow.EditItem as ControlButton;
                string propName = eProp.ChangedItem.PropertyDescriptor.Name;
                if (propName == ReflectionUtility.GetPropertyName(() => cntrlBtn.TextureNormal) &&
                    !string.IsNullOrEmpty(cntrlBtn.TextureNormal))
                {
                    string secondTexture = string.Format("{0}/{1}Activate.png",
                        Path.GetDirectoryName(cntrlBtn.TextureNormal),
                        Path.GetFileNameWithoutExtension(cntrlBtn.TextureNormal));
                    if (File.Exists(secondTexture) &&
                        string.IsNullOrEmpty(cntrlBtn.TextureActivate))
                    {
                        cntrlBtn.TextureActivate = secondTexture;
                    }
                }
                else if (propName == ReflectionUtility.GetPropertyName(() => cntrlBtn.TextureActivate) &&
                    !string.IsNullOrEmpty(cntrlBtn.TextureActivate))
                {
                    string fname = Path.GetFileName(cntrlBtn.TextureActivate);
                    fname = fname.Replace("Activate.png", "");
                    string secondTexture = string.Format("{0}/{1}.png",
                        Path.GetDirectoryName(cntrlBtn.TextureActivate),
                        fname);
                    if (File.Exists(secondTexture) &&
                        string.IsNullOrEmpty(cntrlBtn.TextureNormal))
                    {
                        cntrlBtn.TextureNormal = secondTexture;
                    }
                }
            }
        }

        /// <summary>
        /// Обработка изменения объекта
        /// </summary>
        /// <param name="oEdit"></param>
        void IrrDocument_Changed(object oEdit)
        {
            _needSaveFile = true;
            UpdateState();

            IControlSizeable sizebale = oEdit as IControlSizeable;
            if (sizebale == null)
            {
                return;
            }

            // Оповещаем инспектор об изменении объекта
            //
            string propName = ReflectionUtility.GetPropertyName(() => sizebale.Position);
            _inspector.PropertyChanged(
                sizebale,
                propName,
                sizebale.Position);
        }

        /// <summary>
        /// Обновление описания выбранного элемента
        /// </summary>
        void UpdateState()
        {
            _irrDocument.SetSelection(_inspector.SelectedItem);
            _propertyWindow.EditItem = _inspector.SelectedItem;
            _irrDocument.UpdateIrrView();
            _inspector.UpdateTreeView();
        }

        #endregion

    }
}
