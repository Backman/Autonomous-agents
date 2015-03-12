//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;

//namespace SharpSteer2.Pathway
//{
//    /// <summary>
//    /// A pathway made out of triangular segments
//    /// </summary>
//    public class TrianglePathway
//        :BasePathway
//    {
//        private readonly TriangleData[] _path;
//        private readonly bool _cyclic;
//        private readonly float _totalPathLength;

//        public TrianglePathway(IEnumerable<TriangleData> path, bool cyclic = false)
//        {
//            _path = path.ToArray();
//            _cyclic = cyclic;
//            for (int i = 0; i < _path.Length; i++)
//            {
//                var aIndex = i;
//                var a = _path[aIndex];
//                var bIndex = cyclic ? ((i + 1) % _path.Length) : Math.Min(i, _path.Length - 1);
//                var b = _path[bIndex];

//                _path[aIndex].VectorToNextTriangle = b.Center - a.Center;
//                _path[aIndex].Length = a.VectorToNextTriangle.Length();
//                _path[aIndex].Tangent = a.VectorToNextTriangle / a.Length;
//                _totalPathLength += a.Length;
//            }
//        }

//        public override Vector3 MapPointToPath(Vector3 point, out Vector3 tangent, out float outside)
//        {
//            int index;
//            return MapPointToPath(point, out tangent, out outside, out index);
//        }

//        private Vector3 MapPointToPath(Vector3 point, out Vector3 tangent, out float outside, out int segmentIndex)
//        {
//            TriangleData? closest = null;
//            float distanceSqr = float.PositiveInfinity;
//            Vector3 closestPoint= Vector3.Zero;
//            bool inside = false;
//            segmentIndex = -1;

//            for (int i = 0; i < _path.Length; i++)
//            {
//                var triangleData = _path[i];

//                bool isInside;
//                var p = ClosestPointOnTriangle(triangleData, point, out isInside);

//                var normal = (point - p);
//                var dSqr = normal.LengthSquared();

//                if (dSqr < distanceSqr)
//                {
//                    distanceSqr = dSqr;
//                    closestPoint = p;
//                    closest = triangleData;
//                    inside = isInside;
//                    segmentIndex = i;
//                }
//            }

//            Debug.Assert(closest != null);
//            tangent = closest.Value.Tangent;
//            outside = (float) Math.Sqrt(distanceSqr) * (inside ? -1 : 1);
//            return closestPoint;
//        }

//        public override Vector3 MapPathDistanceToPoint(float pathDistance)
//        {
//            // clip or wrap given path distance according to cyclic flag
//            if (_cyclic)
//                pathDistance = pathDistance % _totalPathLength;
//            else
//            {
//                if (pathDistance < 0)
//                    return _path[0].Center;
//                if (pathDistance >= _totalPathLength)
//                    return _path[_path.Length - 1].Center;
//            }

//            // step through segments, subtracting off segment lengths until
//            // locating the segment that contains the original pathDistance.
//            // Interpolate along that segment to find 3d point value to return.
//            Vector3 result = Vector3.Zero;
//            for (int i = 1; i < _path.Length; i++)
//            {
//                var segment = _path[i];
//                if (segment.Length < pathDistance)
//                {
//                    pathDistance -= segment.Length;
//                }
//                else
//                {
//                    float ratio = pathDistance / segment.Length;
//                    result = Vector3.Lerp(segment.Center, segment.Center + segment.VectorToNextTriangle, ratio);
//                    break;
//                }
//            }
//            return result;
//        }

//        public override float MapPointToPathDistance(Vector3 point)
//        {
//            Vector3 tangent;
//            float outside;
//            int index;
//            MapPointToPath(point, out tangent, out outside, out index);

//            float accumulatedLength = 0;
//            for (int i = 0; i < index - 1; i++)
//                accumulatedLength += _path[i].Length;

//            return accumulatedLength;
//        }

//        public struct TriangleData
//        {
//            internal readonly Vector3 A;

//            internal readonly Vector3 Edge0;
//            internal readonly Vector3 Edge1;

//            internal Vector3 VectorToNextTriangle;
//            internal float Length;
//            internal Vector3 Tangent;
//            internal readonly Vector3 Center;

