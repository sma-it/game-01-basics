using Microsoft.Xna.Framework;

namespace MyGame.Support
{
    static public class Camera
    {
        static private float sScale;
        static private Point sOffset;

        static private Vector2 sFocus = Vector2.Zero;
        static private float sZoom = 1f;

        static public void Setup()
        {
            sOffset.X = Game1.sGraphics.PreferredBackBufferWidth / 2;
            sOffset.Y = Game1.sGraphics.PreferredBackBufferHeight / 2;
            sScale = sOffset.Y;
        }

        static public void Zoom(float delta)
        {
            sZoom += delta;
        }

        static public void Move(Vector2 delta)
        {
            sFocus.X -= delta.X;
            sFocus.Y += delta.Y;
        }

        static public Point ConvertToPosition(Vector2 value)
        {
            Point point = new Point();
            point.X = (int)(value.X * sScale * sZoom);
            point.Y = (int)(-value.Y * sScale * sZoom);
            return sOffset + point + ConvertToSize(sFocus);
        }

        static private Point ConvertToSize(Vector2 value)
        {
            Point point = new Point();
            point.X = (int)(value.X * sScale * sZoom);
            point.Y = (int)(value.Y * sScale * sZoom);
            return point;
        }

        static public Rectangle ScreenRectangle(Vector2 position, Vector2 size)
        {
            Point p = ConvertToPosition(position);
            Point s = ConvertToSize(size);

            return new Rectangle(p - new Point(s.X / 2, s.Y / 2), s);
        }
    }
}
