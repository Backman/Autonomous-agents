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

namespace SharpSteer
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		int _windowWidth = 1024;
		int _windowHeight = 768;

		private Menu _menuWindow;
		private Grid _grid;
		private int _cellSize = 8;
		private Path _currentPath;

		private MouseState _prevMouseState;

		Texture2D map;

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
			graphics.PreferredBackBufferWidth = _windowWidth;
			graphics.PreferredBackBufferHeight = _windowHeight;
			graphics.ApplyChanges();


			IsMouseVisible = true;

			_menuWindow = new Menu();
			Messenger.AddListener<string>("OnLoadMapButton", OnLoadMap);
			Messenger.AddListener<bool>("OnShowGridToggle", OnShowGridToggle);
			Messenger.AddListener("OnExit", OnExit);
			
			_menuWindow.Show();

			_grid = new Grid(_windowWidth / _cellSize, _windowHeight / _cellSize, _cellSize);
			Bitmap bitmap = Bitmap.FromFile("Maps/Test.png") as Bitmap;
			_grid.CreateFromBitmap(bitmap);

			_prevMouseState = Mouse.GetState();

			base.Initialize();
		}

		private void OnLoadMap(string mapPath)
		{
			try
			{
				var file = System.IO.File.OpenRead("Maps/" + mapPath + ".png");
				map = Texture2D.FromStream(GraphicsDevice, file);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private void OnShowGridToggle(bool value)
		{
			_grid.ShowGrid(value);
		}

		private void OnExit()
		{
			Exit();
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
			Content.Unload();
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

				Node startNode = _grid.GetNode(Vector2.One * 10f);
				Node endNode = _grid.GetNode(mousePos);

				_currentPath = AStar.Solve(startNode, endNode, _grid);
			}
			

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

			_grid.Draw(spriteBatch, Color.Red);

			if (map != null)
			{
				spriteBatch.Draw(map, Vector2.Zero, Color.White);
			}

			if (_currentPath != null)
			{
				_currentPath.Draw(spriteBatch, Color.Black);
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
