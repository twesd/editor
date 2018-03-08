namespace UnitEngineUI.Behavior
{
    partial class ControlTransforms
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
            this._obstacleFilterId = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this._dataGridViewPoints = new System.Windows.Forms.DataGridView();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._checkBoxLoop = new System.Windows.Forms.CheckBox();
            this._comboBoxType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._panelDesc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._obstacleFilterId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // _panelDesc
            // 
            this._panelDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._panelDesc.Controls.Add(this._obstacleFilterId);
            this._panelDesc.Controls.Add(this.label3);
            this._panelDesc.Controls.Add(this._dataGridViewPoints);
            this._panelDesc.Controls.Add(this._checkBoxLoop);
            this._panelDesc.Controls.Add(this._comboBoxType);
            this._panelDesc.Controls.Add(this.label2);
            this._panelDesc.Controls.Add(this._textBoxName);
            this._panelDesc.Controls.Add(this.label1);
            this._panelDesc.Location = new System.Drawing.Point(7, 3);
            this._panelDesc.Name = "_panelDesc";
            this._panelDesc.Size = new System.Drawing.Size(371, 356);
            this._panelDesc.TabIndex = 2;
            // 
            // _obstacleFilterId
            // 
            this._obstacleFilterId.Location = new System.Drawing.Point(129, 65);
            this._obstacleFilterId.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._obstacleFilterId.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this._obstacleFilterId.Name = "_obstacleFilterId";
            this._obstacleFilterId.Size = new System.Drawing.Size(120, 20);
            this._obstacleFilterId.TabIndex = 16;
            this._obstacleFilterId.ValueChanged += new System.EventHandler(this.ItemChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Фильтр препятствий:";
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
            this.Time});
            this._dataGridViewPoints.Location = new System.Drawing.Point(9, 119);
            this._dataGridViewPoints.Name = "_dataGridViewPoints";
            this._dataGridViewPoints.Size = new System.Drawing.Size(359, 229);
            this._dataGridViewPoints.TabIndex = 14;
            this._dataGridViewPoints.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewPoints_CellValueChanged);
            this._dataGridViewPoints.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridViewPoints_UserAddedRow);
            this._dataGridViewPoints.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridViewPoints_UserDeletedRow);
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.X.Width = 70;
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Y.Width = 70;
            // 
            // Z
            // 
            this.Z.HeaderText = "Z";
            this.Z.Name = "Z";
            this.Z.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Z.Width = 70;
            // 
            // Time
            // 
            this.Time.HeaderText = "Время";
            this.Time.Name = "Time";
            this.Time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _checkBoxLoop
            // 
            this._checkBoxLoop.AutoSize = true;
            this._checkBoxLoop.Location = new System.Drawing.Point(9, 96);
            this._checkBoxLoop.Name = "_checkBoxLoop";
            this._checkBoxLoop.Size = new System.Drawing.Size(80, 17);
            this._checkBoxLoop.TabIndex = 13;
            this._checkBoxLoop.Text = "Повторять";
            this._checkBoxLoop.UseVisualStyleBackColor = true;
            this._checkBoxLoop.CheckedChanged += new System.EventHandler(this.ItemChanged);
            // 
            // _comboBoxType
            // 
            this._comboBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxType.FormattingEnabled = true;
            this._comboBoxType.Location = new System.Drawing.Point(129, 34);
            this._comboBoxType.Name = "_comboBoxType";
            this._comboBoxType.Size = new System.Drawing.Size(239, 21);
            this._comboBoxType.TabIndex = 11;
            this._comboBoxType.TextChanged += new System.EventHandler(this.ItemChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Тип:";
            // 
            // _textBoxName
            // 
            this._textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxName.Location = new System.Drawing.Point(129, 8);
            this._textBoxName.Name = "_textBoxName";
            this._textBoxName.Size = new System.Drawing.Size(239, 20);
            this._textBoxName.TabIndex = 1;
            this._textBoxName.TextChanged += new System.EventHandler(this.ItemChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Наименование:";
            // 
            // ControlTransforms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panelDesc);
            this.Name = "ControlTransforms";
            this.Size = new System.Drawing.Size(394, 376);
            this._panelDesc.ResumeLayout(false);
            this._panelDesc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._obstacleFilterId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewPoints)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _panelDesc;
        private System.Windows.Forms.DataGridView _dataGridViewPoints;
        private System.Windows.Forms.CheckBox _checkBoxLoop;
        private System.Windows.Forms.ComboBox _comboBoxType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.NumericUpDown _obstacleFilterId;
        private System.Windows.Forms.Label label3;
    }
}
