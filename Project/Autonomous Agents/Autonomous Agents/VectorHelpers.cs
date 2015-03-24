using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Autonomous_Agents
{
	public static class VectorHelpers
	{
		public static void Truncate(this Vector2 v, float max)
		{
			if (v.LengthSquared() > max * max)
			{
				v.Normalize();

				v *= max;
			}
		}

		public static Vector2 Perpendicular(this Vector2 v)
		{
			return new Vector2(-v.Y, v.X);
		}
	}
}
