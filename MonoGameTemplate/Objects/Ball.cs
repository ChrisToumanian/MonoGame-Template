using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTemplate.Objects
{
    class Ball : GameObject
    {
        public Ball()
        {
            name = "ball";
            sprite = new Sprite(Assets.tiles, 12, 2, 16);
            sprite.useLevelDepth = true;
            transform = new Transform(0, 0, 16, 16);
            collider = new Collider(this, 16, 16);
            collider.enabled = true;
            collider.physical = true;
            collider.collision = true;
            collider.borderCollision = true;
            collider.bounce = 0.8f;
            collider.velocity.X = Geometry.RandomFloat(-3, 3);
            collider.velocity.Y = Geometry.RandomFloat(-3, 3);
            collider.gravity.Y = -0.27f;
            collider.maxVelocity = 5;
            collider.mass = 1;
        }
            
        public override void Update()
        {
            if (transform.position.Y > scene.bounds.Bottom - 16)
            {
                enabled = false;
            }

            collider.Update();
        }

        public override void OnCollision(GameObject gameObject)
        {
            if (gameObject.name == "Mario")
            {   
                Assets.marioSmallJump.Play();
            }
        }
    }
}
