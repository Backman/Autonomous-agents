#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Bitmap = System.Drawing.Bitmap;
#endregion

namespace BachelorsProject
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

		public Vector2 Direction;
		public Vector2 Side;

		private SteeringManager _steeringManager;
		public Level Level;

		public AStarPath AStarPath;

		public Vector2 StartPosition;
		public Vector2 GoalPosition;
		public FlowFieldGrid FlowField;

		public bool ReachedGoal = false;

		public bool Tag = false;

		public Vector2 Center
		{
			get
			{
				return new Vector2(Position.X + (_radius ), Position.Y + (_radius));
			}
		}

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

		public Agent(Level level, Vector2 startPos, Vector2 goalPos, float radius, float maxVelocity = 100f, float maxForce = 50f)
		{
			_steeringManager = new SteeringManager(this);

			StartPosition = startPos;
			GoalPosition = goalPos;
			Position = startPos;
			Radius = radius;
			MaxVelocity = maxVelocity;
			MaxForce = maxForce * SteeringManager.STEERING_FORCE_TWEAKER;
			Mass = 1f;

			Level = level;

			if (Application.Settings.Technique == Application.NavigationTechnique.FlowField)
			{
				CreateFlowField(level.Bitmap);
			}
			else if (Application.Settings.Technique == Application.NavigationTechnique.AStar)
			{
				ConstructAStarPath(level.Bitmap);
			}
		}

		private void CreateFlowField(Bitmap bitmap)
		{
			FlowField = new FlowFieldGrid();
			FlowField.FromBitmap(bitmap);
			FlowField.StartNode = FlowField.GetNodeFromWorldPos((int)StartPosition.X, (int)StartPosition.Y);
			FlowField.GoalNode = FlowField.GetNodeFromWorldPos((int)GoalPosition.X, (int)GoalPosition.Y);
			FlowField.ConstructFlowField();
		}

		private void ConstructAStarPath(Bitmap bitmap)
		{
			AStarGrid aStarGrid = new AStarGrid();
			aStarGrid.FromBitmap(bitmap);
			var startNode = aStarGrid.GetNodeFromWorldPos((int)StartPosition.X, (int)StartPosition.Y);
			var goalNode = aStarGrid.GetNodeFromWorldPos((int)GoalPosition.X, (int)GoalPosition.Y);
			AStarPath = AStarSolver.Solve(startNode, goalNode, aStarGrid);
		}

		public void Reset()
		{
			ReachedGoal = false;
			Position = StartPosition;
			Velocity = Vector2.Zero;

			if (Application.Settings.Technique == Application.NavigationTechnique.FlowField)
			{
				if (FlowField == null)
				{
					CreateFlowField(Level.Bitmap);
				}
				AStarPath = null;
			}
			else if (Application.Settings.Technique == Application.NavigationTechnique.AStar)
			{
				if (AStarPath == null)
				{
					ConstructAStarPath(Level.Bitmap);
				}
				FlowField = null;
			}
		}

		public void Tick(float elapsedTime)
		{
			Vector2 steeringForce = _steeringManager.CalculatePrioritized(Application.Settings.Technique == Application.NavigationTechnique.FlowField);
			
			Vector2 clippedForce = steeringForce.TruncateLength(_maxForce);

			Vector2 acceleration = clippedForce / Mass;

			_velocity += acceleration * elapsedTime;
			_velocity = _velocity.TruncateLength(_maxSpeed);

			_position += _velocity * elapsedTime;

			if (_velocity.LengthSquared() > 0.000001f)
			{
				Direction = Vector2.Normalize(_velocity);

				Direction.Perpendicular(out Side);
			}

		}

		public Vector2 PredictFuturePosition(float predictionTime)
		{
			return _position + (_velocity * predictionTime);
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
			spriteBatch.DrawCircle(_position, _radius, 10, color);
			//spriteBatch.DrawLine(Position, Position + Direction * 10f, Color.Yellow);
			//spriteBatch.DrawLine(Position, Position + Side * 10f, Color.Green);
		}
	}
}
