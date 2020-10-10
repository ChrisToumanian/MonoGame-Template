using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameTemplate
{
    class Geometry
    {
        private static Random random = new Random();

        public static float RandomFloat(float min, float max)
        {
            return (float) random.NextDouble() * (max - min) + min;
        }

        public static int RandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public static float GetDistance(Vector2 a, Vector2 b)
        {
            return (float) Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        public static bool IsWithin(Vector2 a, Vector2 b, float distance)
        {
            if (((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y)) < distance * distance)
            {
                return true;
            }
            return false;
        }

        public static Vector2 GetHeading(Vector2 b, Vector2 a)
        {
            return b - a;
        }

        public static Vector2 GetHeading(Rectangle a, Rectangle b)
        {
            return new Vector2(b.Center.X, b.Center.Y) - new Vector2(a.Center.X, a.Center.Y);
        }

        public static bool IsOverlapping(Rectangle a, Rectangle b)
        {
            if (a.Intersects(b))
            {
                return true;
            }
            return false;
        }

        public static bool IsOver(Vector2 a, Rectangle b)
        {
            if (a.X > b.X && a.X < b.X + b.Width && a.Y > b.Y && a.Y < b.Y + b.Height)
            {
                return true;
            }
            return false;
        }

        public static bool IsOver(Vector2 a, Rectangle b, int buffer)
        {
            if (a.X > b.X - buffer && a.X < b.X + buffer + b.Width && a.Y > b.Y - buffer && a.Y < b.Y + buffer + b.Height)
            {
                return true;
            }
            return false;
        }

        public static Vector2 MoveOff(Rectangle c, Rectangle b)
        {
            Vector2 move = new Vector2(0, 0);
            Rectangle a = new Rectangle(c.X, c.Y, c.Width, c.Height);
            int deltaLeft = 0;
            int deltaRight = 0;
            int deltaUp = 0;
            int deltaDown = 0;

            deltaLeft = Math.Abs(b.Left - a.Right);
            deltaRight = Math.Abs(b.Right - a.Left);
            deltaUp = Math.Abs(b.Top - a.Bottom);
            deltaDown = Math.Abs(b.Bottom - a.Top);

            if (deltaLeft < deltaRight && deltaLeft < deltaUp && deltaLeft < deltaDown)
            {
                move.X = -deltaLeft;
            }
            else if (deltaRight < deltaLeft && deltaRight < deltaUp && deltaRight < deltaDown)
            {
                move.X = deltaRight;
            }
            else if (deltaDown < deltaUp && deltaDown < deltaRight && deltaDown < deltaLeft)
            {
                move.Y = deltaDown;
            }
            else if (deltaUp < deltaDown && deltaUp < deltaRight && deltaUp < deltaLeft)
            {
                move.Y = -deltaUp;
            }

            return move;
        }
    }
}
