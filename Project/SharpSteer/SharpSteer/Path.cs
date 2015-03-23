using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpSteer
{
	public class Path
	{
		private Node _start, _end;
		private List<Node> _path = new List<Node>();

		public Path(Node start, Node end)
		{
			_start = start;
			_end = end;
			ConstructPath(start, end);
		}

		private void ConstructPath(Node start, Node end)
		{
			_path.Clear();

			Node current = end;

			while (current != null)
			{
				_path.Add(current);
				current = current.Parent;
			}
		}

		public void Draw(SpriteBatch spriteBatch, Color color, float thickness = 1f)
		{
			for (int i = 1; i < _path.Count; ++i)
			{
				Node n1 = _path[i - 1];
				Node n2 = _path[i];

				spriteBatch.DrawLine(n1.Position, n2.Position, color, thickness);
			}
		}
	}
}
