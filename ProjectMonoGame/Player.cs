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
        private float walkingSpeedAssign = 8;
        private int spriteWidth = 32;
        private int spritesheetWidth = 896;
        private int spriteScale = 5;
        private int animationDelay = 0;

        private bool facingRight = true;
        private bool isIdle = true;
        private bool isJumping;
        private bool isAttacking;



        public Player(Vector2 positionIn, Texture2D textureInLeft, Texture2D textureInRight)
        {
            position = positionIn;
            spritesheetLeft = textureInLeft;
            spritesheetRight = textureInRight;

            animationIdleLeft = new Animation(175);
            animationWalkingLeft = new Animation(50);
            animationAttackLeft = new Animation(400);
            //animationHitLeft = new Animation();
            animationJumpLeft = new Animation(100);
            animationIdleRight = new Animation(175);
            animationWalkingRight = new Animation(50);
            animationAttackRight = new Animation(400);
            //animationHitRight = new Animation();
            animationJumpRight = new Animation(100);

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

            animationWalkingRight.AddFrame(new Rectangle(spriteWidth * 9, 0, spriteWidth, spriteWidth));
            animationWalkingRight.AddFrame(new Rectangle(spriteWidth * 10, 0, spriteWidth, spriteWidth));
            animationWalkingRight.AddFrame(new Rectangle(spriteWidth * 11, 0, spriteWidth, spriteWidth));
            animationWalkingRight.AddFrame(new Rectangle(spriteWidth * 12, 0, spriteWidth, spriteWidth));
            animationWalkingRight.AddFrame(new Rectangle(spriteWidth * 13, 0, spriteWidth, spriteWidth));
            animationWalkingRight.AddFrame(new Rectangle(spriteWidth * 14, 0, spriteWidth, spriteWidth));

            animationWalkingLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 10), 0, spriteWidth, spriteWidth));
            animationWalkingLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 11), 0, spriteWidth, spriteWidth));
            animationWalkingLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 12), 0, spriteWidth, spriteWidth));
            animationWalkingLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 13), 0, spriteWidth, spriteWidth));
            animationWalkingLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 14), 0, spriteWidth, spriteWidth));
            animationWalkingLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 15), 0, spriteWidth, spriteWidth));

            animationAttackRight.AddFrame(new Rectangle(spriteWidth * 23, 0, spriteWidth, spriteWidth));
            animationAttackRight.AddFrame(new Rectangle(spriteWidth * 24, 0, spriteWidth, spriteWidth));
            animationAttackRight.AddFrame(new Rectangle(spriteWidth * 25, 0, spriteWidth, spriteWidth));
            animationAttackRight.AddFrame(new Rectangle(spriteWidth * 26, 0, spriteWidth, spriteWidth));
            animationAttackRight.AddFrame(new Rectangle(spriteWidth * 27, 0, spriteWidth, spriteWidth));

            animationAttackLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 24), 0, spriteWidth, spriteWidth));
            animationAttackLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 25), 0, spriteWidth, spriteWidth));
            animationAttackLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 26), 0, spriteWidth, spriteWidth));
            animationAttackLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 27), 0, spriteWidth, spriteWidth));
            animationAttackLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 28), 0, spriteWidth, spriteWidth));

            animationJumpRight.AddFrame(new Rectangle(spriteWidth * 15, 0, spriteWidth, spriteWidth));

            animationJumpLeft.AddFrame(new Rectangle(spritesheetWidth - (spriteWidth * 16), 0, spriteWidth, spriteWidth));

        }

        public void Update (GameTime gametime)
        {

            if ((Keyboard.GetState().IsKeyUp(Keys.Right) || Keyboard.GetState().IsKeyUp(Keys.Left) || Keyboard.GetState().IsKeyUp(Keys.X)) && !isJumping)
            {
                walkingSpeed = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                walkingSpeed = -walkingSpeedAssign;
                animationWalkingLeft.Update(gametime);
                isIdle = false;
                facingRight = false;

                position.X += walkingSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                walkingSpeed = walkingSpeedAssign;
                animationWalkingRight.Update(gametime);
                isIdle = false;
                facingRight = true;

                position.X += walkingSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                isJumping = true;
                if (facingRight)
                    animationJumpRight.Update(gametime);
                if (!facingRight)
                    animationJumpLeft.Update(gametime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                isAttacking = true;
                for (int i = 0; i < animationAttackLeft.frames.Count; i++)
                {
                    if (facingRight)
                        animationAttackRight.Update(gametime);
                    if (!facingRight)
                        animationAttackLeft.Update(gametime);
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.X))
            {
                isAttacking = false;
            }

            if (walkingSpeed == 0)
            {
                isIdle = true;
                if (facingRight)
                    animationIdleRight.Update(gametime);
                if (!facingRight)
                    animationIdleLeft.Update(gametime);
            }
            


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isAttacking)
            {
                if (facingRight)
                    spriteBatch.Draw(spritesheetRight, new Rectangle((int)position.X, (int)position.Y, spriteWidth * spriteScale, spriteWidth * spriteScale), animationAttackRight.currentFrame.SourceRectangle, Color.White);
                if (!facingRight)
                    spriteBatch.Draw(spritesheetLeft, new Rectangle((int)position.X, (int)position.Y, spriteWidth * spriteScale, spriteWidth * spriteScale), animationAttackLeft.currentFrame.SourceRectangle, Color.White);
            }

            if (isIdle && !isAttacking)
            {
                if (facingRight)
                    spriteBatch.Draw(spritesheetRight, new Rectangle((int)position.X, (int)position.Y, spriteWidth * spriteScale, spriteWidth * spriteScale), animationIdleRight.currentFrame.SourceRectangle, Color.White);

                if (!facingRight)
                    spriteBatch.Draw(spritesheetLeft, new Rectangle((int)position.X, (int)position.Y, spriteWidth * spriteScale, spriteWidth * spriteScale), animationIdleLeft.currentFrame.SourceRectangle, Color.White);
            }

            if (!isIdle)
            {
                if (!isJumping && !isAttacking)
                {
                    if (facingRight)
                        spriteBatch.Draw(spritesheetRight, new Rectangle((int)position.X, (int)position.Y, spriteWidth * spriteScale, spriteWidth * spriteScale), animationWalkingRight.currentFrame.SourceRectangle, Color.White);
                    if (!facingRight)
                        spriteBatch.Draw(spritesheetLeft, new Rectangle((int)position.X, (int)position.Y, spriteWidth * spriteScale, spriteWidth * spriteScale), animationWalkingLeft.currentFrame.SourceRectangle, Color.White);
                }

                if (isJumping && !isAttacking)
                {
                    if (facingRight)
                        spriteBatch.Draw(spritesheetRight, new Rectangle((int)position.X, (int)position.Y, spriteWidth * spriteScale, spriteWidth * spriteScale), animationJumpRight.currentFrame.SourceRectangle, Color.White);
                    if (!facingRight)
                        spriteBatch.Draw(spritesheetLeft, new Rectangle((int)position.X, (int)position.Y, spriteWidth * spriteScale, spriteWidth * spriteScale), animationJumpLeft.currentFrame.SourceRectangle, Color.White);
                }
            }
        }

    }
}
