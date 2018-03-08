namespace StageEngineUI
{
    partial class EditorStageInspector
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorStageInspector));
            this._textBoxFilter = new System.Windows.Forms.TextBox();
            this._treeView = new Aga.Controls.Tree.TreeViewAdv();
            this._contextMenuTreeItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._menuItemCreateInstanceStandard = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemCreateInstanceEnv = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemCreateInstanceArea = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьПустойЮнитToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьКамеруToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьГруппуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._contextDeleteAnim = new System.Windows.Forms.ToolStripMenuItem();
            this._nodeIcon = new Aga.Controls.Tree.NodeControls.NodeIcon();
            this._nodeTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this._imageTree = new System.Windows.Forms.ImageList(this.components);
            this._contextMenuTreeItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // _textBoxFilter
            // 
            this._textBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxFilter.Location = new System.Drawing.Point(0, 28);
            this._textBoxFilter.Name = "_textBoxFilter";
            this._textBoxFilter.Size = new System.Drawing.Size(226, 20);
            this._textBoxFilter.TabIndex = 8;
            // 
            // _treeView
            // 
            this._treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._treeView.BackColor = System.Drawing.SystemColors.Window;
            this._treeView.ContextMenuStrip = this._contextMenuTreeItem;
            this._treeView.DefaultToolTipProvider = null;
            this._treeView.DragDropMarkColor = System.Drawing.Color.Black;
            this._treeView.LineColor = System.Drawing.SystemColors.ControlDark;
            this._treeView.Location = new System.Drawing.Point(0, 6);
            this._treeView.Model = null;
            this._treeView.Name = "_treeView";
            this._treeView.NodeControls.Add(this._nodeIcon);
            this._treeView.NodeControls.Add(this._nodeTextBox);
            this._treeView.SelectedNode = null;
            this._treeView.Size = new System.Drawing.Size(226, 411);
            this._treeView.TabIndex = 6;
            this._treeView.SelectionChanged += new System.EventHandler(this.TreeView_SelectionChanged);
            this._treeView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TreeView_PreviewKeyDown);
            // 
            // _contextMenuTreeItem
            // 
            this._contextMenuTreeItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuItemCreateInstanceStandard,
            this._menuItemCreateInstanceEnv,
            this._menuItemCreateInstanceArea,
            this.создатьПустойЮнитToolStripMenuItem,
            this.создатьКамеруToolStripMenuItem,
            this.создатьГруппуToolStripMenuItem,
            this._contextDeleteAnim});
            this._contextMenuTreeItem.Name = "_contextMenuTreeAnim";
            this._contextMenuTreeItem.Size = new System.Drawing.Size(232, 180);
            // 
            // _menuItemCreateInstanceStandard
            // 
            this._menuItemCreateInstanceStandard.Name = "_menuItemCreateInstanceStandard";
            this._menuItemCreateInstanceStandard.Size = new System.Drawing.Size(231, 22);
            this._menuItemCreateInstanceStandard.Text = "Создать юнит (стандартный)";
            this._menuItemCreateInstanceStandard.Click += new System.EventHandler(this.CreateInstanceStandard_Click);
            // 
            // _menuItemCreateInstanceEnv
            // 
            this._menuItemCreateInstanceEnv.Name = "_menuItemCreateInstanceEnv";
            this._menuItemCreateInstanceEnv.Size = new System.Drawing.Size(231, 22);
            this._menuItemCreateInstanceEnv.Text = "Создать модель";
            this._menuItemCreateInstanceEnv.Click += new System.EventHandler(this.CreateInstanceEnv_Click);
            // 
            // _menuItemCreateInstanceArea
            // 
            this._menuItemCreateInstanceArea.Name = "_menuItemCreateInstanceArea";
            this._menuItemCreateInstanceArea.Size = new System.Drawing.Size(231, 22);
            this._menuItemCreateInstanceArea.Text = "Создать billboard";
            this._menuItemCreateInstanceArea.Click += new System.EventHandler(this.CreateInstanceBillboard_Click);
            // 
            // создатьПустойЮнитToolStripMenuItem
            // 
            this.создатьПустойЮнитToolStripMenuItem.Name = "создатьПустойЮнитToolStripMenuItem";
            this.создатьПустойЮнитToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.создатьПустойЮнитToolStripMenuItem.Text = "Создать пустую модель";
            this.создатьПустойЮнитToolStripMenuItem.Click += new System.EventHandler(this.CreateInstanceEmpty);
            // 
            // создатьКамеруToolStripMenuItem
            // 
            this.создатьКамеруToolStripMenuItem.Name = "создатьКамеруToolStripMenuItem";
            this.создатьКамеруToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.создатьКамеруToolStripMenuItem.Text = "Создать камеру";
            this.создатьКамеруToolStripMenuItem.Click += new System.EventHandler(this.CreateInstanceCamera_Click);
            // 
            // создатьГруппуToolStripMenuItem
            // 
            this.создатьГруппуToolStripMenuItem.Name = "создатьГруппуToolStripMenuItem";
            this.создатьГруппуToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.создатьГруппуToolStripMenuItem.Text = "Создать группу";
            this.создатьГруппуToolStripMenuItem.Click += new System.EventHandler(this.CreateGroup_Click);
            // 
            // _contextDeleteAnim
            // 
            this._contextDeleteAnim.Name = "_contextDeleteAnim";
            this._contextDeleteAnim.Size = new System.Drawing.Size(231, 22);
            this._contextDeleteAnim.Text = "Удалить";
            this._contextDeleteAnim.Click += new System.EventHandler(this.Delete_Click);
            // 
            // _nodeIcon
            // 
            this._nodeIcon.DataPropertyName = "Icon";
            this._nodeIcon.LeftMargin = 1;
            this._nodeIcon.ParentColumn = null;
            this._nodeIcon.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
            // 
            // _nodeTextBox
            // 
            this._nodeTextBox.DataPropertyName = "Text";
            this._nodeTextBox.EditEnabled = true;
            this._nodeTextBox.IncrementalSearchEnabled = true;
            this._nodeTextBox.LeftMargin = 3;
            this._nodeTextBox.ParentColumn = null;
            // 
            // _imageTree
            // 
            this._imageTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageTree.ImageStream")));
            this._imageTree.TransparentColor = System.Drawing.Color.Transparent;
            this._imageTree.Images.SetKeyName(0, "folder.ico");
            this._imageTree.Images.SetKeyName(1, "item.ico");
            this._imageTree.Images.SetKeyName(2, "event.ico");
            this._imageTree.Images.SetKeyName(3, "hand-point.ico");
            this._imageTree.Images.SetKeyName(4, "area.ico");
            this._imageTree.Images.SetKeyName(5, "pers.ico");
            this._imageTree.Images.SetKeyName(6, "home.ico");
            this._imageTree.Images.SetKeyName(7, "cameraStart.ico");
            this._imageTree.Images.SetKeyName(8, "camera.ico");
            // 
            // EditorStageInspector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 416);
            this.Controls.Add(this._treeView);
            this.Controls.Add(this._textBoxFilter);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditorStageInspector";
            this.Text = "Инспектор юнитов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorStageInspector_FormClosing);
            this._contextMenuTreeItem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _textBoxFilter;
        private Aga.Controls.Tree.TreeViewAdv _treeView;
        private System.Windows.Forms.ContextMenuStrip _contextMenuTreeItem;
        private System.Windows.Forms.ToolStripMenuItem _menuItemCreateInstanceStandard;
        private System.Windows.Forms.ToolStripMenuItem _menuItemCreateInstanceEnv;
        private System.Windows.Forms.ToolStripMenuItem _contextDeleteAnim;
        private System.Windows.Forms.ImageList _imageTree;
        private System.Windows.Forms.ToolStripMenuItem _menuItemCreateInstanceArea;
        private System.Windows.Forms.ToolStripMenuItem создатьКамеруToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьПустойЮнитToolStripMenuItem;
        private Aga.Controls.Tree.NodeControls.NodeIcon _nodeIcon;
        private Aga.Controls.Tree.NodeControls.NodeTextBox _nodeTextBox;
        private System.Windows.Forms.ToolStripMenuItem создатьГруппуToolStripMenuItem;
    }
}