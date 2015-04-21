#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Bitmap = System.Drawing.Bitmap;
#endregion

namespace BachelorsProject
{
	public class FlowFieldGrid : BaseGrid<FlowFieldNode>
	{
		private int[,] _costField;

		public FlowFieldGrid() 
			: base()
		{

		}

		public Vector2 Sample(Vector2 pos)
		{
			int x = (int)pos.X / _cellSize;
			int y = (int)pos.Y / _cellSize;

			if (InBounds(x, y))
			{
				return _grid[x, y].Force;
			}

			return Vector2.Zero;
		}

		public void ConstructFlowField()
		{
			ResetGrid();
			CalculateIntegrationField();
			CalculateForceVectors();
		}

		private void ResetGrid()
		{
			for (int y = 0; y < _gridHeight; y++)
			{
				for (int x = 0; x < _gridWidth; x++)
				{
					var node = _grid[x, y];
					node.Cost = int.MaxValue;
				}
			}
		}

		private void CalculateIntegrationField()
		{
			Queue<FlowFieldNode> openList = new Queue<FlowFieldNode>();

			FlowFieldNode node = _grid[GoalNode.GridX, GoalNode.GridY];
			node.Cost = 0;
			
			openList.Enqueue(node);
			
			while (openList.Count > 0)
			{
				FlowFieldNode current = openList.Dequeue();
			
				foreach (var neighbor in GetNeighbors(current))
				{
					int newCost = current.Cost + _costField[neighbor.GridX, neighbor.GridY];
					if (newCost < neighbor.Cost)
					{
						if (!openList.Contains(neighbor))
						{
							openList.Enqueue(neighbor);
						}
			
						neighbor.Cost = newCost;
					}
				}
			}
		}

		private void CalculateForceVectors()
		{
			for (int y = 0; y < _gridHeight; y++)
			{
				for (int x = 0; x < _gridWidth; x++)
				{
					var node = _grid[x, y];

					int nodeCost = node.Cost;

					int minCost = node.Cost;
					int minX = x;
					int minY = y;
					foreach (var neighbor in GetNeighbors(node, true))
					{
						if (neighbor.Cost < minCost)
						{
							minX = neighbor.GridX;
							minY = neighbor.GridY;
						}
					}

					int gridX = minX;
					int gridY = minY;

					node.Force.X = gridX - x;
					node.Force.Y = gridY - y;

					if (node.Force != Vector2.Zero)
					{
						node.Force.Normalize();
					}
				}
			}
		}

		public override void Render(SpriteBatch spriteBatch)
		{
			base.Render(spriteBatch);

			for (int y = 0; y < _gridHeight; ++y)
			{
				for (int x = 0; x < _gridWidth; ++x)
				{
					if (Application.Settings.ShowFlowFieldVectors)
					{
						FlowFieldNode node = _grid[x, y];
						Vector2 dirPos = node.Position + Vector2.One * (_cellSize / 2f);
						spriteBatch.DrawLine(dirPos, dirPos + _grid[x, y].Force * (_cellSize / 2f), Color.Ivory);
						spriteBatch.DrawCircle(dirPos, 1f, 10, Color.Black);
					}
				}
			}
		}

		public override void FromBitmap(Bitmap bitmap)
		{
			_costField = new int[_gridWidth, _gridHeight];

			int w = bitmap.Width / _gridWidth;
			int h = bitmap.Height / _gridHeight;
			int black = System.Drawing.Color.Black.ToArgb();

			for (int y = 0; y < _gridHeight; ++y)
			{
				for (int x = 0; x < _gridWidth; ++x)
				{
					int pX = x * w;
					int pY = y * h;
					System.Drawing.Color pixel = bitmap.GetPixel(pX, pY);

					var node = _grid[x, y];

					if (!pixel.ToArgb().Equals(black))
					{
						node.Walkable = true;
						_costField[x, y] = 1;
					}
					else
					{
						node.Walkable = false;
						_costField[x, y] = 255;
					}
				}
			}
		}
	}
}
