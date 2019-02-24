using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMonoGame
{
    class SpriteRenderer : Renderer
    {
        public SpriteRenderer(GameObject gameObject, Texture2D texture) : base(gameObject)
        {
            this.texture = texture;
        }

        private Texture2D texture;

        public override void Render(SpriteBatch spriteBatch) =>
            spriteBatch.Draw(texture, gameObject.Position, null, color: Color.White, rotation: 0f, origin: new Vector2(texture.Width / 2, texture.Height / 2), scale: Vector2.One, effects: SpriteEffects.None, layerDepth: 0f);
    }
}
