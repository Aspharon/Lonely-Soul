﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LonelySoul
{
    public abstract class GameObject
    {
        public Vector2 position;
        protected int layer;
        protected bool visible;

        public virtual void HandleInput(InputHelper inputHelper) { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }
    }
}
