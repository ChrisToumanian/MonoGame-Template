using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTemplate
{
    class Textbox
    {
        // Font Settings
        public Font font;
        public string text = "";
        public Color color = Color.White;
        public SpriteEffects spriteEffect = SpriteEffects.None;

        // Orientation
        public Vector2 position = new Vector2(0, 0);
        public float scale = 1;
        public float depth = 1;
        public float rotation = 0;
        public Vector2 origin = new Vector2(0, 0);

        // Constructor
        public Textbox(Font font, float x, float y, string text)
        {
            this.font = font;
            this.text = text;
            position.X = x;
            position.Y = y;
        }
    }
}
