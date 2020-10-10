using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameTemplate
{
    class Animation
    {
        public string name = "animation";

        // Sprites in animation in order
        private Rectangle[] sprites;

        // Playback settings
        public Sprite current;
        public bool playing = true;
        public bool loop = true;
        public bool bounce = false;
        public bool reverse = false;
        public int framerate = 5;
        private int framerateCount = 0;
        private int frame = 0;
        private int frames = 0;

        // Constructor
        public Animation(string name, Sprite sprite, Rectangle[] boundCollection)
        {
            this.name = name;
            current = sprite;
            sprites = boundCollection;
            frames = sprites.Length;
        }

        // Alternate Constructor with flip horizontal
        public Animation(string name, Sprite sprite, Rectangle[] sprites, bool flipHorizontal)
        {
            this.name = name;
            current = sprite;
            this.sprites = sprites;
            frames = sprites.Length;
        }

        // Alternate Constructor with flip horizontal and vertical
        public Animation(string name, Sprite sprite, Rectangle[] sprites, bool flipHorizontal, bool flipVertical)
        {
            this.name = name;
            current = sprite;
            this.sprites = sprites;
            frames = sprites.Length;
        }

        // Update
        public void Update()
        {
            if (framerateCount >= framerate) framerateCount = 0;
            if (framerateCount == 0)
            {
                current.bounds = sprites[frame];
                if (playing) frame++;
                if (frame >= frames)
                {
                    if (loop) frame = 0;
                    else Stop();
                }
            }
            framerateCount++;
        }

        // Play
        public void Play()
        {
            playing = true;
            frame = 0;
            framerateCount = 0;
            Update();
        }

        // Pause
        public void Pause()
        {
            playing = false;
        }

        // Stop
        public void Stop()
        {
            frame = 0;
            playing = false;
        }
    }
}
