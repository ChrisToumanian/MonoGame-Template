using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTemplate.Entities
{
    class Mario : Entity
    {
        // Mario Settings
        float jumpPower = 9;

        // Animations
        Animation walking;
        Animation jumping;
        Animation ducking;

        public Mario()
        {
            name = "Mario";
            speed = 3f;

            // Sprite
            sprite = new Sprite(Assets.mario, 0, 0, 32);
            sprite.depth = 0.6f;

            // Transform
            transform = new Transform(0, 0, 32, 32);
            transform.origin.Y = 21;

            // Collider
            collider = new Collider(this, 10, 22);
            collider.enabled = true;
            collider.physical = true;
            collider.collision = true;
            collider.borderCollision = true;
            collider.gravity.Y = -0.25f;
            collider.maxVelocity = 5.8f;
            collider.mass = 3;

            // Animations
            walking = new Animation("walking", sprite, new Rectangle[5]{
                Sprite.GetBounds(1, 1, 32),
                Sprite.GetBounds(2, 1, 32),
                Sprite.GetBounds(3, 1, 32),
                Sprite.GetBounds(4, 1, 32),
                Sprite.GetBounds(5, 1, 32)
            });
            jumping = new Animation("jumping", sprite, new Rectangle[1]{
                Sprite.GetBounds(1, 3, 32)
            });
            ducking = new Animation("ducking", sprite, new Rectangle[1]{
                Sprite.GetBounds(2, 4, 32)
            });
            SetAnimation(walking);
        }

        // Update
        public override void Update()
        {
            // Check Controls
            Controls();

            grounded = CheckIfGrounded();
            if (grounded && collider.velocity.X != 0)
            {
                collider.velocity.X *= 0.01f;
            }

            // Destroy if bottom of level
            if (transform.position.Y > scene.bounds.Bottom - 18)
            {
                scene.Reload();
            }

            // Update Components
            collider.Update();
            animation.Update();
        }

        // Check Controller
        private void Controls()
        {
            // Movement
            float x = 0;
            float y = 0;

            if (!(animation.name == "ducking" && grounded))
            {
                if (Controller.IsKeyPressed(Keys.A)) // Move Left
                {
                    SetAnimation(walking);
                    sprite.FlipHorizontal(true);
                    x = -speed;
                }
                if (Controller.IsKeyPressed(Keys.D)) // Move Right
                {
                    SetAnimation(walking);
                    sprite.FlipHorizontal(false);
                    x = speed;
                }
            }

            if (Controller.IsKeyPressed(Keys.S)) // Ducking
            {
                SetAnimation(ducking);
                sprite.FlipHorizontal(false);
            }
            else if (animation.name == "ducking")
            {
                SetAnimation(walking);
            }

            collider.Move(x, y);

            // Idle Animation
            if (Controller.IsKeyReleased(Keys.W) && Controller.IsKeyReleased(Keys.A) && Controller.IsKeyReleased(Keys.S) && Controller.IsKeyReleased(Keys.D))
            {
                if (animation.name == "walking" || animation.name == "jumping" || animation.name == "ducking")
                {
                    animation.Stop();
                }
            }

            // Jumping
            if (Controller.IsKeyDown(Keys.Space))
            {
                if (grounded)
                {
                    Assets.marioSmallJump.Play();
                    collider.velocity.Y -= jumpPower;
                }
            }

            // Jump Animation
            if (animation.name != "ducking" && collider.velocity.Y < 0)
            {
                SetAnimation(jumping);
            }
            else if (animation.name == "jumping" && collider.velocity.Y >= 0)
            {
                SetAnimation(walking);
            }
        }

        // Check if on the ground
        public bool CheckIfGrounded()
        {
            // Check if on top of an object
            List<GameObject> objects = scene.GetGameObjects(new Rectangle((int)transform.position.X, (int)transform.position.Y + 16, collider.bounds.Width, collider.bounds.Height));
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].collider.enabled)
                {
                    return true;
                }
            }

            // Check if on top of a tile
            List<Tile> tiles = scene.map.GetTiles(new Rectangle((int)transform.position.X, (int)transform.position.Y + 16, collider.bounds.Width, collider.bounds.Height));
            for (int i = 0; i < tiles.Count; i++)
            {
                if (tiles[i].collision)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
