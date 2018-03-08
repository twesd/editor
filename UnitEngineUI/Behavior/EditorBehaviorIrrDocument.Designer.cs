namespace UnitEngineUI.Behavior
{
    partial class EditorBehaviorIrrDocument
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
            this._panelPreview = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // _panelPreview
            // 
            this._panelPreview.BackColor = System.Drawing.Color.Black;
            this._panelPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panelPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelPreview.Location = new System.Drawing.Point(0, 0);
            this._panelPreview.Name = "_panelPreview";
            this._panelPreview.Size = new System.Drawing.Size(455, 491);
            this._panelPreview.TabIndex = 4;
            // 
            // EditorBehaviorIrrDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 491);
            this.Controls.Add(this._panelPreview);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "EditorBehaviorIrrDocument";
            this.Text = "EditorIrrDocument";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _panelPreview;
    }
}