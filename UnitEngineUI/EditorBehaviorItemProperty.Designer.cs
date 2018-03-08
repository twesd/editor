namespace UnitEngineUI
{
    partial class EditorBehaviorItemProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorBehaviorItemProperty));
            this._tabControlProps = new System.Windows.Forms.TabControl();
            this._tabMainPage = new System.Windows.Forms.TabPage();
            this._tabControlProps.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tabControlProps
            // 
            this._tabControlProps.Controls.Add(this._tabMainPage);
            this._tabControlProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControlProps.Location = new System.Drawing.Point(0, 0);
            this._tabControlProps.Name = "_tabControlProps";
            this._tabControlProps.SelectedIndex = 0;
            this._tabControlProps.Size = new System.Drawing.Size(460, 542);
            this._tabControlProps.TabIndex = 12;
            // 
            // _tabMainPage
            // 
            this._tabMainPage.BackColor = System.Drawing.SystemColors.Control;
            this._tabMainPage.Location = new System.Drawing.Point(4, 22);
            this._tabMainPage.Name = "_tabMainPage";
            this._tabMainPage.Padding = new System.Windows.Forms.Padding(3);
            this._tabMainPage.Size = new System.Drawing.Size(452, 516);
            this._tabMainPage.TabIndex = 0;
            this._tabMainPage.Text = "Основные";
            // 
            // EditorBehaviorItemProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 542);
            this.Controls.Add(this._tabControlProps);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditorBehaviorItemProperty";
            this.Text = "Свойства";
            this._tabControlProps.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _tabControlProps;
        private System.Windows.Forms.TabPage _tabMainPage;
    }
}