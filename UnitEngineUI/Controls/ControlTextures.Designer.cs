namespace UnitEngineUI
{
    partial class ControlTextures
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlTextures));
            this._checkBoxLoop = new System.Windows.Forms.CheckBox();
            this._timePerFrame = new System.Windows.Forms.NumericUpDown();
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this._contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._menuItemAddTextures = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this._buttonUp = new System.Windows.Forms.Button();
            this._buttonDown = new System.Windows.Forms.Button();
            this._use32bits = new System.Windows.Forms.CheckBox();
            this.ColumnPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._timePerFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            this._contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _checkBoxLoop
            // 
            this._checkBoxLoop.AutoSize = true;
            this._checkBoxLoop.Location = new System.Drawing.Point(12, 37);
            this._checkBoxLoop.Name = "_checkBoxLoop";
            this._checkBoxLoop.Size = new System.Drawing.Size(80, 17);
            this._checkBoxLoop.TabIndex = 0;
            this._checkBoxLoop.Text = "Повторять";
            this._checkBoxLoop.UseVisualStyleBackColor = true;
            this._checkBoxLoop.CheckedChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _timePerFrame
            // 
            this._timePerFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._timePerFrame.Location = new System.Drawing.Point(132, 15);
            this._timePerFrame.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this._timePerFrame.Name = "_timePerFrame";
            this._timePerFrame.Size = new System.Drawing.Size(227, 20);
            this._timePerFrame.TabIndex = 1;
            this._timePerFrame.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _dataGridView
            // 
            this._dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnPath});
            this._dataGridView.ContextMenuStrip = this._contextMenuStrip;
            this._dataGridView.Location = new System.Drawing.Point(12, 83);
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dataGridView.Size = new System.Drawing.Size(316, 177);
            this._dataGridView.TabIndex = 2;
            this._dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellDoubleClick);
            this._dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellValueChanged);
            this._dataGridView.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridView_UserAddedRow);
            this._dataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridView_UserDeletedRow);
            // 
            // _contextMenuStrip
            // 
            this._contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuItemAddTextures});
            this._contextMenuStrip.Name = "contextMenuStrip1";
            this._contextMenuStrip.Size = new System.Drawing.Size(180, 26);
            // 
            // _menuItemAddTextures
            // 
            this._menuItemAddTextures.Name = "_menuItemAddTextures";
            this._menuItemAddTextures.Size = new System.Drawing.Size(179, 22);
            this._menuItemAddTextures.Text = "Добавить текстуры";
            this._menuItemAddTextures.Click += new System.EventHandler(this.MenuItemAddTextures_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Время одного шага:";
            // 
            // _buttonUp
            // 
            this._buttonUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonUp.Image = ((System.Drawing.Image)(resources.GetObject("_buttonUp.Image")));
            this._buttonUp.Location = new System.Drawing.Point(334, 83);
            this._buttonUp.Name = "_buttonUp";
            this._buttonUp.Size = new System.Drawing.Size(25, 25);
            this._buttonUp.TabIndex = 4;
            this._buttonUp.UseVisualStyleBackColor = true;
            this._buttonUp.Click += new System.EventHandler(this.ButtonUp_Click);
            // 
            // _buttonDown
            // 
            this._buttonDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonDown.Image = ((System.Drawing.Image)(resources.GetObject("_buttonDown.Image")));
            this._buttonDown.Location = new System.Drawing.Point(334, 114);
            this._buttonDown.Name = "_buttonDown";
            this._buttonDown.Size = new System.Drawing.Size(25, 25);
            this._buttonDown.TabIndex = 5;
            this._buttonDown.UseVisualStyleBackColor = true;
            this._buttonDown.Click += new System.EventHandler(this.ButtonDown_Click);
            // 
            // _use32bits
            // 
            this._use32bits.AutoSize = true;
            this._use32bits.Location = new System.Drawing.Point(12, 60);
            this._use32bits.Name = "_use32bits";
            this._use32bits.Size = new System.Drawing.Size(140, 17);
            this._use32bits.TabIndex = 6;
            this._use32bits.Text = "Использовать 32 бита";
            this._use32bits.UseVisualStyleBackColor = true;
            this._use32bits.CheckedChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // ColumnPath
            // 
            this.ColumnPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnPath.HeaderText = "Путь до текстуры";
            this.ColumnPath.Name = "ColumnPath";
            // 
            // ControlTextures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._use32bits);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._buttonDown);
            this.Controls.Add(this._buttonUp);
            this.Controls.Add(this._dataGridView);
            this.Controls.Add(this._timePerFrame);
            this.Controls.Add(this._checkBoxLoop);
            this.Name = "ControlTextures";
            this.Size = new System.Drawing.Size(373, 290);
            this.Load += new System.EventHandler(this.ControlTextures_Load);
            ((System.ComponentModel.ISupportInitialize)(this._timePerFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            this._contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox _checkBoxLoop;
        private System.Windows.Forms.NumericUpDown _timePerFrame;
        private System.Windows.Forms.DataGridView _dataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _buttonUp;
        private System.Windows.Forms.Button _buttonDown;
        private System.Windows.Forms.ContextMenuStrip _contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _menuItemAddTextures;
        private System.Windows.Forms.CheckBox _use32bits;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPath;
    }
}
