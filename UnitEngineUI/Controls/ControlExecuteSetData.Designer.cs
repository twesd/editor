namespace UnitEngineUI
{
    partial class ControlExecuteSetData
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
            this._tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._dataGridItems = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.контролTapSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemId = new System.Windows.Forms.ToolStripMenuItem();
            this._panelProps = new System.Windows.Forms.Panel();
            this._propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.MenuItemDist = new System.Windows.Forms.ToolStripMenuItem();
            this._tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridItems)).BeginInit();
            this._contextMenuStrip.SuspendLayout();
            this._panelProps.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tableLayoutPanel
            // 
            this._tableLayoutPanel.ColumnCount = 2;
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.Controls.Add(this._dataGridItems, 0, 0);
            this._tableLayoutPanel.Controls.Add(this._panelProps, 1, 0);
            this._tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._tableLayoutPanel.Name = "_tableLayoutPanel";
            this._tableLayoutPanel.RowCount = 1;
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.Size = new System.Drawing.Size(372, 348);
            this._tableLayoutPanel.TabIndex = 15;
            // 
            // _dataGridItems
            // 
            this._dataGridItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this._dataGridItems.ContextMenuStrip = this._contextMenuStrip;
            this._dataGridItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridItems.Location = new System.Drawing.Point(3, 3);
            this._dataGridItems.Name = "_dataGridItems";
            this._dataGridItems.Size = new System.Drawing.Size(180, 342);
            this._dataGridItems.TabIndex = 2;
            this._dataGridItems.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewDesc_CellMouseClick);
            this._dataGridItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellValueChanged);
            this._dataGridItems.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridView_UserAddedRow);
            this._dataGridItems.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridView_UserDeletedRow);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Данные";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _contextMenuStrip
            // 
            this._contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.контролTapSceneToolStripMenuItem,
            this.MenuItemId,
            this.MenuItemDist});
            this._contextMenuStrip.Name = "_contextMenuStrip";
            this._contextMenuStrip.Size = new System.Drawing.Size(176, 92);
            // 
            // контролTapSceneToolStripMenuItem
            // 
            this.контролTapSceneToolStripMenuItem.Name = "контролTapSceneToolStripMenuItem";
            this.контролTapSceneToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.контролTapSceneToolStripMenuItem.Text = "Контрол TapScene";
            this.контролTapSceneToolStripMenuItem.Click += new System.EventHandler(this.MenuItemTapScene_Click);
            // 
            // MenuItemId
            // 
            this.MenuItemId.Name = "MenuItemId";
            this.MenuItemId.Size = new System.Drawing.Size(175, 22);
            this.MenuItemId.Text = "Индентификатор";
            this.MenuItemId.Click += new System.EventHandler(this.MenuItemId_Click);
            // 
            // _panelProps
            // 
            this._panelProps.Controls.Add(this._propertyGrid);
            this._panelProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelProps.Location = new System.Drawing.Point(189, 3);
            this._panelProps.Name = "_panelProps";
            this._panelProps.Size = new System.Drawing.Size(180, 342);
            this._panelProps.TabIndex = 3;
            // 
            // _propertyGrid
            // 
            this._propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._propertyGrid.Location = new System.Drawing.Point(0, 0);
            this._propertyGrid.Name = "_propertyGrid";
            this._propertyGrid.Size = new System.Drawing.Size(180, 342);
            this._propertyGrid.TabIndex = 1;
            this._propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
            // 
            // MenuItemDist
            // 
            this.MenuItemDist.Name = "MenuItemDist";
            this.MenuItemDist.Size = new System.Drawing.Size(175, 22);
            this.MenuItemDist.Text = "Расстояние";
            this.MenuItemDist.Click += new System.EventHandler(this.MenuItemDist_Click);
            // 
            // ControlExecuteSetData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._tableLayoutPanel);
            this.Name = "ControlExecuteSetData";
            this.Size = new System.Drawing.Size(372, 348);
            this._tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridItems)).EndInit();
            this._contextMenuStrip.ResumeLayout(false);
            this._panelProps.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _tableLayoutPanel;
        private System.Windows.Forms.DataGridView _dataGridItems;
        private System.Windows.Forms.Panel _panelProps;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.ContextMenuStrip _contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem контролTapSceneToolStripMenuItem;
        private System.Windows.Forms.PropertyGrid _propertyGrid;
        private System.Windows.Forms.ToolStripMenuItem MenuItemId;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDist;
    }
}
