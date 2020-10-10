using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTemplate.Scenery
{
    class SmallPineTree : GameObject
    {
        public SmallPineTree()
        {
            name = "small pine tree";
            transform = new Transform(0, 0, 16, 48);
            sprite = new Sprite(Assets.tiles, 32, 16, 16, 48);
            collider = new Collider(this, 14, 12);
            collider.bounds.Y = 10;
            collider.enabled = true;
            collider.collision = true;
            collider.physical = false;
        }
    }
}
