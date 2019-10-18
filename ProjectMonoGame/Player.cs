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
    class Player
    {

        private Texture2D spritesheetLeft, spritesheetRight;

        private Vector2 position;

        private Animation animationIdleLeft, animationWalkingLeft, 
                          animationAttackLeft, animationHitLeft, 
                          animationJumpLeft, animationIdleRight,
                          animationWalkingRight, animationAttackRight, 
                          animationHitRight, animationJumpRight;

        private float walkingSpeed = 0;
        private float walkingSpeedAssign = 3;
        private int spriteWidth = 32;
        private int spritesheetWidth = 896;
        private int spriteScale = 5;

        private bool facingRight = true;
        private bool isIdle = true;



        public Player(Vector2 positionIn, Texture2D textureInLeft, Texture2D textureInRight)
        {
            position = positionIn;
            spritesheetLeft = textureInLeft;
            spritesheetRight = textureInRight;

            animationIdleLeft = new Animation();
            animationWalkingLeft = new Animation();
            animationAttackLeft = new Animation();
            animationHitLeft = new Animation();
            animationJumpLeft = new Animation();
            animationIdleRight = new Animation();
            animationWalkingRight = new Animation();
            animationAttackRight = new Animation();
            animationHitRight = new Animation();
            animationJumpRight = new Animation();

            animationIdleLeft.AddFrame(new Rectangle(spritesheetWidth - spriteWidth,0, spriteWidth, spriteWidth));
            animationIdleLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 2),0, spriteWidth, spriteWidth));
            animationIdleLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 3),0, spriteWidth, spriteWidth));
            animationIdleLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 4), 0, spriteWidth, spriteWidth));
            animationIdleLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 5), 0, spriteWidth, spriteWidth));
            animationIdleLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 6), 0, spriteWidth, spriteWidth));
            animationIdleLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 7), 0, spriteWidth, spriteWidth));
            animationIdleLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 8), 0, spriteWidth, spriteWidth));
            animationIdleLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 9), 0, spriteWidth, spriteWidth));

            animationIdleRight.AddFrame(new Rectangle(0, 0, spriteWidth, spriteWidth));
            animationIdleRight.AddFrame(new Rectangle(spriteWidth, 0, spriteWidth, spriteWidth));
            animationIdleRight.AddFrame(new Rectangle(spriteWidth * 2, 0, spriteWidth, spriteWidth));
            animationIdleRight.AddFrame(new Rectangle(spriteWidth * 3, 0, spriteWidth, spriteWidth));
            animationIdleRight.AddFrame(new Rectangle(spriteWidth * 4, 0, spriteWidth, spriteWidth));
            animationIdleRight.AddFrame(new Rectangle(spriteWidth * 5, 0, spriteWidth, spriteWidth));
            animationIdleRight.AddFrame(new Rectangle(spriteWidth * 6, 0, spriteWidth, spriteWidth));
            animationIdleRight.AddFrame(new Rectangle(spriteWidth * 7, 0, spriteWidth, spriteWidth));
            animationIdleRight.AddFrame(new Rectangle(spriteWidth * 8, 0, spriteWidth, spriteWidth));





        }

        public void Update (GameTime gametime)
        {

            if (Keyboard.GetState().IsKeyUp(Keys.Right) || Keyboard.GetState().IsKeyUp(Keys.Left))
            {
                walkingSpeed = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                walkingSpeed = -walkingSpeedAssign;
                isIdle = false;
                facingRight = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                walkingSpeed = walkingSpeedAssign;
                isIdle = false;
                facingRight = true;
            }


            if (walkingSpeed == 0)
            {
                isIdle = true;
                if (facingRight)
                {
                    animationIdleRight.Update(gametime);
                }
                if (!facingRight)
                {
                    animationIdleLeft.Update(gametime);
                }
            }
            


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isIdle)
            {
                if (facingRight)
                    spriteBatch.Draw(spritesheetRight, new Rectangle((int)position.X, (int)position.Y, spriteWidth * spriteScale, spriteWidth * spriteScale), animationIdleRight.currentFrame.SourceRectangle, Color.White);

                if (!facingRight)
                    spriteBatch.Draw(spritesheetLeft, new Rectangle((int)position.X, (int)position.Y, spriteWidth * spriteScale, spriteWidth * spriteScale), animationIdleLeft.currentFrame.SourceRectangle, Color.White);
            }
        }

    }
}
