namespace StageEngineUI.Creations
{
    partial class ControlUnitCreationTimer
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
            this.label4 = new System.Windows.Forms.Label();
            this._startTime = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this._endTime = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this._interval = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this._startTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._endTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._interval)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 90;
            this.label4.Text = "Начальное время:";
            // 
            // _startTime
            // 
            this._startTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._startTime.Location = new System.Drawing.Point(110, 7);
            this._startTime.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this._startTime.Name = "_startTime";
            this._startTime.Size = new System.Drawing.Size(172, 20);
            this._startTime.TabIndex = 89;
            this._startTime.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 92;
            this.label1.Text = "Конечное время:";
            // 
            // _endTime
            // 
            this._endTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._endTime.Location = new System.Drawing.Point(110, 35);
            this._endTime.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this._endTime.Name = "_endTime";
            this._endTime.Size = new System.Drawing.Size(172, 20);
            this._endTime.TabIndex = 91;
            this._endTime.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 94;
            this.label2.Text = "Интервал:";
            // 
            // _interval
            // 
            this._interval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._interval.Location = new System.Drawing.Point(110, 65);
            this._interval.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this._interval.Name = "_interval";
            this._interval.Size = new System.Drawing.Size(172, 20);
            this._interval.TabIndex = 93;
            this._interval.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // ControlUnitCreationTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.label2);
            this.Controls.Add(this._interval);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._endTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._startTime);
            this.Name = "ControlUnitCreationTimer";
            this.Size = new System.Drawing.Size(292, 104);
            ((System.ComponentModel.ISupportInitialize)(this._startTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._endTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._interval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown _startTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown _endTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown _interval;
    }
}
