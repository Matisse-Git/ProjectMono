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
                          animationHitRight, animationJumpRight,
                          animationDieRight, animationDieLeft;

        AnimationCreator aniCreator;

        private IController inputHandler;

        private float walkingSpeed = 0;
        private float walkingSpeedAssign = 8;
        private int spriteWidth = 32;
        private int spritesheetWidth = 896;
        private int spriteScale = 5;

        private bool facingRight = true;
        private bool isIdle = true;
        private bool isJumping;
        private bool isAttacking;
        private bool isDead = false;



        public Player(Vector2 positionIn, Texture2D textureInLeft, Texture2D textureInRight, IController inputHandlerIn)
        {
            inputHandler = inputHandlerIn;
            position = positionIn;
            spritesheetLeft = textureInLeft;
            spritesheetRight = textureInRight;

            aniCreator = new AnimationCreator();

            animationIdleLeft = new Animation(175);
            animationWalkingLeft = new Animation(50);
            animationAttackLeft = new Animation(75);
            animationHitLeft = new Animation(50);
            animationDieLeft = new Animation(75);
            animationJumpLeft = new Animation(100);
            animationIdleRight = new Animation(175);
            animationWalkingRight = new Animation(50);
            animationAttackRight = new Animation(75);
            animationHitRight = new Animation(50);
            animationDieRight = new Animation(75);
            animationJumpRight = new Animation(100);

            aniCreator.CreateAniLeft(animationIdleLeft, 1, 9);
            aniCreator.CreateAniRight(animationIdleRight, 0, 8);
            aniCreator.CreateAniRight(animationWalkingRight, 9, 14);
            aniCreator.CreateAniLeft(animationWalkingLeft, 10, 15);
            aniCreator.CreateAniRight(animationAttackRight, 23, 27);
            aniCreator.CreateAniLeft(animationAttackLeft, 24, 28);
            aniCreator.CreateAniRight(animationJumpRight, 15, 15);
            aniCreator.CreateAniLeft(animationJumpLeft, 16, 16);
            aniCreator.CreateAniLeft(animationHitLeft, 17, 18);
            aniCreator.CreateAniRight(animationHitRight, 16, 17);
            aniCreator.CreateAniLeft(animationDieLeft, 19, 23);
            aniCreator.CreateAniRight(animationDieRight, 18, 22);
        }

        public void Update (GameTime gametime)
        {
            switch (inputHandler.GetButtonPressed())
            {
                case "Left":
                    facingRight = false;
                    Walk(gametime);
                    break;
                case "Right":
                    facingRight = true;
                    Walk(gametime);
                    break;
                case "Jump":
                    isJumping = true;
                    Jump(gametime);
                    break;
                case "Attack":
                    isAttacking = true;
                    DoAttack(gametime);
                    break;
                case "Null":
                    walkingSpeed = 0;
                    isIdle = true;
                    DoNothing(gametime);
                    break;

            }
        }

        private void DoAttack(GameTime gametime)
        {
            if (facingRight)
            {
                if (animationAttackRight.UpdateFull(gametime))
                    isAttacking = false;
            }
            if (!facingRight)
            {
                if (animationAttackLeft.UpdateFull(gametime))
                    isAttacking = false;
            }
            isAttacking = false;
        }

        private void Walk(GameTime gametime)
        {
            if (facingRight)
            {
                walkingSpeed = walkingSpeedAssign;
                animationWalkingRight.Update(gametime);
            }
            if (!facingRight)
            {
                walkingSpeed = -walkingSpeedAssign;
                animationWalkingLeft.Update(gametime);
            }

            isIdle = false;
            position.X += walkingSpeed;

        }

        private void Jump(GameTime gametime)
        {
            if (facingRight)
                animationJumpRight.Update(gametime);
            if (!facingRight)
                animationJumpLeft.Update(gametime);
        }

        private void DoNothing(GameTime gametime)
        {
            if (facingRight)
                animationIdleRight.Update(gametime);
            if (!facingRight)
                animationIdleLeft.Update(gametime);
        }
        private void FallDead(GameTime gametime)
        {
            if (animationDieRight.UpdateFull(gametime))
            {
                isDead = false;
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
