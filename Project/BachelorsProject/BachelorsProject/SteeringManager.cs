#region Using Statements
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
#endregion

namespace BachelorsProject
{
	public class SteeringManager
	{
		private Agent _agent;

		private Vector2 _steeringForce = Vector2.Zero;

		public Vector2[] _feelers = new Vector2[3];

		private const float PI = 3.14159f;
		private const float TWO_PI = PI * 2f;
		private const float HALF_PI = PI / 2f;
		private const float QUARTER_PI = PI / 4f;

		public const float STEERING_FORCE_TWEAKER	= 10f;

		private const float SEPARATION_WEIGHT		= 80f * STEERING_FORCE_TWEAKER;
		private const float ALIGNMENT_WEIGHT		= 0.1f * STEERING_FORCE_TWEAKER;
		private const float COHESION_WEIGHT			= 1f * STEERING_FORCE_TWEAKER;
		private const float PATH_FOLLOWING_WEIGHT	= 0.5f * STEERING_FORCE_TWEAKER;
		private const float FLOW_FIELD_WEIGHT		= 0.5f * STEERING_FORCE_TWEAKER;
		private const float SEEK_WEIGHT				= 1f * STEERING_FORCE_TWEAKER;
		private const float ARRIVE_WEIGHT			= 1f * STEERING_FORCE_TWEAKER;
		private const float WALL_AVOIDANCE_WEIGHT	= 0.5f * STEERING_FORCE_TWEAKER;

		public enum Deceleration
		{
			Fast = 1,
			Normal = 2,
			Slow = 3
		}

		private Deceleration _deceleration = Deceleration.Normal;

		public SteeringManager(Agent owner)
		{
			_agent = owner;
		}

		private void ResetSteeringForce()
		{
			_steeringForce = Vector2.Zero;
		}

		public Vector2 CalculatePrioritized(bool flowFieldFollow)
		{
			ResetSteeringForce();

			Vector2 forceToAdd = Vector2.Zero;

			_agent.Level.TagAgentsWithinViewRange(_agent, _agent.Radius * 2f);

			bool debugMouse = false;

			if (!debugMouse)
			{
				forceToAdd = WallAvoidance(_agent.Level.Polygons, 10f) * WALL_AVOIDANCE_WEIGHT;
				if (!AccumulateForce(forceToAdd))
				{
					return _steeringForce;
				}

				forceToAdd = Separation(_agent.Level.Agents) * SEPARATION_WEIGHT;
				if (!AccumulateForce(forceToAdd))
				{
					return _steeringForce;
				}
				forceToAdd = Alignment(_agent.Level.Agents) * ALIGNMENT_WEIGHT;
				if (!AccumulateForce(forceToAdd))
				{
					return _steeringForce;
				}
				forceToAdd = Cohesion(_agent.Level.Agents) * COHESION_WEIGHT;
				if (!AccumulateForce(forceToAdd))
				{
					return _steeringForce;
				}

				if (flowFieldFollow)
				{
					forceToAdd = FlowFieldFollow(_agent.FlowField) * FLOW_FIELD_WEIGHT;
					if (!AccumulateForce(forceToAdd))
					{
						return _steeringForce;
					}
				}
				else
				{
					forceToAdd = StayOnPath(_agent.AStarPath, 20f, 40f) * PATH_FOLLOWING_WEIGHT;
					if (!AccumulateForce(forceToAdd))
					{
						return _steeringForce;
					}
				}
			}
			else
			{
				forceToAdd = Separation(_agent.Level.Agents) * SEPARATION_WEIGHT;
				if (!AccumulateForce(forceToAdd))
				{
					return _steeringForce;
				}
				forceToAdd = Alignment(_agent.Level.Agents) * ALIGNMENT_WEIGHT;
				if (!AccumulateForce(forceToAdd))
				{
					return _steeringForce;
				}
				forceToAdd = Cohesion(_agent.Level.Agents) * COHESION_WEIGHT;
				if (!AccumulateForce(forceToAdd))
				{
					return _steeringForce;
				}

				Microsoft.Xna.Framework.Input.MouseState mouse = Microsoft.Xna.Framework.Input.Mouse.GetState();
				forceToAdd = Arrive(new Vector2((int)mouse.X, (int)mouse.Y), _deceleration) * ARRIVE_WEIGHT;
				if (!AccumulateForce(forceToAdd))
				{
					return _steeringForce;
				}
			}
			return _steeringForce;
		}

