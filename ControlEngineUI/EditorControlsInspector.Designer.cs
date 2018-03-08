namespace ControlEngineUI
{
    partial class EditorControlsInspector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorControlsInspector));
            this._panelData = new System.Windows.Forms.Panel();
            this._textBoxFilter = new System.Windows.Forms.TextBox();
            this._treeView = new Aga.Controls.Tree.TreeViewAdv();
            this._contextMenuTreeItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._contextNewButton = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemCreateImage = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemCreateText = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemNewTapScene = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemCreateBehavior = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemCreatePanel = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemCreateCicrle = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemCreateXRef = new System.Windows.Forms.ToolStripMenuItem();
            this._contextDeleteAnim = new System.Windows.Forms.ToolStripMenuItem();
            this._nodeIconTreeView = new Aga.Controls.Tree.NodeControls.NodeIcon();
            this._nodeTextBoxTreeView = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this._imageTree = new System.Windows.Forms.ImageList(this.components);
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._toolStripSplitButtonAdd = new System.Windows.Forms.ToolStripSplitButton();
            this._MenuItemGroup = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._menuItemCreateGroup = new System.Windows.Forms.ToolStripMenuItem();
            this._panelData.SuspendLayout();
            this._contextMenuTreeItem.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _panelData
            // 
            this._panelData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panelData.Controls.Add(this._toolStrip);
            this._panelData.Controls.Add(this._textBoxFilter);
            this._panelData.Controls.Add(this._treeView);
            this._panelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelData.Location = new System.Drawing.Point(0, 0);
            this._panelData.Name = "_panelData";
            this._panelData.Size = new System.Drawing.Size(426, 479);
            this._panelData.TabIndex = 2;
            // 
            // _textBoxFilter
            // 
            this._textBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxFilter.Location = new System.Drawing.Point(3, 26);
            this._textBoxFilter.Name = "_textBoxFilter";
            this._textBoxFilter.Size = new System.Drawing.Size(417, 20);
            this._textBoxFilter.TabIndex = 4;
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
            this._treeView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._treeView.LineColor = System.Drawing.SystemColors.ControlDarkDark;
            this._treeView.Location = new System.Drawing.Point(3, 52);
            this._treeView.Model = null;
            this._treeView.Name = "_treeView";
            this._treeView.NodeControls.Add(this._nodeIconTreeView);
            this._treeView.NodeControls.Add(this._nodeTextBoxTreeView);
            this._treeView.SelectedNode = null;
            this._treeView.Size = new System.Drawing.Size(418, 414);
            this._treeView.TabIndex = 0;
            this._treeView.SelectionChanged += new System.EventHandler(this.TreeView_SelectionChanged);
            this._treeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView_DragDrop);
            this._treeView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TreeViewItems_PreviewKeyDown);
            // 
            // _contextMenuTreeItem
            // 
            this._contextMenuTreeItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._contextNewButton,
            this._menuItemCreateImage,
            this._menuItemCreateText,
            this._menuItemNewTapScene,
            this._menuItemCreateBehavior,
            this._menuItemCreatePanel,
            this._menuItemCreateCicrle,
            this._menuItemCreateXRef,
            this._menuItemCreateGroup,
            this._contextDeleteAnim});
            this._contextMenuTreeItem.Name = "_contextMenuTreeAnim";
            this._contextMenuTreeItem.Size = new System.Drawing.Size(228, 246);
            // 
            // _contextNewButton
            // 
            this._contextNewButton.Name = "_contextNewButton";
            this._contextNewButton.Size = new System.Drawing.Size(227, 22);
            this._contextNewButton.Text = "Создать кнопку";
            this._contextNewButton.Click += new System.EventHandler(this.MenuItemCreateButton);
            // 
            // _menuItemCreateImage
            // 
            this._menuItemCreateImage.Name = "_menuItemCreateImage";
            this._menuItemCreateImage.Size = new System.Drawing.Size(227, 22);
            this._menuItemCreateImage.Text = "Создать изображение";
            this._menuItemCreateImage.Click += new System.EventHandler(this.MenuItemCreateImage);
            // 
            // _menuItemCreateText
            // 
            this._menuItemCreateText.Name = "_menuItemCreateText";
            this._menuItemCreateText.Size = new System.Drawing.Size(227, 22);
            this._menuItemCreateText.Text = "Создать текст";
            this._menuItemCreateText.Click += new System.EventHandler(this.MenuItemCreateText);
            // 
            // _menuItemNewTapScene
            // 
            this._menuItemNewTapScene.Name = "_menuItemNewTapScene";
            this._menuItemNewTapScene.Size = new System.Drawing.Size(227, 22);
            this._menuItemNewTapScene.Text = "Создать клик по сцене";
            this._menuItemNewTapScene.Click += new System.EventHandler(this.MenuItemCreateTapScene);
            // 
            // _menuItemCreateBehavior
            // 
            this._menuItemCreateBehavior.Name = "_menuItemCreateBehavior";
            this._menuItemCreateBehavior.Size = new System.Drawing.Size(227, 22);
            this._menuItemCreateBehavior.Text = "Создать элемент поведение";
            this._menuItemCreateBehavior.Click += new System.EventHandler(this.MenuItemCreateControlBehavior);
            // 
            // _menuItemCreatePanel
            // 
            this._menuItemCreatePanel.Name = "_menuItemCreatePanel";
            this._menuItemCreatePanel.Size = new System.Drawing.Size(227, 22);
            this._menuItemCreatePanel.Text = "Создать панель";
            this._menuItemCreatePanel.Click += new System.EventHandler(this.MenuItemCreatePanel);
            // 
            // _menuItemCreateCicrle
            // 
            this._menuItemCreateCicrle.Name = "_menuItemCreateCicrle";
            this._menuItemCreateCicrle.Size = new System.Drawing.Size(227, 22);
            this._menuItemCreateCicrle.Text = "Создать круговой элемент";
            this._menuItemCreateCicrle.Click += new System.EventHandler(this.MenuItemCreateCircle);
            // 
            // _menuItemCreateXRef
            // 
            this._menuItemCreateXRef.Name = "_menuItemCreateXRef";
            this._menuItemCreateXRef.Size = new System.Drawing.Size(227, 22);
            this._menuItemCreateXRef.Text = "Подключить ссылку";
            this._menuItemCreateXRef.Click += new System.EventHandler(this.MenuItemCreateXRef);
            // 
            // _contextDeleteAnim
            // 
            this._contextDeleteAnim.Name = "_contextDeleteAnim";
            this._contextDeleteAnim.Size = new System.Drawing.Size(227, 22);
            this._contextDeleteAnim.Text = "Удалить";
            this._contextDeleteAnim.Click += new System.EventHandler(this.MenuItemDelete);
            // 
            // _nodeIconTreeView
            // 
            this._nodeIconTreeView.DataPropertyName = "Icon";
            this._nodeIconTreeView.LeftMargin = 1;
            this._nodeIconTreeView.ParentColumn = null;
            this._nodeIconTreeView.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Fit;
            // 
            // _nodeTextBoxTreeView
            // 
            this._nodeTextBoxTreeView.DataPropertyName = "Text";
            this._nodeTextBoxTreeView.EditEnabled = true;
            this._nodeTextBoxTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._nodeTextBoxTreeView.IncrementalSearchEnabled = true;
            this._nodeTextBoxTreeView.LeftMargin = 3;
            this._nodeTextBoxTreeView.ParentColumn = null;
            // 
            // _imageTree
            // 
            this._imageTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageTree.ImageStream")));
            this._imageTree.TransparentColor = System.Drawing.Color.Transparent;
            this._imageTree.Images.SetKeyName(0, "folder.ico");
            this._imageTree.Images.SetKeyName(1, "item.ico");
            this._imageTree.Images.SetKeyName(2, "event.ico");
            this._imageTree.Images.SetKeyName(3, "hand-point.ico");
            this._imageTree.Images.SetKeyName(4, "behavior.ico");
            this._imageTree.Images.SetKeyName(5, "text.ico");
            this._imageTree.Images.SetKeyName(6, "image.ico");
            this._imageTree.Images.SetKeyName(7, "button.ico");
            this._imageTree.Images.SetKeyName(8, "circle.ico");
            // 
            // _toolStrip
            // 
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripSplitButtonAdd,
            this._toolStripButtonDelete,
            this.toolStripSeparator1});
            this._toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Padding = new System.Windows.Forms.Padding(5, 2, 1, 0);
            this._toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this._toolStrip.Size = new System.Drawing.Size(424, 25);
            this._toolStrip.TabIndex = 14;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _toolStripSplitButtonAdd
            // 
            this._toolStripSplitButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripSplitButtonAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._MenuItemGroup});
            this._toolStripSplitButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("_toolStripSplitButtonAdd.Image")));
            this._toolStripSplitButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripSplitButtonAdd.Name = "_toolStripSplitButtonAdd";
            this._toolStripSplitButtonAdd.Size = new System.Drawing.Size(32, 20);
            this._toolStripSplitButtonAdd.Text = "toolStripSplitButton1";
            this._toolStripSplitButtonAdd.ToolTipText = "Создать";
            // 
            // _MenuItemGroup
            // 
            this._MenuItemGroup.Name = "_MenuItemGroup";
            this._MenuItemGroup.Size = new System.Drawing.Size(152, 22);
            this._MenuItemGroup.Text = "Группу";
            this._MenuItemGroup.Click += new System.EventHandler(this.MenuItemCreateGroup);
            // 
            // _toolStripButtonDelete
            // 
            this._toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("_toolStripButtonDelete.Image")));
            this._toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonDelete.Name = "_toolStripButtonDelete";
            this._toolStripButtonDelete.Size = new System.Drawing.Size(23, 20);
            this._toolStripButtonDelete.Text = "Удалить";
            this._toolStripButtonDelete.Click += new System.EventHandler(this.MenuItemDelete);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // _menuItemCreateGroup
            // 
            this._menuItemCreateGroup.Name = "_menuItemCreateGroup";
            this._menuItemCreateGroup.Size = new System.Drawing.Size(227, 22);
            this._menuItemCreateGroup.Text = "Создать группу";
            this._menuItemCreateGroup.Click += new System.EventHandler(this.MenuItemCreateGroup);
            // 
            // EditorControlsInspector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 479);
            this.Controls.Add(this._panelData);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditorControlsInspector";
            this.Text = "Инспектор контролов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorControlsInspector_FormClosing);
            this._panelData.ResumeLayout(false);
            this._panelData.PerformLayout();
            this._contextMenuTreeItem.ResumeLayout(false);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _panelData;
        private System.Windows.Forms.TextBox _textBoxFilter;
        private Aga.Controls.Tree.TreeViewAdv _treeView;
        private System.Windows.Forms.ImageList _imageTree;
        private System.Windows.Forms.ContextMenuStrip _contextMenuTreeItem;
        private System.Windows.Forms.ToolStripMenuItem _contextNewButton;
        private System.Windows.Forms.ToolStripMenuItem _menuItemNewTapScene;
        private System.Windows.Forms.ToolStripMenuItem _contextDeleteAnim;
        private System.Windows.Forms.ToolStripMenuItem _menuItemCreateBehavior;
        private System.Windows.Forms.ToolStripMenuItem _menuItemCreateImage;
        private System.Windows.Forms.ToolStripMenuItem _menuItemCreateText;
        private System.Windows.Forms.ToolStripMenuItem _menuItemCreateCicrle;
        private System.Windows.Forms.ToolStripMenuItem _menuItemCreatePanel;
        private Aga.Controls.Tree.NodeControls.NodeIcon _nodeIconTreeView;
        private Aga.Controls.Tree.NodeControls.NodeTextBox _nodeTextBoxTreeView;
        private System.Windows.Forms.ToolStripMenuItem _menuItemCreateXRef;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripSplitButton _toolStripSplitButtonAdd;
        private System.Windows.Forms.ToolStripMenuItem _MenuItemGroup;
        private System.Windows.Forms.ToolStripButton _toolStripButtonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem _menuItemCreateGroup;
    }
}