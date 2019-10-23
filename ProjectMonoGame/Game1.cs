using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectMonoGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player finn;
        MushroomEnemy mushy;
        CollisionManager colliManager;
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

            finn = new Player(new Vector2(20, 500), finnSpritesheetLeft, finnSpritesheetRight, new KeyboardHandler());
            mushy = new MushroomEnemy(new Vector2(1500, 500), mushroomSpritesheetLeft, mushroomSpritesheetRight);
            colliManager = new CollisionManager();

            //camera = new Camera2D(GraphicsDevice.Viewport);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gametime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            finn.Update(gametime);
            mushy.Update(gametime);
            base.Update(gametime);
            if (colliManager.CheckCollider(finn, mushy))
            {
                finn.position.X = 0;
                mushy.position.X = 1500;
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //var viewMatrix = camera.GetViewMatrix();
            //camera.position = finn.position;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            mushy.Draw(spriteBatch);
            finn.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
