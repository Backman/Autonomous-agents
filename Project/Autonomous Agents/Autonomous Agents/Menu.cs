using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Autonomous_Agents
{
	public partial class Menu : Form
	{
		private const string MapPath = "Maps/";

		public Menu()
		{
			InitializeComponent();

			techniqueList.DataSource = Enum.GetValues(typeof(FollowTechnique));
			Messenger.Broadcast<FollowTechnique>("SetFollowTechnique", (FollowTechnique)techniqueList.SelectedValue);

			string[] mapPaths = System.IO.Directory.GetFiles(MapPath, "*.png");
			string[] maps = new string[mapPaths.Length];

			for (int i = 0; i < mapPaths.Length; ++i)
			{
				maps[i] = mapPaths[i].Split('/')[1];
			}

			mapList.Items.AddRange(maps);

			if (mapList.Items.Count > 0)
			{
				mapList.SelectedItem = mapList.Items[0];
			}
		}

		private void mapPathField_TextChanged(object sender, EventArgs e)
		{

		}

		private void loadMapButton_Click(object sender, EventArgs e)
		{

		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			Messenger.Broadcast("OnWinFormsExit");
		}

		private void techniques_SelectedIndexChanged(object sender, EventArgs e)
		{
			//Messenger.Broadcast<FollowTechnique>("SetFollowTechnique", (FollowTechnique)techniqueList.SelectedValue);
		}

		private void mapList_SelectedIndexChanged(object sender, EventArgs e)
		{
			//Messenger.Broadcast<string>("SetMap", (string)mapList.SelectedItem);
		}

		private void setAgentCountButton_Click(object sender, EventArgs e)
		{
			//int count = (int)agentCount.Value;

			//Messenger.Broadcast<int>("SetAgentCount", count);
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			Messenger.Broadcast<FollowTechnique>("SetFollowTechnique", (FollowTechnique)techniqueList.SelectedValue);
			Messenger.Broadcast<string>("SetMap", (string)mapList.SelectedItem);

			int count = (int)agentCount.Value;

			Messenger.Broadcast<int>("SetAgentCount", count);
			Messenger.Broadcast<bool>("SetShouldUpdate", false);
		}

		private void playButton_Click(object sender, EventArgs e)
		{
			Messenger.Broadcast<bool>("SetShouldUpdate", true);
		}

		private void pauseButton_Click(object sender, EventArgs e)
		{
			Messenger.Broadcast<bool>("SetShouldUpdate", false);
		}
	}
}
