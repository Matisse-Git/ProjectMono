using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ProjectMonoGame
{
    public enum GameState
    {
        Startscreen,
        Options,
        InGame,
        Credits
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        LevelList allLevels;

        Texture2D JumpTutorialTexture;
        Texture2D moveTutorialTexture;
        Texture2D mushroomSpritesheetRight;
        Texture2D mushroomSpritesheetLeft;

        GameState gameState;

        Menu startScreen;
        ImageDrawer title;
        ImageDrawer backdropTitle;
        ImageDrawer levelBackground;
        ImageDrawer moveTutorial;
        ImageDrawer jumpTutorial;
        ImageDrawer spikeTutorial;
        ImageDrawer wallJumpTutorial;

        Player finn;
        List<Enemy> enemyList;

        bool enemyMade = false;

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
            Texture2D jumpParticleDust = Content.Load<Texture2D>("DoubleJumpDust");

            mushroomSpritesheetLeft = Content.Load<Texture2D>("MushroomLeft");
            mushroomSpritesheetRight = Content.Load<Texture2D>("MushroomRight");

            Texture2D platformSpriteSheet = Content.Load<Texture2D>("TileSet");
            Texture2D spikeTile = Content.Load<Texture2D>("SpikeTile");
            Texture2D startSC = Content.Load<Texture2D>("StartSC");
            Texture2D optionsSC = Content.Load<Texture2D>("OptionSC");
            Texture2D exitSC = Content.Load<Texture2D>("ExitSC");
            Texture2D loadingScreenTexture = Content.Load<Texture2D>("LoadingScreen");
            Texture2D titleTexture = Content.Load<Texture2D>("TitleSC");
            Texture2D backdropTitleTexture = Content.Load<Texture2D>("BackdropTitle");
            Texture2D goalTexture = Content.Load<Texture2D>("GateGoal");
            Texture2D doorTutorialTexture = Content.Load<Texture2D>("DoorButtonTutorial");

            moveTutorialTexture = Content.Load<Texture2D>("MoveTutorial");
            JumpTutorialTexture = Content.Load<Texture2D>("JumpTutorial");
            Texture2D spikeTutorialTexture = Content.Load<Texture2D>("SpikeTutorial");
            Texture2D wallJumpTutorialTexture = Content.Load<Texture2D>("WallJumpTutorial");
            Texture2D backdropOneTexture = Content.Load<Texture2D>("BackdropTwo");

            gameState = GameState.Startscreen;

            //Making Objects
            allLevels = new LevelList(platformSpriteSheet, spikeTile, goalTexture);
            enemyList = new List<Enemy>();
            levelBackground = new ImageDrawer(backdropOneTexture, new Vector2(0,0), new Vector2(1920,1080), new Vector2(1920,1080));
            spriteBatch = new SpriteBatch(GraphicsDevice);
            startScreen = new Menu(startSC, optionsSC,exitSC, new KeyboardHandler(), 2);
            title = new ImageDrawer(titleTexture, new Vector2(430, -150), new Vector2(1000,1000), new Vector2(800,800));
            backdropTitle = new ImageDrawer(backdropTitleTexture, new Vector2(0, 0), new Vector2(1920, 1080), new Vector2(1920, 1080));

            moveTutorial = new ImageDrawer(moveTutorialTexture, new Vector2(200, 600), new Vector2(405, 333), new Vector2(405, 333));
            jumpTutorial = new ImageDrawer(JumpTutorialTexture, new Vector2(900, 400), new Vector2(400, 330), new Vector2(739, 696));
            spikeTutorial = new ImageDrawer(spikeTutorialTexture, new Vector2(800, 300), new Vector2(400, 400), new Vector2(400, 400));
            wallJumpTutorial = new ImageDrawer(wallJumpTutorialTexture, new Vector2(1200, 300), new Vector2(400, 400), new Vector2(800, 800));

            finn = new Player(new Vector2(50, 880), finnSpritesheetLeft, finnSpritesheetRight, doorTutorialTexture,jumpParticleDust, new KeyboardHandler());
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gametime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (gameState == GameState.Startscreen)
            {
                switch (startScreen.Update(gametime))
                {
                    case 0:
                            gameState = GameState.InGame;
                        break;
                    case 1: gameState = GameState.Options;
                        break;
                    case 2:
                        Exit();
                        break;
                    default:
                        break;
                }
            }

            if (gameState == GameState.InGame)
            {

                if (finn.goalReached)
                {
                        gameState = GameState.InGame;
                        allLevels.currentLevelInt++;
                        finn.goalReached = false;
                }



                //if (allLevels.currentLevelInt == 3)
                //{
                //    if (!enemyMade)
                //    {
                //        enemyList.Add(new MushroomEnemy(new Vector2(1500, 890), mushroomSpritesheetLeft, mushroomSpritesheetRight));
                //        enemyMade = true;
                //    }
                //}

                allLevels.Update();
                finn.Update(gametime, allLevels.currentLevel.tileArr, enemyList);
                foreach (Enemy enemy in enemyList)
                {
                    if (enemy != null)
                    {
                        enemy.Update(gametime);
                    }
                }
            }


            base.Update(gametime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);


            if (gameState == GameState.Startscreen)
            {
                backdropTitle.Draw(spriteBatch);

                startScreen.Draw(spriteBatch);
                title.Draw(spriteBatch);
            }

            if (gameState == GameState.InGame)
            {
                levelBackground.Draw(spriteBatch);
                if (allLevels.currentLevelInt == 0)
                {
                    if (finn.position.X < 800)
                    {
                        if (moveTutorial != null)
                            moveTutorial.Draw(spriteBatch);
                    }
                    else
                    {
                        if (jumpTutorial != null)
                            jumpTutorial.Draw(spriteBatch);
                    }
                }
                if (allLevels.currentLevelInt == 1)
                    spikeTutorial.Draw(spriteBatch);

                if (allLevels.currentLevelInt == 2)
                    wallJumpTutorial.Draw(spriteBatch);

                allLevels.currentLevel.DrawLevel(spriteBatch);
                foreach (Enemy enemy in enemyList)
                {
                    if (enemy != null)
                    {
                        enemy.Draw(spriteBatch);
                    }
                }
                finn.Draw(spriteBatch);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
