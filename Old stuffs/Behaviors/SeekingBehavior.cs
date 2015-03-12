using Microsoft.Xna.Framework;

namespace SteerSharp.Behaviors
{
	public class SeekingBehavior : Behavior
	{
		public Vector2 Target { get; set; }

		public SeekingBehavior(BaseVehicle vehicle)
			: base(vehicle)
		{
			
		}

		public SeekingBehavior(BaseVehicle vehicle, Vector2 target)
			: base(vehicle)
		{
			Target = target;
		}

		public override Vector2 CalculateForce()
		{
			Vector2 desiredVel = Target - Vehicle.Position;
			Vector2 force = desiredVel - Vehicle.Velocity;
			return force;
		}
	}
}
