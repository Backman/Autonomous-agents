using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Priority_Queue;

namespace SharpSteer
{
	public class Node : PriorityQueueNode
	{
		public Vector2 Position { get; set; }
		public Node Parent { get; set; }
		public float G { get; set; }
		public float H { get; set; }
		public float F { get { return G + H; } }
		public bool Walkable { get; set; }

		public List<Node> Neighbours = new List<Node>();

		public static float GetCost(Node n1, Node n2)
		{
			Vector2 n1Pos = n1.Position;
			Vector2 n2Pos = n2.Position;
			
			return n1Pos.X != n2Pos.X || n1Pos.Y != n2Pos.Y ? 2f : 1f;
		}
	}
}
