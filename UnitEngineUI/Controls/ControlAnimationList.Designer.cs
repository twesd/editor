namespace UnitEngineUI
{
    partial class ControlAnimationList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._checkBoxRepeat = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this._nmrAnimSpeed = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this._nmrEndFrame = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this._nmrStartFrame = new System.Windows.Forms.NumericUpDown();
            this._panelSetting = new System.Windows.Forms.Panel();
            this._nameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._treeView = new Aga.Controls.Tree.TreeViewAdv();
            this._contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._menuItemCreateAnim = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this._nodeTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            ((System.ComponentModel.ISupportInitialize)(this._nmrAnimSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nmrEndFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nmrStartFrame)).BeginInit();
            this._panelSetting.SuspendLayout();
            this._contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _checkBoxRepeat
            // 
            this._checkBoxRepeat.AutoSize = true;
            this._checkBoxRepeat.Location = new System.Drawing.Point(5, 181);
            this._checkBoxRepeat.Name = "_checkBoxRepeat";
            this._checkBoxRepeat.Size = new System.Drawing.Size(135, 17);
            this._checkBoxRepeat.TabIndex = 4;
            this._checkBoxRepeat.Text = "Повторять анимацию";
            this._checkBoxRepeat.UseVisualStyleBackColor = true;
            this._checkBoxRepeat.CheckedChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Скорость анимации:";
            // 
            // _nmrAnimSpeed
            // 
            this._nmrAnimSpeed.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this._nmrAnimSpeed.Location = new System.Drawing.Point(5, 155);
            this._nmrAnimSpeed.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this._nmrAnimSpeed.Name = "_nmrAnimSpeed";
            this._nmrAnimSpeed.Size = new System.Drawing.Size(270, 20);
            this._nmrAnimSpeed.TabIndex = 3;
            this._nmrAnimSpeed.ThousandsSeparator = true;
            this._nmrAnimSpeed.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Конечный кадр:";
            // 
            // _nmrEndFrame
            // 
            this._nmrEndFrame.Location = new System.Drawing.Point(5, 108);
            this._nmrEndFrame.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this._nmrEndFrame.Name = "_nmrEndFrame";
            this._nmrEndFrame.Size = new System.Drawing.Size(270, 20);
            this._nmrEndFrame.TabIndex = 2;
            this._nmrEndFrame.ThousandsSeparator = true;
            this._nmrEndFrame.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Начальный кадр:";
            // 
            // _nmrStartFrame
            // 
            this._nmrStartFrame.Location = new System.Drawing.Point(5, 67);
            this._nmrStartFrame.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this._nmrStartFrame.Name = "_nmrStartFrame";
            this._nmrStartFrame.Size = new System.Drawing.Size(270, 20);
            this._nmrStartFrame.TabIndex = 1;
            this._nmrStartFrame.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _panelSetting
            // 
            this._panelSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._panelSetting.Controls.Add(this._nameBox);
            this._panelSetting.Controls.Add(this.label1);
            this._panelSetting.Controls.Add(this._checkBoxRepeat);
            this._panelSetting.Controls.Add(this.label3);
            this._panelSetting.Controls.Add(this.label4);
            this._panelSetting.Controls.Add(this._nmrAnimSpeed);
            this._panelSetting.Controls.Add(this._nmrEndFrame);
            this._panelSetting.Controls.Add(this.label2);
            this._panelSetting.Controls.Add(this._nmrStartFrame);
            this._panelSetting.Enabled = false;
            this._panelSetting.Location = new System.Drawing.Point(320, 3);
            this._panelSetting.Name = "_panelSetting";
            this._panelSetting.Size = new System.Drawing.Size(289, 243);
            this._panelSetting.TabIndex = 19;
            // 
            // _nameBox
            // 
            this._nameBox.Location = new System.Drawing.Point(6, 25);
            this._nameBox.Name = "_nameBox";
            this._nameBox.Size = new System.Drawing.Size(269, 20);
            this._nameBox.TabIndex = 0;
            this._nameBox.TextChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Наименование:";
            // 
            // _treeView
            // 
            this._treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._treeView.BackColor = System.Drawing.SystemColors.Window;
            this._treeView.ContextMenuStrip = this._contextMenuStrip;
            this._treeView.DefaultToolTipProvider = null;
            this._treeView.DragDropMarkColor = System.Drawing.Color.Black;
            this._treeView.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this._treeView.LineColor = System.Drawing.SystemColors.ControlDark;
            this._treeView.Location = new System.Drawing.Point(3, 3);
            this._treeView.Model = null;
            this._treeView.Name = "_treeView";
            this._treeView.NodeControls.Add(this._nodeTextBox);
            this._treeView.SelectedNode = null;
            this._treeView.Size = new System.Drawing.Size(311, 243);
            this._treeView.TabIndex = 20;
            this._treeView.SelectionChanged += new System.EventHandler(this.TreeView_SelectionChanged);
            this._treeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeView_KeyDown);
            // 
            // _contextMenuStrip
            // 
            this._contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuItemCreateAnim,
            this._menuItemDelete});
            this._contextMenuStrip.Name = "contextMenuStrip1";
            this._contextMenuStrip.Size = new System.Drawing.Size(180, 48);
            // 
            // _menuItemCreateAnim
            // 
            this._menuItemCreateAnim.Name = "_menuItemCreateAnim";
            this._menuItemCreateAnim.Size = new System.Drawing.Size(179, 22);
            this._menuItemCreateAnim.Text = "Создать анимацию";
            this._menuItemCreateAnim.Click += new System.EventHandler(this.MenuItemCreateAnim);
            // 
            // _menuItemDelete
            // 
            this._menuItemDelete.Name = "_menuItemDelete";
            this._menuItemDelete.Size = new System.Drawing.Size(179, 22);
            this._menuItemDelete.Text = "Удалить";
            this._menuItemDelete.Click += new System.EventHandler(this.MenuItemDelete);
            // 
            // _nodeTextBox
            // 
            this._nodeTextBox.DataPropertyName = "Text";
            this._nodeTextBox.EditEnabled = true;
            this._nodeTextBox.IncrementalSearchEnabled = true;
            this._nodeTextBox.LeftMargin = 3;
            this._nodeTextBox.ParentColumn = null;
            // 
            // ControlAnimationList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._treeView);
            this.Controls.Add(this._panelSetting);
            this.Name = "ControlAnimationList";
            this.Size = new System.Drawing.Size(612, 249);
            ((System.ComponentModel.ISupportInitialize)(this._nmrAnimSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nmrEndFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nmrStartFrame)).EndInit();
            this._panelSetting.ResumeLayout(false);
            this._panelSetting.PerformLayout();
            this._contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox _checkBoxRepeat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown _nmrAnimSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown _nmrEndFrame;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown _nmrStartFrame;
        private System.Windows.Forms.Panel _panelSetting;
        private Aga.Controls.Tree.TreeViewAdv _treeView;
        private System.Windows.Forms.ContextMenuStrip _contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _menuItemCreateAnim;
        private System.Windows.Forms.ToolStripMenuItem _menuItemDelete;
        private Aga.Controls.Tree.NodeControls.NodeTextBox _nodeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _nameBox;
    }
}
