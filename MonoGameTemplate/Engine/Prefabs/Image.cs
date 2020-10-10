using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTemplate
{
    class Image : GameObject
    {
        public Image(Texture2D texture)
        {
            sprite = new Sprite(texture, 0, 0, texture.Bounds.Width, texture.Bounds.Height);
            transform = new Transform(0, 0, texture.Bounds.Width, texture.Bounds.Height);
            transform.origin = new Vector2(0, 0);
        }

        public Image(Texture2D texture, int x, int y)
        {
            sprite = new Sprite(texture, 0, 0, texture.Bounds.Width, texture.Bounds.Height);
            transform = new Transform(x, y, texture.Bounds.Width, texture.Bounds.Height);
            transform.origin = new Vector2(0, 0);
        }

        public Image(Texture2D texture, int x, int y, float scale)
        {
            sprite = new Sprite(texture, 0, 0, texture.Bounds.Width, texture.Bounds.Height);
            transform = new Transform(x, y, texture.Bounds.Width, texture.Bounds.Height);
            transform.scale = scale;
            transform.origin = new Vector2(0, 0);
        }

        public Image(Texture2D texture, int x, int y, int spriteX, int spriteY, int tileWidth, float scale)
        {
            sprite = new Sprite(texture, 0, 0, tileWidth, tileWidth);
            sprite.SetBounds(spriteX, spriteY, tileWidth);
            transform = new Transform(x, y, tileWidth, tileWidth);
            transform.scale = scale;
            transform.origin = new Vector2(0, 0);
        }

        public Image(Texture2D texture, int x, int y, int clipX, int clipY, int clipWidth, int clipHeight)
        {
            sprite = new Sprite(texture, clipX, clipY, clipWidth, clipHeight);
            transform = new Transform(x, y, clipWidth, clipHeight);
            transform.origin = new Vector2(0, 0);
        }

        public Image(Texture2D texture, int x, int y, int clipX, int clipY, int clipWidth, int clipHeight, float scale)
        {
            sprite = new Sprite(texture, clipX, clipY, clipWidth, clipHeight);
            transform = new Transform(x, y, clipWidth, clipHeight);
            transform.scale = scale;
            transform.origin = new Vector2(0, 0);
        }
    }
}
