using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMonoGame
{
    class PlayerMovementComponent : BehaviorComponent
    {
        public PlayerMovementComponent(GameObject gameObject) : base(gameObject) { }
        private float ballSpeed = 100f;

        public override void Update(GameTime gameTime)
        {
            Vector2 updatedPosition = gameObject.Position;
            updatedPosition.X += InputManager.Instance.getAxis(InputAxes.PrimaryHorizontal) * ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            updatedPosition.Y += -InputManager.Instance.getAxis(InputAxes.PrimaryVertical) * ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            gameObject.Position = updatedPosition;
        }
    }
}
