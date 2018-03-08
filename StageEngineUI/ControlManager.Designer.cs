namespace StageEngineUI
{
    partial class ControlManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlManager));
            this._textBoxFilter = new System.Windows.Forms.TextBox();
            this._treeView = new Aga.Controls.Tree.TreeViewAdv();
            this._contextMenuTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._contextNewItem = new System.Windows.Forms.ToolStripMenuItem();
            this._contextDeleteAnim = new System.Windows.Forms.ToolStripMenuItem();
            this.назначитьПоУмолчаниюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._buttonDeleteItem = new System.Windows.Forms.Button();
            this._buttonNewItem = new System.Windows.Forms.Button();
            this._buttonOk = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._panelProps = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this._packageFilename = new System.Windows.Forms.TextBox();
            this._nodeIcon = new Aga.Controls.Tree.NodeControls.NodeIcon();
            this._nodeTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this._contextMenuTree.SuspendLayout();
            this._panelProps.SuspendLayout();
            this.SuspendLayout();
            // 
            // _textBoxFilter
            // 
            this._textBoxFilter.Location = new System.Drawing.Point(12, 12);
            this._textBoxFilter.Name = "_textBoxFilter";
            this._textBoxFilter.Size = new System.Drawing.Size(200, 20);
            this._textBoxFilter.TabIndex = 6;
            // 
            // _treeView
            // 
            this._treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._treeView.BackColor = System.Drawing.SystemColors.Window;
            this._treeView.ContextMenuStrip = this._contextMenuTree;
            this._treeView.DefaultToolTipProvider = null;
            this._treeView.DragDropMarkColor = System.Drawing.Color.Black;
            this._treeView.LineColor = System.Drawing.SystemColors.ControlDark;
            this._treeView.Location = new System.Drawing.Point(11, 38);
            this._treeView.Model = null;
            this._treeView.Name = "_treeView";
            this._treeView.NodeControls.Add(this._nodeIcon);
            this._treeView.NodeControls.Add(this._nodeTextBox);
            this._treeView.SelectedNode = null;
            this._treeView.Size = new System.Drawing.Size(201, 226);
            this._treeView.TabIndex = 5;
            this._treeView.SelectionChanged += new System.EventHandler(this.TreeView_SelectionChanged);
            this._treeView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TreeViewItems_PreviewKeyDown);
            // 
            // _contextMenuTree
            // 
            this._contextMenuTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._contextNewItem,
            this._contextDeleteAnim,
            this.назначитьПоУмолчаниюToolStripMenuItem});
            this._contextMenuTree.Name = "_contextMenuTreeAnim";
            this._contextMenuTree.Size = new System.Drawing.Size(219, 70);
            // 
            // _contextNewItem
            // 
            this._contextNewItem.Name = "_contextNewItem";
            this._contextNewItem.Size = new System.Drawing.Size(218, 22);
            this._contextNewItem.Text = "Добавить пакет";
            this._contextNewItem.Click += new System.EventHandler(this.CreateItem);
            // 
            // _contextDeleteAnim
            // 
            this._contextDeleteAnim.Name = "_contextDeleteAnim";
            this._contextDeleteAnim.Size = new System.Drawing.Size(218, 22);
            this._contextDeleteAnim.Text = "Удалить";
            this._contextDeleteAnim.Click += new System.EventHandler(this.DeleteItem);
            // 
            // назначитьПоУмолчаниюToolStripMenuItem
            // 
            this.назначитьПоУмолчаниюToolStripMenuItem.Name = "назначитьПоУмолчаниюToolStripMenuItem";
            this.назначитьПоУмолчаниюToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.назначитьПоУмолчаниюToolStripMenuItem.Text = "Назначить по умолчанию";
            this.назначитьПоУмолчаниюToolStripMenuItem.Click += new System.EventHandler(this.MenuItemSetByDefault);
            // 
            // _buttonDeleteItem
            // 
            this._buttonDeleteItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("_buttonDeleteItem.Image")));
            this._buttonDeleteItem.Location = new System.Drawing.Point(51, 270);
            this._buttonDeleteItem.Name = "_buttonDeleteItem";
            this._buttonDeleteItem.Size = new System.Drawing.Size(34, 23);
            this._buttonDeleteItem.TabIndex = 8;
            this._buttonDeleteItem.UseVisualStyleBackColor = true;
            this._buttonDeleteItem.Click += new System.EventHandler(this.ButtonDeleteItem_Click);
            // 
            // _buttonNewItem
            // 
            this._buttonNewItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonNewItem.Image = ((System.Drawing.Image)(resources.GetObject("_buttonNewItem.Image")));
            this._buttonNewItem.Location = new System.Drawing.Point(12, 270);
            this._buttonNewItem.Name = "_buttonNewItem";
            this._buttonNewItem.Size = new System.Drawing.Size(33, 23);
            this._buttonNewItem.TabIndex = 7;
            this._buttonNewItem.UseVisualStyleBackColor = true;
            this._buttonNewItem.Click += new System.EventHandler(this.ButtonNewItem_Click);
            // 
            // _buttonOk
            // 
            this._buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonOk.Location = new System.Drawing.Point(573, 270);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 23);
            this._buttonOk.TabIndex = 11;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            this._buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.Location = new System.Drawing.Point(654, 270);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 23);
            this._buttonCancel.TabIndex = 10;
            this._buttonCancel.Text = "Отмена";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // _panelProps
            // 
            this._panelProps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._panelProps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panelProps.Controls.Add(this.label1);
            this._panelProps.Controls.Add(this._packageFilename);
            this._panelProps.Location = new System.Drawing.Point(218, 38);
            this._panelProps.Name = "_panelProps";
            this._panelProps.Size = new System.Drawing.Size(511, 226);
            this._panelProps.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Путь до пакета:";
            // 
            // _packageFilename
            // 
            this._packageFilename.Location = new System.Drawing.Point(3, 30);
            this._packageFilename.Name = "_packageFilename";
            this._packageFilename.ReadOnly = true;
            this._packageFilename.Size = new System.Drawing.Size(503, 20);
            this._packageFilename.TabIndex = 0;
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
            // ControlManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 305);
            this.Controls.Add(this._panelProps);
            this.Controls.Add(this._buttonOk);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonDeleteItem);
            this.Controls.Add(this._buttonNewItem);
            this.Controls.Add(this._textBoxFilter);
            this.Controls.Add(this._treeView);
            this.KeyPreview = true;
            this.Name = "ControlManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Менеджер пакетов управлений";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlManager_KeyDown);
            this._contextMenuTree.ResumeLayout(false);
            this._panelProps.ResumeLayout(false);
            this._panelProps.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _textBoxFilter;
        private Aga.Controls.Tree.TreeViewAdv _treeView;
        private System.Windows.Forms.Button _buttonDeleteItem;
        private System.Windows.Forms.Button _buttonNewItem;
        private System.Windows.Forms.Button _buttonOk;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.ContextMenuStrip _contextMenuTree;
        private System.Windows.Forms.ToolStripMenuItem _contextNewItem;
        private System.Windows.Forms.ToolStripMenuItem _contextDeleteAnim;
        private System.Windows.Forms.Panel _panelProps;
        private System.Windows.Forms.ToolStripMenuItem назначитьПоУмолчаниюToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _packageFilename;
        private Aga.Controls.Tree.NodeControls.NodeIcon _nodeIcon;
        private Aga.Controls.Tree.NodeControls.NodeTextBox _nodeTextBox;
    }
}