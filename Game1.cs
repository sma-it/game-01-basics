using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        List<Support.Texture> Textures = new List<Support.Texture>();
        int selected = 0;

        int windowWidth = 1000;
        int windowHeight = 800;

        public Game1()
        {
            //graphics = new GraphicsDeviceManager(this);
            //graphics.PreferredBackBufferWidth = windowWidth;
            //graphics.PreferredBackBufferHeight = windowHeight;

            sGraphics = new GraphicsDeviceManager(this);
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sSpriteBatch = new SpriteBatch(GraphicsDevice);

            Textures.Add(new Support.Texture("balloon", new Vector2(10, 10), new Vector2(30, 30)));
            Textures.Add(new Support.Texture("balloon", new Vector2(200, 200), new Vector2(100, 100)));
            Textures.Add(new Support.Texture("balloon", new Vector2(50, 10), new Vector2(30, 30)));
            Textures.Add(new Support.Texture("balloon", new Vector2(50, 200), new Vector2(100, 100)));

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

            foreach (var key in Keyboard.GetState().GetPressedKeys())
            {
                if (key == Keys.Space) selected++;
                if (selected >= Textures.Count) selected = 0;
            }

            var position = new Vector2();
            var scale = new Vector2();

            if (Keyboard.GetState().IsKeyDown(Keys.W)) position.Y++;
            if (Keyboard.GetState().IsKeyDown(Keys.S)) position.Y--;
            if (Keyboard.GetState().IsKeyDown(Keys.A)) position.X--;
            if (Keyboard.GetState().IsKeyDown(Keys.D)) position.X++;

            if (Keyboard.GetState().IsKeyDown(Keys.Up)) scale.Y++;
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) scale.Y--;
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) scale.X--;
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) scale.X++;

            Textures[selected].Update(position, scale);
            // TODO: Add your update logic here
            //player.Update();

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
            Textures.ForEach(texture => texture.Draw());
            sSpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
