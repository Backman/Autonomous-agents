using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SharpSteer
{
	public interface IVehicle
	{
		/// <summary>
		/// Velocity of the vehicle
		/// </summary>
		Vector2 Velocity { get; set; }

		/// <summary>
		/// Position of the vehicle
		/// </summary>
		Vector2 Position { get; set; }

		/// <summary>
		/// Position of the vehicle
		/// </summary>
		Vector2 Direction { get; set; }

		/// <summary>
		/// Position of the vehicle
		/// </summary>
		Vector2 Side { get; set; }

		/// <summary>
		/// Mass of the vehicle
		/// </summary>
		float Mass { get; set; }

		/// <summary>
		/// Radius of the vehicle
		/// </summary>
		float Radius { get; set; }

		/// <summary>
		/// The max force of the vehicle
		/// </summary>
		float MaxForce { get; set; }

		/// <summary>
		/// Max speed of the vehicle
		/// </summary>
		float MaxVelocity { get; set; }
		
		/// <summary>
		/// The Inverse mass of the vehicle
		/// </summary>
		float InvMass { get; }
	}
}
