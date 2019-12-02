﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ProjectMonoGame
{
    public enum GameState
    {
        Startscreen,
        Options,
        LoadingScreen,
        InGame,
        Credits
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Backdrop backdropOne;
        Backdrop moveTutorialBackdrop;
        Backdrop JumpTutorialBackdrop;
        LevelList allLevels;

        Texture2D JumpTutorialTexture;
        Texture2D moveTutorialTexture;
        Texture2D mushroomSpritesheetRight;
        Texture2D mushroomSpritesheetLeft;

        GameState gameState;

        Startscreen startScreen;
        LoadingScreen loadingScreen;
        Title title;
        Title backdropTitle;

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
            Texture2D portalTileOne = Content.Load<Texture2D>("PortalEndGoalOne");
            Texture2D portalTileTwo = Content.Load<Texture2D>("PortalEndGoalTwo");
            Texture2D portalTileThree = Content.Load<Texture2D>("PortalEndGoalThree");
            Texture2D portalTileFour = Content.Load<Texture2D>("PortalEndGoalFour");
            Texture2D startSC = Content.Load<Texture2D>("StartSC");
            Texture2D optionsSC = Content.Load<Texture2D>("OptionSC");
            Texture2D exitSC = Content.Load<Texture2D>("ExitSC");
            Texture2D loadingScreenTexture = Content.Load<Texture2D>("LoadingScreen");
            Texture2D titleTexture = Content.Load<Texture2D>("TitleSC");
            Texture2D backdropTitleTexture = Content.Load<Texture2D>("BackdropTitle");

            moveTutorialTexture = Content.Load<Texture2D>("MoveTutorial");
            JumpTutorialTexture = Content.Load<Texture2D>("JumpTutorial");
            Texture2D backdropOneTexture = Content.Load<Texture2D>("BackdropTwo");

            gameState = GameState.Startscreen;

            //Making Objects
            allLevels = new LevelList(platformSpriteSheet, spikeTile, portalTileOne, portalTileTwo, portalTileThree, portalTileFour);
            enemyList = new List<Enemy>();
            backdropOne = new Backdrop(backdropOneTexture, 200, 300, 1, 1920, new Vector2(0, 0));
            spriteBatch = new SpriteBatch(GraphicsDevice);
            startScreen = new Startscreen(startSC, optionsSC,exitSC, new KeyboardHandler(), 2, new Vector2(750,650));
            loadingScreen = new LoadingScreen(loadingScreenTexture, new Vector2(0, 0));
            title = new Title(titleTexture, new Vector2(430, -150), new Vector2(1000,1000), new Vector2(800,800));
            backdropTitle = new Title(backdropTitleTexture, new Vector2(0, 0), new Vector2(1920, 1080), new Vector2(1920, 1080));
            finn = new Player(new Vector2(50, 880), finnSpritesheetLeft, finnSpritesheetRight, jumpParticleDust, new KeyboardHandler());
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

            if (gameState == GameState.InGame || gameState == GameState.LoadingScreen)
            {


                if (finn.goalReached)
                {
                    gameState = GameState.LoadingScreen;
                    if (loadingScreen.Load(gametime, 1))
                    {
                        gameState = GameState.InGame;
                        allLevels.currentLevelInt++;
                        finn.goalReached = false;
                    }
                }

                if (allLevels.currentLevelInt == 0)
                {
                    if (finn.position.X >= 800)
                    {
                        moveTutorialBackdrop = null;
                        JumpTutorialBackdrop = new Backdrop(JumpTutorialTexture, 4000, 4000, 1, 1500, new Vector2(-2700, -1500));
                    }
                    if (finn.position.X < 800)
                    {
                        JumpTutorialBackdrop = null;
                        moveTutorialBackdrop = new Backdrop(moveTutorialTexture, 4000, 4000, 1, 3000, new Vector2(-200, -800));
                    }
                }

                if (allLevels.currentLevelInt == 3)
                {
                    if (!enemyMade)
                    {
                        enemyList.Add(new MushroomEnemy(new Vector2(1500, 890), mushroomSpritesheetLeft, mushroomSpritesheetRight));
                        enemyMade = true;
                    }
                }

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
                backdropOne.Draw(spriteBatch);
                if (allLevels.currentLevelInt == 0)
                {
                    if (moveTutorialBackdrop != null)
                        moveTutorialBackdrop.Draw(spriteBatch);
                    if (JumpTutorialBackdrop != null)
                        JumpTutorialBackdrop.Draw(spriteBatch);
                }
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

            if (gameState == GameState.LoadingScreen)
            {
                loadingScreen.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
