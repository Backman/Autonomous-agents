#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Bitmap = System.Drawing.Bitmap;
#endregion

namespace BachelorsProject
{
	public abstract class BaseGrid<T> where T : BaseNode, new()
	{
		static Vector2[] FOUR_NEIGHBOR_SET = {	new Vector2(1f, 0f), new Vector2(-1f, 0f),
											new Vector2(0f, 1f), new Vector2(0f, -1f) };

		static Vector2[] EIGHT_NEIGHBOR_SET = {	new Vector2(1f, 0f), new Vector2(-1f, 0f),
												new Vector2(0f, 1f), new Vector2(0f, -1f),
												new Vector2(1f, 1f), new Vector2(-1f, -1f),
												new Vector2(1f, -1f), new Vector2(-1f, 1f) };

		protected T[,] _grid;

		public T GoalNode;
		public T StartNode;

		public abstract void FromBitmap(Bitmap bitmap);
		
		protected int _gridWidth = Application.GRID_WIDTH;
		protected int _gridHeight = Application.GRID_HEIGHT;
		protected int _cellSize = Application.CELL_SIZE;

		public int TotalNodeCount { get { return _gridWidth * _gridHeight; } }

		public BaseGrid()
		{
			InitGrid(_gridWidth, _gridHeight);
		}
		
		protected bool InBounds(int x, int y)
		{
			return x >= 0 && y >= 0 && x < _gridWidth && y < _gridHeight;
		}

		protected void InitGrid(int width, int height)
		{
			_grid = new T[width, height];
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					T node = new T();
					node.GridX = x;
					node.GridY = y;
					node.Position = new Vector2(x * _cellSize, y * _cellSize);
					_grid[x, y] = node;
				}
			}
		}

		public virtual void Render(SpriteBatch spriteBatch)
		{
			for (int y = 0; y < _gridHeight; y++)
			{
				for (int x = 0; x < _gridWidth; x++)
				{
					T node = _grid[x, y];
					Vector2 nodePos = new Vector2(x, y);

					if (node.Walkable)
					{
						spriteBatch.DrawRectangle(node.Position, Vector2.One * _cellSize, Color.DimGray);
					}
				}
			}
		}

		public T GetNodeFromWorldPos(int x, int y)
		{
			x /= _cellSize;
			y /= _cellSize;

			if (!InBounds(x, y))
			{
				return null;
			}

			return _grid[x, y];
		}

		public T GetNode(int x, int y)
		{
			if (!InBounds(x, y))
			{
				return null;
			}

			return _grid[x, y];
		}

		public IEnumerable<T> GetNeighbors(T node, bool eightNeighbors = false)
		{
			if (!eightNeighbors)
			{
				foreach (var dir in FOUR_NEIGHBOR_SET)
				{
					int x = node.GridX + (int)dir.X;
					int y = node.GridY + (int)dir.Y;

					var neighbor = GetNode(x, y);
					if (neighbor != null && neighbor.Walkable)
					{
						yield return neighbor;
					}
				}
			}
			else
			{
				foreach (var dir in EIGHT_NEIGHBOR_SET)
				{
					int x = node.GridX + (int)dir.X;
					int y = node.GridY + (int)dir.Y;

					var neighbor = GetNode(x, y);
					if (neighbor != null && neighbor.Walkable)
					{
						yield return neighbor;
					}
				}
			}
		}
	}
}
