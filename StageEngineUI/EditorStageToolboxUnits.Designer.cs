namespace StageEngineUI
{
    partial class EditorStageToolboxUnits
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorStageToolboxUnits));
            this._treeView = new Aga.Controls.Tree.TreeViewAdv();
            this._contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._menuItemCreate = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this._nodeIcon = new Aga.Controls.Tree.NodeControls.NodeIcon();
            this._nodeTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this._filter = new System.Windows.Forms.TextBox();
            this._contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _treeView
            // 
            this._treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._treeView.BackColor = System.Drawing.Color.White;
            this._treeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._treeView.ContextMenuStrip = this._contextMenuStrip;
            this._treeView.DefaultToolTipProvider = null;
            this._treeView.DragDropMarkColor = System.Drawing.Color.Black;
            this._treeView.LineColor = System.Drawing.SystemColors.ControlDark;
            this._treeView.Location = new System.Drawing.Point(1, 29);
            this._treeView.Model = null;
            this._treeView.Name = "_treeView";
            this._treeView.NodeControls.Add(this._nodeIcon);
            this._treeView.NodeControls.Add(this._nodeTextBox);
            this._treeView.SelectedNode = null;
            this._treeView.Size = new System.Drawing.Size(229, 411);
            this._treeView.TabIndex = 0;
            this._treeView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TreeView_PreviewKeyDown);
            // 
            // _contextMenuStrip
            // 
            this._contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuItemCreate,
            this._menuItemDelete});
            this._contextMenuStrip.Name = "_contextMenuStrip";
            this._contextMenuStrip.Size = new System.Drawing.Size(127, 48);
            // 
            // _menuItemCreate
            // 
            this._menuItemCreate.Name = "_menuItemCreate";
            this._menuItemCreate.Size = new System.Drawing.Size(126, 22);
            this._menuItemCreate.Text = "Добавить";
            this._menuItemCreate.Click += new System.EventHandler(this.MenuItemCreate_Click);
            // 
            // _menuItemDelete
            // 
            this._menuItemDelete.Name = "_menuItemDelete";
            this._menuItemDelete.Size = new System.Drawing.Size(126, 22);
            this._menuItemDelete.Text = "Удалить";
            this._menuItemDelete.Click += new System.EventHandler(this.MenuItemDelete_Click);
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
            this._nodeTextBox.IncrementalSearchEnabled = true;
            this._nodeTextBox.LeftMargin = 3;
            this._nodeTextBox.ParentColumn = null;
            // 
            // _filter
            // 
            this._filter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._filter.Location = new System.Drawing.Point(1, 3);
            this._filter.Name = "_filter";
            this._filter.Size = new System.Drawing.Size(229, 20);
            this._filter.TabIndex = 1;
            // 
            // EditorStageToolboxUnits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 441);
            this.Controls.Add(this._filter);
            this.Controls.Add(this._treeView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditorStageToolboxUnits";
            this.Text = "Юниты";
            this._contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Aga.Controls.Tree.TreeViewAdv _treeView;
        private System.Windows.Forms.TextBox _filter;
        private Aga.Controls.Tree.NodeControls.NodeIcon _nodeIcon;
        private Aga.Controls.Tree.NodeControls.NodeTextBox _nodeTextBox;
        private System.Windows.Forms.ContextMenuStrip _contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _menuItemCreate;
        private System.Windows.Forms.ToolStripMenuItem _menuItemDelete;
    }
}