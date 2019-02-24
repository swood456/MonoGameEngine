using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMonoGame
{
    public class GameObject
    {
        public GameObject() {
            Components = new List<Component>();
            position = new Vector2(0.0f, 0.0f);
        }

        public GameObject(Vector2 startionPosition)
        {
            Components = new List<Component>();
            position = startionPosition;
        }

        private List<Component> components;
        private Vector2 position;
        private Renderer renderer;

        internal List<Component> Components { get => components; set => components = value; }
        public Vector2 Position { get => position; set => position = value; }

        public void Update(GameTime gameTime)
        {
            foreach (Component component in Components)
            {
                component.Update(gameTime);
            }
        }

        //public void addComponent(Component component)
        //{
        //    components.Add(component);
        //}

        public void Render()
        {
            renderer.Render();
        }
    }
}
