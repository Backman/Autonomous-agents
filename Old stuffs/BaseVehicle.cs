using Microsoft.Xna.Framework;

namespace SteerSharp
{
	public abstract class BaseVehicle
	{
		/// <summary>
		/// Position of the vehicle
		/// </summary>
		public abstract Vector2 Position { get; set; }

		/// <summary>
		/// Direction of the vehicle
		/// </summary>
		public abstract Vector2 Direction { get; set; }

		/// <summary>
		/// Mass of the vehicle
		/// </summary>
		public abstract float Mass { get; set; }

		/// <summary>
		/// Size of the bounding circle
		/// </summary>
		public abstract float Radius { get; set; }

		/// <summary>
		/// Velocity of the vehicle
		/// </summary>
		public abstract Vector2 Velocity { get; }

		/// <summary>
		/// Acceleration of the vehcicle
		/// </summary>
		public abstract float Acceleration { get; set; }

		/// <summary>
		/// Speed of the vehicle.
		/// May be faster than the magnitude of velcoity
		/// </summary>
		public abstract float Speed { get; set; }

		/// <summary>
		/// The maximum steering force this vehicle can apply
		/// </summary>
		public abstract float MaxForce { get; set; }

		/// <summary>
		/// The maximum speed this vehicle is allowed to move
		/// </summary>
		public abstract float MaxSpeed { get; set; }

		/// <summary>
		/// Predicts the vehicles position in the future
		/// </summary>
		/// <param name="predictionTime">The future time in seconds</param>
		/// <returns></returns>
		public abstract Vector2 PredictFuturePosition(float predictionTime);
	}
}
