using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SharpSteer
{
	public abstract class BaseVehicle : IVehicle
	{
		private float _mass;
		private float _radius;
		private float _invMass;
		private float _maxForce;
		private float _maxSpeed;
		private Vector2 _velocity;
		private Vector2 _position;

		/// <summary>
		/// Velocity of the vehicle
		/// </summary>
		public Vector2 Velocity
		{
			get { return _velocity; }
			set { _velocity = value; }
		}

		/// <summary>
		/// Position of the vehicle
		/// </summary>
		public Vector2 Position
		{
			get { return _position; }
			set { _position = value; }
		}

		/// <summary>
		/// Direction of the vehicle
		/// </summary>
		public Vector2 Direction { get; set; }

		/// <summary>
		/// Side vector of the vehicle
		/// </summary>
		public Vector2 Side { get; set; }

		/// <summary>
		/// Mass of the vehicle
		/// </summary>
		public float Mass
		{
			get { return _mass; }
			set
			{
				_mass = value;
				if (_mass != 0f)
				{
					_invMass = 1f / _mass;
				}
			}
		}

		/// <summary>
		/// Radius of the vehicle
		/// </summary>
		public float Radius
		{
			get { return _radius; }
			set { _radius = value; }
		}

		/// <summary>
		/// The max force of the vehicle
		/// </summary>
		public float MaxForce
		{
			get { return _maxForce; }
			set { _maxForce = value; }
		}

		/// <summary>
		/// Max speed of the vehicle
		/// </summary>
		public float MaxVelocity
		{
			get { return _maxSpeed; }
			set { _maxSpeed = value; }
		}

		/// <summary>
		/// The Inverse mass of the vehicle
		/// </summary>
		public float InvMass
		{
			get { return _invMass; }
			private set { _invMass = value; }
		}
	}
}
