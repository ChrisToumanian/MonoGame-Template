using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameTemplate
{
    class Transform
    {
        public Vector2 position = new Vector2(0, 0);
        public Vector2 origin = new Vector2(0, 0);
        public Vector2 heading = new Vector2(0, 0);
        public float rotation = 0;
        public float scale = 1;
        public int width = 16;
        public int height = 16;

        // Constructor
        public Transform(int x, int y, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.position = new Vector2(x, y);
            this.origin = new Vector2(width / 2, height / 2);
        }

        // Move by x & y amount
        public void Move(float x, float y)
        {
            position.X += x;
            position.Y += y;
            SetHeading(x, y);
        }

        // Move by Vector2
        public void Move(Vector2 movement)
        {
            position.X += movement.X;
            position.Y += movement.Y;
        }

        // Set position to x & y
        public void SetPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

        // Set position by Vector2
        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        // Set new heading
        public void SetHeading(float x, float y)
        {
            heading.X = x;
            heading.Y = y;
        }
    }
}
