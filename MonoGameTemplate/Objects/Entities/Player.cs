using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTemplate.Entities
{
    class Player : Entity
    {
        Animation walkingDown;
        Animation walkingUp;
        Animation walkingLeft;
        Animation walkingRight;

        public Player()
        {
            name = "player";
            speed = 1f;
            sprite = new Sprite(Assets.sprites, 3, 1, 32);
            sprite.depth = 0.6f;
            transform = new Transform(0, 0, 32, 32);
            collider = new Collider(this, 10, 10);
            collider.enabled = true;
            collider.collision = true;
            collider.physical = true;
            collider.mass = 100;

            // Animations
            walkingUp = new Animation("walking up", sprite, new Rectangle[6]{
                Sprite.GetBounds(2, 2, 32),
                Sprite.GetBounds(3, 2, 32),
                Sprite.GetBounds(4, 2, 32),
                Sprite.GetBounds(5, 2, 32),
                Sprite.GetBounds(6, 2, 32),
                Sprite.GetBounds(1, 2, 32)
            });
            walkingDown = new Animation("walking down", sprite, new Rectangle[6]{
                Sprite.GetBounds(2, 1, 32),
                Sprite.GetBounds(3, 1, 32),
                Sprite.GetBounds(4, 1, 32),
                Sprite.GetBounds(5, 1, 32),
                Sprite.GetBounds(6, 1, 32),
                Sprite.GetBounds(1, 1, 32)
            });
            walkingLeft = new Animation("walking left", sprite, new Rectangle[6]{
                Sprite.GetBounds(2, 3, 32),
                Sprite.GetBounds(3, 3, 32),
                Sprite.GetBounds(4, 3, 32),
                Sprite.GetBounds(5, 3, 32),
                Sprite.GetBounds(6, 3, 32),
                Sprite.GetBounds(1, 3, 32)
            });
            walkingRight = new Animation("walking right", sprite, new Rectangle[6]{
                Sprite.GetBounds(2, 3, 32),
                Sprite.GetBounds(3, 3, 32),
                Sprite.GetBounds(4, 3, 32),
                Sprite.GetBounds(5, 3, 32),
                Sprite.GetBounds(6, 3, 32),
                Sprite.GetBounds(1, 3, 32)
            });

            SetAnimation(walkingUp);
        }

        public override void Update()
        {
            Controls();
            collider.Update();
            animation.Update();
        }

        private void Controls()
        {
            // Movement
            float x = 0;
            float y = 0;

            if (Controller.IsKeyPressed(Keys.A)) // Move Left
            {
                SetAnimation(walkingLeft);
                sprite.FlipHorizontal(false);
                x = -speed;
            }
            if (Controller.IsKeyPressed(Keys.D)) // Move Right
            {
                SetAnimation(walkingRight);
                sprite.FlipHorizontal(true);
                x = speed;
            }
            if (x == 0 && Controller.IsKeyPressed(Keys.W)) // Move Up
            {
                SetAnimation(walkingUp);
                y = -speed;
            }
            if (x == 0 && Controller.IsKeyPressed(Keys.S)) // Move Down
            {
                SetAnimation(walkingDown);
                y = speed;
            } 

            collider.Move(x, y);

            // Idle Animation
            if (Controller.IsKeyReleased(Keys.W) && Controller.IsKeyReleased(Keys.A) && Controller.IsKeyReleased(Keys.S) && Controller.IsKeyReleased(Keys.D))
            {
                if (animation.name == "walking up" || animation.name == "walking down" || animation.name == "walking right" || animation.name == "walking left")
                {
                    animation.Stop();
                    collider.SetVelocity(0, 0);
                }
            }

            // Sword
            if (Controller.IsKeyDown(Keys.Space))
            {
                Assets.soundSword1.Play();
            }
        }
    }
}
