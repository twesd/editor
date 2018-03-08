namespace UnitEngineUI
{
    partial class ControlUnitDelete
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
            this.label1 = new System.Windows.Forms.Label();
            this._textBoxBehaviorsPath = new System.Windows.Forms.TextBox();
            this._btnSelectBehaviors = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Путь до файла поведения :";
            // 
            // _textBoxBehaviorsPath
            // 
            this._textBoxBehaviorsPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxBehaviorsPath.Location = new System.Drawing.Point(5, 29);
            this._textBoxBehaviorsPath.Name = "_textBoxBehaviorsPath";
            this._textBoxBehaviorsPath.ReadOnly = true;
            this._textBoxBehaviorsPath.Size = new System.Drawing.Size(251, 20);
            this._textBoxBehaviorsPath.TabIndex = 4;
            this._textBoxBehaviorsPath.TextChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _btnSelectBehaviors
            // 
            this._btnSelectBehaviors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSelectBehaviors.Location = new System.Drawing.Point(262, 27);
            this._btnSelectBehaviors.Name = "_btnSelectBehaviors";
            this._btnSelectBehaviors.Size = new System.Drawing.Size(28, 23);
            this._btnSelectBehaviors.TabIndex = 3;
            this._btnSelectBehaviors.Text = "...";
            this._btnSelectBehaviors.UseVisualStyleBackColor = true;
            this._btnSelectBehaviors.Click += new System.EventHandler(this.BtnSelectBehaviors_Click);
            // 
            // ControlUnitDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this._textBoxBehaviorsPath);
            this.Controls.Add(this._btnSelectBehaviors);
            this.Name = "ControlUnitDelete";
            this.Size = new System.Drawing.Size(295, 247);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _textBoxBehaviorsPath;
        private System.Windows.Forms.Button _btnSelectBehaviors;
    }
}
