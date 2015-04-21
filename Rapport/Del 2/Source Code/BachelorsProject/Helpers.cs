#region Using Statements
using Microsoft.Xna.Framework;
using System;
#endregion

namespace BachelorsProject
{
	public static class Helpers
	{
		public static Vector2 TruncateLength(this Vector2 v, float maxLength)
		{
			float maxLengthSqr = maxLength * maxLength;
			float vecLengthSqr = v.LengthSquared();
			if (vecLengthSqr <= maxLengthSqr)
			{
				return v;
			}

			return (v * (maxLength / (float)Math.Sqrt(vecLengthSqr)));
		}

		public static void Perpendicular(this Vector2 v, out Vector2 perpVec)
		{
			perpVec.X = -v.Y;
			perpVec.Y = v.X;
		}

		public static Vector2 SafeNormalize(this Vector2 v)
		{
			Vector2 ret = v;
			float length = ret.Length();
			if (length > float.Epsilon)
			{
				ret /= length;
			}

			return ret;
		}

		public static bool IsNaN(this Vector2 v)
		{
			return v.X.IsNaN() || v.Y.IsNaN();
		} 

		public static bool IsNaN(this float f)
		{
			return float.IsNaN(f);
		}
	}
}
