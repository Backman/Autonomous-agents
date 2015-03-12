using Microsoft.Xna.Framework;
using SharpSteer2.Pathway;

namespace SharpSteer2.WinDemo.PlugIns.MeshPathFollowing
{
    public class PathWalker
        :SimpleVehicle
    {
        private readonly IPathway _path;

        public override float MaxForce { get { return 32; } }
        public override float MaxSpeed { get { return 4; } }

        public PathWalker(IPathway path, IAnnotationService annotation)
            :base(annotation)
        {
            _path = path;
        }

        public void Update(float dt)
        {
            ApplySteeringForce(SteerToFollowPath(true, 3, _path), dt);

            annotation.VelocityAcceleration(this);
        }

        internal void Draw()
        {
            Drawing.DrawBasic2dCircularVehicle(this, Color.Gray);
        }
    }
}
