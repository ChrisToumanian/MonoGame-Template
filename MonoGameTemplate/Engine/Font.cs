using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTemplate
{
    class Font
    {
        public SpriteFont spriteFont;
        private Texture2D texture;
        int lineSpacing = 0;
        float spacing = 10;
        char defaultCharacter = ' ';
        private List<char> characters = new List<char>();
        private List<Rectangle> glyphBounds = new List<Rectangle>();
        private List<Rectangle> cropping = new List<Rectangle>();
        private List<Vector3> kerning = new List<Vector3>();

        public Font(Texture2D texture, int width, int height, int characterWidth)
        {
            this.texture = texture;

            string chars = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_abcdefghijklmnopqrstuvwxyz{|}~";
            int i = 0;
            for (int y = 0; y < height / 16; y++)
            {
                for (int x = 0; x < width / 16; x++)
                {
                    if (i > chars.Length - 1)
                    {
                        CreateSpriteFont();
                        return;
                    }
                    AddCharacter(chars[i], new Rectangle(x * characterWidth, y * characterWidth, characterWidth, characterWidth));
                    i++;
                }
            }
        }

        public void CreateSpriteFont()
        {
            spriteFont = new SpriteFont(texture, glyphBounds, cropping, characters, lineSpacing, spacing, kerning, defaultCharacter);
        }

        public void AddCharacter(char character, Rectangle glyphBound)
        {
            Rectangle crop = new Rectangle(0, 0, 0, 0);
            if (character == '\'') crop = new Rectangle(-5, 0, -6, 0);
            characters.Add(character);
            glyphBounds.Add(glyphBound);
            cropping.Add(crop);
            kerning.Add(new Vector3(0, 0, 0));
        }

        public void AddCharacter(char character, Rectangle glyphBound, Rectangle crop, Vector3 kern)
        {
            characters.Add(character);
            glyphBounds.Add(glyphBound);
            cropping.Add(crop);
            kerning.Add(kern);
            spriteFont = new SpriteFont(texture, glyphBounds, cropping, characters, lineSpacing, spacing, kerning, defaultCharacter);
        }
    }
}
