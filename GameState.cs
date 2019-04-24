using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class GameState
    {
        // the hero
        Player player;

        // the cows
        List<Cow> cows = new List<Cow>();
        float timeToNewCow;

        // the score
        int score = 0;
        bool gameOver = false;

        public GameState()
        {
            player = new Player(new Vector2(0.0f));

            // cows
            cows = new List<Cow>();
            timeToNewCow = 2.0f;
        }

        public void Update(GameTime gameTime)
        {
            if (gameOver && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                score = 0;
                cows.Clear();
                gameOver = false;
            }

            if (gameOver) return;

            if (cows.Count > 5)
            {
                gameOver = true;
            }

            timeToNewCow -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeToNewCow <= 0)
            {
                cows.Add(new Cow(
                    new Vector2(
                        -1 + (float)Game1.sRandom.NextDouble() * 2.0f,
                        -1 + (float)Game1.sRandom.NextDouble() * 2.0f
                    ),
                    0.1f
                ));
                timeToNewCow = 2;
            }
            cows.ForEach(cow => cow.Update(gameTime));
            player.Update(gameTime);

            for (int i = cows.Count - 1; i >= 0; i--)
            {
                if (cows[i].Collides(player))
                {
                    cows.RemoveAt(i);
                    score++;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (gameOver)
            {
                Support.Font.PrintAt(new Vector2(0f, 0f), "Game Over", Color.Red);
                Support.Font.PrintAt(new Vector2(0f, -0.1f), "Score: " + score, Color.Red);
                return;
            }

            cows.ForEach(cow => cow.Draw());
            player.Draw();

            Support.Font.PrintStatus("Score: " + score, Color.Red);
        }
    }
}
