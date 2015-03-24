using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Autonomous_Agents
{
	public class AStarNode : BaseNode
	{
		public AStarNode Parent { get; set; }
		public float G { get; set; }
		public float H { get; set; }
		public float F { get { return G + H; } }
	}

	public class AStarGrid : Grid<AStarNode>
	{
		public AStarGrid(int width, int height, int cellSize) :
			base(width, height, cellSize)
		{

		}

		public override AStarNode GetNode(Vector2 pos)
		{
			AStarNode node;

			if (_walkableNodes.TryGetValue(pos, out node))
			{
				return node;
			}

			return null;
		}
	}
}
