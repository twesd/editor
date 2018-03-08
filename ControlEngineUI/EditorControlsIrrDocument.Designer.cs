namespace ControlEngineUI
{
    partial class EditorControlsIrrDocument
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorControlsIrrDocument));
            this._tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._toolStripContainerCenter = new System.Windows.Forms.ToolStripContainer();
            this._panelIrrView = new CommonUI.PanelIrr();
            this._toolStripEditControls = new System.Windows.Forms.ToolStrip();
            this._toolStrip2x = new System.Windows.Forms.ToolStripButton();
            this._toolStrip2x2 = new System.Windows.Forms.ToolStripButton();
            this._tableLayoutPanel.SuspendLayout();
            this._toolStripContainerCenter.ContentPanel.SuspendLayout();
            this._toolStripContainerCenter.TopToolStripPanel.SuspendLayout();
            this._toolStripContainerCenter.SuspendLayout();
            this._toolStripEditControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tableLayoutPanel
            // 
            this._tableLayoutPanel.ColumnCount = 3;
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.Controls.Add(this._toolStripContainerCenter, 1, 1);
            this._tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._tableLayoutPanel.Name = "_tableLayoutPanel";
            this._tableLayoutPanel.RowCount = 3;
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.Size = new System.Drawing.Size(794, 470);
            this._tableLayoutPanel.TabIndex = 0;
            // 
            // _toolStripContainerCenter
            // 
            // 
            // _toolStripContainerCenter.ContentPanel
            // 
            this._toolStripContainerCenter.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._toolStripContainerCenter.ContentPanel.Controls.Add(this._panelIrrView);
            this._toolStripContainerCenter.ContentPanel.Size = new System.Drawing.Size(490, 335);
            this._toolStripContainerCenter.Location = new System.Drawing.Point(152, 55);
            this._toolStripContainerCenter.Name = "_toolStripContainerCenter";
            this._toolStripContainerCenter.Size = new System.Drawing.Size(490, 360);
            this._toolStripContainerCenter.TabIndex = 0;
            this._toolStripContainerCenter.Text = "toolStripContainer1";
            // 
            // _toolStripContainerCenter.TopToolStripPanel
            // 
            this._toolStripContainerCenter.TopToolStripPanel.Controls.Add(this._toolStripEditControls);
            // 
            // _panelIrrView
            // 
            this._panelIrrView.BackColor = System.Drawing.Color.Black;
            this._panelIrrView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panelIrrView.Location = new System.Drawing.Point(3, 4);
            this._panelIrrView.Name = "_panelIrrView";
            this._panelIrrView.Size = new System.Drawing.Size(480, 320);
            this._panelIrrView.TabIndex = 3;
            this._panelIrrView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelIrrView_MouseDown);
            // 
            // _toolStripEditControls
            // 
            this._toolStripEditControls.Dock = System.Windows.Forms.DockStyle.None;
            this._toolStripEditControls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStrip2x,
            this._toolStrip2x2});
            this._toolStripEditControls.Location = new System.Drawing.Point(3, 0);
            this._toolStripEditControls.Name = "_toolStripEditControls";
            this._toolStripEditControls.Size = new System.Drawing.Size(89, 25);
            this._toolStripEditControls.TabIndex = 0;
            // 
            // _toolStrip2x
            // 
            this._toolStrip2x.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStrip2x.Image = ((System.Drawing.Image)(resources.GetObject("_toolStrip2x.Image")));
            this._toolStrip2x.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStrip2x.Name = "_toolStrip2x";
            this._toolStrip2x.Size = new System.Drawing.Size(23, 22);
            this._toolStrip2x.Text = "960x640";
            this._toolStrip2x.Click += new System.EventHandler(this.ToolStrip2x_Click);
            // 
            // _toolStrip2x2
            // 
            this._toolStrip2x2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStrip2x2.Image = ((System.Drawing.Image)(resources.GetObject("_toolStrip2x2.Image")));
            this._toolStrip2x2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStrip2x2.Name = "_toolStrip2x2";
            this._toolStrip2x2.Size = new System.Drawing.Size(23, 22);
            this._toolStrip2x2.Text = "1136x640";
            this._toolStrip2x2.Click += new System.EventHandler(this.ToolStrip2x2_Click);
            // 
            // EditorControlsIrrDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 470);
            this.Controls.Add(this._tableLayoutPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "EditorControlsIrrDocument";
            this.Text = "EditorControlsIrrDocument";
            this._tableLayoutPanel.ResumeLayout(false);
            this._toolStripContainerCenter.ContentPanel.ResumeLayout(false);
            this._toolStripContainerCenter.TopToolStripPanel.ResumeLayout(false);
            this._toolStripContainerCenter.TopToolStripPanel.PerformLayout();
            this._toolStripContainerCenter.ResumeLayout(false);
            this._toolStripContainerCenter.PerformLayout();
            this._toolStripEditControls.ResumeLayout(false);
            this._toolStripEditControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _tableLayoutPanel;
        private System.Windows.Forms.ToolStripContainer _toolStripContainerCenter;
        private System.Windows.Forms.ToolStrip _toolStripEditControls;
        private CommonUI.PanelIrr _panelIrrView;
        private System.Windows.Forms.ToolStripButton _toolStrip2x;
        private System.Windows.Forms.ToolStripButton _toolStrip2x2;

    }
}