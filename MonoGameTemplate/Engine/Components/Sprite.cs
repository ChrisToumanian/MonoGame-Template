using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTemplate
{
    class Sprite
    {
        // Color settings
        public Color color = Color.White;
        public Texture2D texture;
        public SpriteEffects spriteEffect = SpriteEffects.None;

        // Orientation settings
        public Rectangle bounds = new Rectangle(0, 0, 16, 16);
        public float depth = 0;
        public bool useLevelDepth = true;
        public bool flippedHorizontally = false;
        public bool flippedVertically = false;

        // Default Constructor
        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }

        // Constructor by sprite sheet pixel clip position and size
        public Sprite(Texture2D texture, int clipX, int clipY, int width, int height)
        {
            SetBounds(clipX, clipY, width, height);
            this.texture = texture;
        }

        // Constructor by sprite position and width
        public Sprite(Texture2D texture, int spriteX, int spriteY, int tileWidth)
        {
            this.texture = texture;
            SetBounds(spriteX, spriteY, tileWidth);
        }

        // Flip horizontal
        public void FlipHorizontal()
        {
            if (flippedHorizontally)
            {
                spriteEffect = SpriteEffects.None;
                flippedHorizontally = false;
            }
            else
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                flippedHorizontally = true;
            }
        }

        // Flip horizontal set
        public void FlipHorizontal(bool flip)
        {
            flippedHorizontally = flip;
            if (flippedHorizontally) spriteEffect = SpriteEffects.FlipHorizontally;
            else spriteEffect = SpriteEffects.None;
        }

        // Flip vertical
        public void FlipVertical()
        {
            if (flippedVertically)
            {
                spriteEffect = SpriteEffects.None;
                flippedVertically = false;
            }
            else
            {
                spriteEffect = SpriteEffects.FlipVertically;
                flippedVertically = true;
            }
        }

        // Flip vertical set
        public void FlipVertical(bool flip)
        {
            flippedVertically = flip;
            if (flippedVertically) spriteEffect = SpriteEffects.FlipVertically;
            else spriteEffect = SpriteEffects.None;
        }

        // Set bounds by sprite pixel position, width and height
        public void SetBounds(int x, int y, int width, int height)
        {
            bounds = new Rectangle(x, y, width, height);
        }

        // Set bounds by sprite position and width
        public void SetBounds(int spriteX, int spriteY, int tileWidth)
        {
            bounds = new Rectangle((spriteX - 1) * tileWidth, (spriteY - 1) * tileWidth, tileWidth, tileWidth);
        }

        // Get bounds of pixel as Rectangle
        public static Rectangle GetBounds(int spriteX, int spriteY, int tileWidth)
        {
            return new Rectangle((spriteX - 1) * tileWidth, (spriteY - 1) * tileWidth, tileWidth, tileWidth);
        }
    }
}
