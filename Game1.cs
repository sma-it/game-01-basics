using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MyGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager sGraphics;
        public static SpriteBatch sSpriteBatch;
        public static ContentManager sContent;
        public static Random sRandom = new Random();

        List<Cow> Cows = new List<Cow>();

        int windowWidth = 1000;
        int windowHeight = 800;
        bool spaceDown = false;

        public Game1()
        {
            sGraphics = new GraphicsDeviceManager(this);
            sGraphics.PreferredBackBufferWidth = windowWidth;
            sGraphics.PreferredBackBufferHeight = windowHeight;

            Content.RootDirectory = "Content";
            sContent = Content;
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

            base.Initialize();
            Support.Camera.Setup();
            Support.Font.Setup();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sSpriteBatch = new SpriteBatch(GraphicsDevice);

            Cows.Add(new Cow(new Vector2(0, 0), 0.3f));

            // TODO: use this.Content to load your game content here
            //player = new Player(this.Content, new Vector2(100, 100));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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

            if(!spaceDown && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Cows.Add(new Cow(new Vector2(0, 0), 0.3f));
                spaceDown = true;
            }
            if(spaceDown && Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                spaceDown = false;
            }

            Cows.ForEach((cow) => cow.Update());


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
            sSpriteBatch.Begin();
            Cows.ForEach(cow => cow.Draw());

            Support.Font.PrintStatus("First cow is at Location " + Cows[0].Position, Color.Black);
            Support.Font.PrintStatus2("Cow Count is: " + Cows.Count, Color.Pink);

            Support.Font.PrintStatusLine(Support.Camera.Min.ToString(), 2, Color.Purple);
            Support.Font.PrintStatusLine(Support.Camera.Max.ToString(), 3, Color.Purple);
            sSpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
