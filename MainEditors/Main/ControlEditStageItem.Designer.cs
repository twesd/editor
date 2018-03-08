namespace MainEditors.Main
{
    partial class ControlEditStageItem
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
            this._scriptAfterComplete = new FastColoredTextBoxNS.FastColoredTextBox();
            this._stagePath = new System.Windows.Forms.TextBox();
            this._selectFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _scriptAfterComplete
            // 
            this._scriptAfterComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._scriptAfterComplete.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this._scriptAfterComplete.BackBrush = null;
            this._scriptAfterComplete.Cursor = System.Windows.Forms.Cursors.IBeam;
            this._scriptAfterComplete.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this._scriptAfterComplete.Language = FastColoredTextBoxNS.Language.CSharp;
            this._scriptAfterComplete.Location = new System.Drawing.Point(3, 73);
            this._scriptAfterComplete.Name = "_scriptAfterComplete";
            this._scriptAfterComplete.Paddings = new System.Windows.Forms.Padding(0);
            this._scriptAfterComplete.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this._scriptAfterComplete.Size = new System.Drawing.Size(521, 263);
            this._scriptAfterComplete.TabIndex = 2;
            this._scriptAfterComplete.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.TextBoxScript_TextChanged);
            // 
            // _stagePath
            // 
            this._stagePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._stagePath.Location = new System.Drawing.Point(3, 27);
            this._stagePath.Name = "_stagePath";
            this._stagePath.ReadOnly = true;
            this._stagePath.Size = new System.Drawing.Size(477, 20);
            this._stagePath.TabIndex = 3;
            this._stagePath.TextChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _selectFile
            // 
            this._selectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._selectFile.Location = new System.Drawing.Point(486, 25);
            this._selectFile.Name = "_selectFile";
            this._selectFile.Size = new System.Drawing.Size(36, 23);
            this._selectFile.TabIndex = 4;
            this._selectFile.Text = "...";
            this._selectFile.UseVisualStyleBackColor = true;
            this._selectFile.Click += new System.EventHandler(this.SelectFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Путь до стадии:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Скрипт после выполнения стадии:";
            // 
            // ControlEditStageItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._selectFile);
            this.Controls.Add(this._stagePath);
            this.Controls.Add(this._scriptAfterComplete);
            this.Name = "ControlEditStageItem";
            this.Size = new System.Drawing.Size(527, 339);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox _scriptAfterComplete;
        private System.Windows.Forms.TextBox _stagePath;
        private System.Windows.Forms.Button _selectFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
