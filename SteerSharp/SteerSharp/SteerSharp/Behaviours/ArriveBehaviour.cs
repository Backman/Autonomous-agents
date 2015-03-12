using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using SteerSharp.Helpers;

namespace SteerSharp.Behaviours
{
	public class ArriveBehaviour : Behaviour
	{
		public Vector2 Target { get; set; }
		public float SlowingDistance { get; set; }

		public ArriveBehaviour(Vehicle vehicle, Vector2 target, float slowingDistance)
			: base(vehicle)
		{
			Target = target;
			SlowingDistance = slowingDistance;
		}

		public override Vector2 CalculateForce()
		{
			return Vehicle.SteerForArrival(Target, Vehicle.MaxSpeed, SlowingDistance);
		}
	}
}
