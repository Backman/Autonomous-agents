using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace SteerSharp.Behaviours
{
	public abstract class Behaviour
	{
		#region Members
		private Vector2 _force = Vector2.Zero;
		private Vehicle _vehicle;
		private float _weight = 1f;
		#endregion

		#region Properties
		public Vehicle Vehicle { get { return _vehicle; } }
		public float Weight
		{
			get { return _weight; }
			set { _weight = value; }
		}
		#endregion

		public Behaviour(Vehicle vehicle)
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
