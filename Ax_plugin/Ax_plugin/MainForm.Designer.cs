namespace AxPlugin
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LenghtBladeTextBox = new System.Windows.Forms.TextBox();
            this.textBoxWidthButt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ButtonCreate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxMountingHole = new System.Windows.Forms.CheckBox();
            this.checkBoxFireAx = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxThicknessButt = new System.Windows.Forms.TextBox();
            this.textBoxWidthHandle = new System.Windows.Forms.TextBox();
            this.textBoxLenghtButt = new System.Windows.Forms.TextBox();
            this.TextBoxLengthHandle = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Длинна лезвия";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Длинна топорища";
            // 
            // LenghtBladeTextBox
            // 
            this.LenghtBladeTextBox.Location = new System.Drawing.Point(166, 31);
            this.LenghtBladeTextBox.Name = "LenghtBladeTextBox";
            this.LenghtBladeTextBox.Size = new System.Drawing.Size(100, 22);
            this.LenghtBladeTextBox.TabIndex = 3;
            this.LenghtBladeTextBox.Leave += new System.EventHandler(this.UniversalTextBoxLeaveHandler);
            // 
            // textBoxWidthButt
            // 
            this.textBoxWidthButt.Location = new System.Drawing.Point(166, 98);
            this.textBoxWidthButt.Name = "textBoxWidthButt";
            this.textBoxWidthButt.Size = new System.Drawing.Size(100, 22);
            this.textBoxWidthButt.TabIndex = 4;
            this.textBoxWidthButt.Leave += new System.EventHandler(this.UniversalTextBoxLeaveHandler);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(154, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "От 100 до 300 мм";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(161, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "От 80 до 150 мм";
            // 
            // ButtonCreate
            // 
            this.ButtonCreate.Location = new System.Drawing.Point(479, 302);
            this.ButtonCreate.Name = "ButtonCreate";
            this.ButtonCreate.Size = new System.Drawing.Size(103, 28);
            this.ButtonCreate.TabIndex = 11;
            this.ButtonCreate.Text = "Создать";
            this.ButtonCreate.UseVisualStyleBackColor = true;
            this.ButtonCreate.Click += new System.EventHandler(this.ButtonCreate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.LenghtBladeTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxWidthButt);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 284);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Основные входные параметры";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxMountingHole);
            this.groupBox3.Controls.Add(this.checkBoxFireAx);
            this.groupBox3.Location = new System.Drawing.Point(6, 157);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(269, 121);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Модификации";
            // 
            // checkBoxMountingHole
            // 
            this.checkBoxMountingHole.AutoSize = true;
            this.checkBoxMountingHole.Location = new System.Drawing.Point(11, 71);
            this.checkBoxMountingHole.Name = "checkBoxMountingHole";
            this.checkBoxMountingHole.Size = new System.Drawing.Size(174, 20);
            this.checkBoxMountingHole.TabIndex = 1;
            this.checkBoxMountingHole.Text = "Крепежное отверстие";
            this.checkBoxMountingHole.UseVisualStyleBackColor = true;
            this.checkBoxMountingHole.CheckedChanged += new System.EventHandler(this.checkBoxMountingHole_CheckedChanged);
            // 
            // checkBoxFireAx
            // 
            this.checkBoxFireAx.AutoSize = true;
            this.checkBoxFireAx.Location = new System.Drawing.Point(11, 28);
            this.checkBoxFireAx.Name = "checkBoxFireAx";
            this.checkBoxFireAx.Size = new System.Drawing.Size(139, 20);
            this.checkBoxFireAx.TabIndex = 0;
            this.checkBoxFireAx.Text = "Пожарный топор";
            this.checkBoxFireAx.UseVisualStyleBackColor = true;
            this.checkBoxFireAx.CheckedChanged += new System.EventHandler(this.checkBoxFireAx_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBoxThicknessButt);
            this.groupBox2.Controls.Add(this.textBoxWidthHandle);
            this.groupBox2.Controls.Add(this.textBoxLenghtButt);
            this.groupBox2.Controls.Add(this.TextBoxLengthHandle);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(301, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(292, 284);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Дополнительные входные параметры";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(165, 253);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(98, 16);
            this.label15.TabIndex = 14;
            this.label15.Text = "От 24 до 72 мм";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(165, 185);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 16);
            this.label13.TabIndex = 11;
            this.label13.Text = "От 20 до 60 мм";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(165, 123);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(105, 16);
            this.label11.TabIndex = 11;
            this.label11.Text = "От 80 до 240 мм";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(158, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "От 300 до 900 мм";
            // 
            // textBoxThicknessButt
            // 
            this.textBoxThicknessButt.Location = new System.Drawing.Point(168, 225);
            this.textBoxThicknessButt.Name = "textBoxThicknessButt";
            this.textBoxThicknessButt.Size = new System.Drawing.Size(100, 22);
            this.textBoxThicknessButt.TabIndex = 13;
            this.textBoxThicknessButt.Leave += new System.EventHandler(this.UniversalTextBoxLeaveHandler);
            // 
            // textBoxWidthHandle
            // 
            this.textBoxWidthHandle.Location = new System.Drawing.Point(168, 157);
            this.textBoxWidthHandle.Name = "textBoxWidthHandle";
            this.textBoxWidthHandle.Size = new System.Drawing.Size(100, 22);
            this.textBoxWidthHandle.TabIndex = 11;
            this.textBoxWidthHandle.Leave += new System.EventHandler(this.UniversalTextBoxLeaveHandler);
            // 
            // textBoxLenghtButt
            // 
            this.textBoxLenghtButt.Location = new System.Drawing.Point(168, 95);
            this.textBoxLenghtButt.Name = "textBoxLenghtButt";
            this.textBoxLenghtButt.Size = new System.Drawing.Size(100, 22);
            this.textBoxLenghtButt.TabIndex = 10;
            this.textBoxLenghtButt.Leave += new System.EventHandler(this.UniversalTextBoxLeaveHandler);
            // 
            // TextBoxLengthHandle
            // 
            this.TextBoxLengthHandle.Location = new System.Drawing.Point(168, 34);
            this.TextBoxLengthHandle.Name = "TextBoxLengthHandle";
            this.TextBoxLengthHandle.Size = new System.Drawing.Size(100, 22);
            this.TextBoxLengthHandle.TabIndex = 9;
            this.TextBoxLengthHandle.Leave += new System.EventHandler(this.UniversalTextBoxLeaveHandler);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(14, 228);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(125, 16);
            this.label16.TabIndex = 8;
            this.label16.Text = "Ширина топорища";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 160);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(114, 16);
            this.label12.TabIndex = 4;
            this.label12.Text = "Ширина рукояти";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 16);
            this.label10.TabIndex = 2;
            this.label10.Text = "Длинна Обуха";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(148, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "Длинна ручки топора";
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 339);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ButtonCreate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "AxPlugin";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LenghtBladeTextBox;
        private System.Windows.Forms.TextBox textBoxWidthButt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ButtonCreate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxThicknessButt;
        private System.Windows.Forms.TextBox textBoxWidthHandle;
        private System.Windows.Forms.TextBox textBoxLenghtButt;
        private System.Windows.Forms.TextBox TextBoxLengthHandle;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxMountingHole;
        private System.Windows.Forms.CheckBox checkBoxFireAx;
    }
}

