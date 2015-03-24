using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Priority_Queue;

namespace Autonomous_Agents
{
	public static class AStarSolver
	{
		public static AStarPath Solve(AStarGrid grid)
		{
			AStarNode startNode = grid.StartNode;
			if (startNode == null)
			{
				return null;
			}

			AStarNode currentNode = null;
			AStarNode endNode = grid.EndNode;

			HeapPriorityQueue<AStarNode> openList = new HeapPriorityQueue<AStarNode>(grid.WalkableNodeCount);
			List<AStarNode> closedList = new List<AStarNode>();

			startNode.G = 0;
			startNode.H = Heuristic(startNode.Position, endNode.Position);
			startNode.Parent = null;

			openList.Enqueue(startNode, startNode.F);
			while (openList.Count > 0)
			{
				currentNode = openList.Dequeue();
				if (currentNode == endNode)
				{
					AStarPath path = new AStarPath(startNode, currentNode);
					return path;
				}

				foreach (AStarNode neighbor in grid.Neighbors(currentNode, true))
				{
					float newG = currentNode.G + GetCost(currentNode, neighbor);
					if (closedList.Contains(neighbor) || openList.Contains(neighbor) && neighbor.G <= newG)
					{
						continue;
					}

					neighbor.Parent = currentNode;
					neighbor.G = newG;
					neighbor.H = Heuristic(neighbor.Position, endNode.Position);
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
			Vector2 n1Pos = n1.Position;
			Vector2 n2Pos = n2.Position;

			return n1Pos.X != n2Pos.X || n1Pos.Y != n2Pos.Y ? 2f : 1f;
		}

		private static float Heuristic(Vector2 pos, Vector2 endPos)
		{
			float dx = Math.Abs(pos.X - endPos.X);
			float dy = Math.Abs(pos.Y - endPos.Y);

			return dx + dy;
		}
	}
}
