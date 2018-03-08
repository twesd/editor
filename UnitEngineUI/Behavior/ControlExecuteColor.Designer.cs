namespace UnitEngineUI.Behavior
{
    partial class ControlExecuteColor
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
            this._panelDesc = new System.Windows.Forms.Panel();
            this._checkBoxLoop = new System.Windows.Forms.CheckBox();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._dataGridViewPoints = new System.Windows.Forms.DataGridView();
            this._panelDesc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // _panelDesc
            // 
            this._panelDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._panelDesc.Controls.Add(this._dataGridViewPoints);
            this._panelDesc.Controls.Add(this._checkBoxLoop);
            this._panelDesc.Location = new System.Drawing.Point(7, 3);
            this._panelDesc.Name = "_panelDesc";
            this._panelDesc.Size = new System.Drawing.Size(442, 356);
            this._panelDesc.TabIndex = 2;
            // 
            // _checkBoxLoop
            // 
            this._checkBoxLoop.AutoSize = true;
            this._checkBoxLoop.Location = new System.Drawing.Point(9, 13);
            this._checkBoxLoop.Name = "_checkBoxLoop";
            this._checkBoxLoop.Size = new System.Drawing.Size(80, 17);
            this._checkBoxLoop.TabIndex = 13;
            this._checkBoxLoop.Text = "Повторять";
            this._checkBoxLoop.UseVisualStyleBackColor = true;
            this._checkBoxLoop.CheckedChanged += new System.EventHandler(this.ItemChanged);
            // 
            // Time
            // 
            this.Time.HeaderText = "Время";
            this.Time.Name = "Time";
            this.Time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnB
            // 
            this.ColumnB.HeaderText = "B";
            this.ColumnB.Name = "ColumnB";
            this.ColumnB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnB.Width = 70;
            // 
            // Z
            // 
            this.Z.HeaderText = "G";
            this.Z.Name = "Z";
            this.Z.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Z.Width = 70;
            // 
            // Y
            // 
            this.Y.HeaderText = "R";
            this.Y.Name = "Y";
            this.Y.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Y.Width = 70;
            // 
            // X
            // 
            this.X.HeaderText = "A";
            this.X.Name = "X";
            this.X.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.X.Width = 70;
            // 
            // _dataGridViewPoints
            // 
            this._dataGridViewPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._dataGridViewPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewPoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.X,
            this.Y,
            this.Z,
            this.ColumnB,
            this.Time});
            this._dataGridViewPoints.Location = new System.Drawing.Point(9, 36);
            this._dataGridViewPoints.Name = "_dataGridViewPoints";
            this._dataGridViewPoints.Size = new System.Drawing.Size(448, 312);
            this._dataGridViewPoints.TabIndex = 14;
            this._dataGridViewPoints.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewPoints_CellValueChanged);
            this._dataGridViewPoints.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridViewPoints_UserAddedRow);
            this._dataGridViewPoints.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridViewPoints_UserDeletedRow);
            // 
            // ControlExecuteColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panelDesc);
            this.Name = "ControlExecuteColor";
            this.Size = new System.Drawing.Size(449, 376);
            this._panelDesc.ResumeLayout(false);
            this._panelDesc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewPoints)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _panelDesc;
        private System.Windows.Forms.CheckBox _checkBoxLoop;
        private System.Windows.Forms.DataGridView _dataGridViewPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
    }
}
