namespace UnitEngineUI.Events
{
    partial class ControlEventTimer
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
            this._checkBoxLoop = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this._interval = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this._interval)).BeginInit();
            this.SuspendLayout();
            // 
            // _checkBoxLoop
            // 
            this._checkBoxLoop.AutoSize = true;
            this._checkBoxLoop.Location = new System.Drawing.Point(7, 52);
            this._checkBoxLoop.Name = "_checkBoxLoop";
            this._checkBoxLoop.Size = new System.Drawing.Size(80, 17);
            this._checkBoxLoop.TabIndex = 14;
            this._checkBoxLoop.Text = "Повторять";
            this._checkBoxLoop.UseVisualStyleBackColor = true;
            this._checkBoxLoop.CheckedChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Интервал (мсек):";
            // 
            // _interval
            // 
            this._interval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._interval.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this._interval.Location = new System.Drawing.Point(7, 24);
            this._interval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this._interval.Name = "_interval";
            this._interval.Size = new System.Drawing.Size(253, 20);
            this._interval.TabIndex = 26;
            this._interval.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // ControlEventTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this._interval);
            this.Controls.Add(this._checkBoxLoop);
            this.Name = "ControlEventTimer";
            this.Size = new System.Drawing.Size(263, 245);
            ((System.ComponentModel.ISupportInitialize)(this._interval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox _checkBoxLoop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown _interval;
    }
}
