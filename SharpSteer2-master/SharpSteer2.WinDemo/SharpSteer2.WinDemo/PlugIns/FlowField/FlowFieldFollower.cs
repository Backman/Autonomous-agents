﻿
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SharpSteer2.Database;
using SharpSteer2.Helpers;
using SharpSteer2.WinDemo.PlugIns.Ctf;

namespace SharpSteer2.WinDemo.PlugIns.FlowField
{
    public class FlowFieldFollower
        : SimpleVehicle
    {
        private readonly FlowFieldPlugIn _plugin;
        public Trail Trail;

        private readonly ITokenForProximityDatabase<IVehicle> _proximityToken;

        public override float MaxSpeed { get { return 2; } }
        public override float MaxForce { get { return 4; } }

        public FlowFieldFollower(FlowFieldPlugIn plugin)
            : base(plugin.Annotations)
        {
            _plugin = plugin;

            _proximityToken = plugin.Database.AllocateToken(this);
        }

        public override void Reset()
        {
            base.Reset();

            RandomizeStartingPositionAndHeading();  // new starting position

            Trail = new Trail(7.5f, 600);
            Trail.Clear();
        }

        private void RandomizeStartingPositionAndHeading()
        {
            // randomize position on a ring between inner and outer radii
            // centered around the home base
            float rRadius = RandomHelpers.Random(10, 50);
            Vector3 randomOnRing = Vector3Helpers.RandomUnitVectorOnXZPlane() * rRadius;
            Position = (Globals.HomeBaseCenter + randomOnRing);
            RandomizeHeadingOnXZPlane();
        }

        public void Update(float currentTime, float elapsedTime)
        {
            ApplySteeringForce(SteeringForce(), elapsedTime);

            annotation.VelocityAcceleration(this);
            Trail.Record(currentTime, Position);

            _proximityToken.UpdateForNewPosition(Position);
        }

        private Vector3 SteeringForce()
        {
            if (Position.X > 25 || Position.X < -25 || Position.Z > 25 || Position.Z < -25)
                return SteerForSeek(Vector3.Zero);

            var flowField = SteerToFollowFlowField(_plugin.FlowField, _plugin.PredictionTime);

			const float caLeadTime = 3;

			// find all neighbors within maxRadius using proximity database
			// (radius is largest distance between vehicles traveling head-on
			// where a collision is possible within caLeadTime seconds.)
			float maxRadius = caLeadTime * MaxSpeed * 2;
            var neighbours = new List<IVehicle>();
			_proximityToken.FindNeighbors(Position, maxRadius, neighbours);

            if (neighbours.Count > 0)
            {
                var avoid = SteerToAvoidNeighbors(caLeadTime, neighbours) * 10;
                if (avoid != Vector3.Zero)
                    return avoid;
            }

            return flowField;
        }

        protected override Vector3 AdjustRawSteeringForce(Vector3 force, float deltaTime)
        {
            return base.AdjustRawSteeringForce(new Vector3(force.X, 0, force.Z), deltaTime);
        }

        internal void Draw()
        {
            Drawing.DrawBasic2dCircularVehicle(this, Color.GhostWhite);
            Trail.Draw(annotation);
        }
    }
}
