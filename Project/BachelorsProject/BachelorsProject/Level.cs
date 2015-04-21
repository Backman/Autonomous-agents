#region Using Statements
using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Common.PolygonManipulation;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Bitmap = System.Drawing.Bitmap;
#endregion

namespace BachelorsProject
{
	public class Level
	{
		private Application _application;

		public Agent[] Agents;
		public int AgentCount
		{
			get
			{
				if (Agents != null)
				{
					return Agents.Length;
				}
				return 0;
			}
		}

		private Texture2D _levelTexture;
		private float _scale;

		private List<PolygonShape> _polygons = new List<PolygonShape>();
		public List<PolygonShape> Polygons { get { return _polygons; } }

		public AStarGrid AStarGrid = new AStarGrid();

		private Vector2 _startPos;
		private Vector2 _goalPos;

		public Bitmap Bitmap;

		private DistanceInput _distanceInput = new DistanceInput();
		private DistanceProxy _distanceProxy1 = new DistanceProxy();
		private DistanceProxy _distanceProxy2 = new DistanceProxy();

		private Vector2 _origin = Vector2.Zero;

		public Level(Application app, Application.LevelSettings settings)
		{
			_application = app;

			_levelTexture = _application.Content.Load<Texture2D>(settings.Level);

			Texture2D tempTexture = _application.Content.Load<Texture2D>(settings.Level);
			System.IO.MemoryStream stream = new System.IO.MemoryStream();
			tempTexture.SaveAsPng(stream, tempTexture.Width, tempTexture.Height);
			stream.Seek(0, System.IO.SeekOrigin.Begin);
			Bitmap = Bitmap.FromStream(stream) as Bitmap;

			_startPos = settings.StartPosition;
			_goalPos = settings.GoalPosition;

			Transform t1 = new Transform();
			t1.SetIdentity();
			Transform t2 = new Transform();
			t2.SetIdentity();

			_distanceInput.TransformA = t1;
			_distanceInput.TransformB = t2;

			_distanceInput.ProxyA = _distanceProxy1;
			_distanceInput.ProxyB = _distanceProxy2;

			CreateFromTexture(_levelTexture);

			//try
			//{
			//	CreateFromBitmap(Bitmap);
			//}
			//catch (Exception e)
			//{
			//	Console.WriteLine(e.Message);
			//}

			//SetAgentCount(50);
		}

		public bool Tick(float elapsedTime)
		{
			int agentCount = AgentCount;
			int updatingAgent = 0;
			for (int i = 0; i < agentCount; i++)
			{
				var agent = Agents[i];
				if (!agent.ReachedGoal)
				{
					updatingAgent++;
					//if (Application.Settings.EnforcePenetrationConstraint)
					//{
					//	NoPenetrationConstraint(agent);
					//}

					HandleCollision(agent);
					agent.Tick(elapsedTime);
				}
			}
			return updatingAgent > 0;
		}

		private void HandleCollision(Agent agent)
		{
			Vector2 newPos = Vector2.Zero;

			float radius = agent.Radius;
			CircleShape c = new CircleShape(radius, 0f);
			c.Position = agent.Position;

			int polygonCount = _polygons.Count;
			for (int i = 0; i < polygonCount; i++)
			{
				var polygon = _polygons[i];

				_distanceInput.ProxyA.Set(c, 0);
				_distanceInput.ProxyB.Set(polygon, 0);

				DistanceOutput output;
				SimplexCache cache;
				Distance.ComputeDistance(out output, out cache, _distanceInput);

				if (output.Distance < radius)
				{
					float amountOverlap = radius - output.Distance;
					Vector2 dir = output.PointA - output.PointB;
					dir = dir.SafeNormalize();
					c.Position += dir * amountOverlap;
				}
			}

			agent.Position = c.Position;
		}

		private void NoPenetrationConstraint(Agent agent)
		{
			int agentCount = AgentCount;
			float agentRadius = agent.Radius;
			for (int i = 0; i < agentCount; i++)
			{
				var curAgent = Agents[i];
				if (agent != curAgent)
				{
					Vector2 toAgent = agent.Position - curAgent.Position;

					float dist = toAgent.Length();
					float radii = (curAgent.Radius + agentRadius);

					float amountOverlap = radii - dist;

					if (amountOverlap > 0f)
					{
						agent.Position += (toAgent / dist) * amountOverlap;
					}
				}
			}
		}

