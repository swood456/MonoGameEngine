using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace TestMonoGame
{
    class InputManager
    {
        private float horizontalInput;
        private float verticalInput;

        public InputManager()
        {
            KeyboardState kState = Keyboard.GetState();

            horizontalInput = 0f;
            verticalInput = 0f;

            if (kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.W))
            {
                verticalInput += 1.0f;
            }

            if (kState.IsKeyDown(Keys.Down) || kState.IsKeyDown(Keys.S))
            {
                verticalInput -= 1.0f;
            }

            if (kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D))
            {
                horizontalInput += 1.0f;
            }

            if (kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.A))
            {
                horizontalInput -= 1.0f;
            }
        }

        public float HorizontalInput { get => horizontalInput; set => horizontalInput = value; }
        public float VerticalInput { get => verticalInput; set => verticalInput = value; }
    }
}
