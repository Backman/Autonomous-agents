using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharpSteer
{
	public class FlowField
	{
		public Vector2[,] Field { get; private set; }
		public int Cols { get; private set; }
		public int Rows { get; private set; }
		public int Resolution { get; private set; }

		public FlowField(int width, int height, int resolution)
		{
			Resolution = resolution;

			Cols = width / resolution;
			Rows = height / resolution;
			Field = new Vector2[Cols, Rows];
		}
		float xoff;
		float yoff;
		public void UpdateField(Vector2 targetPos)
		{
			for (int i = 0; i < Cols; ++i)
			{
				for (int j = 0; j < Rows; ++j)
				{
					//Field[i, j] = Vector2.Normalize(targetPos - GetPosition(i, j));
					float noise = SimplexNoise.Noise.Generate(xoff, yoff);
					Field[i, j] = Vector2.Normalize(new Vector2((float)Math.Cos(noise), (float)Math.Sin(noise)));
					yoff += 0.1f;
				}
				
				xoff += 0.1f;
			}
		}

		public Vector2 Lookup(Vector2 lookup)
		{
			int col = Math.Min((int)lookup.X / Resolution, Cols - 1);
			int row = Math.Min((int)lookup.Y / Resolution, Rows - 1);

			Vector2 v = Field[col, row];
			Vector2 ret = new Vector2(v.X, v.Y);

			return ret;
		}

		public Vector2 GetPosition(int i, int j)
		{
			return new Vector2(i * Resolution, j * Resolution);
		}

		public void DrawField(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < Cols; ++i)
			{
				for (int j = 0; j < Rows; ++j)
				{
					DrawVector(spriteBatch, Field[i, j], GetPosition(i, j));
				}
			}
		}

		public void DrawVector(SpriteBatch spriteBatch, Vector2 dir, Vector2 pos)
		{
			Vector2 dirPos = pos + Vector2.One * (Resolution / 2);
			spriteBatch.DrawLine(dirPos, dirPos + dir * 10f, Color.Red);
			spriteBatch.DrawCircle(dirPos, 3f, 10, Color.Black);
			spriteBatch.DrawRectangle(pos, new Vector2(Resolution), Color.Green);
		}
	}
}
