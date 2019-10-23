using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ProjectMonoGame
{
    class BatchDrawer
    {
        public Texture2D spritesheetRight { get; set; }
        public Texture2D spritesheetLeft { get; set; }
        public int spriteScale { get; set; }
        public int spriteWidth { get; set; }
        public SpriteBatch spriteBatch { get; set; }

        public BatchDrawer(SpriteBatch spriteBatchIn, Texture2D spritesheetInLeft, Texture2D spritesheetInRight, int spriteScaleIn, int spriteWidthIn = 32)
        {
            spriteBatch = spriteBatchIn;
            spritesheetLeft = spritesheetInLeft;
            spritesheetRight = spritesheetInRight;
            spriteScale = spriteScaleIn;
            spriteWidth = spriteWidthIn;
        }
        public void DrawAni(bool facingRightIn,  Vector2 positionIn, Animation aniInRight, Animation aniInLeft)
        {
            if (facingRightIn)
                spriteBatch.Draw(spritesheetRight, new Rectangle((int)positionIn.X, (int)positionIn.Y, spriteWidth * spriteScale, spriteWidth * spriteScale), aniInRight.currentFrame.SourceRectangle, Color.White);
            if (!facingRightIn)
                spriteBatch.Draw(spritesheetLeft, new Rectangle((int)positionIn.X, (int)positionIn.Y, spriteWidth * spriteScale, spriteWidth * spriteScale), aniInLeft.currentFrame.SourceRectangle, Color.White);

        }
    }
}
