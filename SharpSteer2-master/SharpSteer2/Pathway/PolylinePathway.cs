// Copyright (c) 2002-2003, Sony Computer Entertainment America
// Copyright (c) 2002-2003, Craig Reynolds <craig_reynolds@playstation.sony.com>
// Copyright (C) 2007 Bjoern Graf <bjoern.graf@gmx.net>
// Copyright (C) 2007 Michael Coles <michael@digini.com>
// All rights reserved.
//
// This software is licensed as described in the file license.txt, which
// you should have received as part of this distribution. The terms
// are also available at http://www.codeplex.com/SharpSteer/Project/License.aspx.

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SharpSteer2.Pathway
{
	/// <summary>
	/// PolylinePathway: a simple implementation of the Pathway protocol.  The path
	/// is a "polyline" a series of line segments between specified points.  A
	/// radius defines a volume for the path which is the union of a sphere at each
	/// point and a cylinder along each segment.
	/// </summary>
	public class PolylinePathway : IPathway
	{
	    public int PointCount { get; private set; }
	    public Vector3[] Points { get; private set; }
	    public float Radius { get; private set; }
	    public bool Cyclic { get; private set; }

	    private readonly float[] _lengths;
	    private readonly Vector3[] _tangents;

	    public float TotalPathLength { get; private set; }

		/// <summary>
        /// construct a PolylinePathway given the number of points (vertices),
        /// an array of points, and a path radius.
		/// </summary>
		/// <param name="points"></param>
		/// <param name="radius"></param>
		/// <param name="cyclic"></param>
        public PolylinePathway(IList<Vector3> points, float radius, bool cyclic)
		{
            // set data members, allocate arrays
            Radius = radius;
            Cyclic = cyclic;
            PointCount = points.Count;
            TotalPathLength = 0;
            if (Cyclic)
                PointCount++;
            _lengths = new float[PointCount];
            Points = new Vector3[PointCount];
            _tangents = new Vector3[PointCount];

            // loop over all points
            for (int i = 0; i < PointCount; i++)
            {
                // copy in point locations, closing cycle when appropriate
                bool closeCycle = Cyclic && (i == PointCount - 1);
                int j = closeCycle ? 0 : i;
                Points[i] = points[j];

                // for the end of each segment
                if (i > 0)
                {
                    // compute the segment length
                    _tangents[i] = Points[i] - Points[i - 1];
                    _lengths[i] = _tangents[i].Length();

                    // find the normalized vector parallel to the segment
                    _tangents[i] *= 1 / _lengths[i];

                    // keep running total of segment lengths
                    TotalPathLength += _lengths[i];
                }
            }
		}

        public Vector3 MapPointToPath(Vector3 point, out Vector3 tangent, out float outside)
		{
            float minDistance = float.MaxValue;
            Vector3 onPath = Vector3.Zero;
			tangent = Vector3.Zero;

			// loop over all segments, find the one nearest to the given point
			for (int i = 1; i < PointCount; i++)
			{
			    Vector3 chosen;
			    float segmentProjection;
                float d = PointToSegmentDistance(point, Points[i - 1], Points[i], _tangents[i], _lengths[i], out chosen, out segmentProjection);
				if (d < minDistance)
				{
					minDistance = d;
                    onPath = chosen;
                    tangent = _tangents[i];
				}
			}

			// measure how far original point is outside the Pathway's "tube"
			outside = Vector3.Distance(onPath, point) - Radius;

			// return point on path
			return onPath;
		}

        public float MapPointToPathDistance(Vector3 point)
		{
            float minDistance = float.MaxValue;
			float segmentLengthTotal = 0;
			float pathDistance = 0;

			for (int i = 1; i < PointCount; i++)
			{
			    Vector3 chosen;
			    float segmentProjection;
                float d = PointToSegmentDistance(point, Points[i - 1], Points[i], _tangents[i], _lengths[i], out chosen, out segmentProjection);
				if (d < minDistance)
				{
					minDistance = d;
                    pathDistance = segmentLengthTotal + segmentProjection;
				}
                segmentLengthTotal += _lengths[i];
			}

			// return distance along path of onPath point
			return pathDistance;
		}

        public Vector3 MapPathDistanceToPoint(float pathDistance)
		{
			// clip or wrap given path distance according to cyclic flag
			float remaining = pathDistance;
			if (Cyclic)
			{
				remaining = pathDistance % TotalPathLength;
			}
			else
			{
				if (pathDistance < 0) return Points[0];
				if (pathDistance >= TotalPathLength) return Points[PointCount - 1];
			}

			// step through segments, subtracting off segment lengths until
			// locating the segment that contains the original pathDistance.
			// Interpolate along that segment to find 3d point value to return.
			Vector3 result = Vector3.Zero;
			for (int i = 1; i < PointCount; i++)
			{
                if (_lengths[i] < remaining)
				{
                    remaining -= _lengths[i];
				}
				else
				{
                    float ratio = remaining / _lengths[i];
                    result = Vector3.Lerp(Points[i - 1], Points[i], ratio);
					break;
				}
			}
			return result;
		}

	    private static float PointToSegmentDistance(Vector3 point, Vector3 ep0, Vector3 ep1, Vector3 segmentTangent, float segmentLength, out Vector3 chosen, out float segmentProjection)
		{
			// convert the test point to be "local" to ep0
			Vector3 local = point - ep0;

			// find the projection of "local" onto "tangent"
            segmentProjection = Vector3.Dot(segmentTangent, local);

			// handle boundary cases: when projection is not on segment, the
			// nearest point is one of the endpoints of the segment
			if (segmentProjection < 0)
			{
				chosen = ep0;
				segmentProjection = 0;
				return Vector3.Distance(point, ep0);
			}
            if (segmentProjection > segmentLength)
			{
				chosen = ep1;
                segmentProjection = segmentLength;
				return Vector3.Distance(point, ep1);
			}

			// otherwise nearest point is projection point on segment
            chosen = segmentTangent * segmentProjection;
			chosen += ep0;
			return Vector3.Distance(point, chosen);
		}
	}
}
