using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace SharpSteer.Helpers
{
	public static class Maths
	{
		public static Random random = new Random();
		public static T Clamp<T>(T value, T max, T min)
		 where T : System.IComparable<T>
		{
			T result = value;
			if (value.CompareTo(max) > 0)
				result = max;
			if (value.CompareTo(min) < 0)
				result = min;
			return result;
		}
		public static double RoundClamp(double val, double min, double max)
		{
			while (val > max)
				val = min + (val - max);
			while (val < min)
				val = max - (min - val);
			return val;

		}
		public static double RandomClamped(double min, double max)
		{
			return Maths.random.NextDouble() * (max - min) + min;
		}
		public static float DegreeToRadian(float degree)
		{
			return degree * ((float)Math.PI / 180);
		}
		public static float RadianToDegree(float radian)
		{
			return radian * (180 / (float)Math.PI);
		}
		public static double PointsToAngle(Vector2 a, Vector2 b)
		{
			return (double)RadianToDegree((float)Math.Atan2(a.Y - b.Y, a.X - b.X));
		}
		public static double DistanceBetweenPoints(Vector2 a, Vector2 b)
		{
			return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
		}
	}
}
