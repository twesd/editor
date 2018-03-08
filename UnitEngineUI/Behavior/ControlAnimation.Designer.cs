namespace UnitEngineUI.Behavior
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
            this._checkBoxRepeat = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this._nmrAnimSpeed = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this._nmrEndFrame = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this._nmrStartFrame = new System.Windows.Forms.NumericUpDown();
            this._checkBoxEnabled = new System.Windows.Forms.CheckBox();
            this._panelSetting = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this._nmrAnimSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nmrEndFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nmrStartFrame)).BeginInit();
            this._panelSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // _checkBoxRepeat
            // 
            this._checkBoxRepeat.AutoSize = true;
            this._checkBoxRepeat.Location = new System.Drawing.Point(5, 137);
            this._checkBoxRepeat.Name = "_checkBoxRepeat";
            this._checkBoxRepeat.Size = new System.Drawing.Size(135, 17);
            this._checkBoxRepeat.TabIndex = 17;
            this._checkBoxRepeat.Text = "Повторять анимацию";
            this._checkBoxRepeat.UseVisualStyleBackColor = true;
            this._checkBoxRepeat.CheckedChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Скорость анимации:";
            // 
            // _nmrAnimSpeed
            // 
            this._nmrAnimSpeed.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this._nmrAnimSpeed.Location = new System.Drawing.Point(5, 111);
            this._nmrAnimSpeed.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this._nmrAnimSpeed.Name = "_nmrAnimSpeed";
            this._nmrAnimSpeed.Size = new System.Drawing.Size(270, 20);
            this._nmrAnimSpeed.TabIndex = 15;
            this._nmrAnimSpeed.ThousandsSeparator = true;
            this._nmrAnimSpeed.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Конечный кадр:";
            // 
            // _nmrEndFrame
            // 
            this._nmrEndFrame.Location = new System.Drawing.Point(5, 64);
            this._nmrEndFrame.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this._nmrEndFrame.Name = "_nmrEndFrame";
            this._nmrEndFrame.Size = new System.Drawing.Size(270, 20);
            this._nmrEndFrame.TabIndex = 13;
            this._nmrEndFrame.ThousandsSeparator = true;
            this._nmrEndFrame.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Начальный кадр:";
            // 
            // _nmrStartFrame
            // 
            this._nmrStartFrame.Location = new System.Drawing.Point(5, 23);
            this._nmrStartFrame.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this._nmrStartFrame.Name = "_nmrStartFrame";
            this._nmrStartFrame.Size = new System.Drawing.Size(270, 20);
            this._nmrStartFrame.TabIndex = 11;
            this._nmrStartFrame.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _checkBoxEnabled
            // 
            this._checkBoxEnabled.AutoSize = true;
            this._checkBoxEnabled.Location = new System.Drawing.Point(9, 12);
            this._checkBoxEnabled.Name = "_checkBoxEnabled";
            this._checkBoxEnabled.Size = new System.Drawing.Size(130, 17);
            this._checkBoxEnabled.TabIndex = 18;
            this._checkBoxEnabled.Text = "Включить анимацию";
            this._checkBoxEnabled.UseVisualStyleBackColor = true;
            this._checkBoxEnabled.CheckedChanged += new System.EventHandler(this.СheckBoxEnabled_CheckedChanged);
            // 
            // _panelSetting
            // 
            this._panelSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._panelSetting.Controls.Add(this._checkBoxRepeat);
            this._panelSetting.Controls.Add(this.label3);
            this._panelSetting.Controls.Add(this.label4);
            this._panelSetting.Controls.Add(this._nmrAnimSpeed);
            this._panelSetting.Controls.Add(this._nmrEndFrame);
            this._panelSetting.Controls.Add(this.label2);
            this._panelSetting.Controls.Add(this._nmrStartFrame);
            this._panelSetting.Enabled = false;
            this._panelSetting.Location = new System.Drawing.Point(3, 35);
            this._panelSetting.Name = "_panelSetting";
            this._panelSetting.Size = new System.Drawing.Size(283, 211);
            this._panelSetting.TabIndex = 19;
            // 
            // ControlAnimation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._checkBoxEnabled);
            this.Controls.Add(this._panelSetting);
            this.Name = "ControlAnimation";
            this.Size = new System.Drawing.Size(289, 249);
            ((System.ComponentModel.ISupportInitialize)(this._nmrAnimSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nmrEndFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nmrStartFrame)).EndInit();
            this._panelSetting.ResumeLayout(false);
            this._panelSetting.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox _checkBoxRepeat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown _nmrAnimSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown _nmrEndFrame;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown _nmrStartFrame;
        private System.Windows.Forms.CheckBox _checkBoxEnabled;
        private System.Windows.Forms.Panel _panelSetting;
    }
}
