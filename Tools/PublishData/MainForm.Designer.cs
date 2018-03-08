namespace PublishData
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._rootDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._selectInDir = new System.Windows.Forms.Button();
            this._selectOutDirs = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._dataGridViewOutDirs = new System.Windows.Forms.DataGridView();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._btnStart = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.проектToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._menuStripNew = new System.Windows.Forms.ToolStripMenuItem();
            this._menuStripOpen = new System.Windows.Forms.ToolStripMenuItem();
            this._menuStripSave = new System.Windows.Forms.ToolStripMenuItem();
            this._menuStripSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this._treeView = new System.Windows.Forms.TreeView();
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._propertyFolder = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewOutDirs)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // _rootDir
            // 
            this._rootDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._rootDir.Enabled = false;
            this._rootDir.Location = new System.Drawing.Point(12, 45);
            this._rootDir.Name = "_rootDir";
            this._rootDir.Size = new System.Drawing.Size(593, 20);
            this._rootDir.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Входная директория:";
            // 
            // _selectInDir
            // 
            this._selectInDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._selectInDir.Location = new System.Drawing.Point(611, 43);
            this._selectInDir.Name = "_selectInDir";
            this._selectInDir.Size = new System.Drawing.Size(75, 23);
            this._selectInDir.TabIndex = 2;
            this._selectInDir.Text = "...";
            this._selectInDir.UseVisualStyleBackColor = true;
            this._selectInDir.Click += new System.EventHandler(this.SelectInDir_Click);
            // 
            // _selectOutDirs
            // 
            this._selectOutDirs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._selectOutDirs.Location = new System.Drawing.Point(140, 297);
            this._selectOutDirs.Name = "_selectOutDirs";
            this._selectOutDirs.Size = new System.Drawing.Size(75, 23);
            this._selectOutDirs.TabIndex = 5;
            this._selectOutDirs.Text = "...";
            this._selectOutDirs.UseVisualStyleBackColor = true;
            this._selectOutDirs.Click += new System.EventHandler(this.SelectOutDirs_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 302);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Выходные директории:";
            // 
            // _dataGridViewOutDirs
            // 
            this._dataGridViewOutDirs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._dataGridViewOutDirs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewOutDirs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName});
            this._dataGridViewOutDirs.Location = new System.Drawing.Point(15, 326);
            this._dataGridViewOutDirs.Name = "_dataGridViewOutDirs";
            this._dataGridViewOutDirs.Size = new System.Drawing.Size(671, 132);
            this._dataGridViewOutDirs.TabIndex = 6;
            this._dataGridViewOutDirs.DoubleClick += new System.EventHandler(this.DataGridViewOutDirs_DoubleClick);
            // 
            // ColumnName
            // 
            this.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnName.HeaderText = "Путь до директории";
            this.ColumnName.Name = "ColumnName";
            // 
            // _btnStart
            // 
            this._btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnStart.Location = new System.Drawing.Point(593, 475);
            this._btnStart.Name = "_btnStart";
            this._btnStart.Size = new System.Drawing.Size(93, 23);
            this._btnStart.TabIndex = 8;
            this._btnStart.Text = "Старт";
            this._btnStart.UseVisualStyleBackColor = true;
            this._btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.проектToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(698, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // проектToolStripMenuItem
            // 
            this.проектToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuStripNew,
            this._menuStripOpen,
            this._menuStripSave,
            this._menuStripSaveAs});
            this.проектToolStripMenuItem.Name = "проектToolStripMenuItem";
            this.проектToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.проектToolStripMenuItem.Text = "Файл";
            // 
            // _menuStripNew
            // 
            this._menuStripNew.Name = "_menuStripNew";
            this._menuStripNew.Size = new System.Drawing.Size(165, 22);
            this._menuStripNew.Text = "Новый";
            this._menuStripNew.Click += new System.EventHandler(this.MenuStripNew_Click);
            // 
            // _menuStripOpen
            // 
            this._menuStripOpen.Name = "_menuStripOpen";
            this._menuStripOpen.Size = new System.Drawing.Size(165, 22);
            this._menuStripOpen.Text = "Открыть";
            this._menuStripOpen.Click += new System.EventHandler(this.MenuStripOpen_Click);
            // 
            // _menuStripSave
            // 
            this._menuStripSave.Name = "_menuStripSave";
            this._menuStripSave.Size = new System.Drawing.Size(165, 22);
            this._menuStripSave.Text = "Сохранить";
            this._menuStripSave.Click += new System.EventHandler(this.MenuStripSave_Click);
            // 
            // _menuStripSaveAs
            // 
            this._menuStripSaveAs.Name = "_menuStripSaveAs";
            this._menuStripSaveAs.Size = new System.Drawing.Size(165, 22);
            this._menuStripSaveAs.Text = "Сохранить как ...";
            this._menuStripSaveAs.Click += new System.EventHandler(this.MenuStripSaveAs_Click);
            // 
            // _treeView
            // 
            this._treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._treeView.Location = new System.Drawing.Point(0, 0);
            this._treeView.Name = "_treeView";
            this._treeView.Size = new System.Drawing.Size(333, 220);
            this._treeView.TabIndex = 10;
            this._treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
            // 
            // _splitContainer
            // 
            this._splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._splitContainer.Location = new System.Drawing.Point(12, 71);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._treeView);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._propertyFolder);
            this._splitContainer.Size = new System.Drawing.Size(676, 220);
            this._splitContainer.SplitterDistance = 333;
            this._splitContainer.TabIndex = 11;
            // 
            // _propertyFolder
            // 
            this._propertyFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._propertyFolder.Location = new System.Drawing.Point(0, 0);
            this._propertyFolder.Name = "_propertyFolder";
            this._propertyFolder.Size = new System.Drawing.Size(339, 220);
            this._propertyFolder.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 510);
            this.Controls.Add(this._splitContainer);
            this.Controls.Add(this._btnStart);
            this.Controls.Add(this._dataGridViewOutDirs);
            this.Controls.Add(this._selectOutDirs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._selectInDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._rootDir);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(541, 549);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PublishData";
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewOutDirs)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _rootDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _selectInDir;
        private System.Windows.Forms.Button _selectOutDirs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView _dataGridViewOutDirs;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.Button _btnStart;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem проектToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _menuStripOpen;
        private System.Windows.Forms.ToolStripMenuItem _menuStripSave;
        private System.Windows.Forms.ToolStripMenuItem _menuStripSaveAs;
        private System.Windows.Forms.ToolStripMenuItem _menuStripNew;
        private System.Windows.Forms.TreeView _treeView;
        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.PropertyGrid _propertyFolder;
    }
}

