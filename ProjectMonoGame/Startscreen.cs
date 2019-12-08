using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMonoGame
{
    class Menu
    {
        ImageDrawer startSCI;
        ImageDrawer optionsSCI;
        ImageDrawer exitSCI;

        IController inputHandler;

        private int menuItems;
        private int position = 0;

        private float currentTime;
        private float lastTime = 0;

        public Menu(Texture2D startSCIn, Texture2D optionsSCIn, Texture2D exitSCIn, IController inputHandlerIn, int menuItemsIn)
        {
            startSCI = new ImageDrawer(startSCIn, new Vector2(750, 650), new Vector2(400, 400), new Vector2(800, 800));
            optionsSCI = new ImageDrawer(optionsSCIn, new Vector2(750, 650), new Vector2(400, 400), new Vector2(800, 800));
            exitSCI = new ImageDrawer(exitSCIn, new Vector2(750, 650), new Vector2(400, 400), new Vector2(800, 800));

            inputHandler = inputHandlerIn;
            menuItems = menuItemsIn;
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
                    startSCI.Draw(spriteBatch);
                    break;
                case 1:
                    optionsSCI.Draw(spriteBatch);
                    break;
                case 2:
                    exitSCI.Draw(spriteBatch);
                    break;
                default:
                    break;
            }
        }
    }

    class ImageDrawer
    {
        Texture2D[] images;
        Texture2D currentImage;
        int currentImageInt = 0;
        int frameAmount = 1;

        Vector2 screenPos;
        Vector2 targetSize;
        Vector2 sourceSize;

        public ImageDrawer(Texture2D imageIn, Vector2 screenPosIn, Vector2 targetSizeIn, Vector2 sourceSizeIn)
        {
            images = new Texture2D[frameAmount];
            images[0] = imageIn;
            currentImage = images[0];

            screenPos = screenPosIn;

            targetSize = targetSizeIn;
            sourceSize = sourceSizeIn;
        
        }

        //animated image
        public ImageDrawer(Texture2D[] imagesIn, Vector2 screenPosIn, Vector2 targetSizeIn, Vector2 sourceSizeIn, int frameAmountIn)
        {
            frameAmount = frameAmountIn;
            images = imagesIn;
            currentImage = images[0];

            screenPos = screenPosIn;

            targetSize = targetSizeIn;
            sourceSize = sourceSizeIn;
        }

        public void Update()
        {
            if ((currentImageInt + 1) > frameAmount)            
                currentImageInt++;

            else            
                currentImageInt = 0;

            currentImage = images[currentImageInt];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentImage, new Rectangle((int)screenPos.X, (int)screenPos.Y, (int)targetSize.X, (int)targetSize.Y), new Rectangle(0, 0, (int)sourceSize.X, (int)sourceSize.Y), Color.White);
        }
    }
}
