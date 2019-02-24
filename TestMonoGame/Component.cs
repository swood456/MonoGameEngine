using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMonoGame
{
    abstract class Component
    {
        protected readonly GameObject gameObject;
        public Component(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public abstract void Update(GameTime gameTime);
    }
}
