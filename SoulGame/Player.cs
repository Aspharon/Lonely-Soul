using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Linq;

namespace LonelySoul
{
    class Player : GameObject
    {
        public Texture2D[] sprites;
        private byte currentSprite;
        private byte counter;

        public Player()
        {
            sprites = new Texture2D[4];
            position.X = 11 * 16 - 8;
            position.Y = 2 * 16 - 8;
        }

        public override void Update(GameTime gameTime)
        {
            counter++;
            byte animationSpeed = 10;
            if (counter % animationSpeed == 0)
            {
                if (currentSprite == 3)
                    currentSprite = 0;
                else
                    currentSprite++;
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            int moveSpeed = 2;
            Vector2 moveVector = new Vector2(0, 0);
            if (inputHelper.KeyDown(Keys.W) && position.Y > 0)
                moveVector += new Vector2(0, -moveSpeed);
            if (inputHelper.KeyDown(Keys.S) && position.Y < 128)
                moveVector += new Vector2(0, moveSpeed);
            if (inputHelper.KeyDown(Keys.D) && position.X < 208)
                moveVector += new Vector2(moveSpeed, 0);
            if (inputHelper.KeyDown(Keys.A) && position.X > 0)
                moveVector += new Vector2(-moveSpeed, 0);
            position += moveVector;

            if (inputHelper.KeyPressed(Keys.H))
            {
                HouseObject closestObject = null;
                float smallestDist = float.MaxValue;
                foreach (HouseObject h in Objects.List.OfType<HouseObject>())
                {
                    float dist = Vector2.Distance(position, h.position);
                    if (dist < smallestDist)
                    {
                        smallestDist = dist;
                        closestObject = h;
                    }
                }
                if (closestObject != null && !closestObject.haunted && smallestDist < 32)
                {
                    closestObject.haunted = true;
                    closestObject.hauntSound.Play();
                    foreach (Person p in Objects.List.OfType<Person>())
                    {
                        if (Vector2.Distance(p.position, closestObject.position) < 8 * 16)
                            p.WalkTo(closestObject.walkPosition);
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprites[currentSprite], position);
        }
    }
}
