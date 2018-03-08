namespace StUpdater
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this._stFileName = new System.Windows.Forms.TextBox();
            this.origFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this.btnMSelect = new System.Windows.Forms.Button();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Входной файл:";
            // 
            // _stFileName
            // 
            this._stFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._stFileName.Location = new System.Drawing.Point(99, 21);
            this._stFileName.Name = "_stFileName";
            this._stFileName.Size = new System.Drawing.Size(358, 20);
            this._stFileName.TabIndex = 1;
            // 
            // origFile
            // 
            this.origFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.origFile.Location = new System.Drawing.Point(463, 19);
            this.origFile.Name = "origFile";
            this.origFile.Size = new System.Drawing.Size(75, 23);
            this.origFile.TabIndex = 2;
            this.origFile.Text = "...";
            this.origFile.UseVisualStyleBackColor = true;
            this.origFile.Click += new System.EventHandler(this.origFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Выходные файлы:";
            // 
            // _dataGridView
            // 
            this._dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnName});
            this._dataGridView.Location = new System.Drawing.Point(15, 93);
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.Size = new System.Drawing.Size(523, 227);
            this._dataGridView.TabIndex = 4;
            // 
            // btnMSelect
            // 
            this.btnMSelect.Location = new System.Drawing.Point(117, 58);
            this.btnMSelect.Name = "btnMSelect";
            this.btnMSelect.Size = new System.Drawing.Size(75, 23);
            this.btnMSelect.TabIndex = 5;
            this.btnMSelect.Text = "...";
            this.btnMSelect.UseVisualStyleBackColor = true;
            this.btnMSelect.Click += new System.EventHandler(this.btnMSelect_Click);
            // 
            // columnName
            // 
            this.columnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnName.HeaderText = "Имя файла";
            this.columnName.Name = "columnName";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(463, 331);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 366);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnMSelect);
            this.Controls.Add(this._dataGridView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.origFile);
            this.Controls.Add(this._stFileName);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _stFileName;
        private System.Windows.Forms.Button origFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView _dataGridView;
        private System.Windows.Forms.Button btnMSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.Button btnStart;
    }
}