//            internal readonly float Edge0LengthSquared;
//            internal readonly float Edge0DotEdge1;
//            internal readonly float Edge1LengthSquared;

//            internal readonly float Determinant;

//            public TriangleData(Vector3 a, Vector3 b, Vector3 c)
//            {
//                A = a;

//                Center = (a + b + c) / 3f;

//                VectorToNextTriangle = Vector3.Zero;
//                Tangent = Vector3.Zero;
//                Length = 0;

//                Edge0 = b - a;
//                Edge1 = c - a;

//                Edge0LengthSquared = Vector3.Dot(Edge0, Edge0);
//                Edge0DotEdge1 = Vector3.Dot(Edge0, Edge1);
//                Edge1LengthSquared = Vector3.Dot(Edge1, Edge1);

//                Determinant = Edge0LengthSquared * Edge1LengthSquared - Edge0DotEdge1 * Edge0DotEdge1;
//            }
//        }

//        internal static Vector3 ClosestPointOnTriangle(TriangleData triangle, Vector3 sourcePosition, out bool inside)
//        {
//            float a, b;
//            return ClosestPointOnTriangle(triangle, sourcePosition, out a, out b, out inside);
//        }

//        internal static Vector3 ClosestPointOnTriangle(TriangleData triangle, Vector3 sourcePosition, out float edge0Distance, out float edge1Distance, out bool inside)
//        {
//            Vector3 v0 = triangle.A - sourcePosition;

//            float a = triangle.Edge0LengthSquared;
//            float b = triangle.Edge0DotEdge1;
//            float c = triangle.Edge1LengthSquared;
//            float d = Vector3.Dot(triangle.Edge0, v0);
//            float e = Vector3.Dot(triangle.Edge1, v0);

//            float det = triangle.Determinant;
//            float s = b * e - c * d;
//            float t = b * d - a * e;

//            inside = false;
//            if (s + t < det)
//            {
//                if (s < 0)
//                {
//                    if (t < 0)
//                    {
//                        if (d < 0)
//                        {
//                            s = MathHelper.Clamp(-d / a, 0, 1);
//                            t = 0;
//                        }
//                        else
//                        {
//                            s = 0;
//                            t = MathHelper.Clamp(-e / c, 0, 1);
//                        }
//                    }
//                    else
//                    {
//                        s = 0;
//                        t = MathHelper.Clamp(-e / c, 0, 1);
//                    }
//                }
//                else if (t < 0)
//                {
//                    s = MathHelper.Clamp(-d / a, 0, 1);
//                    t = 0;
//                }
//                else
//                {
//                    float invDet = 1 / det;
//                    s *= invDet;
//                    t *= invDet;
//                    inside = true;
//                }
//            }
//            else
//            {
//                if (s < 0)
//                {
//                    float tmp0 = b + d;
//                    float tmp1 = c + e;
//                    if (tmp1 > tmp0)
//                    {
//                        float numer = tmp1 - tmp0;
//                        float denom = a - 2 * b + c;
//                        s = MathHelper.Clamp(numer / denom, 0, 1);
//                        t = 1 -s;
//                    }
//                    else
//                    {
//                        t = MathHelper.Clamp(-e/c, 0, 1);
//                        s = 0;
//                    }
//                }
//                else if (t < 0)
//                {
//                    if (a + d > b + e)
//                    {
//                        float numer = c + e - b - d;
//                        float denom = a - 2 * b + c;
//                        s = MathHelper.Clamp(numer / denom, 0, 1);
//                        t = 1 - s;
//                    }
//                    else
//                    {
//                        s = MathHelper.Clamp(-e / c, 0, 1);
//                        t = 0;
//                    }
//                }
//                else
//                {
//                    float numer = c+e-b-d;
//                    float denom = a-2*b+c;
//                    s = MathHelper.Clamp(numer / denom, 0, 1);
//                    t = 1 - s;
//                }
//            }

//            edge0Distance = s;
//            edge1Distance = t;
//            return triangle.A + s * triangle.Edge0 + t * triangle.Edge1;
//        }
//    }
//}
