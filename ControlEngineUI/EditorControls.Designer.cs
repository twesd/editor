namespace ControlEngineUI
{
    partial class EditorControls
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
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorControls));
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._menuFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._menuItemModels = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemAddEnvModel = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemDeleteEnvModel = new System.Windows.Forms.ToolStripMenuItem();
            this.окнаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemWindowDefaultLocations = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this._dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this._toolStripFile = new System.Windows.Forms.ToolStrip();
            this._toolStripButtonOpenFile = new System.Windows.Forms.ToolStripButton();
            this._toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this._menuStrip.SuspendLayout();
            this._toolStripContainer.ContentPanel.SuspendLayout();
            this._toolStripContainer.TopToolStripPanel.SuspendLayout();
            this._toolStripContainer.SuspendLayout();
            this._toolStripFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuStrip
            // 
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuFiles,
            this._menuItemModels,
            this.окнаToolStripMenuItem});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Size = new System.Drawing.Size(1242, 24);
            this._menuStrip.TabIndex = 0;
            this._menuStrip.Text = "menuStrip1";
            // 
            // _menuFiles
            // 
            this._menuFiles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this._saveToolStripMenuItem,
            this._menuItemSaveAs,
            this.toolStripSeparator1});
            this._menuFiles.Name = "_menuFiles";
            this._menuFiles.Size = new System.Drawing.Size(48, 20);
            this._menuFiles.Text = "Файл";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.newToolStripMenuItem.Text = "Новый";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.MenuItemNewFile_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.openToolStripMenuItem.Text = "Открыть";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.MenuItemOpenFile_Click);
            // 
            // _saveToolStripMenuItem
            // 
            this._saveToolStripMenuItem.Enabled = false;
            this._saveToolStripMenuItem.Name = "_saveToolStripMenuItem";
            this._saveToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this._saveToolStripMenuItem.Text = "Сохранить";
            this._saveToolStripMenuItem.Click += new System.EventHandler(this.MenuItemSaveFile_Click);
            // 
            // _menuItemSaveAs
            // 
            this._menuItemSaveAs.Enabled = false;
            this._menuItemSaveAs.Name = "_menuItemSaveAs";
            this._menuItemSaveAs.Size = new System.Drawing.Size(153, 22);
            this._menuItemSaveAs.Text = "Сохранить как";
            this._menuItemSaveAs.Click += new System.EventHandler(this.MenuItemSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // _menuItemModels
            // 
            this._menuItemModels.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuItemAddEnvModel,
            this._menuItemDeleteEnvModel});
            this._menuItemModels.Enabled = false;
            this._menuItemModels.Name = "_menuItemModels";
            this._menuItemModels.Size = new System.Drawing.Size(62, 20);
            this._menuItemModels.Text = "Модель";
            // 
            // _menuItemAddEnvModel
            // 
            this._menuItemAddEnvModel.Name = "_menuItemAddEnvModel";
            this._menuItemAddEnvModel.Size = new System.Drawing.Size(199, 22);
            this._menuItemAddEnvModel.Text = "Добавить в окружение";
            this._menuItemAddEnvModel.Click += new System.EventHandler(this.MenuItemAddEnvModel_Click);
            // 
            // _menuItemDeleteEnvModel
            // 
            this._menuItemDeleteEnvModel.Name = "_menuItemDeleteEnvModel";
            this._menuItemDeleteEnvModel.Size = new System.Drawing.Size(199, 22);
            this._menuItemDeleteEnvModel.Text = "Удалить из окружения";
            this._menuItemDeleteEnvModel.Click += new System.EventHandler(this.MenuItemDeleteEnvModel_Click);
            // 
            // окнаToolStripMenuItem
            // 
            this.окнаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuItemWindowDefaultLocations});
            this.окнаToolStripMenuItem.Name = "окнаToolStripMenuItem";
            this.окнаToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.окнаToolStripMenuItem.Text = "Окна";
            // 
            // _menuItemWindowDefaultLocations
            // 
            this._menuItemWindowDefaultLocations.Name = "_menuItemWindowDefaultLocations";
            this._menuItemWindowDefaultLocations.Size = new System.Drawing.Size(235, 22);
            this._menuItemWindowDefaultLocations.Text = "Восстановить по умолчанию";
            this._menuItemWindowDefaultLocations.Click += new System.EventHandler(this.MenuItemWindowDefaultLocations_Click);
            // 
            // _toolStripContainer
            // 
            // 
            // _toolStripContainer.ContentPanel
            // 
            this._toolStripContainer.ContentPanel.Controls.Add(this._dockPanel);
            this._toolStripContainer.ContentPanel.Size = new System.Drawing.Size(1242, 602);
            this._toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._toolStripContainer.Location = new System.Drawing.Point(0, 24);
            this._toolStripContainer.Name = "_toolStripContainer";
            this._toolStripContainer.Size = new System.Drawing.Size(1242, 627);
            this._toolStripContainer.TabIndex = 7;
            this._toolStripContainer.Text = "toolStripContainer1";
            // 
            // _toolStripContainer.TopToolStripPanel
            // 
            this._toolStripContainer.TopToolStripPanel.Controls.Add(this._toolStripFile);
            // 
            // _dockPanel
            // 
            this._dockPanel.ActiveAutoHideContent = null;
            this._dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dockPanel.DockBackColor = System.Drawing.SystemColors.Control;
            this._dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
            this._dockPanel.Location = new System.Drawing.Point(0, 0);
            this._dockPanel.Name = "_dockPanel";
            this._dockPanel.Size = new System.Drawing.Size(1242, 602);
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            autoHideStripSkin1.TextFont = new System.Drawing.Font("Segoe UI", 9F);
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            dockPaneStripSkin1.TextFont = new System.Drawing.Font("Segoe UI", 9F);
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.InactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.InactiveCaptionText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this._dockPanel.Skin = dockPanelSkin1;
            this._dockPanel.TabIndex = 0;
            // 
            // _toolStripFile
            // 
            this._toolStripFile.Dock = System.Windows.Forms.DockStyle.None;
            this._toolStripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripButtonOpenFile,
            this._toolStripButtonSave});
            this._toolStripFile.Location = new System.Drawing.Point(3, 0);
            this._toolStripFile.Name = "_toolStripFile";
            this._toolStripFile.Size = new System.Drawing.Size(58, 25);
            this._toolStripFile.TabIndex = 9;
            this._toolStripFile.Text = "_toolStrip1";
            // 
            // _toolStripButtonOpenFile
            // 
            this._toolStripButtonOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("_toolStripButtonOpenFile.Image")));
            this._toolStripButtonOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonOpenFile.Name = "_toolStripButtonOpenFile";
            this._toolStripButtonOpenFile.Size = new System.Drawing.Size(23, 22);
            this._toolStripButtonOpenFile.Text = "Открыть файл";
            this._toolStripButtonOpenFile.Click += new System.EventHandler(this.MenuItemOpenFile_Click);
            // 
            // _toolStripButtonSave
            // 
            this._toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonSave.Image = global::ControlEngineUI.Properties.Resources.save;
            this._toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonSave.Name = "_toolStripButtonSave";
            this._toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
            this._toolStripButtonSave.Text = "Сохранить";
            this._toolStripButtonSave.Click += new System.EventHandler(this.MenuItemSaveFile_Click);
            // 
            // EditorControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 651);
            this.Controls.Add(this._toolStripContainer);
            this.Controls.Add(this._menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this._menuStrip;
            this.Name = "EditorControls";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор пакета управления";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Editor_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_KeyDown);
            
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this._toolStripContainer.ContentPanel.ResumeLayout(false);
            this._toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this._toolStripContainer.TopToolStripPanel.PerformLayout();
            this._toolStripContainer.ResumeLayout(false);
            this._toolStripContainer.PerformLayout();
            this._toolStripFile.ResumeLayout(false);
            this._toolStripFile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem _menuFiles;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _menuItemModels;
        private System.Windows.Forms.ToolStripMenuItem _menuItemAddEnvModel;
        private System.Windows.Forms.ToolStripMenuItem _menuItemDeleteEnvModel;
        private System.Windows.Forms.ToolStripMenuItem _saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _menuItemSaveAs;
        private System.Windows.Forms.ToolStripContainer _toolStripContainer;
        private WeifenLuo.WinFormsUI.Docking.DockPanel _dockPanel;
        private System.Windows.Forms.ToolStrip _toolStripFile;
        private System.Windows.Forms.ToolStripButton _toolStripButtonOpenFile;
        private System.Windows.Forms.ToolStripButton _toolStripButtonSave;
        private System.Windows.Forms.ToolStripMenuItem окнаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _menuItemWindowDefaultLocations;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}