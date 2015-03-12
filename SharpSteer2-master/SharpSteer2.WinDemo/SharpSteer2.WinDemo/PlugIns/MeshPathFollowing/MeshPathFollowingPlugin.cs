using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SharpSteer2.WinDemo.PlugIns.MeshPathFollowing
{
    public class MeshPathFollowingPlugin
        :PlugIn
    {
        private PolylinePathway _path;
        private PathWalker _walker;

        public override bool RequestInitialSelection
        {
            get
            {
                return true;
            }
        }

        public MeshPathFollowingPlugin(IAnnotationService annotations)
            :base(annotations)
        {
        }

        public override void Open()
        {
            GeneratePath();
            _walker = new PathWalker(_path, Annotations);
        }

        private void GeneratePath()
        {
            Random rand = new Random();

            List<Vector3> points = new List<Vector3>();
            for (int i = 0; i < 30; i++)
            {
                float f = MathHelper.TwoPi / 30f * i;
                float r = (float)rand.NextDouble() * 15 + 15;
                points.Add(new Vector3((float)Math.Sin(f) * r, 0, (float)Math.Cos(f) * r));
            }

            _path = new PolylinePathway(
                points.ToArray(), 0.5f, true
            );
        }

        public override void Update(float currentTime, float elapsedTime)
        {
            _walker.Update(elapsedTime);
        }

        public override void Redraw(float currentTime, float elapsedTime)
        {
            Demo.UpdateCamera(elapsedTime, _walker);
            _walker.Draw();

            for (int i = 0; i < _path.PointCount; i++)
                if (i > 0)
                    Drawing.DrawLine(_path.Points[i], _path.Points[i - 1], Color.Red);
        }

        public override void Close()
        {
            
        }

        public override string Name
        {
            get { return "Nav Mesh Path Following"; }
        }

        public override IEnumerable<IVehicle> Vehicles
        {
            get { yield return _walker; }
        }
    }
}
