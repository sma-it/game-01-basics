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

        List<Support.Texture> Textures = new List<Support.Texture>();
        int selected = 0;

        int windowWidth = 1000;
        int windowHeight = 800;
        bool screenToggle = false;

        Random random = new Random();

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

            Textures.Add(new Support.Texture("balloon", new Vector2(0, 0), new Vector2(0.3f, 0.3f)));
            Textures.Add(new Support.Texture("balloon", new Vector2(-0.5f, -0.5f), new Vector2(0.3f, 0.3f)));
            Textures.Add(new Support.Texture("balloon", new Vector2(0.7f, 0.7f), new Vector2(0.1f, 0.1f)));
            Textures.Add(new Support.Texture("balloon", new Vector2(-0.5f, 0.5f), new Vector2(0.4f, 0.2f)));

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

            if(!screenToggle && Keyboard.GetState().IsKeyDown(Keys.F11))
            {
                sGraphics.ToggleFullScreen();
                screenToggle = true;
            }

            if(screenToggle && Keyboard.GetState().IsKeyUp(Keys.F11))
            {
                screenToggle = false;
            }

            foreach (var key in Keyboard.GetState().GetPressedKeys())
            {
                if (key == Keys.Space) selected++;
                if (selected >= Textures.Count) selected = 0;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.N))
            {
                float size = 0.1f + (float)random.NextDouble() * 0.3f;
                Textures.Add(new Support.Texture(
                    "balloon", 
                    new Vector2(-1 + (float)random.NextDouble() * 2, -1 + (float)random.NextDouble() * 2), 
                    new Vector2(size)
                ));
            }

            var position = new Vector2();
            var scale = new Vector2();
            var cam = new Vector2();

            if (Keyboard.GetState().IsKeyDown(Keys.W)) position.Y += 0.01f;
            if (Keyboard.GetState().IsKeyDown(Keys.S)) position.Y -= 0.01f;
            if (Keyboard.GetState().IsKeyDown(Keys.A)) position.X -= 0.01f;
            if (Keyboard.GetState().IsKeyDown(Keys.D)) position.X += 0.01f;

            if (Keyboard.GetState().IsKeyDown(Keys.Up)) scale.Y += 0.01f;
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) scale.Y -= 0.01f;
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) scale.X -= 0.01f;
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) scale.X += 0.01f;

            if (Keyboard.GetState().IsKeyDown(Keys.I)) cam.Y += 0.01f;
            if (Keyboard.GetState().IsKeyDown(Keys.K)) cam.Y -= 0.01f;
            if (Keyboard.GetState().IsKeyDown(Keys.J)) cam.X -= 0.01f;
            if (Keyboard.GetState().IsKeyDown(Keys.L)) cam.X += 0.01f;
            Support.Camera.Move(cam);

            if (Keyboard.GetState().IsKeyDown(Keys.R)) Support.Camera.Zoom(0.01f);
            if (Keyboard.GetState().IsKeyDown(Keys.T)) Support.Camera.Zoom(-0.01f);

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

            Support.Font.PrintStatus("Selected object is: " + selected + " Location=" + Textures[selected].Position, Color.Black);
            Support.Font.PrintStatus2("Texture Count is: " + Textures.Count, Color.Pink);
            Support.Font.PrintAt(Textures[selected].Position, "Selected", Color.Red);

            Support.Font.PrintStatusLine("Line 2", 2, Color.Purple);
            Support.Font.PrintStatusLine("Line 3", 3, Color.Purple);
            sSpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
