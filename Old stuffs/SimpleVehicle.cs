using Microsoft.Xna.Framework;

namespace SteerSharp
{
	public class SimpleVehicle : BaseVehicle
	{
		#region Members
		private Vector2 _acceleration;
		#endregion

		#region Properties
		/// <summary>
		/// Position of the vehicle
		/// </summary>
		public override Vector2 Position { get; set; }

		/// <summary>
		/// Direction of the vehicle
		/// </summary>
		public override Vector2 Direction { get; set; }

		/// <summary>
		/// Mass of the vehicle
		/// </summary>
		public override float Mass { get; set; }

		/// <summary>
		/// Size of the bounding circle
		/// </summary>
		public override float Radius { get; set; }

		/// <summary>
		/// Velocity of the vehicle
		/// </summary>
		public override Vector2 Velocity { get { return Direction * Speed; } }

		/// <summary>
		/// Acceleration of the vehcicle
		/// </summary>
		public override float Acceleration { get; set; }

		/// <summary>
		/// Speed of the vehicle.
		/// May be faster than the magnitude of velcoity
		/// </summary>
		public override float Speed { get; set; }

		/// <summary>
		/// The maximum steering force this vehicle can apply
		/// </summary>
		public override float MaxForce
		{
			get { return 0.1f; }
		}

		/// <summary>
		/// The maximum speed this vehicle is allowed to move
		/// </summary>
		public override float MaxSpeed
		{
			get { return 1.0f; }
		}
		#endregion

		/// <summary>
		/// Predicts the vehicles position in the future
		/// </summary>
		/// <param name="predictionTime">The future time in seconds</param>
		/// <returns></returns>
		public override Vector2 PredictFuturePosition(float predictionTime)
		{
			Vector2 futurePos = Vector2.Zero;

			return futurePos;
		}
	}
}
