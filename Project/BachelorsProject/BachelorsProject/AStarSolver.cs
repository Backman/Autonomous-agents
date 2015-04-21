#region Using Statements
using Priority_Queue;
using System;
using System.Collections.Generic;
#endregion

namespace BachelorsProject
{
	public static class AStarSolver
	{
		public static AStarPath Solve(AStarNode startNode, AStarNode endNode, AStarGrid grid)
		{
			if (startNode == null)
			{
				return null;
			}

			AStarNode currentNode = null;

			HeapPriorityQueue<AStarNode> openList = new HeapPriorityQueue<AStarNode>(grid.TotalNodeCount);
			List<AStarNode> closedList = new List<AStarNode>();

			startNode.G = 0;
			startNode.H = Heuristic(startNode, endNode);
			startNode.Parent = null;

			openList.Enqueue(startNode, startNode.F);
			while (openList.Count > 0)
			{
				currentNode = openList.Dequeue();

				if (currentNode == endNode)
				{
					AStarPath path = new AStarPath();
					path.ConstructPath(startNode, endNode);
					return path;
				}

				foreach (AStarNode neighbor in grid.GetNeighbors(currentNode, true))
				{
					float newG = currentNode.G + GetCost(currentNode, neighbor);
					if (closedList.Contains(neighbor) || openList.Contains(neighbor) && neighbor.G <= newG)
					{
						continue;
					}

					neighbor.Parent = currentNode;
					neighbor.G = newG;
					neighbor.H = Heuristic(neighbor, endNode);
					if (closedList.Contains(neighbor))
					{
						closedList.Remove(neighbor);
					}
					if (!openList.Contains(neighbor))
					{
						openList.Enqueue(neighbor, neighbor.F);
					}
				}
				closedList.Add(currentNode);
			}

			return null;
		}

		private static float GetCost(AStarNode n1, AStarNode n2)
		{
			return n1.GridX != n2.GridX && n1.GridY != n2.GridY ? 2f : 1f;
		}

		private static float Heuristic(AStarNode node, AStarNode goalNode)
		{
			float dx = Math.Abs(node.GridX - goalNode.GridX);
			float dy = Math.Abs(node.GridY - goalNode.GridY);

			return dx + dy;
		}
	}
}
