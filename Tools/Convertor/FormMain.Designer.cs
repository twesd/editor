namespace Convertor
{
    partial class FormMain
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
            this._tabPage_4_5 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.btnStart_960x640 = new System.Windows.Forms.Button();
            this.btnMSelect = new System.Windows.Forms.Button();
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this._tabControl = new System.Windows.Forms.TabControl();
            this._tabPage_4_5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            this._tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tabPage_4_5
            // 
            this._tabPage_4_5.BackColor = System.Drawing.SystemColors.Control;
            this._tabPage_4_5.Controls.Add(this.button1);
            this._tabPage_4_5.Controls.Add(this.btnStart_960x640);
            this._tabPage_4_5.Controls.Add(this.btnMSelect);
            this._tabPage_4_5.Controls.Add(this._dataGridView);
            this._tabPage_4_5.Controls.Add(this.label2);
            this._tabPage_4_5.Location = new System.Drawing.Point(4, 22);
            this._tabPage_4_5.Name = "_tabPage_4_5";
            this._tabPage_4_5.Padding = new System.Windows.Forms.Padding(3);
            this._tabPage_4_5.Size = new System.Drawing.Size(677, 393);
            this._tabPage_4_5.TabIndex = 0;
            this._tabPage_4_5.Text = "Menu controls To 960x640,1136x640";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(523, 362);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Выполнить 1136_640";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn1136x640_Click);
            // 
            // btnStart_960x640
            // 
            this.btnStart_960x640.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart_960x640.Location = new System.Drawing.Point(372, 362);
            this.btnStart_960x640.Name = "btnStart_960x640";
            this.btnStart_960x640.Size = new System.Drawing.Size(145, 23);
            this.btnStart_960x640.TabIndex = 10;
            this.btnStart_960x640.Text = "Выполнить 960_640";
            this.btnStart_960x640.UseVisualStyleBackColor = true;
            this.btnStart_960x640.Click += new System.EventHandler(this.btn960x640_Click);
            // 
            // btnMSelect
            // 
            this.btnMSelect.Location = new System.Drawing.Point(113, 10);
            this.btnMSelect.Name = "btnMSelect";
            this.btnMSelect.Size = new System.Drawing.Size(75, 23);
            this.btnMSelect.TabIndex = 9;
            this.btnMSelect.Text = "...";
            this.btnMSelect.UseVisualStyleBackColor = true;
            this.btnMSelect.Click += new System.EventHandler(this.btnMSelect_Click);
            // 
            // _dataGridView
            // 
            this._dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnName});
            this._dataGridView.Location = new System.Drawing.Point(11, 45);
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.Size = new System.Drawing.Size(658, 227);
            this._dataGridView.TabIndex = 8;
            // 
            // columnName
            // 
            this.columnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnName.HeaderText = "Имя файла";
            this.columnName.Name = "columnName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Выходные файлы:";
            // 
            // _tabControl
            // 
            this._tabControl.Controls.Add(this._tabPage_4_5);
            this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl.Location = new System.Drawing.Point(0, 0);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(685, 419);
            this._tabControl.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 419);
            this.Controls.Add(this._tabControl);
            this.Name = "FormMain";
            this.Text = "Form1";
            this._tabPage_4_5.ResumeLayout(false);
            this._tabPage_4_5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            this._tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage _tabPage_4_5;
        private System.Windows.Forms.Button btnStart_960x640;
        private System.Windows.Forms.Button btnMSelect;
        private System.Windows.Forms.DataGridView _dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.Button button1;

    }
}

