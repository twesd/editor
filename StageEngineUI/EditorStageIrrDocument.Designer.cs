namespace StageEngineUI
{
    partial class EditorStageIrrDocument
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
            this._panelIrrView = new CommonUI.PanelIrr();
            this.SuspendLayout();
            // 
            // _panelIrrView
            // 
            this._panelIrrView.BackColor = System.Drawing.Color.Black;
            this._panelIrrView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panelIrrView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelIrrView.Location = new System.Drawing.Point(0, 0);
            this._panelIrrView.Name = "_panelIrrView";
            this._panelIrrView.Size = new System.Drawing.Size(556, 423);
            this._panelIrrView.TabIndex = 3;
            // 
            // EditorStageIrrDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 423);
            this.Controls.Add(this._panelIrrView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "EditorStageIrrDocument";
            this.Text = "Viewer";
            this.ResumeLayout(false);

        }

        #endregion

        private CommonUI.PanelIrr _panelIrrView;
    }
}