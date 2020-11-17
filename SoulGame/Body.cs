using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LonelySoul
{
    public class Body : GameObject
    {
        public Texture2D sprite;

        public Body()
        {
            position.X = 11 * 16;
            position.Y = 2  * 16;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position);
        }
    }
}
