// Copyright (c) 2002-2003, Sony Computer Entertainment America
// Copyright (c) 2002-2003, Craig Reynolds <craig_reynolds@playstation.sony.com>
// Copyright (C) 2007 Bjoern Graf <bjoern.graf@gmx.net>
// Copyright (C) 2007 Michael Coles <michael@digini.com>
// All rights reserved.
//
// This software is licensed as described in the file license.txt, which
// you should have received as part of this distribution. The terms
// are also available at http://www.codeplex.com/SharpSteer/Project/License.aspx.

using System;
using Microsoft.Xna.Framework;
using SharpSteer2.Helpers;

namespace SharpSteer2.WinDemo.PlugIns.MapDrive
{
	public class TerrainMap
	{
		public TerrainMap(Vector3 c, float x, float z, int r)
		{
			Center = c;
			XSize = x;
			ZSize = z;
			Resolution = r;
			_outsideValue = false;

			_map = new bool[Resolution * Resolution];
			for (int i = 0; i < Resolution * Resolution; i++)
			{
				_map[i] = false;
			}
		}

		// clear the map (to false)
		public void Clear()
		{
			for (int i = 0; i < Resolution; i++)
				for (int j = 0; j < Resolution; j++)
					SetMapBit(i, j, false);
		}

		// get and set a bit based on 2d integer map index
		public bool GetMapBit(int i, int j)
		{
			return _map[MapAddress(i, j)];
		}

		public void SetMapBit(int i, int j, bool value)
		{
			_map[MapAddress(i, j)] = value;
		}

		// get a value based on a position in 3d world space
	    private bool GetMapValue(Vector3 point)
		{
			Vector3 local = point - Center;
            local.Y = 0;
			Vector3 localXZ = local;

			float hxs = XSize / 2;
			float hzs = ZSize / 2;

			float x = localXZ.X;
			float z = localXZ.Z;

			bool isOut = (x > +hxs) || (x < -hxs) || (z > +hzs) || (z < -hzs);

			if (isOut)
			{
				return _outsideValue;
			}
			else
			{
                int i = (int)Utilities.RemapInterval(x, -hxs, hxs, 0.0f, Resolution);
                int j = (int)Utilities.RemapInterval(z, -hzs, hzs, 0.0f, Resolution);
				return GetMapBit(i, j);
			}
		}

		public void xxxDrawMap()
		{
			float xs = XSize / Resolution;
			float zs = ZSize / Resolution;
			Vector3 alongRow = new Vector3(xs, 0, 0);
			Vector3 nextRow = new Vector3(-XSize, 0, zs);
			Vector3 g = new Vector3((XSize - xs) / -2, 0, (ZSize - zs) / -2);
			g += Center;
			for (int j = 0; j < Resolution; j++)
			{
				for (int i = 0; i < Resolution; i++)
				{
					if (GetMapBit(i, j))
					{
						// spikes
						// Vector3 spikeTop (0, 5.0f, 0);
						// drawLine (g, g+spikeTop, gWhite);

						// squares
						const float rockHeight = 0;
						Vector3 v1 = new Vector3(+xs / 2, rockHeight, +zs / 2);
						Vector3 v2 = new Vector3(+xs / 2, rockHeight, -zs / 2);
						Vector3 v3 = new Vector3(-xs / 2, rockHeight, -zs / 2);
						Vector3 v4 = new Vector3(-xs / 2, rockHeight, +zs / 2);
						// Vector3 redRockColor (0.6f, 0.1f, 0.0f);
						Color orangeRockColor = new Color((byte)(255.0f * 0.5f), (byte)(255.0f * 0.2f), (byte)(255.0f * 0.0f));
						Drawing.DrawQuadrangle(g + v1, g + v2, g + v3, g + v4, orangeRockColor);

						// pyramids
						// Vector3 top (0, xs/2, 0);
						// Vector3 redRockColor (0.6f, 0.1f, 0.0f);
						// Vector3 orangeRockColor (0.5f, 0.2f, 0.0f);
						// drawTriangle (g+v1, g+v2, g+top, redRockColor);
						// drawTriangle (g+v2, g+v3, g+top, orangeRockColor);
						// drawTriangle (g+v3, g+v4, g+top, redRockColor);
						// drawTriangle (g+v4, g+v1, g+top, orangeRockColor);
					}
					g += alongRow;
				}
				g += nextRow;
			}
		}

		public float MinSpacing()
		{
			return Math.Min(XSize, ZSize) / Resolution;
		}

		// used to detect if vehicle body is on any obstacles
		public bool ScanLocalXZRectangle(ILocalSpaceBasis localSpace, float xMin, float xMax, float zMin, float zMax)
		{
			float spacing = MinSpacing() / 2;

			for (float x = xMin; x < xMax; x += spacing)
			{
				for (float z = zMin; z < zMax; z += spacing)
				{
					Vector3 sample = new Vector3(x, 0, z);
					Vector3 global = localSpace.GlobalizePosition(sample);
					if (GetMapValue(global)) return true;
				}
			}
			return false;
		}

		// Scans along a ray (directed line segment) on the XZ plane, sampling
		// the map for a "true" cell.  Returns the index of the first sample
		// that gets a "hit", or zero if no hits found.
		public int ScanXZray(Vector3 origin, Vector3 sampleSpacing, int sampleCount)
		{
			Vector3 samplePoint = origin;

			for (int i = 1; i <= sampleCount; i++)
			{
				samplePoint += sampleSpacing;
				if (GetMapValue(samplePoint)) return i;
			}

			return 0;
		}

		public int Cellwidth() { return Resolution; }  // xxx cwr
		public int Cellheight() { return Resolution; }  // xxx cwr
		public bool IsPassable(Vector3 point) { return !GetMapValue(point); }


		public Vector3 Center;
		public readonly float XSize;
		public readonly float ZSize;
		public readonly int Resolution;

	    private readonly bool _outsideValue;

		int MapAddress(int i, int j) { return i + (j * Resolution); }

	    readonly bool[] _map;
	}
}
