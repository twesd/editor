using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PublishData
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Текущая директория сборки
        /// </summary>
        static string AssemblyDirectory
        {
            get
            {
                string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return System.IO.Path.GetDirectoryName(path);
            }
        }

        static string SettingsPath
        {
            get
            {
                return Path.Combine(AssemblyDirectory, "PublishDataSettings.stg");
            }
        }

        ProjectData _projectData;

        string _filename;

        public MainForm()
        {
            InitializeComponent();
            _projectData = new ProjectData();

            try
            {
                ProgramSettings prSettings = SerializeWorker.Load<ProgramSettings>(SettingsPath);
                if (File.Exists(prSettings.LastFileName))
                {
                    _projectData = ProjectWorker.Load(prSettings.LastFileName);
                    _filename = prSettings.LastFileName;
                    OpenRootFolder(_projectData.RootDir);
                    return;
                }
            }
            catch { }

            UpdateView();
            _filename = null;
        }

        /// <summary>
        /// Открыть проект
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuStripOpen_Click(object sender, EventArgs e)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Multiselect = false;
            dialog.Filter = "Project prjpub (*.prjpub)|*.prjpub";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            _projectData = ProjectWorker.Load(dialog.FileName);
            _filename = dialog.FileName;

            SaveSettings();
        }

        private void MenuStripSave_Click(object sender, EventArgs e)
        {
            if (_projectData == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(_filename))
            {
                var dialog = new System.Windows.Forms.SaveFileDialog();
                dialog.RestoreDirectory = true;
                dialog.Filter = "Project prjpub (*.prjpub)|*.prjpub";
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
                _filename = dialog.FileName;
            }
            Save(_filename);
        }

        private void MenuStripSaveAs_Click(object sender, EventArgs e)
        {
            if (_projectData == null)
            {
                return;
            }
            var dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Filter = "Project prjpub (*.prjpub)|*.prjpub";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            Save(dialog.FileName);
        }

        private void Save(string filename)
        {
            _projectData.DestDirs.Clear();
            foreach (DataGridViewRow row in _dataGridViewOutDirs.Rows)
            {
                if (row.Cells[0].Value == null)
                {
                    continue;
                }
                _projectData.DestDirs.Add(row.Cells[0].Value.ToString());
            }

            ProjectWorker.Save(filename, _projectData);
            _filename = filename;
            SaveSettings();
        }

        /// <summary>
        /// Новый проект
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuStripNew_Click(object sender, EventArgs e)
        {
            _projectData = new ProjectData();

            var dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Filter = "Project prjpub (*.prjpub)|*.prjpub";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            ProjectWorker.Save(dialog.FileName, _projectData);
            _filename = dialog.FileName;

            UpdateView();
        }

        private void SaveSettings()
        {
            try
            {
                ProgramSettings stg = new ProgramSettings();
                stg.LastFileName = _filename;
                SerializeWorker.Save<ProgramSettings>(SettingsPath, stg);
            }
            catch { }
        }

        private void UpdateView()
        {
            if (_projectData == null)
            {
                return;
            }

            _treeView.Nodes.Clear();
            AddFolders(_treeView.Nodes, new List<FolderData> { _projectData.RootFolder });
            _treeView.ExpandAll();

            _rootDir.Text = _projectData.RootDir;

            _dataGridViewOutDirs.Rows.Clear();
            foreach (string outDir in _projectData.DestDirs)
            {
                _dataGridViewOutDirs.Rows.Add(outDir);
            }
        }

        private void AddFolders(TreeNodeCollection rootNodes, List<FolderData> folders)
        {
            if (folders == null)
            {
                return;
            }
            foreach (FolderData folder in folders)
            {
                TreeNode node = rootNodes.Add(folder.Name);
                node.Tag = folder;
                AddFolders(node.Nodes, folder.Childs);
            }
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var sNode = _treeView.SelectedNode;
            if (sNode == null)
            {
                _propertyFolder.SelectedObject = null;
                return;
            }
            _propertyFolder.SelectedObject = sNode.Tag;
            _propertyFolder.ExpandAllGridItems();
        }

        private void DataGridViewOutDirs_DoubleClick(object sender, EventArgs e)
        {
            AddOutDir();
        }

        private void SelectOutDirs_Click(object sender, EventArgs e)
        {
            AddOutDir();
        }

        private void AddOutDir()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (File.Exists(_filename))
            {
                string dir = Path.GetDirectoryName(_filename);
                dialog.SelectedPath = dir;
            }

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _projectData.DestDirs.Add(dialog.SelectedPath);
            UpdateView();
        }

        private void SelectInDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (File.Exists(_filename))
            {
                string dir = Path.GetDirectoryName(_filename);
                dialog.SelectedPath = dir;
            }

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            OpenRootFolder(dialog.SelectedPath);
        }

        private void OpenRootFolder(string path)
        {
            _rootDir.Text = path;
            _projectData.RootDir = path;
            RebuildRootFolder();
            _treeView.ExpandAll();
        }

        private void RebuildRootFolder()
        {
            if (_projectData == null ||
                !Directory.Exists(_projectData.RootDir))
            {
                return;
            }

            ProjectData oldData = _projectData.Clone();

            var rootDir = _projectData.RootDir;

            string dirName = rootDir.Split(new char[] { System.IO.Path.DirectorySeparatorChar }, 
                StringSplitOptions.RemoveEmptyEntries).Last();
            FolderData mainFolder = new FolderData(dirName);
            _projectData.RootFolder = mainFolder;

            var dirInfo = new DirectoryInfo(rootDir);
            FillFolders(mainFolder, dirInfo);

            SyncFolders(new List<FolderData> { _projectData.RootFolder },
                new List<FolderData> { oldData.RootFolder });

            UpdateView();
        }

        private void SyncFolders(List<FolderData> newData, List<FolderData> oldData)
        {
            foreach (FolderData newDataFolder in newData)
            {
                FolderData oldDataItem = oldData.Find(x => x.Name == newDataFolder.Name);
                if (oldDataItem != null)
                {
                    newDataFolder.Settings = oldDataItem.Settings;
                    SyncFolders(newDataFolder.Childs, oldDataItem.Childs);
                }
            }
        }

        private void FillFolders(FolderData folder, DirectoryInfo dirInfo)
        {
            DirectoryInfo[] subdirs = dirInfo.GetDirectories();
            if (subdirs == null)
            {
                return;
            }
            foreach (DirectoryInfo subDir in subdirs)
            {
                if (subDir.Name.StartsWith("."))
                {
                    continue;
                }
                FolderData subFolder = new FolderData(subDir.Name);
                folder.Childs.Add(subFolder);
                FillFolders(subFolder, subDir);
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            ProcessWorker.Execute(_projectData);
            MessageBox.Show("Выполнено");
        }

    }
}
