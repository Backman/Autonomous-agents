using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace SteerSharp.Behaviors
{
	public abstract class Behavior
	{
		#region Members
		private Vector2 _force = Vector2.Zero;
		private BaseVehicle _vehicle;
		private float _weight = 1f;
		#endregion

		#region Properties
		public BaseVehicle Vehicle { get { return _vehicle; } }
		public float Weight
		{
			get { return _weight; }
			set { _weight = value; }
		}
		#endregion

		public Behavior(BaseVehicle vehicle)
		{
			_vehicle = vehicle;
		}

		/// <summary>
		/// Calculate the force for this behavior
		/// </summary>
		/// <returns>The calculated force vector</returns>
		public abstract Vector2 CalculateForce();
	}
}
