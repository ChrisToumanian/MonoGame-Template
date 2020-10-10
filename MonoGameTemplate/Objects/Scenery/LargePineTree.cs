using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTemplate.Scenery
{
    class LargePineTree : GameObject
    {
        public LargePineTree()
        {
            name = "large pine tree";
            transform = new Transform(0, 0, 32, 48);
            sprite = new Sprite(Assets.tiles, 0, 16, 32, 48);
            collider = new Collider(this, 26, 12);
            collider.bounds.Y = 11;
            collider.enabled = false;
            collider.collision = true;
            collider.physical = false;
        }
    }
}
