namespace UnitEngineUI
{
    partial class ControlExecuteScript
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlExecuteScript));
            this._textBoxScript = new FastColoredTextBoxNS.FastColoredTextBox();
            this._btnSelect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._textBoxFileName = new System.Windows.Forms.TextBox();
            this._btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _textBoxScript
            // 
            this._textBoxScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxScript.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this._textBoxScript.BackBrush = null;
            this._textBoxScript.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._textBoxScript.Cursor = System.Windows.Forms.Cursors.IBeam;
            this._textBoxScript.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this._textBoxScript.Language = FastColoredTextBoxNS.Language.CSharp;
            this._textBoxScript.Location = new System.Drawing.Point(6, 72);
            this._textBoxScript.Name = "_textBoxScript";
            this._textBoxScript.Paddings = new System.Windows.Forms.Padding(0);
            this._textBoxScript.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this._textBoxScript.Size = new System.Drawing.Size(506, 245);
            this._textBoxScript.TabIndex = 2;
            this._textBoxScript.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.TextBoxScript_TextChanged);
            // 
            // _btnSelect
            // 
            this._btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("_btnSelect.Image")));
            this._btnSelect.Location = new System.Drawing.Point(482, 13);
            this._btnSelect.Name = "_btnSelect";
            this._btnSelect.Size = new System.Drawing.Size(30, 23);
            this._btnSelect.TabIndex = 3;
            this._btnSelect.UseVisualStyleBackColor = true;
            this._btnSelect.Click += new System.EventHandler(this._btnSelect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Путь до файла:";
            // 
            // _textBoxFileName
            // 
            this._textBoxFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxFileName.Location = new System.Drawing.Point(93, 15);
            this._textBoxFileName.Name = "_textBoxFileName";
            this._textBoxFileName.ReadOnly = true;
            this._textBoxFileName.Size = new System.Drawing.Size(383, 20);
            this._textBoxFileName.TabIndex = 5;
            this._textBoxFileName.TextChanged += new System.EventHandler(this._textBoxFileName_TextChanged);
            // 
            // _btnSave
            // 
            this._btnSave.Image = global::UnitEngineUI.Properties.Resources.save;
            this._btnSave.Location = new System.Drawing.Point(6, 34);
            this._btnSave.Name = "_btnSave";
            this._btnSave.Size = new System.Drawing.Size(37, 32);
            this._btnSave.TabIndex = 6;
            this._btnSave.UseVisualStyleBackColor = true;
            this._btnSave.Click += new System.EventHandler(this._btnSave_Click);
            // 
            // ControlExecuteScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._btnSave);
            this.Controls.Add(this._textBoxFileName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._btnSelect);
            this.Controls.Add(this._textBoxScript);
            this.Name = "ControlExecuteScript";
            this.Size = new System.Drawing.Size(515, 317);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox _textBoxScript;
        private System.Windows.Forms.Button _btnSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _textBoxFileName;
        private System.Windows.Forms.Button _btnSave;
    }
}
