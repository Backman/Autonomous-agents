using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Autonomous_Agents
{
	public class FlowFieldNode : BaseNode
	{
		private Vector2 _steeringForce = Vector2.UnitX;
		public Vector2 SteeringForce
		{
			get { return _steeringForce; }
			set { _steeringForce = value; }
		}
		public int Cost { get; set; }
	}

	public class FlowFieldGrid : Grid<FlowFieldNode>, IFlowField
	{
		public FlowFieldGrid(int width, int height, int cellSize) :
			base(width, height, cellSize)
		{

		}

		public Vector2 Sample(Vector2 position)
		{
			return GetNode(position).SteeringForce;
		}

		public override FlowFieldNode GetNode(Vector2 pos)
		{
			FlowFieldNode node;

			int x = (int)pos.X;
			int y = (int)pos.Y;

			while (x % _cellSize != 0)
			{
				x -= 1;
			}
			while (y % _cellSize != 0)
			{
				y -= 1;
			}

			Vector2 position = new Vector2(x, y);

			if (_walkableNodes.TryGetValue(position, out node))
			{
				return node;
			}

			return null;
		}

		public void GenerateFlowField()
		{
			if (EndNode == null)
			{
				return;
			}

			GenerateHeatMap();
			GenerateField();
		}

		private void GenerateHeatMap()
		{
			ResetNodes();

			Priority_Queue.HeapPriorityQueue<FlowFieldNode> openList = new Priority_Queue.HeapPriorityQueue<FlowFieldNode>(WalkableNodeCount);
			List<FlowFieldNode> closedList = new List<FlowFieldNode>(WalkableNodeCount);
			
			FlowFieldNode currentNode = null;
			EndNode.Cost = 0;

			openList.Enqueue(EndNode, EndNode.Cost);
			while (openList.Count > 0)
			{
				currentNode = openList.Dequeue();

				foreach (FlowFieldNode neighbor in Neighbors(currentNode, false))
				{
					if (closedList.Contains(neighbor) || openList.Contains(neighbor))
					{
						continue;
					}

					neighbor.Cost = currentNode.Cost + 1;
					if (!openList.Contains(neighbor))
					{
						openList.Enqueue(neighbor, neighbor.Cost);
					}
				}
				closedList.Add(currentNode);
			}
		}

		private void GenerateField()
		{
			foreach (FlowFieldNode node in _walkableNodes.Values)
			{
				int leftCost = 6666;
				int rightCost = 6666;
				int upCost = 6666;
				int downCost = 6666;

				foreach (FlowFieldNode neighbor in Neighbors(node, false))
				{
					if (neighbor.Position.X > node.Position.X)
					{
						rightCost = neighbor.Cost;
						continue;
					}
					else if (neighbor.Position.X < node.Position.X)
					{
						leftCost = neighbor.Cost;
						continue;
					}

					if (neighbor.Position.Y > node.Position.Y)
					{
						upCost = neighbor.Cost;
						continue;
					}
					else if (neighbor.Position.Y < node.Position.Y)
					{
						downCost = neighbor.Cost;
						continue;
					}
				}
				Vector2 steer = new Vector2(leftCost - rightCost, upCost - downCost);
				node.SteeringForce = Vector2.Normalize(steer);
			}
		}

		private void ResetNodes()
		{
			foreach (FlowFieldNode node in _walkableNodes.Values)
			{
				node.Cost = 6666;
			}
		}

		public override void CreateFromBitmap(System.Drawing.Bitmap bitmap)
		{
			base.CreateFromBitmap(bitmap);

			foreach (FlowFieldNode node in _blockedNodes.Values)
			{
				node.Cost = 6666;
			}
		}

		public override void Render(SpriteBatch spriteBatch, Color walkableColor, Color blockedColor)
		{
			base.Render(spriteBatch, walkableColor, blockedColor);

			if (ShowGrid)
			{
				foreach (FlowFieldNode node in _walkableNodes.Values)
				{
					if (node.Cost > 0)
					{
						Vector2 dirPos = node.Position + Vector2.One * (_cellSize / 2);
						spriteBatch.DrawLine(dirPos, dirPos + node.SteeringForce * 4f, Color.Ivory);
						spriteBatch.DrawCircle(dirPos, 1f, 10, Color.Black);
					}
				}
			}
		}
	}
}
