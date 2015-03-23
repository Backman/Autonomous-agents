namespace SharpSteer
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
			this.mapPathField = new System.Windows.Forms.TextBox();
			this.mapPathLabel = new System.Windows.Forms.Label();
			this.loadMapButton = new System.Windows.Forms.Button();
			this.pathFollowButton = new System.Windows.Forms.RadioButton();
			this.flowFieldFollowButton = new System.Windows.Forms.RadioButton();
			this.showGridToggle = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// mapPathField
			// 
			this.mapPathField.Location = new System.Drawing.Point(71, 205);
			this.mapPathField.Name = "mapPathField";
			this.mapPathField.Size = new System.Drawing.Size(284, 20);
			this.mapPathField.TabIndex = 0;
			this.mapPathField.TextChanged += new System.EventHandler(this.mapPathField_TextChanged);
			// 
			// mapPathLabel
			// 
			this.mapPathLabel.AutoSize = true;
			this.mapPathLabel.Location = new System.Drawing.Point(12, 208);
			this.mapPathLabel.Name = "mapPathLabel";
			this.mapPathLabel.Size = new System.Drawing.Size(53, 13);
			this.mapPathLabel.TabIndex = 1;
			this.mapPathLabel.Text = "Map Path";
			this.mapPathLabel.Click += new System.EventHandler(this.loadMapPath_Click);
			// 
			// loadMapButton
			// 
			this.loadMapButton.Location = new System.Drawing.Point(361, 203);
			this.loadMapButton.Name = "loadMapButton";
			this.loadMapButton.Size = new System.Drawing.Size(75, 23);
			this.loadMapButton.TabIndex = 2;
			this.loadMapButton.Text = "Load Map";
			this.loadMapButton.UseVisualStyleBackColor = true;
			this.loadMapButton.Click += new System.EventHandler(this.loadMapButton_Click);
			// 
			// pathFollowButton
			// 
			this.pathFollowButton.AutoSize = true;
			this.pathFollowButton.Location = new System.Drawing.Point(15, 26);
			this.pathFollowButton.Name = "pathFollowButton";
			this.pathFollowButton.Size = new System.Drawing.Size(94, 17);
			this.pathFollowButton.TabIndex = 3;
			this.pathFollowButton.TabStop = true;
			this.pathFollowButton.Text = "Path Following";
			this.pathFollowButton.UseVisualStyleBackColor = true;
			this.pathFollowButton.CheckedChanged += new System.EventHandler(this.pathFollowButton_CheckedChanged);
			// 
			// flowFieldFollowButton
			// 
			this.flowFieldFollowButton.AutoSize = true;
			this.flowFieldFollowButton.Location = new System.Drawing.Point(15, 50);
			this.flowFieldFollowButton.Name = "flowFieldFollowButton";
			this.flowFieldFollowButton.Size = new System.Drawing.Size(119, 17);
			this.flowFieldFollowButton.TabIndex = 4;
			this.flowFieldFollowButton.TabStop = true;
			this.flowFieldFollowButton.Text = "Flow Field Following";
			this.flowFieldFollowButton.UseVisualStyleBackColor = true;
			this.flowFieldFollowButton.CheckedChanged += new System.EventHandler(this.flowFieldFollowButton_CheckedChanged);
			// 
			// showGridToggle
			// 
			this.showGridToggle.AutoSize = true;
			this.showGridToggle.Location = new System.Drawing.Point(15, 87);
			this.showGridToggle.Name = "showGridToggle";
			this.showGridToggle.Size = new System.Drawing.Size(75, 17);
			this.showGridToggle.TabIndex = 5;
			this.showGridToggle.Text = "Show Grid";
			this.showGridToggle.UseVisualStyleBackColor = true;
			this.showGridToggle.CheckedChanged += new System.EventHandler(this.showGridToggle_CheckedChanged);
			// 
			// Menu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(457, 409);
			this.Controls.Add(this.showGridToggle);
			this.Controls.Add(this.flowFieldFollowButton);
			this.Controls.Add(this.pathFollowButton);
			this.Controls.Add(this.loadMapButton);
			this.Controls.Add(this.mapPathLabel);
			this.Controls.Add(this.mapPathField);
			this.Name = "Menu";
			this.Text = "Menu";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox mapPathField;
		private System.Windows.Forms.Label mapPathLabel;
		private System.Windows.Forms.Button loadMapButton;
		private System.Windows.Forms.RadioButton pathFollowButton;
		private System.Windows.Forms.RadioButton flowFieldFollowButton;
		private System.Windows.Forms.CheckBox showGridToggle;
	}
}