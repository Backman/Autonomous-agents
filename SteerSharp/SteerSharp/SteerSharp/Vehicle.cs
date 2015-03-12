using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace SteerSharp
{
	public abstract class Vehicle
	{
		#region Members
		private Vector2 _acceleration;
		private float _mass = 1f;
		private float _maxForce = 10f;
		private float _maxSpeed = 100f;
		private float _turnRate = 1f;
		#endregion

		#region Properties
		/// <summary>
		/// Position of the vehicle
		/// </summary>
		public Vector2 Position { get; set; }

		/// <summary>
		/// Direction of the vehicle
		/// </summary>
		public Vector2 Direction { get; set; }

		/// <summary>
		/// A vector perpendicular to the direction vector
		/// </summary>
		public Vector2 Side { get; set; }

		/// <summary>
		/// Mass of the vehicle
		/// </summary>
		public float Mass
		{
			get { return _mass; }
			set { _mass = value; }
		}

		/// <summary>
		/// Size of the bounding circle
		/// </summary>
		public float Radius { get; set; }

		/// <summary>
		/// Velocity of the vehicle
		/// </summary>
		public Vector2 Velocity { get; set; }

		/// <summary>
		/// Acceleration of the vehcicle
		/// </summary>
		public Vector2 Acceleration { get; set; }

		/// <summary>
		/// Speed of the vehicle.
		/// May be faster than the magnitude of velcoity
		/// </summary>
		//public float Speed { get; set; }

		/// <summary>
		/// The maximum steering force this vehicle can apply
		/// </summary>
		public float MaxForce
		{
			get { return _maxForce; }
			set { _maxForce = value; }
		}

		/// <summary>
		/// The maximum speed this vehicle is allowed to move
		/// </summary>
		public float MaxSpeed
		{
			get { return _maxSpeed; }
			set { _maxSpeed = value; }
		}

		public float MaxTurnRate
		{
			get { return _turnRate; }
			set { _turnRate = value; }
		}
		#endregion

		/// <summary>
		/// Predicts the vehicles position in the future
		/// </summary>
		/// <param name="predictionTime">The future time in seconds</param>
		/// <returns></returns>
		public Vector2 PredictFuturePosition(float predictionTime)
		{
			Vector2 futurePos = Vector2.Zero;

			return futurePos;
		}
		
	}
}
