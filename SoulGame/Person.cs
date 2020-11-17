using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LonelySoul
{
    public class Person : GameObject
    {
        public Texture2D sprite;

        public Vector2 target;

        public Person()
        {
            position.X = 2 * 16;
            position.Y = 3 * 16;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position);
        }

        public override void Update(GameTime gameTime)
        {
            if (target != null && Vector2.Distance(position, target) > 8)
            {
                //walk
            }
        }

            public void WalkTo(Vector2 pos)
        {
            target = pos;
        }
    }
}
