namespace Autonomous_Agents
{
	partial class Menu
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
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.techniqueList = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.mapList = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.agentCount = new System.Windows.Forms.NumericUpDown();
			this.agentCountLabel = new System.Windows.Forms.Label();
			this.setAgentCountButton = new System.Windows.Forms.Button();
			this.resetButton = new System.Windows.Forms.Button();
			this.playButton = new System.Windows.Forms.Button();
			this.pauseButton = new System.Windows.Forms.Button();
			this.game1BindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.contextMenuStrip2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.agentCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.game1BindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// contextMenuStrip2
			// 
			this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			this.contextMenuStrip2.Size = new System.Drawing.Size(181, 26);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.toolStripMenuItem1.Text = "toolStripMenuItem1";
			// 
			// contextMenuStrip3
			// 
			this.contextMenuStrip3.Name = "contextMenuStrip3";
			this.contextMenuStrip3.Size = new System.Drawing.Size(61, 4);
			// 
			// techniqueList
			// 
			this.techniqueList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.techniqueList.FormattingEnabled = true;
			this.techniqueList.Items.AddRange(new object[] {
            "Flow Field Following",
            "Path Following"});
			this.techniqueList.Location = new System.Drawing.Point(126, 12);
			this.techniqueList.Name = "techniqueList";
			this.techniqueList.Size = new System.Drawing.Size(121, 21);
			this.techniqueList.TabIndex = 5;
			this.techniqueList.SelectedIndexChanged += new System.EventHandler(this.techniques_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Following Technique";
			// 
			// mapList
			// 
			this.mapList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mapList.FormattingEnabled = true;
			this.mapList.Location = new System.Drawing.Point(126, 52);
			this.mapList.Name = "mapList";
			this.mapList.Size = new System.Drawing.Size(121, 21);
			this.mapList.TabIndex = 7;
			this.mapList.SelectedIndexChanged += new System.EventHandler(this.mapList_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 55);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Maps";
			// 
			// agentCount
			// 
			this.agentCount.Location = new System.Drawing.Point(126, 89);
			this.agentCount.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.agentCount.Name = "agentCount";
			this.agentCount.Size = new System.Drawing.Size(120, 20);
			this.agentCount.TabIndex = 9;
			this.agentCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// agentCountLabel
			// 
			this.agentCountLabel.AutoSize = true;
			this.agentCountLabel.Location = new System.Drawing.Point(15, 91);
			this.agentCountLabel.Name = "agentCountLabel";
			this.agentCountLabel.Size = new System.Drawing.Size(66, 13);
			this.agentCountLabel.TabIndex = 10;
			this.agentCountLabel.Text = "Agent Count";
			// 
			// setAgentCountButton
			// 
			this.setAgentCountButton.Location = new System.Drawing.Point(18, 115);
			this.setAgentCountButton.Name = "setAgentCountButton";
			this.setAgentCountButton.Size = new System.Drawing.Size(229, 23);
			this.setAgentCountButton.TabIndex = 11;
			this.setAgentCountButton.Text = "Set Agent Count";
			this.setAgentCountButton.UseVisualStyleBackColor = true;
			this.setAgentCountButton.Click += new System.EventHandler(this.setAgentCountButton_Click);
			// 
			// resetButton
			// 
			this.resetButton.Location = new System.Drawing.Point(18, 281);
			this.resetButton.Name = "resetButton";
			this.resetButton.Size = new System.Drawing.Size(229, 23);
			this.resetButton.TabIndex = 12;
			this.resetButton.Text = "Reset";
			this.resetButton.UseVisualStyleBackColor = true;
			this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
			// 
			// playButton
			// 
			this.playButton.Location = new System.Drawing.Point(18, 252);
			this.playButton.Name = "playButton";
			this.playButton.Size = new System.Drawing.Size(114, 23);
			this.playButton.TabIndex = 13;
			this.playButton.Text = "Play";
			this.playButton.UseVisualStyleBackColor = true;
			this.playButton.Click += new System.EventHandler(this.playButton_Click);
			// 
			// pauseButton
			// 
			this.pauseButton.Location = new System.Drawing.Point(138, 252);
			this.pauseButton.Name = "pauseButton";
			this.pauseButton.Size = new System.Drawing.Size(109, 23);
			this.pauseButton.TabIndex = 14;
			this.pauseButton.Text = "Pause";
			this.pauseButton.UseVisualStyleBackColor = true;
			this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
			// 
			// game1BindingSource
			// 
			this.game1BindingSource.DataSource = typeof(Autonomous_Agents.Game1);
			// 
			// Menu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(557, 370);
			this.Controls.Add(this.pauseButton);
			this.Controls.Add(this.playButton);
			this.Controls.Add(this.resetButton);
			this.Controls.Add(this.setAgentCountButton);
			this.Controls.Add(this.agentCountLabel);
			this.Controls.Add(this.agentCount);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.mapList);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.techniqueList);
			this.Name = "Menu";
			this.Text = "Menu";
			this.contextMenuStrip2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.agentCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.game1BindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
		private System.Windows.Forms.BindingSource game1BindingSource;
		private System.Windows.Forms.ComboBox techniqueList;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox mapList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown agentCount;
		private System.Windows.Forms.Label agentCountLabel;
		private System.Windows.Forms.Button setAgentCountButton;
		private System.Windows.Forms.Button resetButton;
		private System.Windows.Forms.Button playButton;
		private System.Windows.Forms.Button pauseButton;
	}
}