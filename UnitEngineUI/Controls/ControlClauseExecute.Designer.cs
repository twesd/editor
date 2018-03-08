namespace UnitEngineUI
{
    partial class ControlClauseExecute
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlClauseExecute));
            this.label2 = new System.Windows.Forms.Label();
            this._dataGridViewClauses = new System.Windows.Forms.DataGridView();
            this._columnTypeEvent = new System.Windows.Forms.DataGridViewImageColumn();
            this._сolumnNameEvent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._contextClauses = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._menuItemTime = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemClauseDistance = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemAnimationEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemActionEnd = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemOperator = new System.Windows.Forms.ToolStripMenuItem();
            this.скриптToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дочернийЮнитToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._panelLocalParams = new System.Windows.Forms.Panel();
            this._panelGlobalParams = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewClauses)).BeginInit();
            this._contextClauses.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = " Условия:";
            // 
            // _dataGridViewClauses
            // 
            this._dataGridViewClauses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._dataGridViewClauses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewClauses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._columnTypeEvent,
            this._сolumnNameEvent});
            this._dataGridViewClauses.ContextMenuStrip = this._contextClauses;
            this._dataGridViewClauses.Location = new System.Drawing.Point(15, 19);
            this._dataGridViewClauses.MultiSelect = false;
            this._dataGridViewClauses.Name = "_dataGridViewClauses";
            this._dataGridViewClauses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dataGridViewClauses.Size = new System.Drawing.Size(334, 122);
            this._dataGridViewClauses.TabIndex = 12;
            this._dataGridViewClauses.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewClause_CellDoubleClick);
            this._dataGridViewClauses.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridViewClauseParameters_UserAddedRow);
            this._dataGridViewClauses.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridViewClauseParameters_UserDeletedRow);
            // 
            // _columnTypeEvent
            // 
            this._columnTypeEvent.HeaderText = "";
            this._columnTypeEvent.Image = ((System.Drawing.Image)(resources.GetObject("_columnTypeEvent.Image")));
            this._columnTypeEvent.Name = "_columnTypeEvent";
            this._columnTypeEvent.ReadOnly = true;
            this._columnTypeEvent.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._columnTypeEvent.Width = 30;
            // 
            // _сolumnNameEvent
            // 
            this._сolumnNameEvent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._сolumnNameEvent.HeaderText = "Имя";
            this._сolumnNameEvent.Name = "_сolumnNameEvent";
            this._сolumnNameEvent.ReadOnly = true;
            this._сolumnNameEvent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _contextClauses
            // 
            this._contextClauses.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuItemTime,
            this._menuItemClauseDistance,
            this.MenuItemAnimationEnd,
            this.MenuItemActionEnd,
            this._menuItemOperator,
            this.скриптToolStripMenuItem,
            this.дочернийЮнитToolStripMenuItem});
            this._contextClauses.Name = "_contextClauses";
            this._contextClauses.Size = new System.Drawing.Size(189, 180);
            // 
            // _menuItemTime
            // 
            this._menuItemTime.Name = "_menuItemTime";
            this._menuItemTime.Size = new System.Drawing.Size(188, 22);
            this._menuItemTime.Text = "Время";
            this._menuItemTime.Click += new System.EventHandler(this.MenuItemTime_Click);
            // 
            // _menuItemClauseDistance
            // 
            this._menuItemClauseDistance.Name = "_menuItemClauseDistance";
            this._menuItemClauseDistance.Size = new System.Drawing.Size(188, 22);
            this._menuItemClauseDistance.Text = "Расстояние";
            this._menuItemClauseDistance.Click += new System.EventHandler(this.MenuItemClauseDistance_Click);
            // 
            // MenuItemAnimationEnd
            // 
            this.MenuItemAnimationEnd.Name = "MenuItemAnimationEnd";
            this.MenuItemAnimationEnd.Size = new System.Drawing.Size(188, 22);
            this.MenuItemAnimationEnd.Text = "Анимация";
            this.MenuItemAnimationEnd.Click += new System.EventHandler(this.MenuItemAnimation_Click);
            // 
            // MenuItemActionEnd
            // 
            this.MenuItemActionEnd.Name = "MenuItemActionEnd";
            this.MenuItemActionEnd.Size = new System.Drawing.Size(188, 22);
            this.MenuItemActionEnd.Text = "Окончание действия";
            this.MenuItemActionEnd.Click += new System.EventHandler(this.MenuItemActionEnd_Click);
            // 
            // _menuItemOperator
            // 
            this._menuItemOperator.Name = "_menuItemOperator";
            this._menuItemOperator.Size = new System.Drawing.Size(188, 22);
            this._menuItemOperator.Text = "Оператор";
            this._menuItemOperator.Click += new System.EventHandler(this.MenuItemOperator_Click);
            // 
            // скриптToolStripMenuItem
            // 
            this.скриптToolStripMenuItem.Name = "скриптToolStripMenuItem";
            this.скриптToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.скриптToolStripMenuItem.Text = "Скрипт";
            this.скриптToolStripMenuItem.Click += new System.EventHandler(this.MenuItemScript_Click);
            // 
            // дочернийЮнитToolStripMenuItem
            // 
            this.дочернийЮнитToolStripMenuItem.Name = "дочернийЮнитToolStripMenuItem";
            this.дочернийЮнитToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.дочернийЮнитToolStripMenuItem.Text = "Дочерний юнит";
            this.дочернийЮнитToolStripMenuItem.Click += new System.EventHandler(this.MenuItemChildUnit_Click);
            // 
            // _panelLocalParams
            // 
            this._panelLocalParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._panelLocalParams.Location = new System.Drawing.Point(0, 157);
            this._panelLocalParams.Name = "_panelLocalParams";
            this._panelLocalParams.Size = new System.Drawing.Size(364, 110);
            this._panelLocalParams.TabIndex = 16;
            // 
            // _panelGlobalParams
            // 
            this._panelGlobalParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._panelGlobalParams.Location = new System.Drawing.Point(0, 273);
            this._panelGlobalParams.Name = "_panelGlobalParams";
            this._panelGlobalParams.Size = new System.Drawing.Size(364, 110);
            this._panelGlobalParams.TabIndex = 17;
            // 
            // ControlClauseExecute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panelGlobalParams);
            this.Controls.Add(this._panelLocalParams);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._dataGridViewClauses);
            this.Name = "ControlClauseExecute";
            this.Size = new System.Drawing.Size(364, 481);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewClauses)).EndInit();
            this._contextClauses.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView _dataGridViewClauses;
        private System.Windows.Forms.Panel _panelLocalParams;
        private System.Windows.Forms.Panel _panelGlobalParams;
        private System.Windows.Forms.ContextMenuStrip _contextClauses;
        private System.Windows.Forms.ToolStripMenuItem _menuItemTime;
        private System.Windows.Forms.ToolStripMenuItem _menuItemClauseDistance;
        private System.Windows.Forms.ToolStripMenuItem MenuItemAnimationEnd;
        private System.Windows.Forms.ToolStripMenuItem MenuItemActionEnd;
        private System.Windows.Forms.ToolStripMenuItem _menuItemOperator;
        private System.Windows.Forms.DataGridViewImageColumn _columnTypeEvent;
        private System.Windows.Forms.DataGridViewTextBoxColumn _сolumnNameEvent;
        private System.Windows.Forms.ToolStripMenuItem скриптToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дочернийЮнитToolStripMenuItem;
    }
}
