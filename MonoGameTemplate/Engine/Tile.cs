using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTemplate
{
    class Tile
    {
        public int id = 0;
        public string name = "tile";
        public Sprite sprite;
        public string color = "FFFFFF";
        public bool collision = false;
        public int state = 0;

        public Tile(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public Tile(Sprite sprite, bool collision)
        {
            this.sprite = sprite;
            this.collision = collision;
        }

        public Tile(Sprite sprite, bool collision, string color)
        {
            this.sprite = sprite;
            this.color = color;
            this.collision = collision;
        }

        public Tile(Sprite sprite, bool collision, string color, string name)
        {
            this.sprite = sprite;
            this.color = color;
            this.collision = collision;
            this.name = name;
        }
    }
}
