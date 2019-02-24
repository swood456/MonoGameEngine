using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMonoGame
{
    abstract class BehaviorComponent : Component
    {
        public BehaviorComponent (GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        protected GameObject gameObject;
        public abstract void Update(GameTime gameTime);
    }
}
