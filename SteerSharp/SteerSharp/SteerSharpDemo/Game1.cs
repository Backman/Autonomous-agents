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

namespace SteerSharpDemo
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Agent _agent;
		SteerSharp.Behaviours.SeekingBehaviour seek;
		SteerSharp.Behaviours.FleeBehaviour flee;
		SteerSharp.Behaviours.ArriveBehaviour arrive;

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
			IsMouseVisible = true;
			_agent = new Agent(100f, 100f, 10f, 100f, 300f);
			
			seek = new SteerSharp.Behaviours.SeekingBehaviour(_agent, Vector2.Zero);
			flee = new SteerSharp.Behaviours.FleeBehaviour(_agent, Vector2.Zero, 50f);
			arrive = new SteerSharp.Behaviours.ArriveBehaviour(_agent, Vector2.Zero, 50f);
			//_agent.AddBehavior(seek);
			//_agent.AddBehavior(flee);
			_agent.AddBehavior(arrive);

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
			var mouse = Mouse.GetState();
			Vector2 mousePos = new Vector2(mouse.Position.X, mouse.Position.Y);
			seek.Target = mousePos;
			flee.Target = mousePos;
			arrive.Target = mousePos;

			_agent.Tick(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();

			// TODO: Add your drawing code here
			_agent.Draw(spriteBatch);
			
			base.Draw(gameTime);
			spriteBatch.End();
		}
	}
}
