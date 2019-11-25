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


        IController inputHandler;

        Vector2 screenPosition;

        private int menuItems;
        private int position = 0;

        public Startscreen(Texture2D startSCIn, Texture2D optionsSCIn, IController inputHandlerIn, int menuItemsIn, Vector2 screenPositionIn)
        {
            startSC = startSCIn;
            optionsSC = optionsSCIn;
            inputHandler = inputHandlerIn;
            menuItems = menuItemsIn;
            screenPosition = screenPositionIn;
        }

        public void Update()
        {
            if (inputHandler.GetButtonPressed() == "Up")
            {
                if (position < 0)
                    position--;

                if (position == 0)
                    position = menuItems;
            }
            if (inputHandler.GetButtonPressed() == "Down")
            {
                if (position > menuItems)
                    position++;

                if (position == menuItems)
                    position = 0;
            }
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
                default:
                    break;
            }
        }
    }
}
