using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Cow : Support.Texture
    {
        public Vector2 PositionDelta;

        public Cow(Vector2 position, float size) : base("Cow", position, new Vector2(size))
        {
            PositionDelta.X = (float)(Game1.sRandom.NextDouble()) * 0.05f - 0.025f;
            PositionDelta.Y = (float)(Game1.sRandom.NextDouble()) * 0.05f - 0.025f;
            // If all cows must move equally fast, use the code below
            //PositionDelta.X = 0.025f;
            //PositionDelta = Vector2.Transform(PositionDelta, Matrix.CreateRotationZ((float)Game1.sRandom.NextDouble() * (float)Math.PI * 2));
        }

        public float Radius
        {
            get => size.X * 0.5f;
            set => size.X = size.Y = value * 2f;
        }

        public void Update()
        {
            var status = Support.Camera.GetCollision(this);
            switch (status)
            {
                case Support.CollisionStatus.Bottom:
                case Support.CollisionStatus.Top:
                    PositionDelta.Y *= -1;
                    break;
                case Support.CollisionStatus.Left:
                case Support.CollisionStatus.Right:
                    PositionDelta.X *= -1;
                    break;
            }
            Position += PositionDelta;
        }
    }
}