		private bool AccumulateForce(Vector2 forceToAdd)
		{
			float magnitudeSoFar = _steeringForce.Length();

			float magnitudeRemaining = _agent.MaxForce - magnitudeSoFar;

			if (magnitudeRemaining <= 0.0f)
			{
				return false;
			}

			float magnitudeToAdd = forceToAdd.Length();

			if (magnitudeToAdd < magnitudeRemaining)
			{
				_steeringForce += forceToAdd;
			}
			else
			{
				_steeringForce += Vector2.Normalize(forceToAdd) * magnitudeRemaining;
			}

			return true;
		}

		public Vector2 Seek(Vector2 target)
		{
			Vector2 steeringForce = Vector2.Zero;

			steeringForce = DoSeek(target);

			return steeringForce;
		}

		public Vector2 Arrive(Vector2 target, Deceleration deceleration)
		{
			Vector2 steeringForce = Vector2.Zero;

			steeringForce = DoArrive(target, deceleration);

			return steeringForce;
		}

		public Vector2 Flee(Vector2 target, float fleeDist = 100f)
		{
			Vector2 steeringForce = Vector2.Zero;

			steeringForce = DoFlee(target, fleeDist);

			return steeringForce;
		}

		public Vector2 FlowFieldFollow(FlowFieldGrid field)
		{
			Vector2 steeringForce = Vector2.Zero;

			steeringForce = DoFlowFieldFollow(field);

			return steeringForce;
		}

		public Vector2 FollowPath(AStarPath path, bool direction, float predictionTime)
		{
			Vector2 steeringForce = Vector2.Zero;

			steeringForce = DoFollowPath(path, direction, predictionTime);

			return steeringForce;
		}

		public Vector2 StayOnPath(AStarPath path, float seekDistance, float maxSeekDistance)
		{
			Vector2 steeringForce = Vector2.Zero;

			steeringForce = DoStayOnPath(path, seekDistance, maxSeekDistance);

			return steeringForce;
		}

		public Vector2 WallAvoidance(List<PolygonShape> polygons, float feelerLength)
		{
			Vector2 steeringForce = Vector2.Zero;

			steeringForce = DoWallAvoidance(polygons, feelerLength);

			return steeringForce;
		}

		public Vector2 Separation(Agent[] agents)
		{
			Vector2 steeringForce = Vector2.Zero;
			Vector2 sum = Vector2.Zero;
			int neighborCount = 0;
			int agentCount = agents.Length;

			for (int i = 0; i < agentCount; i++)
			{
				var neighbor = agents[i];

				if (neighbor != _agent && neighbor.Tag)
				{
					//Vector2 offset = neighbor.Position - _agent.Position;
					//float distanceSqr = Vector2.DistanceSquared(neighbor.Position, _agent.Position);
					////Vector2 diff = Vector2.Normalize(dist);
					////diff = Vector2.Divide(diff, dist.Length());
					//sum += (offset / -distanceSqr);
					Vector2 offset = neighbor.Position - _agent.Position;
					float distSqr = Vector2.DistanceSquared(_agent.Position, neighbor.Position);

					steeringForce += offset / -distSqr;

					neighborCount++;
				}
			}

			if (neighborCount > 0)
			{
				steeringForce = (steeringForce / (float)neighborCount);
				steeringForce = steeringForce.SafeNormalize();
			}

			//if (steeringForce.Length() > 0f)
			//{
			//	steeringForce.Normalize();
			//	steeringForce *= _agent.MaxVelocity;
			//	steeringForce -= _agent.Velocity;
			//	steeringForce.Truncate(_agent.MaxForce);
			//}

			return steeringForce;
		}

