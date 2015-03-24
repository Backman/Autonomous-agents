using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Autonomous_Agents
{
	public class SteeringManager
	{
		private Agent _agent;

		private Vector2 _steeringForce = Vector2.Zero;

		public SteeringManager(Agent owner)
		{
			_agent = owner;
		}

		private void ResetSteeringForce()
		{
			_steeringForce = Vector2.Zero;
		}

		public void Tick(float elapsedTime)
		{
			_steeringForce.Truncate(_agent.MaxForce);
			_steeringForce *= _agent.InvMass;

			_agent.Velocity += _steeringForce;
			_agent.Velocity.Truncate(_agent.MaxVelocity);

			_agent.Position += _agent.Velocity * elapsedTime;

			ResetSteeringForce();
		}

		public void Seek(Vector2 target, float slowingDist = 20f)
		{
			_steeringForce += DoSeek(target, slowingDist);
		}

		public void Flee(Vector2 target, float fleeDist = 100f)
		{
			_steeringForce += DoFlee(target, fleeDist);
		}

		public void FlowFieldFollow(FlowField field)
		{
			_steeringForce += DoFlowFieldFollow(field);
		}

		public void PathFollow(AStarPath path)
		{
			_steeringForce += DoPathFollow(path);
		}

		public void Flocking(List<Agent> agents, float neighborRadius = 30f)
		{
			_steeringForce += DoFlocking(agents, neighborRadius);
		}

		public void Seperation(List<Agent> agents, float neighborRadius = 30f)
		{
			List<Agent> neighbors = new List<Agent>(agents.Count);
			float neighborRadiusSqr = neighborRadius * neighborRadius;

			foreach (Agent agent in agents)
			{
				float dist = Vector2.DistanceSquared(_agent.Position, agent.Position);

				if (dist > 0.0f && dist < neighborRadiusSqr)
				{
					neighbors.Add(agent);
				}
			}

			_steeringForce += DoSeperation(neighbors, neighborRadius);
		}
		public void Cohesion(List<Agent> agents, float neighborRadius = 30f)
		{
			List<Agent> neighbors = new List<Agent>(agents.Count);
			float neighborRadiusSqr = neighborRadius * neighborRadius;

			foreach (Agent agent in agents)
			{
				float dist = Vector2.DistanceSquared(_agent.Position, agent.Position);

				if (dist > 0.0f && dist < neighborRadiusSqr)
				{
					neighbors.Add(agent);
				}
			}

			_steeringForce += DoCohesion(neighbors, neighborRadius);
		}
		public void Alignment(List<Agent> agents, float neighborRadius = 30f)
		{
			List<Agent> neighbors = new List<Agent>(agents.Count);
			float neighborRadiusSqr = neighborRadius * neighborRadius;

			foreach (Agent agent in agents)
			{
				float dist = Vector2.DistanceSquared(_agent.Position, agent.Position);

				if (dist > 0.0f && dist < neighborRadiusSqr)
				{
					neighbors.Add(agent);
				}
			}

			_steeringForce += DoAlignment(neighbors, neighborRadius);
		}

		#region Internal API
		private Vector2 DoSeek(Vector2 target, float slowingDist)
		{
			Vector2 force = Vector2.Zero;
			Vector2 desiredVel = Vector2.Zero;
			float dist = 0f;

			Vector2 offset = target - _agent.Position;

			dist = offset.Length();

			float rampedSpeed = _agent.MaxVelocity * (dist / slowingDist);
			float clippedSpeed = Math.Min(rampedSpeed, _agent.MaxVelocity);
			desiredVel = (clippedSpeed / dist) * offset;

			force = desiredVel - _agent.Velocity;

			return force;
		}

		private Vector2 DoFlee(Vector2 target, float fleeDist)
		{
			float fleeDistSqr = fleeDist * fleeDist;

			if (Vector2.DistanceSquared(_agent.Position, target) > fleeDistSqr)
			{
				return Vector2.Zero;
			}

			Vector2 desiredVel = Vector2.Normalize(_agent.Position - target) * _agent.MaxVelocity;

			return desiredVel - _agent.Velocity;
		}

		private Vector2 DoFlowFieldFollow(FlowField field)
		{
			Vector2 desiredVel = field.Sample(_agent.Position);

			desiredVel *= _agent.MaxVelocity;

			Vector2 steer = desiredVel - _agent.Velocity;

			return steer;
		}

		private Vector2 DoPathFollow(AStarPath path)
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
				float dist = Vector2.DistanceSquared(_agent.Position, agent.Position);

				if (dist > 0.0f && dist < neighborRadiusSqr)
				{
					neighbors.Add(agent);
				}
			}

			Vector2 seperation = DoSeperation(neighbors, neighborRadius);
			Vector2 cohesion = DoCohesion(neighbors, neighborRadius);
			Vector2 alignment = DoAlignment(neighbors, neighborRadius);

			steer += seperation * 1.5f;
			steer += cohesion * 1.0f;
			steer += alignment * 1.0f;

			return steer;
		}

		private Vector2 DoSeperation(List<Agent> neighbors, float neighborRadius)
		{
			Vector2 steer = Vector2.Zero;
			Vector2 totalSum = Vector2.Zero;
			int neighborCount = neighbors.Count;

			foreach (Agent neighbor in neighbors)
			{
				Vector2 diff = _agent.Position - neighbor.Position;
				diff.Normalize();
				diff = Vector2.Divide(diff, neighborRadius);

				totalSum += diff;
			}

			if (neighborCount > 0)
			{
				totalSum = Vector2.Divide(totalSum, neighborCount);
				totalSum.Normalize();
				totalSum *= _agent.MaxVelocity;

				steer = totalSum - _agent.Velocity;
			}

			return steer;
		}

		private Vector2 DoCohesion(List<Agent> neighbors, float neighborRadius)
		{
			Vector2 steer = Vector2.Zero;
			Vector2 totalSum = Vector2.Zero;
			int neighborCount = neighbors.Count;

			foreach (Agent neighbor in neighbors)
			{
				totalSum += neighbor.Position;
			}

			if (neighborCount > 0)
			{
				totalSum = Vector2.Divide(totalSum, neighborCount);
				steer = DoSeek(totalSum, 20f);
			}

			return steer;
		}

		private Vector2 DoAlignment(List<Agent> neighbors, float neighborRadius)
		{
			Vector2 steer = Vector2.Zero;
			Vector2 totalSum = Vector2.Zero;
			int neighborCount = neighbors.Count;

			foreach (Agent neighbor in neighbors)
			{
				totalSum += neighbor.Velocity;
			}

			if (neighborCount > 0)
			{
				totalSum = Vector2.Divide(totalSum, neighborCount);
				if (totalSum.LengthSquared() != 0f)
				{
					totalSum.Normalize();
				}

				totalSum *= _agent.MaxVelocity;

				steer = totalSum - _agent.Velocity;
			}

			return steer;
		}
		#endregion
	}
}
