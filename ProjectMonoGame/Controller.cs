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
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    return "LeftJump";

                else
                    return "Left";
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    return "RightJump";

                else
                    return "Right";
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                return "Jump";

            if (Keyboard.GetState().IsKeyDown(Keys.X))
                return "Attack";

            else
                return "Null";
        }
    }
}
