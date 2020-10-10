using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameTemplate
{
    class Camera
    {
        public Rectangle bounds;
        public Scene scene;
        public Vector2 zoomInLimit;
        public Vector2 zoomOutLimit;
        public bool stayWithinSceneBounds = true;
        public Transform parent;

        // Constructor
        public Camera(Scene scene)
        {
            this.scene = scene;
            bounds = new Rectangle(0, 0, Game.gameWidth, Game.gameHeight);
            zoomInLimit = new Vector2(200, 140);
            zoomOutLimit = new Vector2(2046, 2046);
        }

        // Move camera by x & y amount
        public void Move(int x, int y)
        {
            bounds.X += x;
            bounds.Y += y;
            CheckSceneBounds();
        }

        // Update
        public void Update()
        {
            if (parent != null)
            {
                SetPosition((int) parent.position.X - (bounds.Width / 2), (int) parent.position.Y - (bounds.Height / 2));
            }
            CheckSceneBounds();
        }

        // Set Position by x & y
        public void SetPosition(int x, int y)
        {
            bounds.X = x;
            bounds.Y = y;
            CheckSceneBounds();
        }

        // Set Position by Vector2
        public void SetPosition(Vector2 position)
        {
            bounds.X = (int) position.X;
            bounds.Y = (int) position.Y;
            CheckSceneBounds();
        }

        // Zoom camera
        public void Zoom(float zoom)
        {
            float w = zoom * bounds.Width;
            float h = zoom * bounds.Height;
            if (w < zoomInLimit.X || w > zoomOutLimit.X || h < zoomInLimit.Y || h > zoomOutLimit.Y) return;
            Move((bounds.Width - (int) w) / 2, (bounds.Height - (int) h) / 2);
            bounds.Width = (int) w;
            bounds.Height = (int) h;
            CheckSceneBounds();
        }

        // Check scene boundaries
        public void CheckSceneBounds()
        {
            if (stayWithinSceneBounds)
            {
                if (bounds.X < 0) { bounds.X = 0; }
                if (bounds.Y < 0) { bounds.Y = 0; }
                if (bounds.X + bounds.Width > scene.bounds.Width) { bounds.X = scene.bounds.Width - bounds.Width; }
                if (bounds.Y + bounds.Height > scene.bounds.Height) { bounds.Y = scene.bounds.Height - bounds.Height; }
            }
        }
    }
}