		public Vector2 Alignment(Agent[] agents)
		{
			Vector2 steeringForce = Vector2.Zero;
			Vector2 averageHeading = Vector2.Zero;

			int neighborCount = 0;
			int agentCount = agents.Length;

			for (int i = 0; i < agentCount; i++)
			{
				var neighbor = agents[i];

				if (neighbor != _agent && neighbor.Tag)
				{
					averageHeading += neighbor.Direction;
					neighborCount++;
				}
			}

			if (neighborCount > 0)
			{
				averageHeading /= (float)neighborCount;

				steeringForce = averageHeading - _agent.Direction;
			}

			return steeringForce;
		}

		public Vector2 Cohesion(Agent[] agents)
		{
			Vector2 steeringForce = Vector2.Zero;
			Vector2 centerOfMass = Vector2.Zero;

			int neighborCount = 0;
			int agentCount = agents.Length;

			for (int i = 0; i < agentCount; i++)
			{
				var neighbor = agents[i];

				if (neighbor != _agent && neighbor.Tag)
				{
					centerOfMass += neighbor.Position;
					neighborCount++;
				}
			}

			if (neighborCount > 0)
			{
				centerOfMass /= (float)neighborCount;

				steeringForce = DoSeek(centerOfMass);
			}

			return steeringForce;
		}

		#region Internal API
		private Vector2 DoSeek(Vector2 target)
		{
			Vector2 offset = target - _agent.Position;
			Vector2 desiredVel = offset.SafeNormalize() * _agent.MaxVelocity;

			return desiredVel - _agent.Velocity;
		}

