using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMonoGame
{
    class Startscreen
    {

        Texture2D startSC;
        Texture2D optionsSC;
        Texture2D exitSC;

        IController inputHandler;

        Vector2 screenPosition;

        private int menuItems;
        private int position = 0;

        public Startscreen(Texture2D startSCIn, Texture2D optionsSCIn, Texture2D exitSCIn, IController inputHandlerIn, int menuItemsIn, Vector2 screenPositionIn)
        private float currentTime;
        private float lastTime = 0;

        {
            startSC = startSCIn;
            optionsSC = optionsSCIn;
            exitSC = exitSCIn;
            inputHandler = inputHandlerIn;
            menuItems = menuItemsIn;
            screenPosition = screenPositionIn;
        }

        public void ChangePos(GameTime gametime)
        {
            currentTime += (float)gametime.ElapsedGameTime.TotalSeconds;
            if ((currentTime - lastTime) > 0.1f)
            {
                if (inputHandler.GetButtonPressed() == "Up")
                {
                    if (position > 0)
                        position--;

                    else if (position == 0)
                        position = menuItems;
                }
                if (inputHandler.GetButtonPressed() == "Down")
                {
                    if (position < menuItems)
                        position++;

                    else if (position == menuItems)
                        position = 0;
                }
                lastTime = currentTime;
            }
        }
        public int ConfirmChoice()
        {
            if (inputHandler.GetButtonPressed() == "Confirm")
            {
                return position;
            }
            return -1;
        }

        public int Update(GameTime gametime)
        {
            ChangePos(gametime);
            return ConfirmChoice();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (position)
            {
                case 0:
                    spriteBatch.Draw(startSC, new Rectangle((int)screenPosition.X, (int)screenPosition.Y, 800, 800), new Rectangle(0, 0, 800, 800), Color.White);
                    break;
                case 1:
                    spriteBatch.Draw(optionsSC, new Rectangle((int)screenPosition.X, (int)screenPosition.Y, 800, 800), new Rectangle(0, 0, 800, 800), Color.White);
                    break;
                case 2:
                    spriteBatch.Draw(exitSC, new Rectangle((int)screenPosition.X, (int)screenPosition.Y, 800, 800), new Rectangle(0, 0, 800, 800), Color.White);
                    break;
                default:
                    break;
            }
        }
    }

    class LoadingScreen
    {
        private float currentTime;
        private float lastTime = 0;

        Vector2 screenPosition;

        private Texture2D loadingTexture;

        public LoadingScreen(Texture2D loadingTextureIn, Vector2 screenPositionIn)
        {
            loadingTexture = loadingTextureIn;
            screenPosition = screenPositionIn;
        }

        public bool Load(GameTime gametime, float loadingTimeIn)
        {
            currentTime += (float)gametime.ElapsedGameTime.TotalSeconds;
            if ((currentTime - lastTime) > loadingTimeIn)
            {
                currentTime = 0;
                return true;
            }
            return false;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(loadingTexture, new Rectangle((int)screenPosition.X, (int)screenPosition.Y, 2000, 1200), new Rectangle(0, 0, 2000 ,1244), Color.White);
        }
    }
}
