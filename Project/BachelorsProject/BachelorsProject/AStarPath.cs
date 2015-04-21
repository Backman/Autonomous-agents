#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
#endregion

namespace BachelorsProject
{
	public class AStarPath
	{
		public struct DistanceOutput
		{
			public Vector2 Tangent;
			public float Outside;
		}

		public const float PATH_RADIUS = 10f;

		//private List<AStarNode> _path = new List<AStarNode>();
		//public List<Vector2> Waypoints = new List<Vector2>();
		//public Vector2 GoalPosition;
		//public Vector2 Start;
		public Vector2[] Waypoints;
		//public Vector2[] Normals;
		//public float[] Lengths;
		public float TotalPathLenght = 0f;

		private int _currentWaypoint = 0;

		public Vector2 CurrentWaypoint
		{
			get
			{
				int pointCount = Waypoints.Length;
				return Waypoints[_currentWaypoint < pointCount ? _currentWaypoint : pointCount - 1];
			}
		}

		public bool Finished { get { return _currentWaypoint >= Waypoints.Length; } }

		public AStarPath()
		{

		}

		public void ConstructPath(AStarNode start, AStarNode end)
		{
			List<Vector2> points = new List<Vector2>();
			
			//Start = start.Position + origin;
			//GoalPosition = end.Position;

			_currentWaypoint = 0;

			AStarNode current = end;

			while (current != null)
			{
				points.Add(current.Position);
				current = current.Parent;
			}

			int pointCount = points.Count;
			
			Waypoints = new Vector2[pointCount];
			//Normals = new Vector2[pointCount];
			//Lengths = new float[pointCount];
			int currIdx = 0;
			for (int i = pointCount - 1; i >= 0; i--)
			{
				Waypoints[currIdx++] = points[i];
			}

			//for (int i = 0; i < pointCount; ++i)
			//{
			//	Waypoints[i] = points[i];
			//	if (i > 0)
			//	{
			//		Vector2 normal = points[i] - points[i - 1];
			//		Lengths[i] = normal.Length();
			//	
			//		normal.Perpendicular(out Normals[i]);
			//	
			//		TotalPathLenght += Lengths[i];
			//	}
			//}
		}

		public void SetNextWaypoint()
		{
			++_currentWaypoint;
		}

		public void FindClosestWaypoint(Vector2 position)
		{
			float minDist = float.MaxValue;
			float dist = 0f;

			int pointCount = Waypoints.Length;
			for (int i = 0; i < pointCount; i++)
			{
				dist = Vector2.DistanceSquared(position, Waypoints[i]);
				if (dist < minDist)
				{
					minDist = dist;
					_currentWaypoint = i;
				}
			}
		}

		//public Vector2 ClosestPointOnPath(Vector2 point, out DistanceOutput output)
		//{
		//	output = new DistanceOutput();
		//	Vector2 pointOnPath = Vector2.Zero;
		//	Vector2 closestPoint = Vector2.Zero;

		//	float dist = 0f;
		//	float minDist = float.MaxValue;
		//	float segmentProj = 0f;

		//	int pointCount = Waypoints.Length;
		//	for (int i = 1; i < pointCount; i++)
		//	{
		//		float segmentLength = Lengths[i];
		//		Vector2 segmentNormal = Normals[i];
		//		dist = PointToSegmentDistance(point, Waypoints[i - 1], Waypoints[i], segmentNormal, segmentLength, out closestPoint, out segmentProj);
		//		if (dist < minDist)
		//		{
		//			minDist = dist;
		//			pointOnPath = closestPoint;
		//			output.Tangent = segmentNormal;
		//		}
		//	}

		//	output.Outside = Vector2.Distance(pointOnPath, point) - PATH_RADIUS;

		//	return pointOnPath;
		//}

		//public float PointToPathDistance(Vector2 point)
		//{
		//	float minDistance = float.MaxValue;
		//	float totalSegmentLength = 0f;
		//	float pathDistance = 0f;

		//	int pointCount = Waypoints.Length;
		//	for (int i = 1; i < pointCount; i++)
		//	{
		//		Vector2 closestPoint;
		//		float segmentProj;
		//		float distance = PointToSegmentDistance(point, Waypoints[i - 1], Waypoints[i], Normals[i], Lengths[i], out closestPoint, out segmentProj);
		//		if (distance < minDistance)
		//		{
		//			minDistance = distance;
		//			pathDistance = totalSegmentLength + segmentProj;
		//		}
		//		totalSegmentLength += Lengths[i];
		//	}

		//	return pathDistance;
		//}

		//public Vector2 PointOnPath(float pathDistance)
		//{
		//	float remaining = pathDistance;
		//	int pointCount = Waypoints.Length;

		//	if (pathDistance < 0f)
		//	{
		//		return Waypoints[0];
		//	}
		//	if (pathDistance >= TotalPathLenght)
		//	{
		//		return Waypoints[pointCount - 1];
		//	}

		//	Vector2 result = Vector2.Zero;

		//	for (int i = 1; i < pointCount; i++)
		//	{
		//		if (Lengths[i] < remaining)
		//		{
		//			remaining -= Lengths[i];
		//		}
		//		else
		//		{
		//			float ratio = remaining / Lengths[i];
		//			result = Vector2.Lerp(Waypoints[i - 1], Waypoints[i], ratio);
		//			break;
		//		}
		//	}

		//	return result;
		//}

		//private float PointToSegmentDistance(Vector2 point, Vector2 v1, Vector2 v2, Vector2 segmentNormal, float segmentLength, out Vector2 closestPoint, out float segmentProj)
		//{
		//	Vector2 local = point - v1;
		//	segmentProj = Vector2.Dot(segmentNormal, local);

		//	if (segmentProj < 0)
		//	{
		//		closestPoint = v1;
		//		segmentProj = 0f;
		//		return Vector2.Distance(point, v1);
		//	}
		//	if (segmentProj > segmentLength)
		//	{
		//		closestPoint = v2;
		//		segmentProj = 0f;
		//		return Vector2.Distance(point, v2);
		//	}

		//	closestPoint = segmentNormal * segmentProj;
		//	closestPoint += v1;

		//	return Vector2.Distance(point, closestPoint);
		//}

		public void Render(SpriteBatch spriteBatch)
		{
			int waypointCount = Waypoints.Length;
			Vector2 origin = Vector2.One * (Application.CELL_SIZE / 2f);

			for (int i = 1; i < waypointCount; ++i)
			{
				Vector2 n1 = Waypoints[i - 1];
				Vector2 n2 = Waypoints[i];

				Vector2 n1Pos = new Vector2(n1.X, n1.Y);
				Vector2 n2Pos = new Vector2(n2.X, n2.Y);

				spriteBatch.DrawLine(n1Pos, n2Pos, Color.HotPink, 1f);
			}
		}
	}
}
