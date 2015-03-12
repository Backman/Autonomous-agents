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

namespace SharpSteer
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Agent agent;
		Grid grid;
		Path path;
		int gridSize = 80;
		int windowWidth = 1280;
		int windowHeight = 720;
		public static FlowField flowField;
		MouseState oldMouseState;
		private Node _lastNode;

		public Game1()
			: base()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
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
			graphics.PreferredBackBufferWidth = windowWidth;
			graphics.PreferredBackBufferHeight = windowHeight;
			graphics.ApplyChanges();


			IsMouseVisible = true;
			agent = new Agent(new Vector2(300f), 10f);
			flowField = new FlowField(Window.ClientBounds.Width, Window.ClientBounds.Height, 10);
			oldMouseState = Mouse.GetState();
			grid = Grid.CreateRandomGrid(windowWidth / gridSize, windowHeight / gridSize, gridSize);

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
			MouseState newMouseState = Mouse.GetState();

			if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
			{
				Vector2 mousePos = new Vector2(newMouseState.Position.X, newMouseState.Position.Y);

				//grid.ToggleWalkable(mousePos);

				path = AStar.Solve(grid.GetNode(Vector2.One * 10f), grid.GetNode(mousePos), grid);
			}

			//agent.Tick(elapsedTime);

			oldMouseState = newMouseState;

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
			//flowField.DrawField(spriteBatch);
			//agent.Draw(spriteBatch);
			grid.Draw(spriteBatch);

			if (path != null)
			{
				path.Draw(spriteBatch);
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
