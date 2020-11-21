using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LonelySoul
{
    class HouseObject : GameObject
    {
        public SoundEffect hauntSound;
        public Texture2D defaultSprite;
        public Texture2D hauntSprite;
        public Vector2 walkPosition;

        public bool haunted;
        protected int hauntTime;
        protected int haunter;

        protected string spriteName;

        public HouseObject(byte X, byte Y)
        {
            position.X = X * 16;
            position.Y = Y * 16;

            walkPosition = position;
            walkPosition.Y += 16;
            haunted = false;
        }

        public void LoadSprites(ContentManager content)
        {
            defaultSprite = content.Load<Texture2D>(Path.Combine("House Objects", spriteName));
            hauntSprite = content.Load<Texture2D>(Path.Combine("House Objects", spriteName + "H"));
            hauntSound = content.Load<SoundEffect>(Path.Combine("House Objects", "Sound", spriteName));
        }

        public override void Update(GameTime gameTime)
        {
            if (haunted)
            {
                haunter++;
                if (haunter == hauntTime)
                {
                    haunted = false;
                    haunter = 0;
                }
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (haunted)
                spriteBatch.Draw(hauntSprite, position);
            else
                spriteBatch.Draw(defaultSprite, position);
        }
    }
}
