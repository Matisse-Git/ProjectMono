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

        Backdrop backdropOne;
        Level levelOne;

        Player finn;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            //Textures
            Texture2D finnSpritesheetLeft = Content.Load<Texture2D>("FinnSpriteLeft");
            Texture2D finnSpritesheetRight = Content.Load<Texture2D>("FinnSpriteRight");

            Texture2D mushroomSpritesheetLeft = Content.Load<Texture2D>("MushroomLeft");
            Texture2D mushroomSpritesheetRight = Content.Load<Texture2D>("MushroomRight");

            Texture2D platformSpriteSheet = Content.Load<Texture2D>("TileSet");

            Texture2D backdropOneTexture = Content.Load<Texture2D>("BackdropTwo");


            //Making Objects
            byte[,] byteArrLevelOne = new byte[,]
            {
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,8,1,1,9,0,0,0,0,0,0,8,1,1,9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,5,2,2,4,0,0,0,0,0,0,5,2,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,5,2,2,4,0,0,0,0,0,0,5,2,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,5,2,2,4,0,0,0,0,0,0,5,2,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,5,2,2,4,0,0,0,0,0,0,5,2,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,5,2,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,5,2,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,5,2,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,5,2,2,4,0,0,0,0,0,0,8,1,1,1,1,9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,5,2,2,4,0,0,0,0,0,0,5,2,2,2,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,5,2,2,4,0,0,0,0,0,0,5,2,2,2,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,5,2,2,4,0,0,0,0,0,0,5,2,2,2,2,4,0,0,10,11,11,12,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,5,2,2,4,0,0,0,0,0,0,5,2,2,2,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,5,2,2,4,0,0,0,0,0,0,5,2,2,2,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {6,1,1,1,1,7,2,2,6,1,1,1,1,1,1,7,2,2,2,2,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,7  },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2  },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2  },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2  }
            };

            levelOne = new Level(platformSpriteSheet, byteArrLevelOne);
            levelOne.CreateLevel();

            finn = new Player(new Vector2(32, 0), finnSpritesheetLeft, finnSpritesheetRight, new KeyboardHandler());
            backdropOne = new Backdrop(backdropOneTexture);
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gametime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            finn.Update(gametime, levelOne.tileArr);
            
            base.Update(gametime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

            backdropOne.Draw(spriteBatch);
            levelOne.DrawLevel(spriteBatch);
            finn.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
