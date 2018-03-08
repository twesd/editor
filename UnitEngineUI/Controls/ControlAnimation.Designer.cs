namespace UnitEngineUI.Controls
{
    partial class ControlAnimation
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
            this._animationsBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _animations
            // 
            this._animationsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._animationsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._animationsBox.FormattingEnabled = true;
            this._animationsBox.Location = new System.Drawing.Point(3, 37);
            this._animationsBox.Name = "_animations";
            this._animationsBox.Size = new System.Drawing.Size(408, 21);
            this._animationsBox.TabIndex = 0;
            this._animationsBox.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выберите анимацию:";
            // 
            // ControlAnimation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this._animationsBox);
            this.Name = "ControlAnimation";
            this.Size = new System.Drawing.Size(411, 361);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox _animationsBox;
        private System.Windows.Forms.Label label1;
    }
}
