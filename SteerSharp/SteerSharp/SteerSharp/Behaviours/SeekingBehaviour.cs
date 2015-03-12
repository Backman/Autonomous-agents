using Microsoft.Xna.Framework;
using SteerSharp.Helpers;


namespace SteerSharp.Behaviours
{
	public class SeekingBehaviour : Behaviour
	{
		public Vector2 Target { get; set; }

		public SeekingBehaviour(Vehicle vehicle)
			: base(vehicle)
		{
			
		}

		public SeekingBehaviour(Vehicle vehicle, Vector2 target)
			: base(vehicle)
		{
			Target = target;
		}

		public override Vector2 CalculateForce()
		{
			return this.Vehicle.SteerForSeek(Target, this.Vehicle.MaxSpeed);
		}
	}
}
