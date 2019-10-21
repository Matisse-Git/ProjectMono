using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectMonoGame
{
    class AnimationCreator
    {
        public void CreateAniRight(Animation aniIn, int beginSprite, int endSprite, int aniSpeedIn = 150, int spriteWidthIn = 32)
        {
            for (int i = beginSprite; i < endSprite + 1; i++)
            {
                aniIn.AddFrame(new Rectangle(spriteWidthIn * i, 0, spriteWidthIn, spriteWidthIn));
            }
        }

        public void CreateAniLeft(Animation aniIn, int beginSprite, int endSprite, int aniSpeedIn = 150, int spritesheetWidthIn = 896, int spriteWidthIn = 32)
        {
            for (int i = beginSprite; i < endSprite + 1; i++)
            {
                aniIn.AddFrame(new Rectangle(spritesheetWidthIn - (spriteWidthIn * i), 0, spriteWidthIn, spriteWidthIn));
            }
        }
    }
}
