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
			return 1f;
		}
	}
}
