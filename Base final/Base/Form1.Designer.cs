namespace Base
{
    partial class Form1
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
            this.chanVolt = new System.Windows.Forms.ComboBox();
            this.thermType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numbTest = new System.Windows.Forms.TextBox();
            this.expRun = new System.Windows.Forms.TextBox();
            this.startBt = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.saveLocal = new System.Windows.Forms.TextBox();
            this.saveName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.searchBt = new System.Windows.Forms.Button();
            this.timeCheck = new System.Windows.Forms.CheckBox();
            this.comVolt = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comSour = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.conectVolt = new System.Windows.Forms.Button();
            this.conectSour = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.serialPortV = new System.IO.Ports.SerialPort(this.components);
            this.serialPortS = new System.IO.Ports.SerialPort(this.components);
            this.timerCOM = new System.Windows.Forms.Timer(this.components);
            this.resetBt = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.currValue = new System.Windows.Forms.TextBox();
            this.timerSpace = new System.Windows.Forms.Timer(this.components);
            this.info1 = new System.Windows.Forms.Button();
            this.timerTextBox = new System.Windows.Forms.Timer(this.components);
            this.currCombo = new System.Windows.Forms.ComboBox();
            this.testCheck = new System.Windows.Forms.CheckBox();
            this.timeDisplay = new System.Windows.Forms.Label();
            this.tempDisplay = new System.Windows.Forms.Label();
            this.timerTest = new System.Windows.Forms.Timer(this.components);
            this.graphBt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // chanVolt
            // 
            this.chanVolt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.chanVolt.FormattingEnabled = true;
            this.chanVolt.Location = new System.Drawing.Point(23, 43);
            this.chanVolt.Name = "chanVolt";
            this.chanVolt.Size = new System.Drawing.Size(121, 21);
            this.chanVolt.TabIndex = 7;
            // 
            // thermType
            // 
            this.thermType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.thermType.FormattingEnabled = true;
            this.thermType.Location = new System.Drawing.Point(23, 94);
            this.thermType.Name = "thermType";
            this.thermType.Size = new System.Drawing.Size(121, 21);
            this.thermType.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(20, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Voltmeter Channel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(20, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Thermocouple Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label4.Location = new System.Drawing.Point(191, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Number of tests";
            // 
            // numbTest
            // 
            this.numbTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.numbTest.Location = new System.Drawing.Point(194, 43);
            this.numbTest.Name = "numbTest";
            this.numbTest.Size = new System.Drawing.Size(68, 20);
            this.numbTest.TabIndex = 9;
            this.numbTest.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numbTest_KeyPress);
            // 
            // expRun
            // 
            this.expRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.expRun.Location = new System.Drawing.Point(17, 66);
            this.expRun.Multiline = true;
            this.expRun.Name = "expRun";
            this.expRun.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.expRun.Size = new System.Drawing.Size(279, 249);
            this.expRun.TabIndex = 0;
            this.expRun.TextChanged += new System.EventHandler(this.expRun_TextChanged);
            // 
            // startBt
            // 
            this.startBt.BackColor = System.Drawing.SystemColors.Control;
            this.startBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.startBt.Location = new System.Drawing.Point(17, 321);
            this.startBt.Name = "startBt";
            this.startBt.Size = new System.Drawing.Size(279, 47);
            this.startBt.TabIndex = 17;
            this.startBt.Text = "Start";
            this.startBt.UseVisualStyleBackColor = true;
            this.startBt.Click += new System.EventHandler(this.button5_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label5.Location = new System.Drawing.Point(16, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "Save at:";
            // 
            // saveLocal
            // 
            this.saveLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.saveLocal.Location = new System.Drawing.Point(19, 41);
            this.saveLocal.Name = "saveLocal";
            this.saveLocal.Size = new System.Drawing.Size(121, 20);
            this.saveLocal.TabIndex = 13;
            this.saveLocal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.saveLocal_KeyPress);
            // 
            // saveName
            // 
            this.saveName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.saveName.Location = new System.Drawing.Point(190, 41);
            this.saveName.Name = "saveName";
            this.saveName.Size = new System.Drawing.Size(121, 20);
            this.saveName.TabIndex = 15;
            this.saveName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.saveName_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label6.Location = new System.Drawing.Point(187, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Save as:";
            // 
            // searchBt
            // 
            this.searchBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.searchBt.Location = new System.Drawing.Point(19, 67);
            this.searchBt.Name = "searchBt";
            this.searchBt.Size = new System.Drawing.Size(75, 23);
            this.searchBt.TabIndex = 14;
            this.searchBt.Text = "Search";
            this.searchBt.UseVisualStyleBackColor = true;
            this.searchBt.Click += new System.EventHandler(this.button7_Click);
            // 
            // timeCheck
            // 
            this.timeCheck.AutoSize = true;
            this.timeCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.timeCheck.Location = new System.Drawing.Point(190, 71);
            this.timeCheck.Name = "timeCheck";
            this.timeCheck.Size = new System.Drawing.Size(128, 17);
            this.timeCheck.TabIndex = 16;
            this.timeCheck.Text = "Add Time to Filename";
            this.timeCheck.UseVisualStyleBackColor = true;
            // 
            // comVolt
            // 
            this.comVolt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.comVolt.FormattingEnabled = true;
            this.comVolt.Location = new System.Drawing.Point(23, 40);
            this.comVolt.Name = "comVolt";
            this.comVolt.Size = new System.Drawing.Size(121, 21);
            this.comVolt.TabIndex = 1;
            this.comVolt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comVolt_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label7.Location = new System.Drawing.Point(20, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Voltmeter COM";
            // 
            // comSour
            // 
            this.comSour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.comSour.FormattingEnabled = true;
            this.comSour.Location = new System.Drawing.Point(194, 40);
            this.comSour.Name = "comSour";
            this.comSour.Size = new System.Drawing.Size(121, 21);
            this.comSour.TabIndex = 4;
            this.comSour.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comSour_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label8.Location = new System.Drawing.Point(191, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "SourceMeter COM";
            // 
            // conectVolt
            // 
            this.conectVolt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.conectVolt.Location = new System.Drawing.Point(23, 67);
            this.conectVolt.Name = "conectVolt";
            this.conectVolt.Size = new System.Drawing.Size(121, 35);
            this.conectVolt.TabIndex = 2;
            this.conectVolt.Text = "Conect";
            this.conectVolt.UseVisualStyleBackColor = true;
            this.conectVolt.Click += new System.EventHandler(this.conectVolt_Click);
            // 
            // conectSour
            // 
            this.conectSour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.conectSour.Location = new System.Drawing.Point(194, 67);
            this.conectSour.Name = "conectSour";
            this.conectSour.Size = new System.Drawing.Size(121, 35);
            this.conectSour.TabIndex = 5;
            this.conectSour.Text = "Conect";
            this.conectSour.UseVisualStyleBackColor = true;
            this.conectSour.Click += new System.EventHandler(this.conectSour_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(13, 146);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 13);
            this.label12.TabIndex = 27;
            // 
            // timerCOM
            // 
            this.timerCOM.Interval = 1000;
            this.timerCOM.Tick += new System.EventHandler(this.timerCOM_Tick_1);
            // 
            // resetBt
            // 
            this.resetBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.resetBt.Location = new System.Drawing.Point(17, 374);
            this.resetBt.Name = "resetBt";
            this.resetBt.Size = new System.Drawing.Size(279, 23);
            this.resetBt.TabIndex = 18;
            this.resetBt.Text = "Reset";
            this.resetBt.UseVisualStyleBackColor = true;
            this.resetBt.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label15.Location = new System.Drawing.Point(191, 79);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 13);
            this.label15.TabIndex = 31;
            this.label15.Text = "Current Value";
            // 
            // currValue
            // 
            this.currValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.currValue.Location = new System.Drawing.Point(194, 95);
            this.currValue.Name = "currValue";
            this.currValue.Size = new System.Drawing.Size(68, 20);
            this.currValue.TabIndex = 11;
            this.currValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.currValue_KeyPress);
            // 
            // timerSpace
            // 
            this.timerSpace.Tick += new System.EventHandler(this.timerSpace_Tick);
            // 
            // info1
            // 
            this.info1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.info1.Location = new System.Drawing.Point(639, 4);
            this.info1.Name = "info1";
            this.info1.Size = new System.Drawing.Size(32, 21);
            this.info1.TabIndex = 19;
            this.info1.Text = "i";
            this.info1.UseVisualStyleBackColor = true;
            this.info1.Click += new System.EventHandler(this.info1_Click);
            // 
            // timerTextBox
            // 
            this.timerTextBox.Tick += new System.EventHandler(this.timerTextBox_Tick);
            // 
            // currCombo
            // 
            this.currCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.currCombo.FormattingEnabled = true;
            this.currCombo.Location = new System.Drawing.Point(268, 94);
            this.currCombo.Name = "currCombo";
            this.currCombo.Size = new System.Drawing.Size(47, 21);
            this.currCombo.TabIndex = 12;
            // 
            // testCheck
            // 
            this.testCheck.AutoSize = true;
            this.testCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.testCheck.Location = new System.Drawing.Point(268, 43);
            this.testCheck.Name = "testCheck";
            this.testCheck.Size = new System.Drawing.Size(69, 17);
            this.testCheck.TabIndex = 10;
            this.testCheck.Text = "Unlimited";
            this.testCheck.UseVisualStyleBackColor = true;
            this.testCheck.CheckedChanged += new System.EventHandler(this.testCheck_CheckedChanged);
            // 
            // timeDisplay
            // 
            this.timeDisplay.AutoSize = true;
            this.timeDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.timeDisplay.Location = new System.Drawing.Point(14, 24);
            this.timeDisplay.Name = "timeDisplay";
            this.timeDisplay.Size = new System.Drawing.Size(33, 13);
            this.timeDisplay.TabIndex = 37;
            this.timeDisplay.Text = "Time:\r\n";
            // 
            // tempDisplay
            // 
            this.tempDisplay.AutoSize = true;
            this.tempDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tempDisplay.Location = new System.Drawing.Point(14, 37);
            this.tempDisplay.Name = "tempDisplay";
            this.tempDisplay.Size = new System.Drawing.Size(70, 26);
            this.tempDisplay.TabIndex = 38;
            this.tempDisplay.Text = "Voltage:\r\nTemperature:";
            // 
            // timerTest
            // 
            this.timerTest.Tick += new System.EventHandler(this.timerTest_Tick);
            // 
            // graphBt
            // 
            this.graphBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.graphBt.Location = new System.Drawing.Point(221, 23);
            this.graphBt.Name = "graphBt";
            this.graphBt.Size = new System.Drawing.Size(75, 38);
            this.graphBt.TabIndex = 40;
            this.graphBt.Text = "Open Graph";
            this.graphBt.UseVisualStyleBackColor = true;
            this.graphBt.Click += new System.EventHandler(this.graphBt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.conectSour);
            this.groupBox1.Controls.Add(this.comVolt);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.comSour);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.conectVolt);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(12, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 119);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Voltmeter and SourceMeter Conections";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.chanVolt);
            this.groupBox2.Controls.Add(this.thermType);
            this.groupBox2.Controls.Add(this.testCheck);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.currCombo);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numbTest);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.currValue);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(12, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(340, 134);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.saveLocal);
            this.groupBox3.Controls.Add(this.saveName);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.searchBt);
            this.groupBox3.Controls.Add(this.timeCheck);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(12, 318);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(340, 109);
            this.groupBox3.TabIndex = 43;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Save Settings";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.graphBt);
            this.groupBox4.Controls.Add(this.timeDisplay);
            this.groupBox4.Controls.Add(this.tempDisplay);
            this.groupBox4.Controls.Add(this.expRun);
            this.groupBox4.Controls.Add(this.startBt);
            this.groupBox4.Controls.Add(this.resetBt);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox4.Location = new System.Drawing.Point(358, 24);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(313, 403);
            this.groupBox4.TabIndex = 44;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Test Return and Experiment Running";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 439);
            this.Controls.Add(this.info1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "4 Wire";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox chanVolt;
        private System.Windows.Forms.ComboBox thermType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox numbTest;
        private System.Windows.Forms.Button startBt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox saveLocal;
        private System.Windows.Forms.TextBox saveName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button searchBt;
        private System.Windows.Forms.CheckBox timeCheck;
        private System.Windows.Forms.ComboBox comVolt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comSour;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button conectVolt;
        private System.Windows.Forms.Button conectSour;
        private System.Windows.Forms.Label label12;
        private System.IO.Ports.SerialPort serialPortV;
        private System.IO.Ports.SerialPort serialPortS;
        private System.Windows.Forms.Timer timerCOM;
        private System.Windows.Forms.Button resetBt;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox currValue;
        private System.Windows.Forms.Timer timerSpace;
        private System.Windows.Forms.Button info1;
        private System.Windows.Forms.Timer timerTextBox;
        private System.Windows.Forms.ComboBox currCombo;
        private System.Windows.Forms.CheckBox testCheck;
        private System.Windows.Forms.Label timeDisplay;
        private System.Windows.Forms.Label tempDisplay;
        private System.Windows.Forms.Timer timerTest;
        public System.Windows.Forms.TextBox expRun;
        private System.Windows.Forms.Button graphBt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}

