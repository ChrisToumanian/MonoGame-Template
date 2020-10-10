using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTemplate.Scenery
{
    class GreenPlatform : GameObject
    {
        // Constructor
        public GreenPlatform()
        {
            name = "green platform";
            transform = new Transform(0, 0, 64, 48);

            // Sprite settings
            sprite = new Sprite(Assets.tiles, 1, 11, 16);
            sprite.depth = 0.0001f;
            sprite.bounds.Width = 64;
            sprite.bounds.Height = 48;

            // Collider settings
            collider = new Collider(this, 64, 32);
            collider.bounds.Y = -8;
            collider.enabled = true;
            collider.physical = true;
            collider.collision = true;
            collider.velocity.X = -0.5f;
        }

        // Update - called from Scene
        public override void Update()
        {
            // Move object by its velocity
            transform.position.X += collider.velocity.X;
            transform.position.Y += collider.velocity.Y;

            // Reverse direction if platform hits edge of level
            if (transform.position.X > scene.bounds.Width)
            {
                collider.velocity.X = -1;
            }
            if (transform.position.X < 0)
            {
                collider.velocity.X = 1;
            }
        }

        // Called from collider when collision occurs
        public override void OnCollision(GameObject gameObject)
        {
            if (collider.velocity.X > 0) // If platform is moving forward, move object forward
            {
                gameObject.transform.Move(2.15f, 0);
            }
            else // If platform is moving backward, move object backward
            {
                gameObject.transform.Move(-2.15f, 0);
            }
        }
    }
}
