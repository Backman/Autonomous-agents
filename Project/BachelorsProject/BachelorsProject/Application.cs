#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using FarseerPhysics.Dynamics;
using FarseerPhysics;

using System.Diagnostics;
#endregion


namespace BachelorsProject
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Application : Game
	{
		public enum NavigationTechnique
		{
			FlowField,
			AStar
		}

		public struct DebugSettings
		{
			public bool ShowAStarPath;
			public bool ShowFlowFieldVectors;
			public bool ShowLevelPolygons;
			public bool EnforcePenetrationConstraint;
			public bool ShowGoalPositions;
			public NavigationTechnique Technique;
			public int AgentCount;
			public float AgentRadius;
		}

		public struct LevelSettings
		{
			public string Level;
			public Vector2 StartPosition;
			public Vector2 GoalPosition;

			public LevelSettings(string level, Vector2 startPos, Vector2 goalPos)
			{
				Level = level;
				StartPosition = startPos;
				GoalPosition = goalPos;
			}
		}

		public class MemoryUsageInfo
		{
			public long CurrentMemoryUsage = 0;
			public long HighestMemoryUsage = 0;
			public long LowestMemoryUsage = long.MaxValue;
			public long AverageMemoryUsage = 0;

			public long TotalMemoryUsage = 0;
			public long MemoryUsageSampleCount = 0;

			public void Update()
			{
				Process currentProcess = Process.GetCurrentProcess();

				CurrentMemoryUsage = currentProcess.PrivateMemorySize64;
				if (CurrentMemoryUsage > HighestMemoryUsage)
				{
					HighestMemoryUsage = CurrentMemoryUsage;
				}
				if (CurrentMemoryUsage < LowestMemoryUsage)
				{
					LowestMemoryUsage = CurrentMemoryUsage;
				}

				TotalMemoryUsage += CurrentMemoryUsage / 1000000;
				MemoryUsageSampleCount++;

				AverageMemoryUsage = TotalMemoryUsage / MemoryUsageSampleCount;
			}

			public void Reset()
			{
				AverageMemoryUsage = 0;
				MemoryUsageSampleCount = 0;
				TotalMemoryUsage = 0;
				CurrentMemoryUsage = 0;
				HighestMemoryUsage = 0;
				LowestMemoryUsage = long.MaxValue;
			}
		}

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public static Application Instance;

		public static int WINDOW_WIDTH = 1024;
		public static int WINDOW_HEIGHT = 768;
		public static int CELL_SIZE = 16;

		public static int GRID_WIDTH = WINDOW_WIDTH / CELL_SIZE;
		public static int GRID_HEIGHT = WINDOW_HEIGHT / CELL_SIZE;

		public static DebugSettings Settings;

		private ControlPanel _controlPanel;

		private LevelSettings[] _levelSettings = new LevelSettings[4];
		private Level _currentLevel;
		private int _currentLevelIndex = 0;

		public Agent SelectedAgent = null;

		private bool _doUpdate = false;

		public MemoryUsageInfo MemoryUsage;

		private MouseState _prevMouseState;

		public World World = new World(Vector2.Zero);

		public static float METER_IN_PIXELS = 24f;
		
		public Application()
			: base()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			Instance = this;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			Settings.Technique = NavigationTechnique.FlowField;
			Settings.AgentCount = 50;
			Settings.AgentRadius = 5f;
			//Settings.EnforcePenetrationConstraint = true;

			MemoryUsage = new MemoryUsageInfo();

			_controlPanel = new ControlPanel(this);
			_controlPanel.Show();

			// TODO: Add your initialization logic here
			graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
			graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
			ConvertUnits.SetDisplayUnitToSimUnitRatio(METER_IN_PIXELS);
			IsMouseVisible = true;
			graphics.ApplyChanges();

			_prevMouseState = Mouse.GetState();

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			_levelSettings[0] = new LevelSettings("envA", new Vector2(370, 650), new Vector2(370, 10));
			_levelSettings[1] = new LevelSettings("envB", new Vector2(350, 620), new Vector2(350, 10));
			_levelSettings[2] = new LevelSettings("envC", new Vector2(30, 485), new Vector2(580, 150));
			_levelSettings[3] = new LevelSettings("envD", new Vector2(350, 620), new Vector2(350, 10));

			_currentLevel = new Level(this, _levelSettings[0]);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
			
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();


			// TODO: Add your update logic here
			MouseState currMouseState = Mouse.GetState();
			float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (currMouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
			{
				Vector2 mousePos = new Vector2((float)currMouseState.X, (float)currMouseState.Y);

				var agent = _currentLevel.TryGetAgent(mousePos);
				if (agent != null)
				{
					SelectedAgent = agent;
				}
			}

			if (_doUpdate)
			{
				_doUpdate = _currentLevel.Tick(elapsedTime);

				MemoryUsage.Update();
			}

			_controlPanel.UpdateDebugSettings();
			_prevMouseState = currMouseState;

			World.ProcessChanges();
			

			base.Update(gameTime);
		}

		public void SaveDataToFile(string filePath)
		{
			string environment = string.Format("Environment: {0}", (_currentLevelIndex + 1));
			string agentCount = string.Format("Agent count: {0}", Settings.AgentCount);
			string averageMemory = string.Format("Average Memory Usage: {0} MB", MemoryUsage.AverageMemoryUsage);
			string highestMemory = string.Format("Highest Memory Usage: {0} MB", MemoryUsage.HighestMemoryUsage / 1000000);
			string lowestMemory = string.Format("Lowest Memory Usage: {0} MB", MemoryUsage.LowestMemoryUsage / 1000000);
			string technique = string.Format("Technique: {0}", Settings.Technique);

			string[] lines = { environment, agentCount, technique, averageMemory, highestMemory, lowestMemory, "\n" };
			System.IO.File.AppendAllLines(filePath, lines);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			spriteBatch.Begin();

			_currentLevel.Render(spriteBatch);

			if (SelectedAgent != null)
			{
				if (Settings.ShowAStarPath && SelectedAgent.AStarPath != null)
				{
					SelectedAgent.AStarPath.Render(spriteBatch);
				}
				if (Settings.ShowFlowFieldVectors && SelectedAgent.FlowField != null)
				{
					SelectedAgent.FlowField.Render(spriteBatch);
				}
			}
			
			spriteBatch.End();

			base.Draw(gameTime);
		}

		public void Start()
		{
			_doUpdate = true;
		}

		public void Reset()
		{
			MemoryUsage.Reset();
			_doUpdate = false;
			SelectedAgent = null;
			_currentLevel.Reset();
		}

		public void SetEnvironment(int envIndex)
		{
			if (_currentLevelIndex != envIndex)
			{
				_currentLevel = new Level(this, _levelSettings[envIndex]);
				_currentLevelIndex = envIndex;
			}
		}
	}
}
