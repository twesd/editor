namespace UnitEngineUI.Events
{
    partial class ControlEventControlBase
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
            this._panelControlDesc = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this._comboBoxType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // _panelControlDesc
            // 
            this._panelControlDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._panelControlDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panelControlDesc.Location = new System.Drawing.Point(7, 49);
            this._panelControlDesc.Name = "_panelControlDesc";
            this._panelControlDesc.Size = new System.Drawing.Size(349, 287);
            this._panelControlDesc.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Тип камеры:";
            // 
            // _comboBoxType
            // 
            this._comboBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxType.FormattingEnabled = true;
            this._comboBoxType.Location = new System.Drawing.Point(7, 22);
            this._comboBoxType.Name = "_comboBoxType";
            this._comboBoxType.Size = new System.Drawing.Size(349, 21);
            this._comboBoxType.TabIndex = 3;
            this._comboBoxType.TextChanged += new System.EventHandler(this.СomboBoxType_TextChanged);
            // 
            // ControlEventControlBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panelControlDesc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._comboBoxType);
            this.Name = "ControlEventControlBase";
            this.Size = new System.Drawing.Size(359, 339);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel _panelControlDesc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _comboBoxType;
    }
}
