using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpSteer.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SharpSteer
{
	public class Agent : BaseVehicle
	{
		private SteeringManager _steeringManager;

		public Agent(Vector2 pos, float radius, float maxSpeed = 100f, float maxForce = 100f)
		{
			Position = pos;
			Radius = radius;
			MaxSpeed = maxSpeed;
			MaxForce = maxForce;
			Mass = 1f;

			_steeringManager = new SteeringManager(this);
		}

		public void Tick(float elapsedTime)
		{
			MouseState mouse = Mouse.GetState();
			Vector2 mousePos = new Vector2(mouse.Position.X, mouse.Position.Y);

			_steeringManager.Follow(Game1.flowField);

			_steeringManager.Update(elapsedTime);

			if (Velocity.LengthSquared() > 0.000001f)
			{
				Direction = Vector2.Normalize(Velocity);

				Side = Direction.Perpendicular();
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawCircle(Position, Radius, 10, Color.Red);
			spriteBatch.DrawLine(Position, Position + Direction * 10f, Color.Yellow);
			spriteBatch.DrawLine(Position, Position + Side * 10f, Color.Green);
		}
	}
}
