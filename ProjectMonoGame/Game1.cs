using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace ProjectMonoGame
{
    public enum GameState
    {
        Startscreen,
        Options,
        InGame,
        GameOver,
        Endscreen
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        LevelList allLevels;

        Texture2D mushroomSpritesheetRight;
        Texture2D mushroomSpritesheetLeft;
        Texture2D collisionRectangleTexture;

        GameState gameState;

        Menu startScreen;
        Menu endScreen;
        Menu gameOverScreen;
        ImageDrawer title;
        ImageDrawer thanksES;
        ImageDrawer fullscreenQuestion;
        ImageDrawer fullscreenYes;
        ImageDrawer fullscreenNo;
        ImageDrawer gameOver;
        ImageDrawer backdropTitle;
        ImageDrawer levelBackground;
        ImageDrawer moveTutorial;
        ImageDrawer jumpTutorial;
        ImageDrawer spikeTutorial;
        ImageDrawer wallJumpTutorial;
        ImageDrawer collisionRectangle;
        ImageDrawer collisionRightRectangle;
        ImageDrawer collisionLeftRectangle;
        List<ImageDrawer> tileCollisionRectangles;
        SoundEffect runningSFX;

        float currentTime = 0;
        float lastTime = 0;

        ScoreHUD score;
        HPBar hpBar;

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

            mushroomSpritesheetLeft = Content.Load<Texture2D>("MushroomLeft");
            mushroomSpritesheetRight = Content.Load<Texture2D>("MushroomRight");

            Texture2D platformSpriteSheet = Content.Load<Texture2D>("TileSet");
            Texture2D spikeTile = Content.Load<Texture2D>("SpikeTile");
            Texture2D startSC = Content.Load<Texture2D>("StartSC");
            Texture2D optionsSC = Content.Load<Texture2D>("OptionSC");
            Texture2D exitSC = Content.Load<Texture2D>("ExitSC");
            Texture2D titleTexture = Content.Load<Texture2D>("TitleSC");
            Texture2D backdropTitleTexture = Content.Load<Texture2D>("BackdropTitle");
            Texture2D goalTexture = Content.Load<Texture2D>("GateGoal");
            Texture2D doorTutorialTexture = Content.Load<Texture2D>("DoorButtonTutorial");

            Texture2D restartES = Content.Load<Texture2D>("RestartES");
            Texture2D mainMenuES = Content.Load<Texture2D>("MainMenuES");
            Texture2D closeGameES = Content.Load<Texture2D>("CloseGameES");
            Texture2D thanksESTexture = Content.Load<Texture2D>("ThanksES");
            Texture2D HPTexture = Content.Load<Texture2D>("HPTexture");
            Texture2D scoreTexture = Content.Load<Texture2D>("ScoreTexture");
            Texture2D scoreNumberTexture = Content.Load<Texture2D>("ScoreNumberTexture");
            Texture2D fullscreenTexture = Content.Load<Texture2D>("FullscreenTexture");
            Texture2D fullscreenYesTexture = Content.Load<Texture2D>("FullscreenYesTexture");
            Texture2D fullscreenNoTexture = Content.Load<Texture2D>("FullscreenNoTexture");
            Texture2D gameOverRestartTexture = Content.Load<Texture2D>("GameOverRestartTexture");
            Texture2D gameOverCloseGameTexture = Content.Load<Texture2D>("GameOverCloseGameTexture");
            Texture2D gameOverTexture = Content.Load<Texture2D>("GameOverTexture");

            Texture2D moveTutorialTexture = Content.Load<Texture2D>("MoveTutorial");
            Texture2D JumpTutorialTexture = Content.Load<Texture2D>("JumpTutorial");
            Texture2D spikeTutorialTexture = Content.Load<Texture2D>("SpikeTutorial");
            Texture2D wallJumpTutorialTexture = Content.Load<Texture2D>("WallJumpTutorial");
            Texture2D backdropOneTexture = Content.Load<Texture2D>("BackdropTwo");
            collisionRectangleTexture = Content.Load<Texture2D>("CollisionRectangle");

            runningSFX = Content.Load<SoundEffect>("RunningSFX");
            List<SoundEffect> playerSFX = new List<SoundEffect>();
            playerSFX.Add(runningSFX);

            gameState = GameState.Startscreen;

            //Making Objects
            allLevels = new LevelList(platformSpriteSheet, spikeTile, goalTexture);
            levelBackground = new ImageDrawer(backdropOneTexture, new Vector2(0,0), new Vector2(1920,1080), new Vector2(1920,1080));
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D[] SCOptionTextures = new Texture2D[3];
            SCOptionTextures[0] = startSC;
            SCOptionTextures[1] = optionsSC;
            SCOptionTextures[2] = exitSC;

            Texture2D[] ESOptionTextures = new Texture2D[3];
            ESOptionTextures[0] = restartES;
            ESOptionTextures[1] = mainMenuES;
            ESOptionTextures[2] = closeGameES;

            Texture2D[] GOOptionTextures = new Texture2D[2];
            GOOptionTextures[0] = gameOverRestartTexture;
            GOOptionTextures[1] = gameOverCloseGameTexture;

            startScreen = new Menu(SCOptionTextures, new KeyboardHandler(), 3);
            endScreen = new Menu(ESOptionTextures, new KeyboardHandler(), 3);
            gameOverScreen = new Menu(GOOptionTextures, new KeyboardHandler(), 2);

            score = new ScoreHUD(scoreTexture, scoreNumberTexture);

            hpBar = new HPBar(HPTexture);

            title = new ImageDrawer(titleTexture, new Vector2(430, -150), new Vector2(1000,1000), new Vector2(800,800));
            backdropTitle = new ImageDrawer(backdropTitleTexture, new Vector2(0, 0), new Vector2(1920, 1080), new Vector2(1920, 1080));
            thanksES = new ImageDrawer(thanksESTexture, new Vector2(550, 250), new Vector2(800, 300), new Vector2(800, 300));
            fullscreenQuestion = new ImageDrawer(fullscreenTexture, new Vector2(550, 250), new Vector2(800, 800), new Vector2(800, 800));
            fullscreenNo = new ImageDrawer(fullscreenNoTexture, new Vector2(750, 550), new Vector2(400, 400), new Vector2(800, 800));
            fullscreenYes = new ImageDrawer(fullscreenYesTexture, new Vector2(750, 550), new Vector2(400, 400), new Vector2(800, 800));
            gameOver = new ImageDrawer(gameOverTexture, new Vector2(550, 250), new Vector2(800, 800), new Vector2(800, 800));
            collisionRectangle = new ImageDrawer(collisionRectangleTexture, new Vector2(0, 0), new Vector2(0, 0), new Vector2(16, 16));
            tileCollisionRectangles = new List<ImageDrawer>();


            moveTutorial = new ImageDrawer(moveTutorialTexture, new Vector2(200, 600), new Vector2(405, 333), new Vector2(405, 333));
            jumpTutorial = new ImageDrawer(JumpTutorialTexture, new Vector2(900, 400), new Vector2(400, 330), new Vector2(739, 696));
            spikeTutorial = new ImageDrawer(spikeTutorialTexture, new Vector2(800, 300), new Vector2(400, 400), new Vector2(400, 400));
            wallJumpTutorial = new ImageDrawer(wallJumpTutorialTexture, new Vector2(1200, 300), new Vector2(400, 400), new Vector2(800, 800));

            finn = new Player(new Vector2(50, 880), finnSpritesheetLeft, finnSpritesheetRight, doorTutorialTexture, hpBar, new KeyboardHandler(), playerSFX);
            allLevels.Update();
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

            if (gameState == GameState.Options)
            {
                currentTime += (float)gametime.ElapsedGameTime.TotalSeconds;
                if ((currentTime - lastTime) > 1f)
                {
                    KeyboardHandler keyboardHandler = new KeyboardHandler();
                    if (keyboardHandler.GetButtonPressed() == "Confirm")
                    {
                        graphics.ToggleFullScreen();
                        lastTime = currentTime;
                    }
                    else if(keyboardHandler.GetButtonPressed() == "Back")
                    {
                        gameState = GameState.Startscreen;
                    }
                }
            }

            if (gameState == GameState.InGame)
            {

                if (finn.goalReached)
                {
                        gameState = GameState.InGame;
                        allLevels.currentLevelInt++;
                        finn.goalReached = false;
                        allLevels.Update();
                }

                if (allLevels.currentLevelInt == (allLevels.levelAmount))
                {
                    gameState = GameState.Endscreen;
                }

                finn.Update(gametime, allLevels.currentLevel.tileArr);
                if (finn.resetLevels)
                {
                    allLevels.currentLevelInt = 0;
                    allLevels.Update();
                    finn.score = 0;
                    score.Update(finn.score);
                    gameState = GameState.GameOver;
                    finn.resetLevels = false;
                }
                score.Update(finn.score);
                tileCollisionRectangles.Clear();
                foreach (Tile tile in allLevels.currentLevel.tileArr)
                {
                    if (tile != null)
                        tileCollisionRectangles.Add(new ImageDrawer(collisionRectangleTexture, new Vector2(tile.collisionRectangle.X, tile.collisionRectangle.Y), new Vector2(tile.collisionRectangle.Width, tile.collisionRectangle.Height), new Vector2(16, 16)));
                }
                collisionRectangle = new ImageDrawer(collisionRectangleTexture, new Vector2(finn.downCollisionRectangle.X, finn.downCollisionRectangle.Y), new Vector2(finn.downCollisionRectangle.Width, finn.downCollisionRectangle.Height), new Vector2(16, 16));
                collisionLeftRectangle = new ImageDrawer(collisionRectangleTexture, new Vector2(finn.leftCollisionRectangle.X, finn.leftCollisionRectangle.Y), new Vector2(finn.leftCollisionRectangle.Width, finn.leftCollisionRectangle.Height), new Vector2(16, 16));
                collisionRightRectangle = new ImageDrawer(collisionRectangleTexture, new Vector2(finn.rightCollisionRectangle.X, finn.rightCollisionRectangle.Y), new Vector2(finn.rightCollisionRectangle.Width, finn.rightCollisionRectangle.Height), new Vector2(16, 16));
            }

            if (gameState == GameState.GameOver)
            {
                switch (gameOverScreen.Update(gametime))
                {
                    case 0:
                        gameState = GameState.InGame;
                        break;
                    case 1:
                        Exit();
                        break;
                }
            }

            if (gameState == GameState.Endscreen)
            {
                switch (endScreen.Update(gametime))
                {
                    case 0:
                        allLevels.currentLevelInt = 0;
                        gameState = GameState.InGame;
                        break;
                    case 1:
                        allLevels.currentLevelInt = 0;
                        gameState = GameState.Startscreen;
                        break;
                    case 2:
                        Exit();
                        break;
                    default:
                        break;
                }
            }

                base.Update(gametime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);


            if (gameState == GameState.Startscreen)
            {
                backdropTitle.Draw(spriteBatch);

                startScreen.Draw(spriteBatch);
                title.Draw(spriteBatch);
            }

            if ( gameState == GameState.Options)
            {
                backdropTitle.Draw(spriteBatch);

                if (graphics.IsFullScreen)
                {
                    fullscreenYes.Draw(spriteBatch);
                }
                else
                    fullscreenNo.Draw(spriteBatch);
                fullscreenQuestion.Draw(spriteBatch);

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

                //foreach (ImageDrawer cs in tileCollisionRectangles)
                //{
                //    cs.Draw(spriteBatch);
                //}
                score.Draw(spriteBatch);
                finn.Draw(spriteBatch);
                //collisionRectangle.Draw(spriteBatch);
                //collisionLeftRectangle.Draw(spriteBatch);
                //collisionRightRectangle.Draw(spriteBatch);

            }

            if (gameState == GameState.GameOver)
            {
                backdropTitle.Draw(spriteBatch);

                gameOverScreen.Draw(spriteBatch);
                gameOver.Draw(spriteBatch);
            }

            if (gameState == GameState.Endscreen)
            {
                backdropTitle.Draw(spriteBatch);
                thanksES.Draw(spriteBatch);
                endScreen.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
