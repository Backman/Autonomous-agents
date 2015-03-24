using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Autonomous_Agents
{
	public class Agent
	{
		#region Members
		private float _mass;
		private float _radius;
		private float _invMass;
		private float _maxForce;
		private float _maxSpeed;
		private Vector2 _velocity;
		private Vector2 _position;

		private SteeringManager _steeringManager;
		#endregion

		#region Properties
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
		#endregion

		public Agent(Vector2 pos, float radius, float maxVelocity = 100f, float maxForce = 30f)
		{
			Position = pos;
			Radius = radius;
			MaxVelocity = maxVelocity;
			MaxForce = maxForce;
			Mass = 5f;

			_steeringManager = new SteeringManager(this);
		}

		public void Tick(Game1 game, float elapsedTime)
		{
			MouseState mouse = Mouse.GetState();
			Vector2 mousePos = new Vector2(mouse.X, mouse.Y);

			_steeringManager.Seek(mousePos);
			//_steeringManager.Flocking(game.Agents);
			_steeringManager.Seperation(game.Agents);

			_steeringManager.Tick(elapsedTime);

			DoWraparound(game.WindowWidth, game.WindowHeight);

			if (Velocity.LengthSquared() > 0.000001f)
			{
				Direction = Vector2.Normalize(Velocity);

				Side = Direction.Perpendicular();
			}
		}

		private void DoWraparound(int width, int height)
		{
			if (_position.X < -_radius) _position.X = width + _radius;
			if (_position.Y < -_radius) _position.Y = height + _radius;
			if (_position.X > width + _radius) _position.X = -_radius;
			if (_position.Y > height + _radius) _position.Y = -_radius;
		}

		public void Render(SpriteBatch spriteBatch, Color color)
		{
			spriteBatch.DrawCircle(Position, Radius, 10, color);
			//spriteBatch.DrawLine(Position, Position + Direction * 10f, Color.Yellow);
			//spriteBatch.DrawLine(Position, Position + Side * 10f, Color.Green);
		}
	}
}
