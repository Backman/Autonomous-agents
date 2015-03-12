using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;

namespace SharpSteer
{
	public static class AStar
	{
		public static Path Solve(Node startNode, Node endNode, Grid grid)
		{
			Node currentNode = null;
			HeapPriorityQueue<Node> openList = new HeapPriorityQueue<Node>(grid.TotalSize);
			List<Node> closedList = new List<Node>(grid.TotalSize);

			startNode.G = 0;
			startNode.H = Heuristic(startNode, endNode);
			startNode.Parent = null;

			openList.Enqueue(startNode, startNode.F);

			while (openList.Count() > 0)
			{
				currentNode = openList.Dequeue();

				if (currentNode == endNode)
				{
					Path path = new Path(startNode, currentNode);

					return path;
				}

				closedList.Add(currentNode);
				foreach (Node n in currentNode.Neighbours)
				{
					float newG = currentNode.G + Node.GetCost(currentNode, n);
					if (closedList.Contains(n) || openList.Contains(n) && n.G <= newG)
					{
						continue;
					}

					n.Parent = currentNode;
					n.G = newG;
					n.H = Heuristic(n, endNode);
					if (closedList.Contains(n))
					{
						closedList.Remove(n);
					}
					if (!openList.Contains(n))
					{
						openList.Enqueue(n, n.F);
					}
				}
				closedList.Add(currentNode);
			}

			return null;
		}

		private static float Heuristic(Node node, Node endNode)
		{
			float dx = Math.Abs(node.Position.X - endNode.Position.X);
			float dy = Math.Abs(node.Position.Y - endNode.Position.Y);

			return dx + dy;
		}

	}
}
