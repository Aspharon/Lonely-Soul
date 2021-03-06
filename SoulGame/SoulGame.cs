﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace LonelySoul
{
    public class SoulGame : Game
    {
        private InputHelper inputHelper;
        private GraphicsHelper graphicsHelper;

        Texture2D endScreen, personP;
        Player player;
        Body body;
        Person person;
        byte gamestate = 0;

        public SoulGame()
        {
            graphicsHelper = new GraphicsHelper(this);
            inputHelper = new InputHelper();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Overworld overworld = new Overworld(Content.Load<TiledMap>(Path.Combine("Tilemaps", "Room 1", "room1")), graphicsHelper.graphics.GraphicsDevice);
            player = new Player();
            player.sprites[0] = Content.Load<Texture2D>("soul1");
            player.sprites[1] = Content.Load<Texture2D>("soul2");
            player.sprites[2] = Content.Load<Texture2D>("soul3");
            player.sprites[3] = Content.Load<Texture2D>("soul4");
            endScreen = Content.Load<Texture2D>("endScreen");

            body = new Body();
            body.sprite = Content.Load<Texture2D>("body");

            person = new Person();
            person.sprite = Content.Load<Texture2D>("person");
            personP = Content.Load<Texture2D>("personP");

            HouseObjects.Lamp lamp = new HouseObjects.Lamp(3,1);
            HouseObjects.Computer computer = new HouseObjects.Computer(2, 1);
            HouseObjects.Bed bed = new HouseObjects.Bed(3, 3);
            HouseObjects.Sink sink = new HouseObjects.Sink(6, 1);
            HouseObjects.Door door = new HouseObjects.Door(7, 0);
            HouseObjects.Piano piano = new HouseObjects.Piano(8, 1);
            HouseObjects.BathroomSink bathroomSink = new HouseObjects.BathroomSink(10, 0);
            HouseObjects.Shower shower = new HouseObjects.Shower(10, 3);
            HouseObjects.Roll roll = new HouseObjects.Roll(12, 0);
            HouseObjects.Toilet toilet = new HouseObjects.Toilet(12, 1);
            HouseObjects.Bath bath = new HouseObjects.Bath(12, 2);

            Objects.List.Add(overworld);
            Objects.List.Add(computer);
            Objects.List.Add(lamp);
            Objects.List.Add(bed);
            Objects.List.Add(sink);
            Objects.List.Add(door);
            Objects.List.Add(piano);
            Objects.List.Add(bathroomSink);
            Objects.List.Add(shower);
            Objects.List.Add(roll);
            Objects.List.Add(toilet);
            Objects.List.Add(bath);

            Objects.List.Add(body);

            Objects.List.Add(person);

            Objects.List.Add(player);

            foreach (HouseObject h in Objects.List.OfType<HouseObject>())
                h.LoadSprites(Content);
        }

        protected override void UnloadContent()
        {
            endScreen = null;
            player = null;
            body = null;
            person = null;
            Objects.List.Clear();
            gamestate = 0;
        }

        protected override void Update(GameTime gameTime)
        {
            inputHelper.Update(gameTime);

            if (inputHelper.ButtonDown(Buttons.Back) || inputHelper.KeyDown(Keys.Escape))
                Exit();
            
            base.Update(gameTime);
            graphicsHelper.Update(gameTime);
            graphicsHelper.HandleInput(inputHelper);
            foreach(GameObject obj in Objects.List)
                obj.HandleInput(inputHelper);
            if (gamestate == 0)
            {
                foreach (GameObject obj in Objects.List)
                    obj.Update(gameTime);
            }

            if (person.position == body.position)
            {
                person.sprite = personP;
                gamestate = 1;
            }

            if (inputHelper.KeyDown(Keys.Space) && gamestate == 1)
            {
                UnloadContent();
                LoadContent();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.SetRenderTarget(null);
            graphicsHelper.Draw(gameTime);
            if (gamestate == 1)
            {
                graphicsHelper.spriteBatch.Begin(0, null, SamplerState.PointClamp);
                graphicsHelper.spriteBatch.Draw(endScreen, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                graphicsHelper.spriteBatch.End();
            }
        }
    }
}
