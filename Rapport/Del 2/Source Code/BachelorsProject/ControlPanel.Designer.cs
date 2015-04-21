namespace BachelorsProject
{
	partial class ControlPanel
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
			this.startButton = new System.Windows.Forms.Button();
			this.resetButton = new System.Windows.Forms.Button();
			this.envA = new System.Windows.Forms.RadioButton();
			this.envB = new System.Windows.Forms.RadioButton();
			this.envC = new System.Windows.Forms.RadioButton();
			this.flowFieldRadioButton = new System.Windows.Forms.RadioButton();
			this.aStarRadioButton = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.envD = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.showLevelPolygons = new System.Windows.Forms.CheckBox();
			this.showFlowField = new System.Windows.Forms.CheckBox();
			this.showAStarPath = new System.Windows.Forms.CheckBox();
			this.showGoalPositions = new System.Windows.Forms.CheckBox();
			this.agentCount = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.lowestMemoryUsageLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.highestMemoryUsageLabel = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.averageMemoryUsageLabel = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.currentMemoryUsageLabel = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.saveMemoryUsageInfo = new System.Windows.Forms.Button();
			this.saveFile = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.forceGCButton = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.agentCount)).BeginInit();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(12, 265);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(108, 23);
			this.startButton.TabIndex = 0;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// resetButton
			// 
			this.resetButton.Location = new System.Drawing.Point(126, 265);
			this.resetButton.Name = "resetButton";
			this.resetButton.Size = new System.Drawing.Size(108, 23);
			this.resetButton.TabIndex = 1;
			this.resetButton.Text = "Reset";
			this.resetButton.UseVisualStyleBackColor = true;
			this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
			// 
			// envA
			// 
			this.envA.AutoSize = true;
			this.envA.Checked = true;
			this.envA.Location = new System.Drawing.Point(6, 19);
			this.envA.Name = "envA";
			this.envA.Size = new System.Drawing.Size(94, 17);
			this.envA.TabIndex = 2;
			this.envA.TabStop = true;
			this.envA.Text = "Environment A";
			this.envA.UseVisualStyleBackColor = true;
			this.envA.CheckedChanged += new System.EventHandler(this.envA_CheckedChanged);
			// 
			// envB
			// 
			this.envB.AutoSize = true;
			this.envB.Location = new System.Drawing.Point(6, 42);
			this.envB.Name = "envB";
			this.envB.Size = new System.Drawing.Size(94, 17);
			this.envB.TabIndex = 2;
			this.envB.Text = "Environment B";
			this.envB.UseVisualStyleBackColor = true;
			this.envB.CheckedChanged += new System.EventHandler(this.envB_CheckedChanged);
			// 
			// envC
			// 
			this.envC.AutoSize = true;
			this.envC.Location = new System.Drawing.Point(6, 65);
			this.envC.Name = "envC";
			this.envC.Size = new System.Drawing.Size(94, 17);
			this.envC.TabIndex = 2;
			this.envC.Text = "Environment C";
			this.envC.UseVisualStyleBackColor = true;
			this.envC.CheckedChanged += new System.EventHandler(this.envC_CheckedChanged);
			// 
			// flowFieldRadioButton
			// 
			this.flowFieldRadioButton.AutoSize = true;
			this.flowFieldRadioButton.Checked = true;
			this.flowFieldRadioButton.Location = new System.Drawing.Point(6, 19);
			this.flowFieldRadioButton.Name = "flowFieldRadioButton";
			this.flowFieldRadioButton.Size = new System.Drawing.Size(72, 17);
			this.flowFieldRadioButton.TabIndex = 3;
			this.flowFieldRadioButton.TabStop = true;
			this.flowFieldRadioButton.Text = "Flow Field";
			this.flowFieldRadioButton.UseVisualStyleBackColor = true;
			this.flowFieldRadioButton.CheckedChanged += new System.EventHandler(this.flowFieldRadioButton_CheckedChanged);
			// 
			// aStarRadioButton
			// 
			this.aStarRadioButton.AutoSize = true;
			this.aStarRadioButton.Location = new System.Drawing.Point(6, 42);
			this.aStarRadioButton.Name = "aStarRadioButton";
			this.aStarRadioButton.Size = new System.Drawing.Size(51, 17);
			this.aStarRadioButton.TabIndex = 3;
			this.aStarRadioButton.Text = "AStar";
			this.aStarRadioButton.UseVisualStyleBackColor = true;
			this.aStarRadioButton.CheckedChanged += new System.EventHandler(this.aStarRadioButton_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.envD);
			this.groupBox1.Controls.Add(this.envA);
			this.groupBox1.Controls.Add(this.envB);
			this.groupBox1.Controls.Add(this.envC);
			this.groupBox1.Location = new System.Drawing.Point(12, 94);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 128);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Environments";
			// 
			// envD
			// 
			this.envD.AutoSize = true;
			this.envD.Location = new System.Drawing.Point(6, 88);
			this.envD.Name = "envD";
			this.envD.Size = new System.Drawing.Size(95, 17);
			this.envD.TabIndex = 3;
			this.envD.Text = "Environment D";
			this.envD.UseVisualStyleBackColor = true;
			this.envD.CheckedChanged += new System.EventHandler(this.envD_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.flowFieldRadioButton);
			this.groupBox2.Controls.Add(this.aStarRadioButton);
			this.groupBox2.Location = new System.Drawing.Point(12, 22);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 66);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Navigation Techniques";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.showLevelPolygons);
			this.groupBox3.Controls.Add(this.showFlowField);
			this.groupBox3.Controls.Add(this.showAStarPath);
			this.groupBox3.Controls.Add(this.showGoalPositions);
			this.groupBox3.Location = new System.Drawing.Point(12, 294);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(200, 122);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Debug Settings";
			// 
			// showLevelPolygons
			// 
			this.showLevelPolygons.AutoSize = true;
			this.showLevelPolygons.Location = new System.Drawing.Point(7, 43);
			this.showLevelPolygons.Name = "showLevelPolygons";
			this.showLevelPolygons.Size = new System.Drawing.Size(128, 17);
			this.showLevelPolygons.TabIndex = 1;
			this.showLevelPolygons.Text = "Show Level Polygons";
			this.showLevelPolygons.UseVisualStyleBackColor = true;
			this.showLevelPolygons.CheckedChanged += new System.EventHandler(this.showLevelPolygons_CheckedChanged);
			// 
			// showFlowField
			// 
			this.showFlowField.AutoSize = true;
			this.showFlowField.Location = new System.Drawing.Point(7, 89);
			this.showFlowField.Name = "showFlowField";
			this.showFlowField.Size = new System.Drawing.Size(142, 17);
			this.showFlowField.TabIndex = 0;
			this.showFlowField.Text = "Show Flow Field Vectors";
			this.showFlowField.UseVisualStyleBackColor = true;
			this.showFlowField.CheckedChanged += new System.EventHandler(this.showFlowField_CheckedChanged);
			// 
			// showAStarPath
			// 
			this.showAStarPath.AutoSize = true;
			this.showAStarPath.Location = new System.Drawing.Point(7, 66);
			this.showAStarPath.Name = "showAStarPath";
			this.showAStarPath.Size = new System.Drawing.Size(107, 17);
			this.showAStarPath.TabIndex = 0;
			this.showAStarPath.Text = "Show AStar Path";
			this.showAStarPath.UseVisualStyleBackColor = true;
			this.showAStarPath.CheckedChanged += new System.EventHandler(this.showAStarPath_CheckedChanged);
			// 
			// showGoalPositions
			// 
			this.showGoalPositions.AutoSize = true;
			this.showGoalPositions.Location = new System.Drawing.Point(7, 20);
			this.showGoalPositions.Name = "showGoalPositions";
			this.showGoalPositions.Size = new System.Drawing.Size(123, 17);
			this.showGoalPositions.TabIndex = 0;
			this.showGoalPositions.Text = "Show Goal Positions";
			this.showGoalPositions.UseVisualStyleBackColor = true;
			this.showGoalPositions.CheckedChanged += new System.EventHandler(this.showGoalPositions_CheckedChanged);
			// 
			// agentCount
			// 
			this.agentCount.Location = new System.Drawing.Point(117, 228);
			this.agentCount.Maximum = new decimal(new int[] {
            70,
            0,
            0,
            0});
			this.agentCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.agentCount.Name = "agentCount";
			this.agentCount.Size = new System.Drawing.Size(95, 20);
			this.agentCount.TabIndex = 7;
			this.agentCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.agentCount.ValueChanged += new System.EventHandler(this.agentCount_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 230);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(102, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Agent Count (1 - 70)";
			// 
			// lowestMemoryUsageLabel
			// 
			this.lowestMemoryUsageLabel.AutoSize = true;
			this.lowestMemoryUsageLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lowestMemoryUsageLabel.Location = new System.Drawing.Point(57, 68);
			this.lowestMemoryUsageLabel.Name = "lowestMemoryUsageLabel";
			this.lowestMemoryUsageLabel.Size = new System.Drawing.Size(15, 15);
			this.lowestMemoryUsageLabel.TabIndex = 9;
			this.lowestMemoryUsageLabel.Text = "0";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 45);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Highest";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 23);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Current";
			// 
			// highestMemoryUsageLabel
			// 
			this.highestMemoryUsageLabel.AutoSize = true;
			this.highestMemoryUsageLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.highestMemoryUsageLabel.Location = new System.Drawing.Point(57, 45);
			this.highestMemoryUsageLabel.Name = "highestMemoryUsageLabel";
			this.highestMemoryUsageLabel.Size = new System.Drawing.Size(15, 15);
			this.highestMemoryUsageLabel.TabIndex = 11;
			this.highestMemoryUsageLabel.Text = "0";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.averageMemoryUsageLabel);
			this.groupBox4.Controls.Add(this.label2);
			this.groupBox4.Controls.Add(this.currentMemoryUsageLabel);
			this.groupBox4.Controls.Add(this.label6);
			this.groupBox4.Controls.Add(this.label4);
			this.groupBox4.Controls.Add(this.lowestMemoryUsageLabel);
			this.groupBox4.Controls.Add(this.highestMemoryUsageLabel);
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Location = new System.Drawing.Point(12, 433);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(200, 127);
			this.groupBox4.TabIndex = 13;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Memory Usage";
			// 
			// averageMemoryUsageLabel
			// 
			this.averageMemoryUsageLabel.AutoSize = true;
			this.averageMemoryUsageLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.averageMemoryUsageLabel.Location = new System.Drawing.Point(57, 90);
			this.averageMemoryUsageLabel.Name = "averageMemoryUsageLabel";
			this.averageMemoryUsageLabel.Size = new System.Drawing.Size(15, 15);
			this.averageMemoryUsageLabel.TabIndex = 16;
			this.averageMemoryUsageLabel.Text = "0";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 90);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 13);
			this.label2.TabIndex = 15;
			this.label2.Text = "Average";
			// 
			// currentMemoryUsageLabel
			// 
			this.currentMemoryUsageLabel.AutoSize = true;
			this.currentMemoryUsageLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.currentMemoryUsageLabel.Location = new System.Drawing.Point(57, 23);
			this.currentMemoryUsageLabel.Name = "currentMemoryUsageLabel";
			this.currentMemoryUsageLabel.Size = new System.Drawing.Size(15, 15);
			this.currentMemoryUsageLabel.TabIndex = 14;
			this.currentMemoryUsageLabel.Text = "0";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 68);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(41, 13);
			this.label6.TabIndex = 13;
			this.label6.Text = "Lowest";
			// 
			// saveMemoryUsageInfo
			// 
			this.saveMemoryUsageInfo.Location = new System.Drawing.Point(12, 602);
			this.saveMemoryUsageInfo.Name = "saveMemoryUsageInfo";
			this.saveMemoryUsageInfo.Size = new System.Drawing.Size(200, 21);
			this.saveMemoryUsageInfo.TabIndex = 15;
			this.saveMemoryUsageInfo.Text = "Save Memory Usage Information";
			this.saveMemoryUsageInfo.UseVisualStyleBackColor = true;
			this.saveMemoryUsageInfo.Click += new System.EventHandler(this.saveMemoryUsageInfo_Click);
			// 
			// saveFile
			// 
			this.saveFile.Location = new System.Drawing.Point(80, 579);
			this.saveFile.Name = "saveFile";
			this.saveFile.Size = new System.Drawing.Size(132, 20);
			this.saveFile.TabIndex = 16;
			this.saveFile.Text = "Data";
			this.saveFile.TextChanged += new System.EventHandler(this.saveFile_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(13, 582);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(61, 13);
			this.label5.TabIndex = 17;
			this.label5.Text = "Output File:";
			// 
			// forceGCButton
			// 
			this.forceGCButton.Location = new System.Drawing.Point(240, 265);
			this.forceGCButton.Name = "forceGCButton";
			this.forceGCButton.Size = new System.Drawing.Size(123, 23);
			this.forceGCButton.TabIndex = 18;
			this.forceGCButton.Text = "Force GC";
			this.forceGCButton.UseVisualStyleBackColor = true;
			this.forceGCButton.Click += new System.EventHandler(this.forceGCButton_Click);
			// 
			// label7
			// 
			this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
			this.label7.Location = new System.Drawing.Point(12, 672);
			this.label7.Name = "label7";
			this.label7.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.label7.Size = new System.Drawing.Size(200, 75);
			this.label7.TabIndex = 19;
			this.label7.Text = "This application is created and used as a bachelor\'s project that\'s focusing on g" +
    "ame development at Högskolan i Skövde ";
			// 
			// ControlPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(375, 775);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.forceGCButton);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.saveFile);
			this.Controls.Add(this.saveMemoryUsageInfo);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.agentCount);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.resetButton);
			this.Controls.Add(this.startButton);
			this.Name = "ControlPanel";
			this.Text = "ControlPanel";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.agentCount)).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button resetButton;
		private System.Windows.Forms.RadioButton envA;
		private System.Windows.Forms.RadioButton envB;
		private System.Windows.Forms.RadioButton envC;
		private System.Windows.Forms.RadioButton flowFieldRadioButton;
		private System.Windows.Forms.RadioButton aStarRadioButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox showFlowField;
		private System.Windows.Forms.CheckBox showAStarPath;
		private System.Windows.Forms.CheckBox showGoalPositions;
		private System.Windows.Forms.CheckBox showLevelPolygons;
		private System.Windows.Forms.NumericUpDown agentCount;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lowestMemoryUsageLabel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label highestMemoryUsageLabel;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label currentMemoryUsageLabel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label averageMemoryUsageLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button saveMemoryUsageInfo;
		private System.Windows.Forms.TextBox saveFile;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button forceGCButton;
		private System.Windows.Forms.RadioButton envD;
		private System.Windows.Forms.Label label7;
	}
}