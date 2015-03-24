using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Bitmap = System.Drawing.Bitmap;

namespace Autonomous_Agents
{
	public abstract class Grid<T> where T : INode, new()
	{
		public static Vector2[] DiagonalNeighborSet = { new Vector2(1f, 0f), new Vector2(-1f, 0f),
														new Vector2(0f, -1f), new Vector2(0f, 1f),
														new Vector2(1f, 1f), new Vector2(1f, -1f),
														new Vector2(-1f, 1f), new Vector2(-1f, -1f) };

		public static Vector2[] OrthogonalNeighborSet = { new Vector2(1f, 0f), new Vector2(-1f, 0f),
														  new Vector2(0f, -1f), new Vector2(0f, 1f) };

		protected int _width;
		protected int _height;
		protected int _cellSize;
		protected Dictionary<Vector2, T> _walkableNodes;
		protected Dictionary<Vector2, T> _blockedNodes;

		public T StartNode { get; set; }
		public T EndNode { get; set; }

		public int WalkableNodeCount { get { return _walkableNodes.Count; } }
		public int BlockedNodeCount { get { return _blockedNodes.Count; } }

		public bool ShowGrid = false;

		public Grid(int width, int height, int cellSize)
		{
			_width = width;
			_height = height;
			_cellSize = cellSize;

			_walkableNodes = new Dictionary<Vector2, T>();
			_blockedNodes = new Dictionary<Vector2, T>();
		}

		public virtual void CreateFromBitmap(Bitmap bitmap)
		{
			_walkableNodes.Clear();
			_blockedNodes.Clear();

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

					T node = new T();
					node.Position = new Vector2(x * _cellSize, y * _cellSize);

					if (!pixel.ToArgb().Equals(black))
					{
						_walkableNodes.Add(node.Position, node);
					}
					else
					{
						_blockedNodes.Add(node.Position, node);
					}
				}
			}
		}

		public void RandomizeStarEndNode()
		{
			if (_walkableNodes.Count < 1)
			{
				return;
			}

			Random random = new Random();
			Vector2 startPos = new Vector2(random.Next(0, _width * _cellSize), random.Next(0, _width * _cellSize));

			StartNode = GetNode(startPos);
			while (StartNode == null)
			{
				startPos = new Vector2(random.Next(0, _width * _cellSize), random.Next(0, _width * _cellSize));
				StartNode = GetNode(startPos);
			}

			Vector2 endPos = new Vector2(random.Next(0, _width * _cellSize), random.Next(0, _width * _cellSize));
			EndNode = GetNode(endPos);
			while (EndNode == null)
			{
				endPos = new Vector2(random.Next(0, _width * _cellSize), random.Next(0, _width * _cellSize));
				EndNode = GetNode(endPos);
			}
		}

		public abstract T GetNode(Vector2 pos);

		public bool IsBlocked(float x, float y)
		{
			return IsBlocked(new Vector2(x, y));
		}

		public bool IsBlocked(Vector2 pos)
		{
			return _blockedNodes.ContainsKey(pos);
		}

		protected bool InBound(float x, float y)
		{
			return x >= 0f && y >= 0f && x < (_width * _cellSize) && y < (_height * _cellSize);
		}

		protected bool InBound(Vector2 pos)
		{
			return InBound(pos.X, pos.Y);
		}

		public virtual void Render(SpriteBatch spriteBatch, Color walkableColor, Color blockedColor)
		{
			foreach (T node in _blockedNodes.Values)
			{
				spriteBatch.DrawRectangle(node.Position, Vector2.One * _cellSize, blockedColor);
			}

			if (ShowGrid)
			{
				foreach (T node in _walkableNodes.Values)
				{
					spriteBatch.DrawRectangle(node.Position, Vector2.One * _cellSize, walkableColor);
				}
			}

			if (StartNode != null && EndNode != null)
			{
				spriteBatch.DrawRectangle(StartNode.Position, Vector2.One * _cellSize, Color.Yellow);
				spriteBatch.DrawRectangle(EndNode.Position, Vector2.One * _cellSize, Color.IndianRed);
			}
		}

		public IEnumerable<T> Neighbors(T node, bool canGoDiagonal)
		{
			Vector2[] neighborSet = canGoDiagonal ? DiagonalNeighborSet : OrthogonalNeighborSet;

			foreach (Vector2 dir in neighborSet)
			{
				float x = node.Position.X + dir.X * _cellSize;
				float y = node.Position.Y + dir.Y * _cellSize;
				Vector2 pos = new Vector2(x, y);

				T neighbor = GetNode(pos);
				if (neighbor != null)
				{
					yield return neighbor;
				}
			}
		}
	}
}
