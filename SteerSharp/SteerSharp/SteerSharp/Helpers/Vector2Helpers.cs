using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace SteerSharp.Helpers
{
	public static class Vector2Helpers
	{
		public static Vector2 Truncate(this Vector2 v, float maxLenght)
		{
			float maxLengthSqr = maxLenght * maxLenght;
			float vecLengthSqr = v.LengthSquared();

			if (vecLengthSqr <= maxLenght)
			{
				return v;
			}

			return (v * (maxLenght / (float)Math.Sqrt(vecLengthSqr)));
		}

		public static Vector2 Perpendicular(this Vector2 v)
		{
			return new Vector2(-v.Y, v.X);
		}
	}
}
