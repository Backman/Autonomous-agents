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
		
		public SteeringManager(IVehicle owner)
		{
			Owner = owner;
			ResetSteeringForce();
		}

		public void Update(float elapsedTime)
		{
			SteeringForce.Truncate(Owner.MaxForce);
			SteeringForce *= Owner.InvMass;
			Vector2 acceleration = SteeringForce;

			Owner.Velocity += acceleration;
			Owner.Velocity.Truncate(Owner.MaxVelocity);

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

		public void FlowFieldFollow(FlowField field)
		{
			SteeringForce += DoFlowFieldFollow(field);
		}

		public void PathFollow(Path path)
		{
			SteeringForce += DoPathFollow(path);
		}

		public void Flocking(List<Agent> agents, float neighborRadius = 30f)
		{
			SteeringForce += DoFlocking(agents, neighborRadius);
		}

		#region Internal API
		private Vector2 DoSeek(Vector2 target, float slowingDist)
		{
			Vector2 force = Vector2.Zero;
			Vector2 desiredVel = Vector2.Zero;
			float dist = 0f;

			Vector2 offset = target - Owner.Position;

			dist = offset.Length();

			float rampedSpeed = Owner.MaxVelocity * (dist / slowingDist);
			float clippedSpeed = Math.Min(rampedSpeed, Owner.MaxVelocity);
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

			Vector2 desiredVel = Vector2.Normalize(Owner.Position - target) * Owner.MaxVelocity;

			return desiredVel - Owner.Velocity;
		}

		private Vector2 DoFlowFieldFollow(FlowField field)
		{
			Vector2 desiredVel = field.Lookup(Owner.Position);

			desiredVel *= Owner.MaxVelocity;

			Vector2 steer = desiredVel - Owner.Velocity;

			return steer;
		}

		private Vector2 DoPathFollow(Path path)
		{
			return Vector2.Zero;
		}

		private Vector2 DoFlocking(List<Agent> agents, float neighborRadius)
		{
			Vector2 steer = Vector2.Zero;

			List<Agent> neighbors = new List<Agent>(agents.Count);
			float neighborRadiusSqr = neighborRadius * neighborRadius;

			foreach (Agent agent in agents)
			{
				float dist = Vector2.DistanceSquared(Owner.Position, agent.Position);

				if (dist > 0.0f && dist < neighborRadiusSqr)
				{
					neighbors.Add(agent);
				}
			}

			steer += DoSeperation(neighbors, neighborRadiusSqr);
			steer += DoCohesion(neighbors, neighborRadius);
			steer += DoAlignment(neighbors, neighborRadius);

			return steer;
		}

		private Vector2 DoSeperation(List<Agent> neighbors, float neighborRadius)
		{
			Vector2 steer = Vector2.Zero;
			Vector2 totalSum = Vector2.Zero;
			int neighborCount = neighbors.Count;

			foreach (Agent agent in neighbors)
			{
				Vector2 diff = Owner.Position - agent.Position;
				diff.Normalize();
				diff = Vector2.Divide(diff, neighborRadius);

				totalSum += diff;
			}

			if (neighborCount > 0)
			{
				totalSum = Vector2.Divide(totalSum, neighborCount);
				totalSum.Normalize();
				totalSum *= Owner.MaxVelocity;

				steer = totalSum - Owner.Velocity;
			}

			return steer;
		}

		private Vector2 DoCohesion(List<Agent> neighbors, float neighborRadius)
		{
			Vector2 steer = Vector2.Zero;

			return steer;
		}

		private Vector2 DoAlignment(List<Agent> neighbors, float neighborRadius)
		{
			Vector2 steer = Vector2.Zero;

			return steer;
		}
		#endregion
	}
}
