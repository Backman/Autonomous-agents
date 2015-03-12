using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SharpSteer
{
	public class Grid
	{
		private int _cellSize;
		private int _width;
		private int _height;
		private Node[,] _nodes;

		public int TotalSize
		{
			get { return _width + _height; }
		}

		public Grid(int[,] map, int size)
		{
			Generate(map, size);
		}

		public void Generate(int[,] map, int size)
		{
			_width = map.GetLength(0);
			_height = map.GetLength(1);
			_cellSize = size;

			GenerateNodes(_width, _height, _cellSize, map);
			GenerateNodeNeighbours();
		}

		private void GenerateNodes(int width, int height, int cellSize, int[,] map)
		{
			_nodes = new Node[width, height];

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Node node = new Node();
					node.Position = new Vector2(x * cellSize, y * cellSize);

					int number = map[x, y];
					if (number > 0)
					{
						node.Walkable = false;
					}
					else
					{
						node.Walkable = true;
					}

					_nodes[x, y] = node;
				}
			}
		}

		private void GenerateNodeNeighbours()
		{
			Vector2[] neighbourSet = { new Vector2(1f, 0f) * _cellSize, new Vector2(-1f, 0f) * _cellSize, new Vector2(0f, 1f) * _cellSize, new Vector2(0f, -1f) * _cellSize };

			foreach (Node n in _nodes)
			{
				foreach (Vector2 v in neighbourSet)
				{
					float x = n.Position.X + v.X;
					float y = n.Position.Y + v.Y;
					if (IsOnMap(x, y))
					{
						Vector2 pos = new Vector2(x, y);
						n.Neighbours.Add(GetNode(pos));
					}
				}
			}
		}

		public bool IsOnMap(float x, float y)
		{
			return x >= 0f && y >= 0f && x < _width && y < _height;
		}

		public Node GetNode(Vector2 position)
		{
			int col = Math.Min((int)position.X / _cellSize, _width - 1);
			int row = Math.Min((int)position.Y / _cellSize, _height - 1);

			Node ret = _nodes[col, row];

			return ret;
		}

		public void SetWalkable(Vector2 position, bool walkable)
		{
			Node node = GetNode(position);
			node.Walkable = walkable;
		}

		public void ToggleWalkable(Vector2 position)
		{
			Node node = GetNode(position);
			node.Walkable = !node.Walkable;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (Node node in _nodes)
			{
				if (!node.Walkable)
				{
					spriteBatch.DrawRectangle(node.Position, Vector2.One * _cellSize, Color.White);
				}
			}
		}

		public static Grid CreateRandomGrid(int width, int height, int size)
		{
			int[,] map = new int[width, height];

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					map[x, y] = Helpers.Maths.random.Next(0, 2);
				}
			}

			Grid ret = new Grid(map, size);

			return ret;
		}
	}
}
