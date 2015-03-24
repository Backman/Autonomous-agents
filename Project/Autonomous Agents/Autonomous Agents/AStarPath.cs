using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Autonomous_Agents
{
	public class AStarPath
	{
		private List<AStarNode> _path = new List<AStarNode>();

		public AStarPath(AStarNode start, AStarNode end)
		{
			ConstructPath(start, end);
		}

		private void ConstructPath(AStarNode start, AStarNode end)
		{
			_path.Clear();

			AStarNode current = end;

			while (current != null)
			{
				_path.Add(current);
				current = current.Parent;
			}
		}

		public void Render(SpriteBatch spriteBatch, Color color, float thickness = 1f)
		{
			for (int i = 1; i < _path.Count; ++i)
			{
				AStarNode n1 = _path[i - 1];
				AStarNode n2 = _path[i];

				spriteBatch.DrawLine(n1.Position, n2.Position, color, thickness);
			}
		}
	}
}
