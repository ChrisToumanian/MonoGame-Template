using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameTemplate
{
    class Collider
    {
        // Settings
        public bool enabled = false;
        public bool physical = false;
        public bool collision = false;
        public bool borderCollision = false;

        // Dimensions
        public Rectangle bounds = new Rectangle(0, 0, 0, 0);
        public int radius;

        // Physics settings
        public float mass = 0;
        public float bounce = 0;
        public float rotationalVelocity = 0;
        public Vector2 gravity = new Vector2(0, 0);
        public Vector2 velocity = new Vector2(0, 0);
        public float maxVelocity = 0;
        public float drag = 0f;
        
        // Gravitational Attractor Settings
        public bool isAttractor = false;
        public int attractionDistance = 500;
        public float attraction = 0;

        // Object References
        private GameObject gameObject;
        private Transform transform;

        // Constructor
        public Collider(GameObject gameObject, int width, int height)
        {
            bounds.Width = width;
            bounds.Height = height;
            radius = width / 2;
            this.gameObject = gameObject;
            transform = gameObject.transform;
        }

        // Update - Called by GameObject each frame
        public void Update()
        {
            if (enabled && physical) // Only update position if it is enabled and physical
            {
                // Have gravity affect current velocity
                if (gravity.X != 0 || gravity.Y != 0)
                {
                    velocity.X -= gravity.X;
                    velocity.Y -= gravity.Y;
                }

                // Limit the velocity of the object and move it if velocity is greater than 0.
                if (velocity.X != 0 || velocity.Y != 0)
                {
                    LimitVelocity();
                    Move(velocity.X, velocity.Y);
                }

                // If collider is a gravitational attractor, attract other objects
                if (isAttractor)
                {
                    Attract();
                }

                // Changes rotation based on current rotational velocity
                if (rotationalVelocity != 0) 
                {
                    transform.rotation += rotationalVelocity;
                }
            }
        }

        // Change Velocity by x & y amount
        public void ChangeVelocity(float x, float y)
        {
            velocity.X += x;
            velocity.Y += y;
        }

        // Set Velocity to a new amount
        public void SetVelocity(float x, float y)
        {
            velocity.X = x;
            velocity.Y = y;
        }

        // Move by x & y amount
        public void Move(float x, float y)
        {
            transform.position.X += x;
            transform.position.Y += y;
            CheckCollision();
        }

        // Move by Vector2 amount
        public void Move(Vector2 movement)
        {
            transform.position.X += movement.X;
            transform.position.Y += movement.Y;
            CheckCollision();
        }

        // Check for collisions
        public void CheckCollision()
        {
            if (enabled && collision)
            {
                if (borderCollision) CheckSceneBounds(); // Check for border collisions if enabled and compensate
                ObjectCollision(); // Check for collisions with other objects and compensate for them
                CheckTileCollision(); // Check for collisions with tiles and compensate for them
            }
        }

        // Checks for collisions with other objects
        public void ObjectCollision()
        {
            Scene scene = gameObject.scene;
            Rectangle rect = GetBounds(); // Obtains Rectangle of collider's current position and size in scene

            // Gets list of GameObjects closer than 200 pixels away from this collider
            List<GameObject> objects = scene.GetGameObjects(new Rectangle(rect.X - 200, rect.Y - 200, rect.Width + 200, rect.Height + 200));

            // Iterates through GameObjects and checks if their collider overlaps with this one
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] != gameObject && objects[i].collider.enabled)
                {
                    BoxCollision(objects[i]);
                }
            }
        }

        // Box-based collision
        private void BoxCollision(GameObject obj)
        {
            Rectangle a = GetBounds(); 
            Rectangle b = obj.collider.GetBounds();

            if (Geometry.IsOverlapping(a, b)) // Checks if the Rectangles of this collider and the other object overlap based on method in Geometry.cs
            {
                Vector2 move = Geometry.MoveOff(a, b); // If the colliders overlap, this returns the x & y amount the object must move to get away from other.

                // If this is physical, move it completely out of the way and change its velocity
                if (physical) 
                {
                    transform.Move(move);
                    if (move.X > 0) velocity.X *= -bounce - obj.collider.mass;
                    if (move.X < 0) velocity.X *= -bounce - obj.collider.mass;
                    if (move.Y > 0) velocity.Y *= -bounce - obj.collider.mass;
                    if (move.Y < 0) velocity.Y *= -bounce - obj.collider.mass;
                }

                // If the other object is physical, change its velocity
                if (obj.collider.physical) 
                {
                    if (move.X > 0) obj.collider.velocity.X *= bounce + mass;
                    if (move.X < 0) obj.collider.velocity.X *= bounce + mass;
                    if (move.Y > 0) obj.collider.velocity.Y *= bounce + mass;
                    if (move.Y < 0) obj.collider.velocity.Y *= bounce + mass;
                }

                // Calls the OnCollision methods of each object so they can handle the event
                gameObject.OnCollision(obj);
                obj.OnCollision(gameObject);
            }
        }

        // Check for collisions with tiles
        private void CheckTileCollision()
        {
            Rectangle rect = new Rectangle((int) transform.position.X - bounds.Width / 2, (int) transform.position.Y - bounds.Height / 2, bounds.Width, bounds.Height);
            Vector2 tilePos = GetTilePosition();
            int tileWidth = gameObject.scene.map.tileWidth;

            int maxX = (int)tilePos.X + 4;
            if (maxX >= gameObject.scene.map.width) maxX = gameObject.scene.map.width - 1;
            int minX = (int)tilePos.X - 4;
            if (minX < 0) minX = 0;
            int maxY = (int)tilePos.Y + 4;
            if (maxY >= gameObject.scene.map.height) maxY = gameObject.scene.map.height - 1;
            int minY = (int)tilePos.Y - 4;
            if (minY < 0) minY = 0;

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    if (x > -1 && y > -1 && x < gameObject.scene.bounds.Width - tileWidth && y < gameObject.scene.bounds.Height - tileWidth)
                    {
                        if (gameObject.scene.map.tiles[x, y] != null && gameObject.scene.map.tiles[x, y].collision)
                        {
                            Rectangle tile = new Rectangle(x * tileWidth, y * tileWidth, gameObject.scene.map.tiles[x, y].sprite.bounds.Width, gameObject.scene.map.tiles[x, y].sprite.bounds.Height);
                            if (Geometry.IsOverlapping(rect, tile))
                            {
                                if (physical)
                                {
                                    Vector2 move = Geometry.MoveOff(rect, tile);
                                    transform.Move(move.X, move.Y);
                                    if (move.X > 0) velocity.X *= -bounce;
                                    if (move.X < 0) velocity.X *= -bounce;
                                    if (move.Y > 0) velocity.Y *= -bounce;
                                    if (move.Y < 0) velocity.Y *= -bounce;
                                    if (move.X < 2 || move.X > -2 || move.Y > -2 || move.Y < 2) return;
                                }
                            }
                        }
                    }
                }
            }
        }

        // Check for collision with scene bounds
        private void CheckSceneBounds()
        {
            Rectangle boundary = Game.scene.bounds;
            
            if (transform.position.Y < boundary.Top + (bounds.Height / 2 * transform.scale))
            {
                transform.position.Y = boundary.Top + (bounds.Height / 2 * transform.scale);
                velocity.Y *= -bounce;
            }
            if (transform.position.Y > boundary.Bottom - (bounds.Height / 2 * transform.scale))
            {
                transform.position.Y = boundary.Bottom - (bounds.Height / 2 * transform.scale);
                velocity.Y *= -bounce;
            }
            if (transform.position.X < boundary.Left + (bounds.Width / 2 * transform.scale))
            {
                transform.position.X = boundary.Left + (bounds.Width / 2 * transform.scale);
                velocity.X *= -bounce;
            }
            if (transform.position.X > boundary.Right - (bounds.Width / 2 * transform.scale))
            {
                transform.position.X = boundary.Right - (bounds.Width / 2 * transform.scale);
                velocity.X *= -bounce;
            }
        }

        // Attract other objects
        public void Attract()
        {
            for (int i = 0; i < gameObject.scene.gameObjects.Count; i++)
            {
                if (gameObject.scene.gameObjects[i] != gameObject && gameObject.scene.gameObjects[i].collider.enabled && Geometry.IsWithin(transform.position, gameObject.scene.gameObjects[i].transform.position, attractionDistance))
                {
                    Vector2 heading = Geometry.GetHeading(transform.position, gameObject.scene.gameObjects[i].transform.position);
                    gameObject.scene.gameObjects[i].collider.ChangeVelocity(attraction * heading.X / 100, attraction * heading.Y / 100);
                }
            }
        }

        // Velocity limiter
        public void LimitVelocity()
        {
            if (drag != 0)
            {
                if (velocity.X > 0) velocity.X *= -drag;
                if (velocity.X < 0) velocity.X *= drag;
                if (velocity.Y > 0) velocity.Y *= -drag;
                if (velocity.Y < 0) velocity.Y *= drag;
            }

            if (maxVelocity != 0)
            {
                if (velocity.X > maxVelocity)
                {
                    velocity.X = maxVelocity;
                }
                if (velocity.X < -maxVelocity)
                {
                    velocity.X = -maxVelocity;
                }
                if (velocity.Y > maxVelocity)
                {
                    velocity.Y = maxVelocity;
                }
                if (velocity.Y < -maxVelocity)
                {
                    velocity.Y = -maxVelocity;
                }
            }
        }

        // Get current bounds of collider in scene
        public Rectangle GetBounds()
        {
            float x = transform.position.X + (bounds.X * transform.scale) - (bounds.Width / 2 * transform.scale);
            float y = transform.position.Y + (bounds.Y * transform.scale) - (bounds.Height / 2 * transform.scale);
            return new Rectangle((int) x, (int) y, bounds.Width, bounds.Height);
        }

        // Get what tile collider is over
        public Vector2 GetTilePosition()
        {
            int x = (int)(transform.position.X / gameObject.scene.map.tileWidth);
            int y = (int)(transform.position.Y / gameObject.scene.map.tileWidth);
            return new Vector2(x, y);
        }
    }
}
