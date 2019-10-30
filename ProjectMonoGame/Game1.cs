using Microsoft.Xna.Framework;
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
        MushroomEnemy mushy;
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

            byte[,] byteArrLevelOne = new byte[,]
            {
               { 0,0,0,0,1,0,1,0,0,0,0,0,0,1,1,0,0,0,1,1,1,0,0,0,1,0,0,0,0,0 },
               {1,1,1,1,0,1,0,1,1,1,1,1,1,0,0,1,1,1,0,0,0,1,1,1,0,1,1,1,1,1 }
            };

            levelOne = new Level(platformSpriteSheet, byteArrLevelOne);
            levelOne.CreateLevel();

            finn = new Player(new Vector2(20, 500), finnSpritesheetLeft, finnSpritesheetRight, new KeyboardHandler());
            mushy = new MushroomEnemy(new Vector2(1500, 500), mushroomSpritesheetLeft, mushroomSpritesheetRight);

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
                if (tile != null)
                {
                    finn.CheckCollision(tile.collisionRectangle);
                }
            }

            finn.Update(gametime);
            mushy.Update(gametime);


            base.Update(gametime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //var viewMatrix = camera.GetViewMatrix();
            //camera.position = finn.position;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            levelOne.DrawLevel(spriteBatch);
            mushy.Draw(spriteBatch);
            finn.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