		private Vector2 DoArrive(Vector2 target, Deceleration deceleration)
		{
			Vector2 steeringForce = Vector2.Zero;

			Vector2 toTarget = target - _agent.Position;

			float dist = toTarget.Length();

			if (dist > 0f)
			{
				const float decelerationTweaker = 0.3f;

				float speed = dist / ((float)deceleration * decelerationTweaker);

				speed = Math.Min(speed, _agent.MaxVelocity);

				Vector2 desiredVel = toTarget * speed / dist;

				steeringForce = desiredVel - _agent.Velocity;
			}

			return steeringForce;
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

		private Vector2 DoFlowFieldFollow(FlowFieldGrid field)
		{
			Vector2 desiredVel = field.Sample(_agent.Position);

			desiredVel *= _agent.MaxVelocity;

			Vector2 steer = desiredVel - _agent.Velocity;

			return steer;
		}

		private Vector2 DoWallAvoidance(List<PolygonShape> polygons, float feelerLength)
		{
			CreateFeelers(feelerLength);
			
			Vector2 force = Vector2.Zero;
			Vector2 point = Vector2.Zero;
			Vector2 closestPoint = Vector2.Zero;
			Vector2 intersection = Vector2.Zero;
			Vector2 agentPos = _agent.Position;

			float distToThisIP = 0.0f;
			float distToClosestIP = float.MaxValue;
			Vector2 normal = Vector2.Zero;

			for (int i = 0; i < _feelers.Length; i++)
			{
				Vector2 feeler = _feelers[i];
				for (int j = 0; j < polygons.Count; j++)
				{
					var polygon = polygons[j];
					int vertCount = polygon.Vertices.Count;

					EdgeShape line = new EdgeShape(_agent.Position, feeler);

					for (int ip = 0; ip < vertCount - 1; ip++)
					{
						Vector2 p1 = polygon.Vertices[ip];
						Vector2 p2 = polygon.Vertices[ip + 1];

						if (LineTools.LineIntersect(ref agentPos, ref feeler, ref p1, ref p2, out intersection))
						{
							distToThisIP = Vector2.Distance(agentPos, intersection);
							if (distToThisIP < distToClosestIP)
							{
								distToClosestIP = distToThisIP;

								closestPoint = intersection;
								normal = intersection - feeler;
							}
						}
					}
				}
				
				if (closestPoint != Vector2.Zero)
				{
					Vector2 overshoot = feeler - closestPoint;
					float length = overshoot.Length();
					force = normal * length;
				}
			}

			return force;
		}

		private void CreateFeelers(float length)
		{
			//feeler pointing straight in front
			_feelers[0] = _agent.Position + length * _agent.Direction;

			//feeler to left
			Vector2 temp = _agent.Direction;
			Vector2 rot = Vector2.Transform(temp, Matrix.CreateRotationZ(HALF_PI * 3.5f));
			_feelers[1] = _agent.Position + length / 2.0f * rot;

			//feeler to right
			temp = _agent.Direction;
			rot = Vector2.Transform(temp, Matrix.CreateRotationZ(HALF_PI * 0.5f));
			_feelers[2] = _agent.Position + length / 2.0f * rot;
		}

		private Vector2 DoFollowPath(AStarPath path, bool direction, float predictionTime)
		{
			Vector2 steeringForce = Vector2.Zero;
			return steeringForce;
			//float pathDistanceOffset = (direction ? 1 : -1) * _agent.Velocity.Length();
			//
			//Vector2 futurePos = _agent.PredictFuturePosition(predictionTime);
			//
			//float nowPathDistance = path.PointToPathDistance(_agent.Position);
			//float futureDistance = path.PointToPathDistance(futurePos);
			//
			//Target = path.PointOnPath(futureDistance);
			//
			//bool rightWay = pathDistanceOffset > 0f ? nowPathDistance < futureDistance : nowPathDistance > futureDistance;
			//
			//AStarPath.DistanceOutput output;
			//Vector2 closestPoint = path.ClosestPointOnPath(futurePos, out output);
			//
			//if (output.Outside < 0f && rightWay)
			//{
			//	return Vector2.Zero;
			//}
			//
			//float targetPathDistance = nowPathDistance + pathDistanceOffset;
			//Vector2 target = path.PointOnPath(targetPathDistance);
			////Target = target;
			//return DoSeek(target);
		}

		private Vector2 DoStayOnPath(AStarPath path, float seekDistance, float maxSeekDistance)
		{
			Vector2 steeringForce = Vector2.Zero;

			if (Vector2.DistanceSquared(path.CurrentWaypoint, _agent.Position) < seekDistance * seekDistance)
			{
				path.SetNextWaypoint();
			}
			else if (Vector2.DistanceSquared(path.CurrentWaypoint, _agent.Position) > maxSeekDistance * maxSeekDistance)
			{
				path.FindClosestWaypoint(_agent.Position);
			}

			Target = path.CurrentWaypoint;

			if (!path.Finished)
			{
				steeringForce = Seek(path.CurrentWaypoint);
			}
			else
			{
				steeringForce = Arrive(path.CurrentWaypoint, Deceleration.Normal);
			}

			return steeringForce;

			//float predictionTime = 0.2f;
			//Vector2 predictPos = _agent.PredictFuturePosition(predictionTime);
			//
			//AStarPath.DistanceOutput output = new AStarPath.DistanceOutput();
			//Vector2 pointOnPath = path.ClosestPointOnPath(_agent.Position, out output);
			//
			////float futurePosDist = path.PointToPathDistance(predictPos);
			////Vector2 futurePointOnPath = path.PointOnPath(futurePosDist);
			//
			//if (output.Outside < 0f)
			//{
			//	return Vector2.Zero;
			//}
			//
			//Target = pointOnPath;
			//return Seek(pointOnPath);

			//Vertices vertices = path.Vertices;

			//Vector2 normal = Vector2.Zero;
			//Vector2 target = Vector2.Zero;
			
			//float shortestDistance = float.MaxValue;
			//int waypointCount = path.Waypoints.Length;

			//for (int i = 0; i < waypointCount - 1; i++)
			//{
			//	Vector2 a = path.Waypoints[i];
			//	Vector2 b = path.Waypoints[i + 1];

			//	Vector2 normalPoint = ClosestPointOnSegment(a, b, predictPos);
				
			//	if (normalPoint.X < a.X || normalPoint.X > a.X)
			//	{
			//		normalPoint = b;
			//	}

			//	float distance = Vector2.Distance(predictPos, normalPoint);
			//	if (distance < shortestDistance)
			//	{
			//		shortestDistance = distance;
			//		normal = normalPoint;

			//		Vector2 dir = b - a;
			//		dir.Normalize();
			//		dir *= 50f;

			//		target = normalPoint;
			//		target += dir;
			//	}
			//}

			//if (shortestDistance > pathRadius)
			//{
			//	Target = target;
			//	force = DoSeek(target);
			//}

			//return force;
		}
		public Vector2 Target;

		private Vector2 ClosestPointOnSegment(Vector2 A, Vector2 B, Vector2 p)
		{
			Vector2 AP = p - A;
			Vector2 AB = B - A;

			float magnitudeAB = AB.LengthSquared();
			float ABAPproduct = Vector2.Dot(AP, AB);
			float distance = ABAPproduct / magnitudeAB;

			if (distance < 0.0f)
			{
				return A;
			}
			else if (distance > 1.0f)
			{
				return B;
			}

			return A + AB * distance;
		} 

		//private Vector2 DoFlocking(List<Agent> agents, float seperationWeight, float cohesionWeight, float alignmentWeight, float neighborRadius)
		//{
		//	Vector2 steer = Vector2.Zero;

		//	List<Agent> neighbors = new List<Agent>(agents.Count);
		//	float neighborRadiusSqr = neighborRadius * neighborRadius;

		//	foreach (Agent agent in agents)
		//	{
		//		float dist = Vector2.DistanceSquared(_agent.Position, agent.Position);

		//		if (dist > 0.0f && dist < neighborRadiusSqr)
		//		{
		//			neighbors.Add(agent);
		//		}
		//	}

		//	Vector2 seperation = DoSeperation(neighbors, neighborRadius);
		//	Vector2 cohesion = DoCohesion(neighbors, neighborRadius);
		//	Vector2 alignment = DoAlignment(neighbors, neighborRadius);

		//	steer += seperation * seperationWeight;
		//	steer += cohesion * cohesionWeight;
		//	steer += alignment * alignmentWeight;

		//	return steer;
		//}

		private Vector2 DoSeperation(List<Agent> neighbors, float neighborRadius)
		{
			Vector2 steer = Vector2.Zero;
			Vector2 totalSum = Vector2.Zero;
			int neighborCount = neighbors.Count;

			for (int i = 0; i < neighborCount; i++)
			{
				var neighbor = neighbors[i];

				Vector2 diff = _agent.Position - neighbor.Position;
				diff.Normalize();
				diff = Vector2.Divide(diff, neighborRadius);

				totalSum += diff;
			}

			if (neighborCount > 0)
			{
				totalSum = Vector2.Divide(totalSum, (float)neighborCount);
				if (totalSum != Vector2.Zero)
				{
					totalSum.Normalize();
				}

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

			for (int i = 0; i < neighborCount; i++)
			{
				var neighbor = neighbors[i];
				totalSum += neighbor.Position;
			}

			if (neighborCount > 0)
			{
				totalSum = Vector2.Divide(totalSum, neighborCount);
				steer = DoSeek(totalSum);
			}

			return steer;
		}

		private Vector2 DoAlignment(List<Agent> neighbors, float neighborRadius)
		{
			Vector2 steer = Vector2.Zero;
			Vector2 totalSum = Vector2.Zero;
			int neighborCount = neighbors.Count;

			for (int i = 0; i < neighborCount; i++)
			{
				var neighbor = neighbors[i];
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
