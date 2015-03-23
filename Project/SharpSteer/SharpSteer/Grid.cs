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
		private bool _showGrid = false;
		
		private int _cellSize;
		private int _width;
		private int _height;
		private HashSet<Node> _walkableNodes = new HashSet<Node>();
		private HashSet<Node> _blockedNodes = new HashSet<Node>();
		private Node[,] _nodes;

		private static Vector2[] neighbourSet = { new Vector2(1f, 0f), new Vector2(-1f, 0f),
												  new Vector2(0f, -1f), new Vector2(0f, 1f),
												  new Vector2(1f, 1f), new Vector2(1f, -1f),
												  new Vector2(-1f, 1f), new Vector2(-1f, -1f) };

		public int TotalSize
		{
			get { return _width * _height; }
		}

		public Grid(int width, int height, int cellSize)
		{
			_width = width;
			_height = height;
			_cellSize = cellSize;

			InitGrid();
		}

		private void InitGrid()
		{
			_nodes = new Node[_width, _height];

			for (int x = 0; x < _width; x++)
			{
				for (int y = 0; y < _height; y++)
				{
					Node node = new Node();
					node.Position = new Vector2(x * _cellSize, y * _cellSize);
					node.Walkable = true;

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
					foreach (Vector2 v in neighbourSet)
					{
						float xPos = node.Position.X + v.X * _cellSize;
						float yPos = node.Position.Y + v.Y * _cellSize;
						
						Vector2 pos = new Vector2(xPos, yPos);

						Node neighbour;
						if (IsOnMap(xPos, yPos) && (neighbour = GetNode(pos)).Walkable)
						{
							node.Neighbours.Add(neighbour);
						}
					}
				}
			}

			//foreach (Vector2 v in neighbourSet)
			//{
			//	float xPos = node.Position.X + v.X * _cellSize;
			//	float yPos = node.Position.Y + v.Y * _cellSize;

			//	Vector2 pos = new Vector2(xPos, yPos);

			//	Node neighbour;
			//	if (IsOnMap(xPos, yPos) && (neighbour = GetNode(pos)).Walkable)
			//	{
			//		node.Neighbours.Add(neighbour);
			//	}
			//}
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

		public void ShowGrid(bool value)
		{
			_showGrid = value;
		}

		public void Draw(SpriteBatch spriteBatch, Color color)
		{
			for (int x = 0; x < _width; x++)
			{
				for (int y = 0; y < _height; y++)
				{
					Node n = _nodes[x, y];
					if (!n.Walkable)
					{
						spriteBatch.DrawRectangle(n.Position, Vector2.One * _cellSize, Color.Green);
					}
					else if (n.Walkable && _showGrid)
					{
						spriteBatch.DrawRectangle(n.Position, Vector2.One * _cellSize, color);
					}
				}
			}
		}

		public void CreateFromBitmap(System.Drawing.Bitmap bitmap)
		{
			int w = bitmap.Width / _width;
			int h = bitmap.Height / _height;
			int black = System.Drawing.Color.Black.ToArgb();

			for (int y = 0; y < _height; ++y)
			{
				for (int x = 0; x < _width; ++x)
				{
					int pX = x * w;
					int pY = y * h;
					System.Drawing.Color pixel = bitmap.GetPixel(pX, pY);

					int ix = x;
					int iy = y;

					Node n = new Node();
					n.Position = new Vector2(x * _cellSize, y * _cellSize);

					Node node = _nodes[ix, iy];
					if (!pixel.ToArgb().Equals(black))
					{
						_walkableNodes.Add(n);
						node.Walkable = true;
					}
					else
					{
						_blockedNodes.Add(n);
						node.Walkable = false;
					}
				}
			}

			GenerateNodeNeighbours();
		}
	}
}
