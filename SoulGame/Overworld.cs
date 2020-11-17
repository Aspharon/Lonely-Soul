using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace LonelySoul
{
    class Overworld : GameObject
    {
        TiledMapRenderer mapRenderer;
        private TiledMap map;

        private OrthographicCamera camera;

        public Overworld(TiledMap newMap, GraphicsDevice graphics)
        {
            layer = 0;
            visible = true;
            map = newMap;
            mapRenderer = new TiledMapRenderer(graphics, map);
            camera = new OrthographicCamera(graphics);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            int camSpeed = 2;
            Vector2 camMoveVector = new Vector2(0, 0);
            if (inputHelper.KeyDown(Keys.W) && camera.Position.Y > 0)
                camMoveVector += new Vector2(0, -camSpeed);
            if (inputHelper.KeyDown(Keys.S) && camera.Position.Y < map.HeightInPixels - GraphicsHelper.renderHeight)
                camMoveVector += new Vector2(0, camSpeed);
            if (inputHelper.KeyDown(Keys.D) && camera.Position.X < map.WidthInPixels - GraphicsHelper.renderWidth)
                camMoveVector += new Vector2(camSpeed, 0);
            if (inputHelper.KeyDown(Keys.A) && camera.Position.X > 0)
                camMoveVector += new Vector2(-camSpeed, 0);
            camera.Move(camMoveVector);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            mapRenderer.Draw(camera.GetViewMatrix());
        }
    }
}