		public void CreateFromTexture(Texture2D levelTex)
		{
			_polygons.Clear();

			//Create an array to hold the data from the texture
			uint[] data = new uint[levelTex.Width * levelTex.Height];

			//Transfer the texture data to the array
			levelTex.GetData(data);

			//Find the vertices that makes up the outline of the shape in the texture
			List<Vertices> textureVertices = PolygonTools.CreatePolygon(data, levelTex.Width, 1f, 1, true, false);

			//The tool return vertices as they were found in the texture.
			//We need to find the real center (centroid) of the vertices for 2 reasons:
			for (int i = 0; i < textureVertices.Count; i++)
			{
				Vertices verts = textureVertices[i];
				//1. To translate the vertices so the polygon is centered around the centroid.
				//Vector2 centroid = verts.GetCentroid();
				Vector2 centroid = Vector2.One * (Application.CELL_SIZE / 2f);
				//verts.Translate(ref centroid);

				//2. To draw the texture the correct place.
				//_origin = -centroid;

				//We simplify the vertices found in the texture.
				verts = SimplifyTools.ReduceByDistance(verts, 4f);

				//Since it is a concave polygon, we need to partition it into several smaller convex polygons
				//verts.ForceCounterClockWise();
				List<Vertices> list = Triangulate.ConvexPartition(verts, TriangulationAlgorithm.Bayazit);
				//List<Vertices> list = new List<Vertices>();
				//if (isConvex)
				//{
				//	list = Triangulate.ConvexPartition(verts, TriangulationAlgorithm.Bayazit);
				//}
				//else
				//{
				//	list.Add(verts);
				//}

				_scale = 1f;
				
				//scale the vertices from graphics space to sim space
				Vector2 vertScale = new Vector2(ConvertUnits.ToSimUnits(Application.METER_IN_PIXELS)) * _scale;
				foreach (Vertices vertices in list)
				{
					vertices.Scale(ref vertScale);
				}

				//Create a single body with multiple fixtures
				Body compound = BodyFactory.CreateCompoundPolygon(_application.World, list, 0f, BodyType.Static);
				compound.BodyType = BodyType.Static;
				foreach (var fixture in compound.FixtureList)
				{
					PolygonShape shape = (PolygonShape)fixture.Shape;
					shape.Vertices.ForceCounterClockWise();
					_polygons.Add(shape);
				}
			}
		}

		public void SetAgentCount(int count)
		{
			Agents = new Agent[count];

			float yOffset = 0;
			float xOffset = 0;
			float radius = Application.Settings.AgentRadius;

			for (int i = 0; i < count; i++)
			{
				if (i % 19 == 0)
				{
					yOffset += Application.CELL_SIZE;
					xOffset = 0;
				}
				Vector2 startPos = new Vector2(_startPos.X + xOffset, _startPos.Y + yOffset);
				Vector2 goalPos = new Vector2(_goalPos.X + xOffset, _goalPos.Y + yOffset);
				Agents[i] = new Agent(this, startPos, goalPos, radius);
				xOffset += Application.CELL_SIZE;
			}
		}

		private void ResetAgents()
		{
			int agentCount = AgentCount;
			if (Application.Settings.AgentCount != agentCount)
			{
				SetAgentCount(Application.Settings.AgentCount);
			}
			else
			{
				for (int i = 0; i < agentCount; i++)
				{
					Agents[i].Reset();
				}
			}
		}

		private void CreateFromBitmap(Bitmap bitmap)
		{
			//AStarGrid = new AStarGrid();
			//AStarGrid.FromBitmap(bitmap);
		}

		public void Reset()
		{
			ResetAgents();
		}

		public void Render(SpriteBatch spriteBatch)
		{
			//if (Application.Settings.ShowGrid)
			//{
			//	AStarGrid.Render(spriteBatch);
			//}

			if (Application.Settings.ShowLevelPolygons)
			{
				foreach (PolygonShape p in _polygons)
				{
					int vertCount = p.Vertices.Count;
					Vector2 v1;
					Vector2 v2;
					for (int i = 0; i < vertCount - 1; i++)
					{
						v1 = p.Vertices[i];
						v2 = p.Vertices[i + 1];
						spriteBatch.DrawLine(v1, v2, Color.Green, 1f);
					}
					v1 = p.Vertices[vertCount - 1];
					v2 = p.Vertices[0];
					spriteBatch.DrawLine(v1, v2, Color.Green, 1f);
				}
			}
			else
			{
				spriteBatch.Draw(_levelTexture, ConvertUnits.ToSimUnits(Vector2.Zero), null, Color.White, 0f, _origin, 1f, SpriteEffects.None, 0f);
			}

			int agentCount = AgentCount;

			if (Application.Settings.ShowGoalPositions && !Application.Settings.ShowFlowFieldVectors && !Application.Settings.ShowAStarPath)
			{
				for (int i = 0; i < agentCount; i++)
				{
					var agent = Agents[i];
					spriteBatch.DrawCircle(agent.GoalPosition, agent.Radius / 2f, 10, Color.Khaki);
				}
			}

			for (int i = 0; i < agentCount; i++)
			{
				Agents[i].Render(spriteBatch, Color.AntiqueWhite);
			}

			//if (Application.Settings.ShowAStarPath)
			//{
			//	for (int i = 0; i < agentCount; i++)
			//	{
			//		if (Agents[i].AStarPath != null)
			//		{
			//			Agents[i].AStarPath.Render(spriteBatch);
			//		}
			//	}
			//}
		}

		public void TagAgentsWithinViewRange(Agent agent, float range)
		{
			TagNeighbors(agent, Agents, range);
		}

		private void TagNeighbors(Agent agent, Agent[] agents, float radius)
		{
			int agentCount = agents.Length;
			for (int i = 0; i < agentCount; i++)
			{
				var curAgent = agents[i];
				curAgent.Tag = false;

				if (curAgent != agent)
				{

					Vector2 offset = curAgent.Position - agent.Position;

					float distanceSqr = offset.LengthSquared();
					if (distanceSqr < radius * radius)
					{
						curAgent.Tag = true;
					}
				}
			}
		}

		public Agent TryGetAgent(Vector2 pos)
		{
			int agentCount = AgentCount;
			for (int i = 0; i < agentCount; i++)
			{
				var agent = Agents[i];

				float diffX = pos.X - agent.Position.X;
				float diffY = pos.Y - agent.Position.Y;

				if (((diffX * diffX) + (diffY * diffY)) < agent.Radius * agent.Radius)
				{
					return agent;
				}
			}

			return null;
		}
	}
}
