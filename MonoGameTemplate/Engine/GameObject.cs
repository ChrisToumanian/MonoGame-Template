using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;

namespace MonoGameTemplate
{
    class GameObject
    {
        public int id = 0;
        public string name = "gameObject";
        public bool enabled = true;
        public Sprite sprite;
        public Animation animation;
        public Transform transform;
        public Collider collider;
        public Scene scene;

        public GameObject()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Click()
        {
        }

        public virtual void OnTrigger(GameObject gameObject)
        {
        }

        public virtual void OnCollision(GameObject gameObject)
        {
        }

        public void SetAnimation(Animation animation)
        {
            if (this.animation != animation)
            {
                this.animation = animation;
                animation.Play();
            }
            else
            {
                if (!this.animation.playing)
                {
                    this.animation.Play();
                }
            }
        }

        public void Destroy()
        {
            scene.RemoveGameObject(this);
        }

        public void Destroy(GameObject gameObject)
        {
            scene.RemoveGameObject(gameObject);
        }
    }
}
