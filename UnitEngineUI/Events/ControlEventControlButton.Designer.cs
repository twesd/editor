namespace UnitEngineUI.Events
{
    partial class ControlEventControlButton
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
            this._comboBoxState = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._comboBoxName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Состояние:";
            // 
            // _comboBoxState
            // 
            this._comboBoxState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._comboBoxState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxState.FormattingEnabled = true;
            this._comboBoxState.Location = new System.Drawing.Point(3, 71);
            this._comboBoxState.Name = "_comboBoxState";
            this._comboBoxState.Size = new System.Drawing.Size(338, 21);
            this._comboBoxState.TabIndex = 5;
            this._comboBoxState.TextChanged += new System.EventHandler(this.ComboBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Наименование:";
            // 
            // _comboBoxName
            // 
            this._comboBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._comboBoxName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._comboBoxName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._comboBoxName.FormattingEnabled = true;
            this._comboBoxName.Location = new System.Drawing.Point(3, 26);
            this._comboBoxName.Name = "_comboBoxName";
            this._comboBoxName.Size = new System.Drawing.Size(338, 21);
            this._comboBoxName.TabIndex = 7;
            this._comboBoxName.TextChanged += new System.EventHandler(this.ComboBox_TextChanged);
            // 
            // ControlEventControlButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this._comboBoxName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._comboBoxState);
            this.Name = "ControlEventControlButton";
            this.Size = new System.Drawing.Size(344, 140);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _comboBoxState;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _comboBoxName;


    }
}
