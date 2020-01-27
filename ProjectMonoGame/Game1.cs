using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace ProjectMonoGame
{
    public enum GameState
    {
        Startscreen,
        Options,
        InGame,
        GameOver,
        Fullscreen,
        Input,
        Endscreen
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        LevelList allLevels;
        ScoreHUD score;
        HPBar hpBar;
        Player finn;

        AssetFactory<Texture2D> textureFactory;
        AssetFactory<SoundEffect> sfxFactory;
        AssetFactory<Song> songFactory;

        GameState gameState;

        Menu startScreen;
        Menu endScreen;
        Menu gameOverScreen;
        Menu Options;


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
        ImageDrawer ControllerQuestion;
        List<ImageDrawer> tileCollisionRectangles;

        IController input;

        float currentTime = 0;
        float lastTime = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;

            this.IsMouseVisible = true;

            Content.RootDirectory = "Content";
            textureFactory = new AssetFactory<Texture2D>(Content);
            sfxFactory = new AssetFactory<SoundEffect>(Content);
            songFactory = new AssetFactory<Song>(Content);
            input = new KeyboardHandler();
            Mouse.WindowHandle = Window.Handle;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Textures
            //player
            textureFactory.Add("Textures/Sprites/Player/FinnSpriteLeft", "FinnSpriteLeft");
            textureFactory.Add("Textures/Sprites/Player/FinnSpriteRight", "FinnSpriteRight");

            //tiles
            textureFactory.Add("Textures/Tiles/TileSet", "TileSet");
            textureFactory.Add("Textures/Tiles/SpikeTile", "SpikeTile");
            textureFactory.Add("Textures/Tiles/GateGoal", "GateGoal");

            //menus
            //startscreen
            textureFactory.Add("Textures/Menus/Startscreen/StartSC", "StartSC");
            textureFactory.Add("Textures/Menus/Startscreen/OptionSC", "OptionSC");
            textureFactory.Add("Textures/Menus/Startscreen/ExitSC", "ExitSC");
            textureFactory.Add("Textures/Menus/Startscreen/TitleSC", "TitleSC");

            //endscreen
            textureFactory.Add("Textures/Menus/Endscreen/RestartES", "RestartES");
            textureFactory.Add("Textures/Menus/Endscreen/MainMenuES", "MainMenuES");
            textureFactory.Add("Textures/Menus/Endscreen/CloseGameES", "CloseGameES");
            textureFactory.Add("Textures/Menus/Endscreen/ThanksES", "ThanksES");

            //options
            textureFactory.Add("Textures/Menus/Options/FullscreenTexture", "FullscreenTexture");
            textureFactory.Add("Textures/Menus/Options/FullscreenYesTexture", "FullscreenYesTexture");
            textureFactory.Add("Textures/Menus/Options/FullscreenNoTexture", "FullscreenNoTexture");
            textureFactory.Add("Textures/Menus/Options/DisplayOptionTexture", "DisplayOptionTexture");
            textureFactory.Add("Textures/Menus/Options/InputOptionTexture", "InputOptionTexture");
            textureFactory.Add("Textures/Menus/Options/ControllerTexture", "ControllerTexture");

            //gameover
            textureFactory.Add("Textures/Menus/GameOver/GameOverRestartTexture", "GameOverRestartTexture");
            textureFactory.Add("Textures/Menus/GameOver/GameOverCloseGameTexture", "GameOverCloseGameTexture");
            textureFactory.Add("Textures/Menus/GameOver/GameOverTexture", "GameOverTexture");

            //backgrounds
            textureFactory.Add("Textures/Backgrounds/BackdropTwo", "BackdropTwo");
            textureFactory.Add("Textures/Backgrounds/BackdropTitle", "BackdropTitle");

            //HUD
            textureFactory.Add("Textures/HUD/ScoreNumberTexture", "ScoreNumberTexture");
            textureFactory.Add("Textures/HUD/ScoreTexture", "ScoreTexture");
            textureFactory.Add("Textures/HUD/HPTexture", "HPTexture");

            //tutorials
            textureFactory.Add("Textures/Tutorials/DoorButtonTutorial", "DoorButtonTutorial");
            textureFactory.Add("Textures/Tutorials/MoveTutorial", "MoveTutorial");
            textureFactory.Add("Textures/Tutorials/SpikeTutorial", "SpikeTutorial");
            textureFactory.Add("Textures/Tutorials/WallJumpTutorial", "WallJumpTutorial");
            textureFactory.Add("Textures/Tutorials/JumpTutorial", "JumpTutorial");

            //debug
            textureFactory.Add("Textures/Debug/CollisionRectangle", "CollisionRectangle");


            //SFX
            //player
            sfxFactory.Add("SFX/Finn/FootstepsSFX", "FootstepsSFX");
            sfxFactory.Add("SFX/Finn/LandSFX", "LandSFX");
            sfxFactory.Add("SFX/Finn/JumpSFX", "JumpSFX");
            sfxFactory.Add("SFX/Finn/WallJumpSFX", "WallJumpSFX");
            sfxFactory.Add("SFX/Finn/WallSlideSFX", "WallSlideSFX");

            //tiles
            sfxFactory.Add("SFX/Dying/SpikesSFX", "SpikesSFX");

            //Songs
            //main song
            songFactory.Add("Music/InGame/BackgroundMusic", "BackgroundMusic");

            List<SoundEffect> playerSFX = new List<SoundEffect>();
            playerSFX.Add(sfxFactory.Find("FootstepsSFX"));
            playerSFX.Add(sfxFactory.Find("LandSFX"));
            playerSFX.Add(sfxFactory.Find("JumpSFX"));
            playerSFX.Add(sfxFactory.Find("WallJumpSFX"));
            playerSFX.Add(sfxFactory.Find("WallSlideSFX"));
            playerSFX.Add(sfxFactory.Find("SpikesSFX"));

            MediaPlayer.Play(songFactory.Find("BackgroundMusic"));
            MediaPlayer.Volume -= 0.85f;
            MediaPlayer.IsRepeating = true;

            gameState = GameState.Startscreen;

            //Making Objects
            allLevels = new LevelList(textureFactory.Find("TileSet"), textureFactory.Find("SpikeTile"), textureFactory.Find("GateGoal"));
            levelBackground = new ImageDrawer(textureFactory.Find("BackdropTwo"), new Vector2(0,0), new Vector2(1920,1080), new Vector2(1920,1080));
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D[] SCOptionTextures = new Texture2D[3];
            SCOptionTextures[0] = textureFactory.Find("StartSC");
            SCOptionTextures[1] = textureFactory.Find("OptionSC");
            SCOptionTextures[2] = textureFactory.Find("ExitSC");

            Texture2D[] ESOptionTextures = new Texture2D[3];
            ESOptionTextures[0] = textureFactory.Find("RestartES");
            ESOptionTextures[1] = textureFactory.Find("MainMenuES");
            ESOptionTextures[2] = textureFactory.Find("CloseGameES");

            Texture2D[] GOOptionTextures = new Texture2D[2];
            GOOptionTextures[0] = textureFactory.Find("GameOverRestartTexture");
            GOOptionTextures[1] = textureFactory.Find("GameOverCloseGameTexture");

            Texture2D[] OptionsOptionTextures = new Texture2D[2];
            OptionsOptionTextures[0] = textureFactory.Find("DisplayOptionTexture");
            OptionsOptionTextures[1] = textureFactory.Find("InputOptionTexture");

            startScreen = new Menu(SCOptionTextures, input, 3);
            endScreen = new Menu(ESOptionTextures, input, 3);
            gameOverScreen = new Menu(GOOptionTextures, input, 2);
            Options = new Menu(OptionsOptionTextures, input, 2);


            score = new ScoreHUD(textureFactory.Find("ScoreTexture"), textureFactory.Find("ScoreNumberTexture"));

            hpBar = new HPBar(textureFactory.Find("HPTexture"));

            title = new ImageDrawer(textureFactory.Find("TitleSC"), new Vector2(430, -150), new Vector2(1000,1000), new Vector2(800,800));
            backdropTitle = new ImageDrawer(textureFactory.Find("BackdropTitle"), new Vector2(0, 0), new Vector2(1920, 1080), new Vector2(1920, 1080));
            thanksES = new ImageDrawer(textureFactory.Find("ThanksES"), new Vector2(550, 250), new Vector2(800, 300), new Vector2(800, 300));
            fullscreenQuestion = new ImageDrawer(textureFactory.Find("FullscreenTexture"), new Vector2(550, 250), new Vector2(800, 800), new Vector2(800, 800));
            fullscreenNo = new ImageDrawer(textureFactory.Find("FullscreenNoTexture"), new Vector2(750, 550), new Vector2(400, 400), new Vector2(800, 800));
            fullscreenYes = new ImageDrawer(textureFactory.Find("FullscreenYesTexture"), new Vector2(750, 550), new Vector2(400, 400), new Vector2(800, 800));
            ControllerQuestion = new ImageDrawer(textureFactory.Find("ControllerTexture"), new Vector2(550, 250), new Vector2(800, 800), new Vector2(800, 800));

            gameOver = new ImageDrawer(textureFactory.Find("GameOverTexture"), new Vector2(550, 250), new Vector2(800, 800), new Vector2(800, 800));
            collisionRectangle = new ImageDrawer(textureFactory.Find("CollisionRectangle"), new Vector2(0, 0), new Vector2(0, 0), new Vector2(16, 16));
            tileCollisionRectangles = new List<ImageDrawer>();


            moveTutorial = new ImageDrawer(textureFactory.Find("MoveTutorial"), new Vector2(200, 600), new Vector2(405, 333), new Vector2(405, 333));
            jumpTutorial = new ImageDrawer(textureFactory.Find("JumpTutorial"), new Vector2(900, 400), new Vector2(400, 330), new Vector2(739, 696));
            spikeTutorial = new ImageDrawer(textureFactory.Find("SpikeTutorial"), new Vector2(800, 300), new Vector2(400, 400), new Vector2(400, 400));
            wallJumpTutorial = new ImageDrawer(textureFactory.Find("WallJumpTutorial"), new Vector2(1200, 300), new Vector2(400, 400), new Vector2(800, 800));

            finn = new Player(new Vector2(50, 880), textureFactory.Find("FinnSpriteLeft"), textureFactory.Find("FinnSpriteRight"), textureFactory.Find("DoorButtonTutorial"), hpBar, input, playerSFX);
            allLevels.Update();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gametime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseHandler.Update();

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
                switch (Options.Update(gametime))
                {
                    case 0:
                        gameState = GameState.Fullscreen;
                        break;
                    case 1:
                        gameState = GameState.Input;
                        break;
                    default:
                        break;
                }
                if (input.GetButtonPressed() == "Back")
                {
                    gameState = GameState.Startscreen;
                }

            }

            if (gameState == GameState.Fullscreen)
            {
                currentTime += (float)gametime.ElapsedGameTime.TotalSeconds;
                if ((currentTime - lastTime) > 1f)
                {
                   
                    if (input.GetButtonPressed() == "Confirm")
                    {
                        graphics.ToggleFullScreen();
                        lastTime = currentTime;
                    }
                    if (MouseHandler.getMouseButtonClicked() == "LeftClick")
                    {
                        graphics.ToggleFullScreen();
                        lastTime = currentTime;
                    }
                    else if (input.GetButtonPressed() == "Back")
                    {
                        gameState = GameState.Options;
                    }
                }
            }

            if (gameState == GameState.Input)
            {
                currentTime += (float)gametime.ElapsedGameTime.TotalSeconds;
                if ((currentTime - lastTime) > 1f)
                {

                    if (input.GetButtonPressed() == "Confirm" || MouseHandler.getMouseButtonClicked() == "LeftClick")
                    {
                        if (input is ControllerHandler)
                            input = new KeyboardHandler();

                        else
                            input = new ControllerHandler();

                        startScreen.UpdateInput();
                        endScreen.UpdateInput();
                        Options.UpdateInput();
                        gameOverScreen.UpdateInput();
                        finn.UpdateInput();

                        lastTime = currentTime;
                    }
                    else if (input.GetButtonPressed() == "Back")
                    {
                        gameState = GameState.Options;
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
                        tileCollisionRectangles.Add(new ImageDrawer(textureFactory.Find("CollisionRectangle"), new Vector2(tile.collisionRectangle.X, tile.collisionRectangle.Y), new Vector2(tile.collisionRectangle.Width, tile.collisionRectangle.Height), new Vector2(16, 16)));
                }
                collisionRectangle = new ImageDrawer(textureFactory.Find("CollisionRectangle"), new Vector2(finn.downCollisionRectangle.X, finn.downCollisionRectangle.Y), new Vector2(finn.downCollisionRectangle.Width, finn.downCollisionRectangle.Height), new Vector2(16, 16));
                collisionLeftRectangle = new ImageDrawer(textureFactory.Find("CollisionRectangle"), new Vector2(finn.leftCollisionRectangle.X, finn.leftCollisionRectangle.Y), new Vector2(finn.leftCollisionRectangle.Width, finn.leftCollisionRectangle.Height), new Vector2(16, 16));
                collisionRightRectangle = new ImageDrawer(textureFactory.Find("CollisionRectangle"), new Vector2(finn.rightCollisionRectangle.X, finn.rightCollisionRectangle.Y), new Vector2(finn.rightCollisionRectangle.Width, finn.rightCollisionRectangle.Height), new Vector2(16, 16));
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
                        allLevels.Update();
                        finn.score = 0;
                        gameState = GameState.InGame;
                        break;
                    case 1:
                        allLevels.currentLevelInt = 0;
                        allLevels.Update();
                        finn.score = 0;
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

            if ( gameState == GameState.Fullscreen)
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

            if (gameState == GameState.Options)
            {
                backdropTitle.Draw(spriteBatch);
                Options.Draw(spriteBatch);
            }

            if (gameState == GameState.Input)
            {
                backdropTitle.Draw(spriteBatch);

                if (input is ControllerHandler)
                {
                    fullscreenYes.Draw(spriteBatch);
                }
                else
                    fullscreenNo.Draw(spriteBatch);
                ControllerQuestion.Draw(spriteBatch);
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
