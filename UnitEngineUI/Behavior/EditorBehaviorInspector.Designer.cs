namespace UnitEngineUI.Behavior
{
    partial class EditorBehaviorInspector
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorBehaviorInspector));
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._buttonCreateGroup = new System.Windows.Forms.Button();
            this._buttonDeleteAnim = new System.Windows.Forms.Button();
            this._treeViewItems = new System.Windows.Forms.TreeView();
            this._contextMenuTreeItems = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._contextNewAnim = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemAddBlockAction = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemAddXRef = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemCreateGroup = new System.Windows.Forms.ToolStripMenuItem();
            this._contextDeleteAnim = new System.Windows.Forms.ToolStripMenuItem();
            this._imageTree = new System.Windows.Forms.ImageList(this.components);
            this._textBoxFilter = new System.Windows.Forms.TextBox();
            this._panelDesc = new System.Windows.Forms.Panel();
            this._checkBoxNoChangeCurrentAction = new System.Windows.Forms.CheckBox();
            this._labelAnimation = new System.Windows.Forms.Label();
            this._labelBreak = new System.Windows.Forms.Label();
            this._labelGlobalParams = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._dataGridViewExecute = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this._nmrPropPriority = new System.Windows.Forms.NumericUpDown();
            this._labelParams = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._dataGridViewClauses = new System.Windows.Forms.DataGridView();
            this._columnTypeEvent = new System.Windows.Forms.DataGridViewImageColumn();
            this._сolumnNameEvent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._contextMenuClause = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemClauseDistance = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemPositionInsideArea = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemClauseSelection = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemOperator = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemTime = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemScriptAdd = new System.Windows.Forms.ToolStripMenuItem();
            this._textBoxPropName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._contextMenuStripBlockAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._menuItemAddBlockActionChild = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._contextMenuParameters = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._contextParametersMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemChildUnitAdd = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._contextMenuTreeItems.SuspendLayout();
            this._panelDesc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewExecute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nmrPropPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewClauses)).BeginInit();
            this._contextMenuClause.SuspendLayout();
            this._contextMenuStripBlockAction.SuspendLayout();
            this._contextMenuParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(0, 0);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._buttonCreateGroup);
            this._splitContainer.Panel1.Controls.Add(this._buttonDeleteAnim);
            this._splitContainer.Panel1.Controls.Add(this._treeViewItems);
            this._splitContainer.Panel1.Controls.Add(this._textBoxFilter);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._panelDesc);
            this._splitContainer.Size = new System.Drawing.Size(571, 536);
            this._splitContainer.SplitterDistance = 274;
            this._splitContainer.TabIndex = 0;
            // 
            // _buttonCreateGroup
            // 
            this._buttonCreateGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonCreateGroup.Image = ((System.Drawing.Image)(resources.GetObject("_buttonCreateGroup.Image")));
            this._buttonCreateGroup.Location = new System.Drawing.Point(3, 501);
            this._buttonCreateGroup.Name = "_buttonCreateGroup";
            this._buttonCreateGroup.Size = new System.Drawing.Size(32, 23);
            this._buttonCreateGroup.TabIndex = 8;
            this._buttonCreateGroup.UseVisualStyleBackColor = true;
            this._buttonCreateGroup.Click += new System.EventHandler(this.CreateGroup_Click);
            // 
            // _buttonDeleteAnim
            // 
            this._buttonDeleteAnim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonDeleteAnim.Image = ((System.Drawing.Image)(resources.GetObject("_buttonDeleteAnim.Image")));
            this._buttonDeleteAnim.Location = new System.Drawing.Point(41, 501);
            this._buttonDeleteAnim.Name = "_buttonDeleteAnim";
            this._buttonDeleteAnim.Size = new System.Drawing.Size(34, 23);
            this._buttonDeleteAnim.TabIndex = 7;
            this._buttonDeleteAnim.UseVisualStyleBackColor = true;
            this._buttonDeleteAnim.Click += new System.EventHandler(this.Delete_Click);
            // 
            // _treeViewItems
            // 
            this._treeViewItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._treeViewItems.ContextMenuStrip = this._contextMenuTreeItems;
            this._treeViewItems.HideSelection = false;
            this._treeViewItems.ImageIndex = 1;
            this._treeViewItems.ImageList = this._imageTree;
            this._treeViewItems.Location = new System.Drawing.Point(0, 29);
            this._treeViewItems.Name = "_treeViewItems";
            this._treeViewItems.SelectedImageIndex = 0;
            this._treeViewItems.Size = new System.Drawing.Size(271, 466);
            this._treeViewItems.TabIndex = 6;
            this._treeViewItems.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView_AfterLabelEdit);
            this._treeViewItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewItems_AfterSelect);
            this._treeViewItems.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TreeViewItems_PreviewKeyDown);
            // 
            // _contextMenuTreeItems
            // 
            this._contextMenuTreeItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._contextNewAnim,
            this._menuItemAddBlockAction,
            this._menuItemAddXRef,
            this._menuItemCreateGroup,
            this._contextDeleteAnim});
            this._contextMenuTreeItems.Name = "_contextMenuTreeAnim";
            this._contextMenuTreeItems.Size = new System.Drawing.Size(201, 114);
            // 
            // _contextNewAnim
            // 
            this._contextNewAnim.Name = "_contextNewAnim";
            this._contextNewAnim.Size = new System.Drawing.Size(200, 22);
            this._contextNewAnim.Text = "Создать действие";
            this._contextNewAnim.Click += new System.EventHandler(this.MenuItemNewAction_Click);
            // 
            // _menuItemAddBlockAction
            // 
            this._menuItemAddBlockAction.Name = "_menuItemAddBlockAction";
            this._menuItemAddBlockAction.Size = new System.Drawing.Size(200, 22);
            this._menuItemAddBlockAction.Text = "Создать блок действий";
            this._menuItemAddBlockAction.Click += new System.EventHandler(this.MenuItemAddBlockAction_Click);
            // 
            // _menuItemAddXRef
            // 
            this._menuItemAddXRef.Name = "_menuItemAddXRef";
            this._menuItemAddXRef.Size = new System.Drawing.Size(200, 22);
            this._menuItemAddXRef.Text = "Подключить ссылку";
            this._menuItemAddXRef.Click += new System.EventHandler(this.MenuItemAddXRef_Click);
            // 
            // _menuItemCreateGroup
            // 
            this._menuItemCreateGroup.Name = "_menuItemCreateGroup";
            this._menuItemCreateGroup.Size = new System.Drawing.Size(200, 22);
            this._menuItemCreateGroup.Text = "Создать группу";
            this._menuItemCreateGroup.Click += new System.EventHandler(this.CreateGroup_Click);
            // 
            // _contextDeleteAnim
            // 
            this._contextDeleteAnim.Name = "_contextDeleteAnim";
            this._contextDeleteAnim.Size = new System.Drawing.Size(200, 22);
            this._contextDeleteAnim.Text = "Удалить";
            this._contextDeleteAnim.Click += new System.EventHandler(this.Delete_Click);
            // 
            // _imageTree
            // 
            this._imageTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageTree.ImageStream")));
            this._imageTree.TransparentColor = System.Drawing.Color.Transparent;
            this._imageTree.Images.SetKeyName(0, "folder.ico");
            this._imageTree.Images.SetKeyName(1, "item.ico");
            this._imageTree.Images.SetKeyName(2, "xref.ico");
            this._imageTree.Images.SetKeyName(3, "block.ico");
            // 
            // _textBoxFilter
            // 
            this._textBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxFilter.Location = new System.Drawing.Point(3, 3);
            this._textBoxFilter.Name = "_textBoxFilter";
            this._textBoxFilter.Size = new System.Drawing.Size(268, 20);
            this._textBoxFilter.TabIndex = 5;
            // 
            // _panelDesc
            // 
            this._panelDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panelDesc.Controls.Add(this._checkBoxNoChangeCurrentAction);
            this._panelDesc.Controls.Add(this._labelAnimation);
            this._panelDesc.Controls.Add(this._labelBreak);
            this._panelDesc.Controls.Add(this._labelGlobalParams);
            this._panelDesc.Controls.Add(this.label5);
            this._panelDesc.Controls.Add(this._dataGridViewExecute);
            this._panelDesc.Controls.Add(this.label4);
            this._panelDesc.Controls.Add(this._nmrPropPriority);
            this._panelDesc.Controls.Add(this._labelParams);
            this._panelDesc.Controls.Add(this.label2);
            this._panelDesc.Controls.Add(this._dataGridViewClauses);
            this._panelDesc.Controls.Add(this._textBoxPropName);
            this._panelDesc.Controls.Add(this.label1);
            this._panelDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelDesc.Location = new System.Drawing.Point(0, 0);
            this._panelDesc.Name = "_panelDesc";
            this._panelDesc.Size = new System.Drawing.Size(293, 536);
            this._panelDesc.TabIndex = 2;
            // 
            // _checkBoxNoChangeCurrentAction
            // 
            this._checkBoxNoChangeCurrentAction.AutoSize = true;
            this._checkBoxNoChangeCurrentAction.Location = new System.Drawing.Point(10, 64);
            this._checkBoxNoChangeCurrentAction.Name = "_checkBoxNoChangeCurrentAction";
            this._checkBoxNoChangeCurrentAction.Size = new System.Drawing.Size(185, 17);
            this._checkBoxNoChangeCurrentAction.TabIndex = 3;
            this._checkBoxNoChangeCurrentAction.Text = "Неизменять текущее действие";
            this._checkBoxNoChangeCurrentAction.UseVisualStyleBackColor = true;
            this._checkBoxNoChangeCurrentAction.CheckedChanged += new System.EventHandler(this.ItemChanged);
            // 
            // _labelAnimation
            // 
            this._labelAnimation.AutoSize = true;
            this._labelAnimation.Cursor = System.Windows.Forms.Cursors.Hand;
            this._labelAnimation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._labelAnimation.ForeColor = System.Drawing.Color.Black;
            this._labelAnimation.Location = new System.Drawing.Point(7, 91);
            this._labelAnimation.Name = "_labelAnimation";
            this._labelAnimation.Size = new System.Drawing.Size(70, 13);
            this._labelAnimation.TabIndex = 4;
            this._labelAnimation.Text = "Анимация ...";
            this._labelAnimation.Click += new System.EventHandler(this.LabelAnimation_Click);
            // 
            // _labelBreak
            // 
            this._labelBreak.AutoSize = true;
            this._labelBreak.Cursor = System.Windows.Forms.Cursors.Hand;
            this._labelBreak.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._labelBreak.ForeColor = System.Drawing.Color.Black;
            this._labelBreak.Location = new System.Drawing.Point(7, 145);
            this._labelBreak.Name = "_labelBreak";
            this._labelBreak.Size = new System.Drawing.Size(105, 13);
            this._labelBreak.TabIndex = 7;
            this._labelBreak.Text = "Условия отмены ...";
            this._labelBreak.Click += new System.EventHandler(this.LabelBreak_Click);
            // 
            // _labelGlobalParams
            // 
            this._labelGlobalParams.AutoSize = true;
            this._labelGlobalParams.Cursor = System.Windows.Forms.Cursors.Hand;
            this._labelGlobalParams.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._labelGlobalParams.ForeColor = System.Drawing.Color.Black;
            this._labelGlobalParams.Location = new System.Drawing.Point(7, 126);
            this._labelGlobalParams.Name = "_labelGlobalParams";
            this._labelGlobalParams.Size = new System.Drawing.Size(141, 13);
            this._labelGlobalParams.TabIndex = 6;
            this._labelGlobalParams.Text = "Глобальные параметры ...";
            this._labelGlobalParams.Click += new System.EventHandler(this.LabelGlobalParams_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 311);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 88;
            this.label5.Text = " Выполнить действия:";
            // 
            // _dataGridViewExecute
            // 
            this._dataGridViewExecute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._dataGridViewExecute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewExecute.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn2,
            this.dataGridViewTextBoxColumn2});
            this._dataGridViewExecute.Location = new System.Drawing.Point(8, 327);
            this._dataGridViewExecute.MultiSelect = false;
            this._dataGridViewExecute.Name = "_dataGridViewExecute";
            this._dataGridViewExecute.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dataGridViewExecute.Size = new System.Drawing.Size(282, 153);
            this._dataGridViewExecute.TabIndex = 9;
            this._dataGridViewExecute.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellValueChanged);
            this._dataGridViewExecute.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DataGridView_RowsAdded);
            this._dataGridViewExecute.SelectionChanged += new System.EventHandler(this.DataGridViewExecute_SelectionChanged);
            this._dataGridViewExecute.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridView_UserDeletedRow);
            this._dataGridViewExecute.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DataGridViewExecute_MouseClick);
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn2.Image")));
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn2.Width = 21;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Имя";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 86;
            this.label4.Text = "Приоритет:";
            // 
            // _nmrPropPriority
            // 
            this._nmrPropPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._nmrPropPriority.Location = new System.Drawing.Point(101, 32);
            this._nmrPropPriority.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this._nmrPropPriority.Name = "_nmrPropPriority";
            this._nmrPropPriority.Size = new System.Drawing.Size(187, 20);
            this._nmrPropPriority.TabIndex = 2;
            this._nmrPropPriority.ValueChanged += new System.EventHandler(this.ItemChanged);
            // 
            // _labelParams
            // 
            this._labelParams.AutoSize = true;
            this._labelParams.Cursor = System.Windows.Forms.Cursors.Hand;
            this._labelParams.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._labelParams.ForeColor = System.Drawing.Color.Black;
            this._labelParams.Location = new System.Drawing.Point(7, 107);
            this._labelParams.Name = "_labelParams";
            this._labelParams.Size = new System.Drawing.Size(112, 13);
            this._labelParams.TabIndex = 5;
            this._labelParams.Text = "Параметры юнита ...";
            this._labelParams.Click += new System.EventHandler(this.LabelParams_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 11;
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
            this._dataGridViewClauses.ContextMenuStrip = this._contextMenuClause;
            this._dataGridViewClauses.Location = new System.Drawing.Point(6, 184);
            this._dataGridViewClauses.MultiSelect = false;
            this._dataGridViewClauses.Name = "_dataGridViewClauses";
            this._dataGridViewClauses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dataGridViewClauses.Size = new System.Drawing.Size(282, 124);
            this._dataGridViewClauses.TabIndex = 8;
            this._dataGridViewClauses.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellValueChanged);
            this._dataGridViewClauses.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DataGridView_RowsAdded);
            this._dataGridViewClauses.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridView_UserDeletedRow);
            this._dataGridViewClauses.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DataGridViewClauses_MouseClick);
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
            // _contextMenuClause
            // 
            this._contextMenuClause.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this._menuItemClauseDistance,
            this._menuItemPositionInsideArea,
            this._menuItemClauseSelection,
            this._menuItemOperator,
            this._menuItemTime,
            this._menuItemScriptAdd,
            this._menuItemChildUnitAdd});
            this._contextMenuClause.Name = "_contextMenuProps";
            this._contextMenuClause.Size = new System.Drawing.Size(218, 202);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(217, 22);
            this.toolStripMenuItem1.Text = "Событие контрола";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.EventControlAdd_Click);
            // 
            // _menuItemClauseDistance
            // 
            this._menuItemClauseDistance.Name = "_menuItemClauseDistance";
            this._menuItemClauseDistance.Size = new System.Drawing.Size(217, 22);
            this._menuItemClauseDistance.Text = "Расстояние";
            this._menuItemClauseDistance.Click += new System.EventHandler(this.EventDistanceAdd_Click);
            // 
            // _menuItemPositionInsideArea
            // 
            this._menuItemPositionInsideArea.Name = "_menuItemPositionInsideArea";
            this._menuItemPositionInsideArea.Size = new System.Drawing.Size(217, 22);
            this._menuItemPositionInsideArea.Text = "Позиция внутри площади";
            this._menuItemPositionInsideArea.Click += new System.EventHandler(this.EventPositionInsideArea_Click);
            // 
            // _menuItemClauseSelection
            // 
            this._menuItemClauseSelection.Name = "_menuItemClauseSelection";
            this._menuItemClauseSelection.Size = new System.Drawing.Size(217, 22);
            this._menuItemClauseSelection.Text = "Выборка объектов";
            this._menuItemClauseSelection.Click += new System.EventHandler(this.EventSelection_Click);
            // 
            // _menuItemOperator
            // 
            this._menuItemOperator.Name = "_menuItemOperator";
            this._menuItemOperator.Size = new System.Drawing.Size(217, 22);
            this._menuItemOperator.Text = "Оператор";
            this._menuItemOperator.Click += new System.EventHandler(this.EventOperator_Click);
            // 
            // _menuItemTime
            // 
            this._menuItemTime.Name = "_menuItemTime";
            this._menuItemTime.Size = new System.Drawing.Size(217, 22);
            this._menuItemTime.Text = "Время";
            this._menuItemTime.Click += new System.EventHandler(this.EventTime_Click);
            // 
            // _menuItemScriptAdd
            // 
            this._menuItemScriptAdd.Name = "_menuItemScriptAdd";
            this._menuItemScriptAdd.Size = new System.Drawing.Size(217, 22);
            this._menuItemScriptAdd.Text = "Скрипт";
            this._menuItemScriptAdd.Click += new System.EventHandler(this.EventScript_Click);
            // 
            // _textBoxPropName
            // 
            this._textBoxPropName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxPropName.Location = new System.Drawing.Point(101, 7);
            this._textBoxPropName.Name = "_textBoxPropName";
            this._textBoxPropName.Size = new System.Drawing.Size(187, 20);
            this._textBoxPropName.TabIndex = 1;
            this._textBoxPropName.TextChanged += new System.EventHandler(this.ItemChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Наименование:";
            // 
            // _contextMenuStripBlockAction
            // 
            this._contextMenuStripBlockAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuItemAddBlockActionChild,
            this.удалитьToolStripMenuItem});
            this._contextMenuStripBlockAction.Name = "_contextMenuStripBlockAction";
            this._contextMenuStripBlockAction.Size = new System.Drawing.Size(235, 48);
            // 
            // _menuItemAddBlockActionChild
            // 
            this._menuItemAddBlockActionChild.Name = "_menuItemAddBlockActionChild";
            this._menuItemAddBlockActionChild.Size = new System.Drawing.Size(234, 22);
            this._menuItemAddBlockActionChild.Text = "Добавить дочерние действие";
            this._menuItemAddBlockActionChild.Click += new System.EventHandler(this.MenuItemNewAction_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.Delete_Click);
            // 
            // _contextMenuParameters
            // 
            this._contextMenuParameters.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._contextParametersMenuItemAdd});
            this._contextMenuParameters.Name = "_contextMenuProps";
            this._contextMenuParameters.Size = new System.Drawing.Size(127, 26);
            // 
            // _contextParametersMenuItemAdd
            // 
            this._contextParametersMenuItemAdd.Name = "_contextParametersMenuItemAdd";
            this._contextParametersMenuItemAdd.Size = new System.Drawing.Size(126, 22);
            this._contextParametersMenuItemAdd.Text = "Добавить";
            this._contextParametersMenuItemAdd.Click += new System.EventHandler(this.ParamtersAdd_Click);
            // 
            // _menuItemChildUnitAdd
            // 
            this._menuItemChildUnitAdd.Name = "_menuItemChildUnitAdd";
            this._menuItemChildUnitAdd.Size = new System.Drawing.Size(217, 22);
            this._menuItemChildUnitAdd.Text = "Дочерний юнит";
            this._menuItemChildUnitAdd.Click += new System.EventHandler(this.EventChildUnit_Click);
            // 
            // EditorBehaviorInspector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 536);
            this.Controls.Add(this._splitContainer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditorBehaviorInspector";
            this.Text = "Инспектор действий";
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel1.PerformLayout();
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this._contextMenuTreeItems.ResumeLayout(false);
            this._panelDesc.ResumeLayout(false);
            this._panelDesc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewExecute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nmrPropPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewClauses)).EndInit();
            this._contextMenuClause.ResumeLayout(false);
            this._contextMenuStripBlockAction.ResumeLayout(false);
            this._contextMenuParameters.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.TextBox _textBoxFilter;
        private System.Windows.Forms.TreeView _treeViewItems;
        private System.Windows.Forms.Button _buttonCreateGroup;
        private System.Windows.Forms.Button _buttonDeleteAnim;
        private System.Windows.Forms.Panel _panelDesc;
        private System.Windows.Forms.CheckBox _checkBoxNoChangeCurrentAction;
        private System.Windows.Forms.Label _labelAnimation;
        private System.Windows.Forms.Label _labelBreak;
        private System.Windows.Forms.Label _labelGlobalParams;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView _dataGridViewExecute;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown _nmrPropPriority;
        private System.Windows.Forms.Label _labelParams;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView _dataGridViewClauses;
        private System.Windows.Forms.TextBox _textBoxPropName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip _contextMenuTreeItems;
        private System.Windows.Forms.ToolStripMenuItem _contextNewAnim;
        private System.Windows.Forms.ToolStripMenuItem _menuItemAddBlockAction;
        private System.Windows.Forms.ToolStripMenuItem _menuItemAddXRef;
        private System.Windows.Forms.ToolStripMenuItem _contextDeleteAnim;
        private System.Windows.Forms.ContextMenuStrip _contextMenuStripBlockAction;
        private System.Windows.Forms.ToolStripMenuItem _menuItemAddBlockActionChild;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _menuItemCreateGroup;
        private System.Windows.Forms.ContextMenuStrip _contextMenuParameters;
        private System.Windows.Forms.ToolStripMenuItem _contextParametersMenuItemAdd;
        private System.Windows.Forms.ImageList _imageTree;
        private System.Windows.Forms.ContextMenuStrip _contextMenuClause;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem _menuItemClauseDistance;
        private System.Windows.Forms.ToolStripMenuItem _menuItemPositionInsideArea;
        private System.Windows.Forms.ToolStripMenuItem _menuItemClauseSelection;
        private System.Windows.Forms.ToolStripMenuItem _menuItemOperator;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewImageColumn _columnTypeEvent;
        private System.Windows.Forms.DataGridViewTextBoxColumn _сolumnNameEvent;
        private System.Windows.Forms.ToolStripMenuItem _menuItemTime;
        private System.Windows.Forms.ToolStripMenuItem _menuItemScriptAdd;
        private System.Windows.Forms.ToolStripMenuItem _menuItemChildUnitAdd;
    }
}