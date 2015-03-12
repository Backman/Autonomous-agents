using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using SteerSharp.Helpers;

namespace SteerSharp.Behaviours
{
	public class FleeBehaviour : Behaviour
	{
		public Vector2 Target { get; set; }
		public float FleeDistance { get; set; }

		public FleeBehaviour(Vehicle vehicle, Vector2 target, float fleeDistance)
			: base(vehicle)
		{
			Target = target;
			FleeDistance = fleeDistance;
		}

		public override Vector2 CalculateForce()
		{
			return Vehicle.SteerForFlee(Target, Vehicle.MaxSpeed, FleeDistance);
		}
	}
}
