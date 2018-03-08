namespace CommonUI.UITypeEditors
{
    partial class ControlEditorScript
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
            this._textBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.SuspendLayout();
            // 
            // _textBox
            // 
            this._textBox.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this._textBox.BackBrush = null;
            this._textBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this._textBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this._textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textBox.Language = FastColoredTextBoxNS.Language.CSharp;
            this._textBox.Location = new System.Drawing.Point(0, 0);
            this._textBox.Name = "_textBox";
            this._textBox.Paddings = new System.Windows.Forms.Padding(0);
            this._textBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this._textBox.Size = new System.Drawing.Size(491, 103);
            this._textBox.TabIndex = 1;
            // 
            // ControlEditorScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._textBox);
            this.Name = "ControlEditorScript";
            this.Size = new System.Drawing.Size(491, 103);
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox _textBox;

    }
}
