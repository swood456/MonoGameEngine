using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMonoGame
{
    abstract class Renderer
    {
        public Renderer(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        protected readonly GameObject gameObject;

        public abstract void Render(SpriteBatch spriteBatch);
    }
}
