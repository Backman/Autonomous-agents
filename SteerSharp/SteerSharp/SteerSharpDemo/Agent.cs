using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SteerSharp;
using SteerSharp.Behaviours;
using SteerSharp.Helpers;

namespace SteerSharpDemo
{
	public class Agent : Vehicle
	{
		private List<Behaviour> _Behaviours = new List<Behaviour>();

		public Agent(float x, float y, float radius, float maxForce, float maxSpeed)
		{
			Position = new Vector2(x, y);
			Radius = radius;
			MaxForce = maxForce;
			MaxSpeed = maxSpeed;
		}

		public Agent(Vector2 startPos, float radius, float maxForce, float maxSpeed)
		{
			Position = startPos;
			Radius = radius;
			MaxForce = maxForce;
			MaxSpeed = maxSpeed;
		}

		public void AddBehavior(Behaviour behavior)
		{
			_Behaviours.Add(behavior);
		}

		public void Tick(GameTime gameTime)
		{
			float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			Vector2 steeringForce = Vector2.Zero;

			Vector2 acceleration = steeringForce / Mass;

			Vector2 newVel = Velocity + acceleration * elapsedTime;

			//Vector2 steeringForce = Vector2.Zero;

			//foreach (Behaviour b in _Behaviours)
			//{
			//	steeringForce += b.CalculateForce();
			//}

			//steeringForce = steeringForce.Truncate(MaxForce);

			//Vector2 acceleration = steeringForce / Mass;
			
			//Vector2 newVel = Velocity + acceleration * elapsedTime;
			//newVel = newVel.Truncate(MaxSpeed);

			//Speed = newVel.Length();

			//Position += newVel * elapsedTime;

			//if (newVel.LengthSquared() > 0.00000001f)
			//{
			//	Direction = Vector2.Normalize(newVel);

			//	Side = Direction.Perpendicular();
			//}
		}
		
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawCircle(Position, Radius, 10, Color.Red);
			spriteBatch.DrawLine(Position, Position + Direction * 10f, Color.Yellow);
			spriteBatch.DrawLine(Position, Position + Side * 10f, Color.Green);
		}
	}
}
