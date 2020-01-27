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
        Texture2D[] optionTextures;
        ImageDrawer[] options;

        IController inputHandler;

        Vector2 mousePos;

        private int menuItems;
        public int position = 0;

        private float currentTime;
        private float lastTime = 0;
        private float currentTime2;
        private float lastTime2 = 0;

        public Menu(Texture2D[] optionTexturesIn, IController inputHandlerIn, int menuItemsIn)
        {
            options = new ImageDrawer[menuItemsIn];
            optionTextures = optionTexturesIn;
            
            for (int i = 0; i < menuItemsIn; i++)
                options[i] = new ImageDrawer(optionTextures[i], new Vector2(750, 650), new Vector2(400, 400), new Vector2(800, 800));

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
                        position = menuItems- 1;
                }
                if (inputHandler.GetButtonPressed() == "Down")
                {
                    if (position < menuItems-1)
                        position++;

                    else if (position == menuItems - 1)
                        position = 0;
                }
                lastTime = currentTime;
            }
            if (mousePos.Y > 685 && mousePos.Y < 750)
            {
                position = 0;
            }
            if (mousePos.Y > 810 && mousePos.Y < 875)
            {
                position = 1;
            }
            if (menuItems >= 3)
            {
                if (mousePos.Y > 935 && mousePos.Y < 935+125)
                    position = 2;
            }
        }
        public int ConfirmChoice(GameTime gametime)
        {

            currentTime2 += (float)gametime.ElapsedGameTime.TotalSeconds;
            if ((currentTime2 - lastTime2) > 1f)
            {
                if (MouseHandler.getMouseButtonClicked() == "LeftClick")
                {
                    lastTime2 = currentTime2;
                    return position;
                }
                if (inputHandler.GetButtonPressed() == "Confirm")
                {
                    lastTime2 = currentTime2;
                    return position;
                }
            }
            return -1;
        }

        public int Update(GameTime gametime)
        {
            Console.WriteLine(mousePos.Y);
            mousePos = MouseHandler.getMousePosition();
            ChangePos(gametime);
            return ConfirmChoice(gametime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            options[position].Draw(spriteBatch);
        }

        public void UpdateInput()
        {
            if (inputHandler is KeyboardHandler)
                inputHandler = new ControllerHandler();

            else
                inputHandler = new KeyboardHandler();
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
        Vector2 spritesheetPos = new Vector2(0, 0);

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

        //spritesheet
        public ImageDrawer(Texture2D imageIn, Vector2 screenPosIn, Vector2 targetSizeIn, Vector2 sourceSizeIn, Vector2 spritesheetPosIn)
        {
            images = new Texture2D[frameAmount];
            images[0] = imageIn;
            currentImage = images[0];
            spritesheetPos = spritesheetPosIn;

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
            spriteBatch.Draw(currentImage, new Rectangle((int)screenPos.X, (int)screenPos.Y, (int)targetSize.X, (int)targetSize.Y), new Rectangle((int)spritesheetPos.X , (int)spritesheetPos.Y, (int)sourceSize.X, (int)sourceSize.Y), Color.White);
        }
    }
}
