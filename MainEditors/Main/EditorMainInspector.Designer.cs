namespace MainEditors.Main
{
    partial class EditorMainInspector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorMainInspector));
            this._treeView = new Aga.Controls.Tree.TreeViewAdv();
            this._contextMenuTreeItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemStartStage = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemCreateInstanceStandard = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьГруппуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._contextDeleteAnim = new System.Windows.Forms.ToolStripMenuItem();
            this._nodeIcon = new Aga.Controls.Tree.NodeControls.NodeIcon();
            this._nodeTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._toolStripSplitButtonAdd = new System.Windows.Forms.ToolStripSplitButton();
            this._MenuItemGroup = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemStandard = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this._textBoxFilter = new System.Windows.Forms.TextBox();
            this._contextMenuTreeItem.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
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
            this._treeView.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this._treeView.LineColor = System.Drawing.SystemColors.ControlDark;
            this._treeView.Location = new System.Drawing.Point(1, 54);
            this._treeView.Model = null;
            this._treeView.Name = "_treeView";
            this._treeView.NodeControls.Add(this._nodeIcon);
            this._treeView.NodeControls.Add(this._nodeTextBox);
            this._treeView.SelectedNode = null;
            this._treeView.Size = new System.Drawing.Size(246, 335);
            this._treeView.TabIndex = 10;
            this._treeView.NodeMouseClick += new System.EventHandler<Aga.Controls.Tree.TreeNodeAdvMouseEventArgs>(this.TreeView_NodeMouseClick);
            this._treeView.NodeMouseDoubleClick += new System.EventHandler<Aga.Controls.Tree.TreeNodeAdvMouseEventArgs>(this.TreeView_NodeMouseDoubleClick);
            this._treeView.SelectionChanged += new System.EventHandler(this.TreeView_SelectionChanged);
            this._treeView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TreeViewItems_PreviewKeyDown);
            // 
            // _contextMenuTreeItem
            // 
            this._contextMenuTreeItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuItemOpen,
            this._menuItemStartStage,
            this._menuItemCreateInstanceStandard,
            this.создатьГруппуToolStripMenuItem,
            this._contextDeleteAnim});
            this._contextMenuTreeItem.Name = "_contextMenuTreeAnim";
            this._contextMenuTreeItem.Size = new System.Drawing.Size(200, 136);
            // 
            // _menuItemOpen
            // 
            this._menuItemOpen.Name = "_menuItemOpen";
            this._menuItemOpen.Size = new System.Drawing.Size(199, 22);
            this._menuItemOpen.Text = "Открыть";
            this._menuItemOpen.Visible = false;
            this._menuItemOpen.Click += new System.EventHandler(this.MenuItemOpenStage);
            // 
            // _menuItemStartStage
            // 
            this._menuItemStartStage.Name = "_menuItemStartStage";
            this._menuItemStartStage.Size = new System.Drawing.Size(199, 22);
            this._menuItemStartStage.Text = "Установить начальной";
            this._menuItemStartStage.Visible = false;
            this._menuItemStartStage.Click += new System.EventHandler(this.MenuItemStartStage);
            // 
            // _menuItemCreateInstanceStandard
            // 
            this._menuItemCreateInstanceStandard.Name = "_menuItemCreateInstanceStandard";
            this._menuItemCreateInstanceStandard.Size = new System.Drawing.Size(199, 22);
            this._menuItemCreateInstanceStandard.Text = "Добавить стадию";
            this._menuItemCreateInstanceStandard.Click += new System.EventHandler(this.MenuItemCreateStageItem);
            // 
            // создатьГруппуToolStripMenuItem
            // 
            this.создатьГруппуToolStripMenuItem.Name = "создатьГруппуToolStripMenuItem";
            this.создатьГруппуToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.создатьГруппуToolStripMenuItem.Text = "Создать группу";
            this.создатьГруппуToolStripMenuItem.Click += new System.EventHandler(this.CreateGroup);
            // 
            // _contextDeleteAnim
            // 
            this._contextDeleteAnim.Name = "_contextDeleteAnim";
            this._contextDeleteAnim.Size = new System.Drawing.Size(199, 22);
            this._contextDeleteAnim.Text = "Удалить";
            this._contextDeleteAnim.Click += new System.EventHandler(this.MenuItemDelete_Click);
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
            // _toolStrip
            // 
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripSplitButtonAdd,
            this._toolStripButtonDelete,
            this.toolStripSeparator1,
            this._toolStripButtonRefresh});
            this._toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Padding = new System.Windows.Forms.Padding(5, 2, 1, 0);
            this._toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this._toolStrip.Size = new System.Drawing.Size(248, 25);
            this._toolStrip.TabIndex = 12;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _toolStripSplitButtonAdd
            // 
            this._toolStripSplitButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripSplitButtonAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._MenuItemGroup,
            this._menuItemStandard});
            this._toolStripSplitButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("_toolStripSplitButtonAdd.Image")));
            this._toolStripSplitButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripSplitButtonAdd.Name = "_toolStripSplitButtonAdd";
            this._toolStripSplitButtonAdd.Size = new System.Drawing.Size(32, 20);
            this._toolStripSplitButtonAdd.Text = "toolStripSplitButton1";
            this._toolStripSplitButtonAdd.ToolTipText = "Создать";
            this._toolStripSplitButtonAdd.ButtonClick += new System.EventHandler(this.MenuItemCreateStageItem);
            // 
            // _MenuItemGroup
            // 
            this._MenuItemGroup.Name = "_MenuItemGroup";
            this._MenuItemGroup.Size = new System.Drawing.Size(169, 22);
            this._MenuItemGroup.Text = "Группу";
            this._MenuItemGroup.Click += new System.EventHandler(this.CreateGroup);
            // 
            // _menuItemStandard
            // 
            this._menuItemStandard.Name = "_menuItemStandard";
            this._menuItemStandard.Size = new System.Drawing.Size(169, 22);
            this._menuItemStandard.Text = "Добавить стадию";
            this._menuItemStandard.Click += new System.EventHandler(this.MenuItemCreateStageItem);
            // 
            // _toolStripButtonDelete
            // 
            this._toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("_toolStripButtonDelete.Image")));
            this._toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonDelete.Name = "_toolStripButtonDelete";
            this._toolStripButtonDelete.Size = new System.Drawing.Size(23, 20);
            this._toolStripButtonDelete.Text = "Удалить";
            this._toolStripButtonDelete.Click += new System.EventHandler(this.MenuItemDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // _toolStripButtonRefresh
            // 
            this._toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("_toolStripButtonRefresh.Image")));
            this._toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonRefresh.Name = "_toolStripButtonRefresh";
            this._toolStripButtonRefresh.Size = new System.Drawing.Size(23, 20);
            this._toolStripButtonRefresh.Text = "Обновить";
            this._toolStripButtonRefresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // _textBoxFilter
            // 
            this._textBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxFilter.Location = new System.Drawing.Point(0, 28);
            this._textBoxFilter.Name = "_textBoxFilter";
            this._textBoxFilter.Size = new System.Drawing.Size(247, 20);
            this._textBoxFilter.TabIndex = 11;
            // 
            // EditorMainInspector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 392);
            this.Controls.Add(this._treeView);
            this.Controls.Add(this._toolStrip);
            this.Controls.Add(this._textBoxFilter);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditorMainInspector";
            this.Text = "MainEditorInspector";
            this._contextMenuTreeItem.ResumeLayout(false);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Aga.Controls.Tree.TreeViewAdv _treeView;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripSplitButton _toolStripSplitButtonAdd;
        private System.Windows.Forms.ToolStripMenuItem _MenuItemGroup;
        private System.Windows.Forms.ToolStripMenuItem _menuItemStandard;
        private System.Windows.Forms.ToolStripButton _toolStripButtonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _toolStripButtonRefresh;
        private System.Windows.Forms.TextBox _textBoxFilter;
        private System.Windows.Forms.ContextMenuStrip _contextMenuTreeItem;
        private System.Windows.Forms.ToolStripMenuItem _menuItemCreateInstanceStandard;
        private System.Windows.Forms.ToolStripMenuItem _contextDeleteAnim;
        private System.Windows.Forms.ToolStripMenuItem создатьГруппуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem _menuItemStartStage;
        private Aga.Controls.Tree.NodeControls.NodeIcon _nodeIcon;
        private Aga.Controls.Tree.NodeControls.NodeTextBox _nodeTextBox;
    }
}