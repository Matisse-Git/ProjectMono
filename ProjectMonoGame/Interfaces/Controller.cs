using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectMonoGame
{
    interface IController
    {
        string GetButtonPressed();
    }

    class KeyboardHandler : IController
    {
        public string GetButtonPressed()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    return "LeftJump";

                else
                    return "Left";
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    return "RightJump";

                else
                    return "Right";
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Space))
                return "Jump";

            else if (Keyboard.GetState().IsKeyDown(Keys.X))
                return "Attack";

            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                return "Down";

            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                return "Up";

            else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                return "Confirm";

            else if (Keyboard.GetState().IsKeyDown(Keys.Back))
                return "Back";

            else
                return "Null";
        }
    }
}
