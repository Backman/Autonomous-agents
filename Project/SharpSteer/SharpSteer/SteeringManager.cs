using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using SharpSteer.Helpers;

namespace SharpSteer
{
	public class SteeringManager
	{
		/// <summary>
		/// The steering force
		/// </summary>
		public Vector2 SteeringForce { get; private set; }

		/// <summary>
		/// The owner of this manager
		/// </summary>
		public IVehicle Owner { get; private set; }

		private float _wanderAngle = 0f;
		private float _wanderChange = 1f;
		private Vector2 _wanderTarget = Vector2.Zero;

		public SteeringManager(IVehicle owner)
		{
			Owner = owner;
			ResetSteeringForce();
		}

		public void Update(float elapsedTime)
		{
			SteeringForce.Truncate(Owner.MaxForce);
			Vector2 acceleration = SteeringForce;

			Owner.Velocity += acceleration;
			Owner.Velocity.Truncate(Owner.MaxSpeed);

			Owner.Position += Owner.Velocity * elapsedTime;

			ResetSteeringForce();
		}

		public void ResetSteeringForce()
		{
			SteeringForce = Vector2.Zero;
		}

		public void Seek(Vector2 target, float slowingDist = 20f)
		{
			SteeringForce += DoSeek(target, slowingDist);
		}

		public void Flee(Vector2 target, float fleeDist = 100f)
		{
			SteeringForce += DoFlee(target, fleeDist);
		}

		public void Follow(FlowField field)
		{
			SteeringForce += DoFollow(field);
		}

		#region Internal API
		private Vector2 DoSeek(Vector2 target, float slowingDist)
		{
			Vector2 force = Vector2.Zero;
			Vector2 desiredVel = Vector2.Zero;
			float dist = 0f;

			Vector2 offset = target - Owner.Position;

			dist = offset.Length();

			float rampedSpeed = Owner.MaxSpeed * (dist / slowingDist);
			float clippedSpeed = Math.Min(rampedSpeed, Owner.MaxSpeed);
			desiredVel = (clippedSpeed / dist) * offset;

			force = desiredVel - Owner.Velocity;

			return force;
		}

		private Vector2 DoFlee(Vector2 target, float fleeDist)
		{
			float fleeDistSqr = fleeDist * fleeDist;

			if (Vector2.DistanceSquared(Owner.Position, target) > fleeDistSqr)
			{
				return Vector2.Zero;
			}

			Vector2 desiredVel = Vector2.Normalize(Owner.Position - target) * Owner.MaxSpeed;

			return desiredVel - Owner.Velocity;
		}

		private Vector2 DoFollow(FlowField field)
		{
			Vector2 desiredVel = field.Lookup(Owner.Position);

			desiredVel *= Owner.MaxSpeed;

			Vector2 steer = desiredVel - Owner.Velocity;

			return steer;
		}
		#endregion
	}
}
