﻿namespace CommonUI
{
    partial class FormDialogGetValue
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._buttonOk = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this.PanelContent = new System.Windows.Forms.Panel();
            this._textInput = new System.Windows.Forms.TextBox();
            this.PanelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buttonOk
            // 
            this._buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonOk.Location = new System.Drawing.Point(184, 52);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 23);
            this._buttonOk.TabIndex = 9;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            this._buttonOk.Click += new System.EventHandler(this._buttonOk_Click);
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.Location = new System.Drawing.Point(265, 52);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 23);
            this._buttonCancel.TabIndex = 8;
            this._buttonCancel.Text = "Отмена";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonCancel.Click += new System.EventHandler(this._buttonCancel_Click);
            // 
            // PanelContent
            // 
            this.PanelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelContent.Controls.Add(this._textInput);
            this.PanelContent.Location = new System.Drawing.Point(0, 2);
            this.PanelContent.Name = "PanelContent";
            this.PanelContent.Size = new System.Drawing.Size(340, 44);
            this.PanelContent.TabIndex = 10;
            // 
            // _textInput
            // 
            this._textInput.Location = new System.Drawing.Point(12, 10);
            this._textInput.Name = "_textInput";
            this._textInput.Size = new System.Drawing.Size(318, 20);
            this._textInput.TabIndex = 0;
            // 
            // FormDialogGetValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 87);
            this.Controls.Add(this.PanelContent);
            this.Controls.Add(this._buttonOk);
            this.Controls.Add(this._buttonCancel);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(358, 125);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(358, 125);
            this.Name = "FormDialogGetValue";
            this.Text = "FormDialog";
            this.Shown += new System.EventHandler(this.FormDialog_Shown);
            this.PanelContent.ResumeLayout(false);
            this.PanelContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _buttonOk;
        private System.Windows.Forms.Button _buttonCancel;
        public System.Windows.Forms.Panel PanelContent;
        private System.Windows.Forms.TextBox _textInput;
    }
}