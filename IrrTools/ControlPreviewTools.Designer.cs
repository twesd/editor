namespace IrrTools
{
    partial class ControlPreviewTools
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
            this.components = new System.ComponentModel.Container();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._toolStripInstruments = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonClearSelection = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMove = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEditNode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemZoomAll = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripInstruments.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolStripInstruments
            // 
            this._toolStripInstruments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonClearSelection,
            this.toolStripButtonMove,
            this.toolStripButtonEditNode,
            this.toolStripSplitButton1});
            this._toolStripInstruments.Location = new System.Drawing.Point(0, 0);
            this._toolStripInstruments.Name = "_toolStripInstruments";
            this._toolStripInstruments.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this._toolStripInstruments.Size = new System.Drawing.Size(298, 25);
            this._toolStripInstruments.TabIndex = 5;
            this._toolStripInstruments.Text = "toolStrip1";
            // 
            // toolStripButtonClearSelection
            // 
            this.toolStripButtonClearSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonClearSelection.Image = global::IrrTools.Properties.Resources.clear;
            this.toolStripButtonClearSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClearSelection.Name = "toolStripButtonClearSelection";
            this.toolStripButtonClearSelection.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonClearSelection.Text = "toolStripButton1";
            this.toolStripButtonClearSelection.ToolTipText = "Очистить выборку";
            this.toolStripButtonClearSelection.Click += new System.EventHandler(this.ButtonClearSelection_Click);
            // 
            // toolStripButtonMove
            // 
            this.toolStripButtonMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMove.Image = global::IrrTools.Properties.Resources.move;
            this.toolStripButtonMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMove.Name = "toolStripButtonMove";
            this.toolStripButtonMove.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonMove.Text = "toolStripButton2";
            this.toolStripButtonMove.ToolTipText = "Переместить модель";
            this.toolStripButtonMove.Click += new System.EventHandler(this.Move_Click);
            // 
            // toolStripButtonEditNode
            // 
            this.toolStripButtonEditNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEditNode.Image = global::IrrTools.Properties.Resources.edit;
            this.toolStripButtonEditNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditNode.Name = "toolStripButtonEditNode";
            this.toolStripButtonEditNode.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonEditNode.Text = "toolStripButton3";
            this.toolStripButtonEditNode.ToolTipText = "Редактировать модель";
            this.toolStripButtonEditNode.Click += new System.EventHandler(this.NodeEdit_Click);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItemZoomAll});
            this.toolStripSplitButton1.Image = global::IrrTools.Properties.Resources.zoom;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButton1.Text = "Приблизиться к объекту";
            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.ZoomToNode_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = global::IrrTools.Properties.Resources.zoom;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(209, 22);
            this.toolStripMenuItem2.Text = "Приблизиться к объекту";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.ZoomToNode_Click);
            // 
            // toolStripMenuItemZoomAll
            // 
            this.toolStripMenuItemZoomAll.Image = global::IrrTools.Properties.Resources.zoom_extent;
            this.toolStripMenuItemZoomAll.Name = "toolStripMenuItemZoomAll";
            this.toolStripMenuItemZoomAll.Size = new System.Drawing.Size(209, 22);
            this.toolStripMenuItemZoomAll.Text = "Все объекты";
            this.toolStripMenuItemZoomAll.Click += new System.EventHandler(this.ZoomAll_Click);
            // 
            // ControlPreviewTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._toolStripInstruments);
            this.Name = "ControlPreviewTools";
            this.Size = new System.Drawing.Size(298, 28);
            this._toolStripInstruments.ResumeLayout(false);
            this._toolStripInstruments.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.ToolStrip _toolStripInstruments;
        private System.Windows.Forms.ToolStripButton toolStripButtonClearSelection;
        private System.Windows.Forms.ToolStripButton toolStripButtonMove;
        private System.Windows.Forms.ToolStripButton toolStripButtonEditNode;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemZoomAll;
    }
}
