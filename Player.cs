using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    class Player
    {
        private Texture2D texture;
        private Vector2 position;

        public Player(ContentManager contentManager, Vector2 position)
        {
            texture = contentManager.Load<Texture2D>("balloon");
            this.position = position;
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) position.X++; 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 0.3f, SpriteEffects.None, 0f);
        }
    }
}
