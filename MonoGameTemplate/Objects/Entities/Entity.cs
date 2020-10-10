using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTemplate.Entities
{
    class Entity : GameObject
    {
        public int health = 100;
        public int mana = 100;
        public bool alive = true;
        public float speed = 2;
        public bool grounded = true;

        // Constructor
        public Entity()
        {

        }
    }
}
