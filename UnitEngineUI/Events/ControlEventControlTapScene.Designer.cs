namespace UnitEngineUI.Events
{
    partial class ControlEventControlTapScene
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
            this.label2 = new System.Windows.Forms.Label();
            this._comboBoxName = new System.Windows.Forms.ComboBox();
            this._checkBoxIgnoreNode = new System.Windows.Forms.CheckBox();
            this._checkBoxIdentNode = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this._comboBoxState = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this._filterId = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this._comboBoxDataName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this._filterId)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Наименование:";
            // 
            // _comboBoxName
            // 
            this._comboBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._comboBoxName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._comboBoxName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._comboBoxName.FormattingEnabled = true;
            this._comboBoxName.Location = new System.Drawing.Point(3, 20);
            this._comboBoxName.Name = "_comboBoxName";
            this._comboBoxName.Size = new System.Drawing.Size(336, 21);
            this._comboBoxName.TabIndex = 9;
            this._comboBoxName.SelectedIndexChanged += new System.EventHandler(this.Control_ItemChanged);
            this._comboBoxName.TextChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _checkBoxIgnoreNode
            // 
            this._checkBoxIgnoreNode.AutoSize = true;
            this._checkBoxIgnoreNode.Location = new System.Drawing.Point(6, 51);
            this._checkBoxIgnoreNode.Name = "_checkBoxIgnoreNode";
            this._checkBoxIgnoreNode.Size = new System.Drawing.Size(139, 17);
            this._checkBoxIgnoreNode.TabIndex = 11;
            this._checkBoxIgnoreNode.Text = "Игнорировать модель";
            this._checkBoxIgnoreNode.UseVisualStyleBackColor = true;
            this._checkBoxIgnoreNode.CheckedChanged += new System.EventHandler(this.CheckBoxIgnoreNode_CheckedChanged);
            // 
            // _checkBoxIdentNode
            // 
            this._checkBoxIdentNode.AutoSize = true;
            this._checkBoxIdentNode.Location = new System.Drawing.Point(6, 74);
            this._checkBoxIdentNode.Name = "_checkBoxIdentNode";
            this._checkBoxIdentNode.Size = new System.Drawing.Size(304, 17);
            this._checkBoxIdentNode.TabIndex = 12;
            this._checkBoxIdentNode.Text = "Модели должны совпадать (отключено - не совпадать)";
            this._checkBoxIdentNode.UseVisualStyleBackColor = true;
            this._checkBoxIdentNode.CheckedChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Состояние:";
            // 
            // _comboBoxState
            // 
            this._comboBoxState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._comboBoxState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxState.FormattingEnabled = true;
            this._comboBoxState.Location = new System.Drawing.Point(74, 126);
            this._comboBoxState.Name = "_comboBoxState";
            this._comboBoxState.Size = new System.Drawing.Size(268, 21);
            this._comboBoxState.TabIndex = 13;
            this._comboBoxState.SelectedIndexChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Фильтр:";
            // 
            // _filterId
            // 
            this._filterId.Location = new System.Drawing.Point(74, 100);
            this._filterId.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this._filterId.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this._filterId.Name = "_filterId";
            this._filterId.Size = new System.Drawing.Size(120, 20);
            this._filterId.TabIndex = 18;
            this._filterId.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this._filterId.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Сохранить модель в данных:";
            // 
            // _comboBoxDataName
            // 
            this._comboBoxDataName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._comboBoxDataName.Location = new System.Drawing.Point(7, 184);
            this._comboBoxDataName.Name = "_comboBoxDataName";
            this._comboBoxDataName.Size = new System.Drawing.Size(332, 20);
            this._comboBoxDataName.TabIndex = 20;
            this._comboBoxDataName.TextChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // ControlEventControlTapScene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._comboBoxDataName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._filterId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._comboBoxState);
            this.Controls.Add(this._checkBoxIdentNode);
            this.Controls.Add(this._checkBoxIgnoreNode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._comboBoxName);
            this.Name = "ControlEventControlTapScene";
            this.Size = new System.Drawing.Size(342, 225);
            ((System.ComponentModel.ISupportInitialize)(this._filterId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _comboBoxName;
        private System.Windows.Forms.CheckBox _checkBoxIgnoreNode;
        private System.Windows.Forms.CheckBox _checkBoxIdentNode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _comboBoxState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown _filterId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _comboBoxDataName;
    }
}
