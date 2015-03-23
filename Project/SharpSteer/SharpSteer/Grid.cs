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

		private static Vector2[] neighbourSetFourDir = { new Vector2(1f, 0f), new Vector2(-1f, 0f), new Vector2(0f, -1f), new Vector2(0f, 1f) };

		private static Vector2[] neighbourSetEightDir = { new Vector2(1f, 0f), new Vector2(-1f, 0f),
														  new Vector2(0f, -1f), new Vector2(0f, 1f),
														  new Vector2(1f, 1f), new Vector2(1f, -1f),
														  new Vector2(-1f, 1f), new Vector2(-1f, -1f) };

		public int TotalSize
		{
			get { return _width * _height; }
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
			for (int x = 0; x < _width; x++)
			{
				for (int y = 0; y < _height; y++)
				{
					Node node = _nodes[x, y];
					foreach (Vector2 v in neighbourSetEightDir)
					{
						float xPos = node.Position.X + v.X * _cellSize;
						float yPos = node.Position.Y + v.Y * _cellSize;
						if (IsOnMap(xPos, yPos))
						{
							Vector2 pos = new Vector2(xPos, yPos);
							node.Neighbours.Add(GetNode(pos));
						}
					}
				}
			}
		}

		public bool IsOnMap(float x, float y)
		{
			return x >= 0f && y >= 0f && x < (_width * _cellSize) && y < (_height * _cellSize);
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

		public void Draw(SpriteBatch spriteBatch, Color color)
		{
			foreach (Node node in _nodes)
			{
				//if (!node.Walkable)
				{
					spriteBatch.DrawRectangle(node.Position, Vector2.One * _cellSize, color);
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
