﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ProjectMonoGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player finn;
        Backdrop backdropOne;
        //MushroomEnemy mushy;
        Level levelOne;
        //Camera2D camera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D finnSpritesheetLeft = Content.Load<Texture2D>("FinnSpriteLeft");
            Texture2D finnSpritesheetRight = Content.Load<Texture2D>("FinnSpriteRight");

            Texture2D mushroomSpritesheetLeft = Content.Load<Texture2D>("MushroomLeft");
            Texture2D mushroomSpritesheetRight = Content.Load<Texture2D>("MushroomRight");

            Texture2D platformSpriteSheet = Content.Load<Texture2D>("TileSet");

            Texture2D backdropOneTexture = Content.Load<Texture2D>("BackdropOne");

            backdropOne = new Backdrop(backdropOneTexture);

            byte[,] byteArrLevelOne = new byte[,]
            {
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,5 },
               {4,0,0,0,0,0,0,0,0,0,0,0,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
               {4,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,5 },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
               {4,0,0,0,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
               {4,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
               {4,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
               {4,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
               {4,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,3,0,0,0,0,0,0,0,0,5 },
               {6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,7 },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2 },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2 },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2 },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2 }
            };

            levelOne = new Level(platformSpriteSheet, byteArrLevelOne);
            levelOne.CreateLevel();

            finn = new Player(new Vector2(600, 200), finnSpritesheetLeft, finnSpritesheetRight, new KeyboardHandler());
            //mushy = new MushroomEnemy(new Vector2(1500, 500), mushroomSpritesheetLeft, mushroomSpritesheetRight);

            //camera = new Camera2D(GraphicsDevice.Viewport);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gametime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (ITile tile in levelOne.tileArr)
            {
                if (tile is ICollidable)
                {
                    if (tile != null)
                    {
                        if (finn.CheckCollision(tile.collisionRectangle))
                        {
                            break;
                        }
                    }
                }
            }
            int counter = 0;
            finn.Update(gametime);
            System.Console.WriteLine($"Finn Down: {finn.downCollisionRectangle}");
            foreach (ITile floatingTile in levelOne.tileArr)
            {
                if (floatingTile is floatingTile)
                {
                    counter++;
                    System.Console.WriteLine($"ft {counter + 1}: {floatingTile.collisionRectangle}");
                }
            }
            
            //mushy.Update(gametime);

            System.Console.WriteLine(finn.rightCollisionRectangle);

            base.Update(gametime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //var viewMatrix = camera.GetViewMatrix();
            //camera.position = finn.position;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            backdropOne.Draw(spriteBatch);
            levelOne.DrawLevel(spriteBatch);
            //mushy.Draw(spriteBatch);
            finn.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
