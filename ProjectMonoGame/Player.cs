﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ProjectMonoGame
{
    class Player : IMultipleCollidable, IGravitational
    {
        public Rectangle upCollisionRectangle { get; set; }
        public Rectangle downCollisionRectangle { get; set; }
        public Rectangle leftCollisionRectangle { get; set; }
        public Rectangle rightCollisionRectangle { get; set; }


        private Texture2D spritesheetLeft, spritesheetRight;

        public Vector2 position;

        private Animation animationIdleLeft, animationWalkingLeft, 
                          animationAttackLeft, animationHitLeft, 
                          animationJumpLeft, animationIdleRight,
                          animationWalkingRight, animationAttackRight, 
                          animationHitRight, animationJumpRight,
                          animationDieRight, animationDieLeft;

        private AnimationCreator aniCreator;

        private BatchDrawer finnDrawer;

        private IController inputHandler;

        private CollisionManager colliManager;

        public float gravity { get; set; } = 4;
        private float walkingSpeed = 0;
        private float walkingSpeedAssign = 8;
        private int spriteWidth = 32;
        private int spritesheetWidth = 896;
        private int spriteScale = 5;
        private int offset = 20;
        private double lastJumpTime = 0;
        private double jumpHeight = 20;

        private bool leftColliding;
        private bool rightColliding;
        private bool facingRight = true;
        private bool isIdle = true;
        private bool isJumping;
        private bool isGrounded = false;
        private bool isAttacking;
        private bool isDead = false;
        private bool isFalling;



        public Player(Vector2 positionIn, Texture2D textureInLeft, Texture2D textureInRight, IController inputHandlerIn)
        {
            inputHandler = inputHandlerIn;
            position = positionIn;
            spritesheetLeft = textureInLeft;
            spritesheetRight = textureInRight;

            aniCreator = new AnimationCreator();

            colliManager = new CollisionManager();

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
            downCollisionRectangle = new Rectangle((int)position.X + spriteWidth + 30, (int)position.Y, (spriteWidth * spriteScale) / 5, spriteWidth / 2 * spriteScale);
            rightCollisionRectangle = new Rectangle((int)position.X + ((spriteWidth * spriteScale) / 8)  * 4, (int)position.Y - ((spriteWidth * spriteScale) / 8), (spriteWidth * spriteScale) / 4, (spriteWidth * spriteScale) / 2);
            leftCollisionRectangle = new Rectangle((int)position.X + ((spriteWidth * spriteScale) / 8) * 2, (int)position.Y - ((spriteWidth * spriteScale) / 8), (spriteWidth * spriteScale) / 4, (spriteWidth * spriteScale) / 2);

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
                case "JumpKeyUp":

                    break;
                case "LeftJump":
                case "RightJump":
                    isJumping = true;
                    Jump(gametime);
                    Walk(gametime);
                    break;
                case "Attack":
                    isAttacking = true;
                    break;
                case "Null":
                    walkingSpeed = 0;
                    isIdle = true;
                    DoNothing(gametime);
                    break;
            }

            //When using a full animation by one press of the button, you have to use it like this, otherwise it will not work!!
            if (isAttacking)
            {
                DoAttack(gametime);
            }

            if (!isGrounded)
            {
                ApplyGravity();
            }


            Console.WriteLine(rightColliding);
            rightColliding = false;
            leftColliding = false;

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
        }

        private void Walk(GameTime gametime)
        {
            if (isFalling)
            {
                walkingSpeedAssign = 2;
            }
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

            if ((!rightColliding && facingRight) || (!leftColliding && !facingRight))
            {
                    position.X += walkingSpeed;
            }
            walkingSpeedAssign = 8;
        }

        private void Jump(GameTime gametime)
        {
            if (jumpHeight > 0)
            {
                jumpHeight -= 0.5;
            }
            position.Y -= (float)jumpHeight;
            isGrounded = false;
            if (facingRight)
                animationJumpRight.Update(gametime);
            if (!facingRight)
                animationJumpLeft.Update(gametime);

            lastJumpTime = gametime.ElapsedGameTime.Milliseconds;
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
        public void ApplyGravity()
        {
            if (gravity < 15)
            {
                gravity += 0.3f;
            }
            position.Y += gravity;
        }

        public bool CheckCollision(Rectangle rectangleIn)
        {

            if (colliManager.CheckCollider(rightCollisionRectangle, rectangleIn))
            {
                rightColliding = true;
            }

            if (colliManager.CheckCollider(leftCollisionRectangle, rectangleIn))
            {
                leftColliding = true;
            }

            if (gravity >= 12)
            {
                isFalling = true;
            }

            if (colliManager.CheckCollider(downCollisionRectangle, rectangleIn))
            {
                isGrounded = true;
                isJumping = false;
                jumpHeight = 15;
                gravity = 0;
                isFalling = false;
                return true;
            }
            else
            {
                isGrounded = false;
                isJumping = true;
                return false;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            finnDrawer = new BatchDrawer(spriteBatch, spritesheetLeft, spritesheetRight, spriteScale);

            if (isAttacking)
                finnDrawer.DrawAni(facingRight, position, animationAttackRight, animationAttackLeft);

            if (isIdle && !isAttacking)
            {
                finnDrawer.DrawAni(facingRight, position, animationIdleRight, animationIdleLeft);
            }

            if (!isIdle)
            {
                if (!isJumping && isGrounded && !isAttacking)
                {
                    finnDrawer.DrawAni(facingRight, position, animationWalkingRight, animationWalkingLeft);
                }

                if (isJumping && !isGrounded && !isAttacking)
                {
                    finnDrawer.DrawAni(facingRight, position, animationJumpRight, animationJumpLeft);
                }
            }
        }

    }
}
