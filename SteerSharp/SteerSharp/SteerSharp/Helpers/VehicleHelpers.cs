using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace SteerSharp.Helpers
{
	public static class VehicleHelpers
	{
		public static Vector2 SteerForSeek(this Vehicle vehicle, Vector2 target, float maxSpeed)
		{
			Vector2 offset = target - vehicle.Position;
			Vector2 desiredVel = offset.Truncate(maxSpeed);
			return desiredVel - vehicle.Velocity;
		}

		public static Vector2 SteerForFlee(this Vehicle vehicle, Vector2 target, float maxSpeed, float fleeDist)
		{
			float fleeDistSqr = fleeDist * fleeDist;

			if (Vector2.DistanceSquared(vehicle.Position, target) > fleeDistSqr)
			{
				return Vector2.Zero;
			}

			Vector2 offset = vehicle.Position - target;
			Vector2 desiredVel = offset.Truncate(maxSpeed);
			return desiredVel - vehicle.Velocity;
		}

		public static Vector2 SteerForArrival(this Vehicle vehicle, Vector2 target, float maxSpeed, float slowingDist)
		{
			Vector2 offset = target - vehicle.Position;
			float dist = offset.Length();

			if (dist > 0f)
			{
				float rampedSpeed = maxSpeed * (dist / slowingDist);
				float clippedSpeed = Math.Min(rampedSpeed, maxSpeed);

				Vector2 desiredVel = (clippedSpeed / dist) * offset;
				return desiredVel - vehicle.Velocity;
			}

			return Vector2.Zero;
		}
	}
}
