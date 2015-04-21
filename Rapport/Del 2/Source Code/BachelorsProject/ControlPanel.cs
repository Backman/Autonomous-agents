using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BachelorsProject
{
	public partial class ControlPanel : Form
	{
		private Application _application;
		private Application.MemoryUsageInfo _applicationMemory;

		public ControlPanel(Application application)
		{
			InitializeComponent();
			
			_application = application;
			_applicationMemory = application.MemoryUsage;
			agentCount.Value = (int)Application.Settings.AgentCount;
			//forcePenetrationConstraint.Checked = Application.Settings.EnforcePenetrationConstraint;
		}

		public void UpdateDebugSettings()
		{
			Application.NavigationTechnique technique = Application.Settings.Technique;

			currentMemoryUsageLabel.Text = string.Format("{0} MB", _applicationMemory.CurrentMemoryUsage / 1000000);
			highestMemoryUsageLabel.Text = string.Format("{0} MB", _applicationMemory.HighestMemoryUsage / 1000000);
			lowestMemoryUsageLabel.Text =  string.Format("{0} MB", _applicationMemory.LowestMemoryUsage / 1000000);
			averageMemoryUsageLabel.Text = string.Format("{0} MB", _applicationMemory.AverageMemoryUsage);

			if (_application.SelectedAgent == null)
			{
				showAStarPath.Enabled = false;
				showFlowField.Enabled = false;

				showAStarPath.Checked = false;
				showFlowField.Checked = false;

				return;
			}

			if (technique == Application.NavigationTechnique.FlowField)
			{
				showFlowField.Enabled = true;
				showAStarPath.Enabled = false;
				showAStarPath.Checked = false;
			}
			else if (technique == Application.NavigationTechnique.AStar)
			{
				showAStarPath.Enabled = true;
				showFlowField.Enabled = false;
				showFlowField.Checked = false;
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			_application.Exit();
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			_application.Start();
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			_application.Reset();
		}

		private void envA_CheckedChanged(object sender, EventArgs e)
		{
			if (envA.Checked)
			{
				_application.SetEnvironment(0);
				_application.Reset();
			}
		}

		private void envB_CheckedChanged(object sender, EventArgs e)
		{
			if (envB.Checked)
			{
				_application.SetEnvironment(1);
				_application.Reset();
			}
		}

		private void envC_CheckedChanged(object sender, EventArgs e)
		{
			if (envC.Checked)
			{
				_application.SetEnvironment(2);
				_application.Reset();
			}
		}

		private void envD_CheckedChanged(object sender, EventArgs e)
		{
			if (envD.Checked)
			{
				_application.SetEnvironment(3);
				_application.Reset();
			}
		}

		private void flowFieldRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (flowFieldRadioButton.Checked)
			{
				Application.Settings.Technique = Application.NavigationTechnique.FlowField;
				_application.Reset();
			}
		}

		private void aStarRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (aStarRadioButton.Checked)
			{
				Application.Settings.Technique = Application.NavigationTechnique.AStar;
				_application.Reset();
			}
		}

		private void showGoalPositions_CheckedChanged(object sender, EventArgs e)
		{
			Application.Settings.ShowGoalPositions = showGoalPositions.Checked;
		}

		private void showAStarPath_CheckedChanged(object sender, EventArgs e)
		{
			Application.Settings.ShowAStarPath = showAStarPath.Checked;
		}

		private void showFlowField_CheckedChanged(object sender, EventArgs e)
		{
			Application.Settings.ShowFlowFieldVectors = showFlowField.Checked;
		}

		private void showLevelPolygons_CheckedChanged(object sender, EventArgs e)
		{
			Application.Settings.ShowLevelPolygons = showLevelPolygons.Checked;
		}

		//private void forcePenetrationConstraint_CheckedChanged(object sender, EventArgs e)
		//{
		//	Application.Settings.EnforcePenetrationConstraint = forcePenetrationConstraint.Checked;
		//}

		private void agentCount_ValueChanged(object sender, EventArgs e)
		{
			Application.Settings.AgentCount = (int)agentCount.Value;
		}

		private void saveMemoryUsageInfo_Click(object sender, EventArgs e)
		{
			string filePath = saveFile.Text + ".txt";
			_application.SaveDataToFile(filePath);
		}

		private void saveFile_TextChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(saveFile.Text))
			{
				saveFile.Text = "Data";
			}
		}

		private void forceGCButton_Click(object sender, EventArgs e)
		{
			GC.Collect();
		}
	}
}
