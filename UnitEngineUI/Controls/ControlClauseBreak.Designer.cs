namespace UnitEngineUI
{
    partial class ControlClauseBreak
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlClauseBreak));
            this._checkBoxStartClauseNotApprove = new System.Windows.Forms.CheckBox();
            this._checkBoxExecuteOnly = new System.Windows.Forms.CheckBox();
            this._checkBoxAnimation = new System.Windows.Forms.CheckBox();
            this._comboBoxAnimatorEnd = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this._checkBoxStartClauseApprove = new System.Windows.Forms.CheckBox();
            this._scriptFileName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._btnSelect = new System.Windows.Forms.Button();
            this._buttonDeleteScript = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _checkBoxStartClauseNotApprove
            // 
            this._checkBoxStartClauseNotApprove.AutoSize = true;
            this._checkBoxStartClauseNotApprove.Location = new System.Drawing.Point(15, 34);
            this._checkBoxStartClauseNotApprove.Name = "_checkBoxStartClauseNotApprove";
            this._checkBoxStartClauseNotApprove.Size = new System.Drawing.Size(214, 17);
            this._checkBoxStartClauseNotApprove.TabIndex = 0;
            this._checkBoxStartClauseNotApprove.Text = "Начальные условия не выполняются";
            this._checkBoxStartClauseNotApprove.UseVisualStyleBackColor = true;
            this._checkBoxStartClauseNotApprove.CheckedChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _checkBoxExecuteOnly
            // 
            this._checkBoxExecuteOnly.AutoSize = true;
            this._checkBoxExecuteOnly.Location = new System.Drawing.Point(15, 80);
            this._checkBoxExecuteOnly.Name = "_checkBoxExecuteOnly";
            this._checkBoxExecuteOnly.Size = new System.Drawing.Size(260, 17);
            this._checkBoxExecuteOnly.TabIndex = 1;
            this._checkBoxExecuteOnly.Text = "После выполнения должно сразу завершится";
            this._checkBoxExecuteOnly.UseVisualStyleBackColor = true;
            this._checkBoxExecuteOnly.CheckedChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _checkBoxAnimation
            // 
            this._checkBoxAnimation.AutoSize = true;
            this._checkBoxAnimation.Location = new System.Drawing.Point(15, 11);
            this._checkBoxAnimation.Name = "_checkBoxAnimation";
            this._checkBoxAnimation.Size = new System.Drawing.Size(208, 17);
            this._checkBoxAnimation.TabIndex = 2;
            this._checkBoxAnimation.Text = "Завершить по окончанию анимации";
            this._checkBoxAnimation.UseVisualStyleBackColor = true;
            this._checkBoxAnimation.CheckedChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _comboBoxAnimatorEnd
            // 
            this._comboBoxAnimatorEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._comboBoxAnimatorEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxAnimatorEnd.FormattingEnabled = true;
            this._comboBoxAnimatorEnd.Location = new System.Drawing.Point(15, 118);
            this._comboBoxAnimatorEnd.Name = "_comboBoxAnimatorEnd";
            this._comboBoxAnimatorEnd.Size = new System.Drawing.Size(310, 21);
            this._comboBoxAnimatorEnd.TabIndex = 3;
            this._comboBoxAnimatorEnd.SelectedIndexChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Завершить по окончанию аниматора:";
            // 
            // _checkBoxStartClauseApprove
            // 
            this._checkBoxStartClauseApprove.AutoSize = true;
            this._checkBoxStartClauseApprove.Location = new System.Drawing.Point(15, 57);
            this._checkBoxStartClauseApprove.Name = "_checkBoxStartClauseApprove";
            this._checkBoxStartClauseApprove.Size = new System.Drawing.Size(199, 17);
            this._checkBoxStartClauseApprove.TabIndex = 5;
            this._checkBoxStartClauseApprove.Text = "Начальные условия выполняются";
            this._checkBoxStartClauseApprove.UseVisualStyleBackColor = true;
            this._checkBoxStartClauseApprove.CheckedChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _textBoxFileName
            // 
            this._scriptFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._scriptFileName.Location = new System.Drawing.Point(111, 148);
            this._scriptFileName.Name = "_textBoxFileName";
            this._scriptFileName.ReadOnly = true;
            this._scriptFileName.Size = new System.Drawing.Size(142, 20);
            this._scriptFileName.TabIndex = 11;
            this._scriptFileName.TextChanged += new System.EventHandler(this._textBoxFileName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Путь до скрипта:";
            // 
            // _btnSelect
            // 
            this._btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("_btnSelect.Image")));
            this._btnSelect.Location = new System.Drawing.Point(259, 146);
            this._btnSelect.Name = "_btnSelect";
            this._btnSelect.Size = new System.Drawing.Size(30, 23);
            this._btnSelect.TabIndex = 9;
            this._btnSelect.UseVisualStyleBackColor = true;
            this._btnSelect.Click += new System.EventHandler(this._btnSelect_Click);
            // 
            // _buttonDeleteScript
            // 
            this._buttonDeleteScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonDeleteScript.Image = ((System.Drawing.Image)(resources.GetObject("_buttonDeleteScript.Image")));
            this._buttonDeleteScript.Location = new System.Drawing.Point(295, 146);
            this._buttonDeleteScript.Name = "_buttonDeleteScript";
            this._buttonDeleteScript.Size = new System.Drawing.Size(30, 23);
            this._buttonDeleteScript.TabIndex = 12;
            this._buttonDeleteScript.UseVisualStyleBackColor = true;
            this._buttonDeleteScript.Click += new System.EventHandler(this._buttonDeleteScript_Click);
            // 
            // ControlClauseBreak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._buttonDeleteScript);
            this.Controls.Add(this._scriptFileName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._btnSelect);
            this.Controls.Add(this._checkBoxStartClauseApprove);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._comboBoxAnimatorEnd);
            this.Controls.Add(this._checkBoxAnimation);
            this.Controls.Add(this._checkBoxExecuteOnly);
            this.Controls.Add(this._checkBoxStartClauseNotApprove);
            this.Name = "ControlClauseBreak";
            this.Size = new System.Drawing.Size(328, 310);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox _checkBoxStartClauseNotApprove;
        private System.Windows.Forms.CheckBox _checkBoxExecuteOnly;
        private System.Windows.Forms.CheckBox _checkBoxAnimation;
        private System.Windows.Forms.ComboBox _comboBoxAnimatorEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox _checkBoxStartClauseApprove;
        private System.Windows.Forms.TextBox _scriptFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button _btnSelect;
        private System.Windows.Forms.Button _buttonDeleteScript;
    }
}
