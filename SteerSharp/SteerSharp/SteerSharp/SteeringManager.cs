using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using SteerSharp.Behaviours;

namespace SteerSharp
{
	public class SteeringManager
	{
		public Vector2 SteeringForce { get; set; }
		public Vehicle Owner { get; private set; }

		private List<Behaviour> _behaviours = new List<Behaviour>();

		public SteeringManager(Vehicle owner)
		{
			Owner = owner;
			Reset();
		}

		public void Reset()
		{
			SteeringForce = Vector2.Zero;
			_behaviours.Clear();
		}

		public void AddBehaviour(Behaviour b)
		{
			_behaviours.Add(b);
		}

		public void Update()
		{
			foreach (Behaviour b in _behaviours)
			{
				SteeringForce += b.CalculateForce();
			}
		}
	}
}
