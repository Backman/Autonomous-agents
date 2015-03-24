#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

using Bitmap = System.Drawing.Bitmap;

namespace Autonomous_Agents
{
	public enum FollowTechnique
	{
		FlowFieldFollowing,
		PathFollowing
	}

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		public static Random random = new Random();

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private Menu _menu;
		private FollowTechnique _followTechnique;
		private string _bitmapPath;
		private Bitmap _bitmap;
		private bool _shouldUpdate = false;

		private int _windowWidth = 1024;
		private int _windowHeight = 768;
		public int WindowWidth { get { return _windowWidth; } }
		public int WindowHeight { get { return _windowHeight; } }

		private int _cellSize = 16;

		private AStarGrid _aStarGrid;
		private AStarPath _astarPath;

		private FlowFieldGrid _flowField;

		private List<Agent> _agents = new List<Agent>();
		public List<Agent> Agents { get { return _agents; } }

		private MouseState _prevMouseState;

		public Game1()
			: base()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			Messenger.AddListener<FollowTechnique>("SetFollowTechnique", SetFollowTechnique);
			Messenger.AddListener<string>("SetMap", SetMap);
			Messenger.AddListener("OnWinFormsExit", OnWinFormsExit);
			Messenger.AddListener<int>("SetAgentCount", SetAgentCount);
			Messenger.AddListener<bool>("SetShouldUpdate", SetShouldUpdate);
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			graphics.PreferredBackBufferWidth = _windowWidth;
			graphics.PreferredBackBufferHeight = _windowHeight;
			graphics.ApplyChanges();

			IsMouseVisible = true;

			int width = _windowWidth / _cellSize;
			int height = _windowHeight / _cellSize;

			_aStarGrid = new AStarGrid(width, height, _cellSize);
			_flowField = new FlowFieldGrid(width, height, _cellSize);
			_flowField.ShowGrid = true;
			_menu = new Menu();

			_menu.Show();

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

			float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			MouseState mouseState = Mouse.GetState();
			if (mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
			{
				Vector2 mousePos = new Vector2(mouseState.Position.X, mouseState.Position.Y);

				_aStarGrid.RandomizeStarEndNode();
				_flowField.RandomizeStarEndNode();

				_flowField.EndNode = _flowField.GetNode(mousePos);

				_flowField.GenerateFlowField();

				_astarPath = AStarSolver.Solve(_aStarGrid);
			}

			if (_shouldUpdate)
			{
				foreach (Agent agent in _agents)
				{
					agent.Tick(this, elapsedTime);
				}
			}

			_prevMouseState = mouseState;

			base.Update(gameTime);
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

			//_aStarGrid.Render(spriteBatch, Color.Red);
			_flowField.Render(spriteBatch, Color.Red, Color.Green);

			if (_astarPath != null)
			{
				//_astarPath.Render(spriteBatch, Color.Pink);
			}

			foreach (Agent agent in _agents)
			{
				agent.Render(spriteBatch, Color.White);
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}

		#region Callbacks
		private void InitAgents(int count)
		{
			_agents.Clear();

			Vector2 startPos = new Vector2(random.Next(0, _windowWidth - 100), random.Next(0, _windowHeight - 100));

			float yPos = 0f;
			float xPos = 0f;
			float distBetween = 15f;

			for (int i = 0; i < count; ++i)
			{
				xPos++;
				if ((i % (count / 10)) == 0)
				{
					yPos += distBetween;
					xPos = 0f;
				}

				Vector2 pos = startPos + new Vector2(xPos * distBetween, yPos);

				Agent agent = new Agent(pos, 5f);
				_agents.Add(agent);
			}
		}

		private void OnWinFormsExit()
		{
			Exit();
		}

		private void SetFollowTechnique(FollowTechnique value)
		{
			_followTechnique = value;
		}

		private void SetMap(string map)
		{
			string path = "Maps/" + map;
			if (path != _bitmapPath)
			{
				try
				{
					_bitmap = Bitmap.FromFile(path) as Bitmap;
					_bitmapPath = path;
					_aStarGrid.CreateFromBitmap(_bitmap);
					_flowField.CreateFromBitmap(_bitmap);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}

		private void SetAgentCount(int count)
		{
			InitAgents(count);
		}

		private void SetShouldUpdate(bool value)
		{
			_shouldUpdate = value;
		}
		#endregion
	}
}
