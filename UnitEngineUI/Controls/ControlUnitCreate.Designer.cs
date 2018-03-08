namespace UnitEngineUI
{
    partial class ControlUnitCreate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlUnitCreate));
            this._btnSelectBehaviors = new System.Windows.Forms.Button();
            this._textBoxBehaviorsPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._rotZ = new System.Windows.Forms.NumericUpDown();
            this._rotY = new System.Windows.Forms.NumericUpDown();
            this._rotX = new System.Windows.Forms.NumericUpDown();
            this._z = new System.Windows.Forms.NumericUpDown();
            this._y = new System.Windows.Forms.NumericUpDown();
            this._x = new System.Windows.Forms.NumericUpDown();
            this._checkBoxAllowSeveralInst = new System.Windows.Forms.CheckBox();
            this._creationType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this._getPosFromTapScene = new System.Windows.Forms.CheckBox();
            this._tapSceneName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this._buttonDeleteScript = new System.Windows.Forms.Button();
            this._scriptFileName = new System.Windows.Forms.TextBox();
            this._btnSelect = new System.Windows.Forms.Button();
            this._jointName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._rotZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._rotY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._rotX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._x)).BeginInit();
            this.SuspendLayout();
            // 
            // _btnSelectBehaviors
            // 
            this._btnSelectBehaviors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSelectBehaviors.Location = new System.Drawing.Point(260, 26);
            this._btnSelectBehaviors.Name = "_btnSelectBehaviors";
            this._btnSelectBehaviors.Size = new System.Drawing.Size(28, 23);
            this._btnSelectBehaviors.TabIndex = 0;
            this._btnSelectBehaviors.Text = "...";
            this._btnSelectBehaviors.UseVisualStyleBackColor = true;
            this._btnSelectBehaviors.Click += new System.EventHandler(this.BtnSelectBehaviors_Click);
            // 
            // _textBoxBehaviorsPath
            // 
            this._textBoxBehaviorsPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxBehaviorsPath.Location = new System.Drawing.Point(6, 28);
            this._textBoxBehaviorsPath.Name = "_textBoxBehaviorsPath";
            this._textBoxBehaviorsPath.ReadOnly = true;
            this._textBoxBehaviorsPath.Size = new System.Drawing.Size(251, 20);
            this._textBoxBehaviorsPath.TabIndex = 1;
            this._textBoxBehaviorsPath.TextChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Путь  до файла поведения :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Поворот (X,Y,Z):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Позиция (X,Y,Z):";
            // 
            // _rotZ
            // 
            this._rotZ.DecimalPlaces = 1;
            this._rotZ.Location = new System.Drawing.Point(175, 254);
            this._rotZ.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this._rotZ.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this._rotZ.Name = "_rotZ";
            this._rotZ.Size = new System.Drawing.Size(78, 20);
            this._rotZ.TabIndex = 18;
            this._rotZ.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _rotY
            // 
            this._rotY.DecimalPlaces = 1;
            this._rotY.Location = new System.Drawing.Point(91, 254);
            this._rotY.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this._rotY.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this._rotY.Name = "_rotY";
            this._rotY.Size = new System.Drawing.Size(78, 20);
            this._rotY.TabIndex = 17;
            this._rotY.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _rotX
            // 
            this._rotX.DecimalPlaces = 1;
            this._rotX.Location = new System.Drawing.Point(6, 254);
            this._rotX.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this._rotX.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this._rotX.Name = "_rotX";
            this._rotX.Size = new System.Drawing.Size(78, 20);
            this._rotX.TabIndex = 16;
            this._rotX.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _z
            // 
            this._z.DecimalPlaces = 1;
            this._z.Location = new System.Drawing.Point(175, 119);
            this._z.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this._z.Name = "_z";
            this._z.Size = new System.Drawing.Size(78, 20);
            this._z.TabIndex = 15;
            this._z.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _y
            // 
            this._y.DecimalPlaces = 1;
            this._y.Location = new System.Drawing.Point(91, 119);
            this._y.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this._y.Name = "_y";
            this._y.Size = new System.Drawing.Size(78, 20);
            this._y.TabIndex = 14;
            this._y.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _x
            // 
            this._x.DecimalPlaces = 1;
            this._x.Location = new System.Drawing.Point(6, 119);
            this._x.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this._x.Name = "_x";
            this._x.Size = new System.Drawing.Size(78, 20);
            this._x.TabIndex = 13;
            this._x.ValueChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _checkBoxAllowSeveralInst
            // 
            this._checkBoxAllowSeveralInst.AutoSize = true;
            this._checkBoxAllowSeveralInst.Location = new System.Drawing.Point(6, 291);
            this._checkBoxAllowSeveralInst.Name = "_checkBoxAllowSeveralInst";
            this._checkBoxAllowSeveralInst.Size = new System.Drawing.Size(265, 17);
            this._checkBoxAllowSeveralInst.TabIndex = 21;
            this._checkBoxAllowSeveralInst.Text = "Позволить создавать несколько экземпляров";
            this._checkBoxAllowSeveralInst.UseVisualStyleBackColor = true;
            this._checkBoxAllowSeveralInst.CheckedChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _creationType
            // 
            this._creationType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._creationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._creationType.FormattingEnabled = true;
            this._creationType.Location = new System.Drawing.Point(6, 68);
            this._creationType.Name = "_creationType";
            this._creationType.Size = new System.Drawing.Size(247, 21);
            this._creationType.TabIndex = 22;
            this._creationType.SelectedIndexChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Тип создаваемого юнита :";
            // 
            // _getPosFromTapScene
            // 
            this._getPosFromTapScene.AutoSize = true;
            this._getPosFromTapScene.Location = new System.Drawing.Point(6, 147);
            this._getPosFromTapScene.Name = "_getPosFromTapScene";
            this._getPosFromTapScene.Size = new System.Drawing.Size(188, 17);
            this._getPosFromTapScene.TabIndex = 24;
            this._getPosFromTapScene.Text = "Получить позицию из TapScene";
            this._getPosFromTapScene.UseVisualStyleBackColor = true;
            this._getPosFromTapScene.CheckedChanged += new System.EventHandler(this.GetPosFromTapScene_CheckedChanged);
            // 
            // _tapSceneName
            // 
            this._tapSceneName.Enabled = false;
            this._tapSceneName.Location = new System.Drawing.Point(101, 171);
            this._tapSceneName.Name = "_tapSceneName";
            this._tapSceneName.Size = new System.Drawing.Size(156, 20);
            this._tapSceneName.TabIndex = 25;
            this._tapSceneName.TextChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Имя TapScene:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 324);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Начальный скрипт юнита:";
            // 
            // _buttonDeleteScript
            // 
            this._buttonDeleteScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonDeleteScript.Image = ((System.Drawing.Image)(resources.GetObject("_buttonDeleteScript.Image")));
            this._buttonDeleteScript.Location = new System.Drawing.Point(190, 338);
            this._buttonDeleteScript.Name = "_buttonDeleteScript";
            this._buttonDeleteScript.Size = new System.Drawing.Size(30, 23);
            this._buttonDeleteScript.TabIndex = 32;
            this._buttonDeleteScript.UseVisualStyleBackColor = true;
            this._buttonDeleteScript.Click += new System.EventHandler(this._buttonDeleteScript_Click);
            // 
            // _scriptFileName
            // 
            this._scriptFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._scriptFileName.Location = new System.Drawing.Point(6, 340);
            this._scriptFileName.Name = "_scriptFileName";
            this._scriptFileName.ReadOnly = true;
            this._scriptFileName.Size = new System.Drawing.Size(142, 20);
            this._scriptFileName.TabIndex = 31;
            this._scriptFileName.TextChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // _btnSelect
            // 
            this._btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("_btnSelect.Image")));
            this._btnSelect.Location = new System.Drawing.Point(154, 338);
            this._btnSelect.Name = "_btnSelect";
            this._btnSelect.Size = new System.Drawing.Size(30, 23);
            this._btnSelect.TabIndex = 29;
            this._btnSelect.UseVisualStyleBackColor = true;
            this._btnSelect.Click += new System.EventHandler(this._btnSelect_Click);
            // 
            // _jointName
            // 
            this._jointName.Location = new System.Drawing.Point(101, 202);
            this._jointName.Name = "_jointName";
            this._jointName.Size = new System.Drawing.Size(156, 20);
            this._jointName.TabIndex = 33;
            this._jointName.TextChanged += new System.EventHandler(this.Control_ItemChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 205);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Joint Name:";
            // 
            // ControlUnitCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label7);
            this.Controls.Add(this._jointName);
            this.Controls.Add(this._buttonDeleteScript);
            this.Controls.Add(this._scriptFileName);
            this.Controls.Add(this._btnSelect);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._tapSceneName);
            this.Controls.Add(this._getPosFromTapScene);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._creationType);
            this.Controls.Add(this._checkBoxAllowSeveralInst);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._rotZ);
            this.Controls.Add(this._rotY);
            this.Controls.Add(this._rotX);
            this.Controls.Add(this._z);
            this.Controls.Add(this._y);
            this.Controls.Add(this._x);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._textBoxBehaviorsPath);
            this.Controls.Add(this._btnSelectBehaviors);
            this.Name = "ControlUnitCreate";
            this.Size = new System.Drawing.Size(295, 499);
            ((System.ComponentModel.ISupportInitialize)(this._rotZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._rotY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._rotX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._x)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btnSelectBehaviors;
        private System.Windows.Forms.TextBox _textBoxBehaviorsPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown _rotZ;
        private System.Windows.Forms.NumericUpDown _rotY;
        private System.Windows.Forms.NumericUpDown _rotX;
        private System.Windows.Forms.NumericUpDown _z;
        private System.Windows.Forms.NumericUpDown _y;
        private System.Windows.Forms.NumericUpDown _x;
        private System.Windows.Forms.CheckBox _checkBoxAllowSeveralInst;
        private System.Windows.Forms.ComboBox _creationType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox _getPosFromTapScene;
        private System.Windows.Forms.TextBox _tapSceneName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button _buttonDeleteScript;
        private System.Windows.Forms.TextBox _scriptFileName;
        private System.Windows.Forms.Button _btnSelect;
        private System.Windows.Forms.TextBox _jointName;
        private System.Windows.Forms.Label label7;
    }
}
