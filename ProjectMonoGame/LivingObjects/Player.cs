﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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


        private Texture2D spritesheetLeft, spritesheetRight, doorButtonTutorial;

        public Vector2 position;

        private Animation animationIdleLeft, animationWalkingLeft,
                          animationAttackLeft, animationHitLeft,
                          animationJumpLeft, animationIdleRight,
                          animationWalkingRight, animationAttackRight,
                          animationHitRight, animationJumpRight,
                          animationDieRight, animationDieLeft, animationJumpDust, animationDoorButtonTutorial;

        private AnimationCreator aniCreator;

        private BatchDrawer finnDrawer, tutorialDrawer;

        private IController inputHandler;

        private CollisionManager colliManager;

        public HPBar hpBar;


        //Sprite Related
        private int spriteWidth = 32;
        private int spriteScale = 4;

        //Movement Related
        private bool leftColliding;
        private bool rightColliding;
        private bool facingRight = true;
        private bool isIdle = true;
        private bool isJumping;
        private bool isGrounded = false;
        private bool isAttacking;
        private bool isDead = false;
        private bool isFalling;
        private bool holdingRight;
        private bool holdingSpace;
        private bool isHurt;
        private bool inFrontOfDoor;
        private bool hasHitGround;
        public bool goalReached;
        public bool resetLevels = false;

        private bool canWallJump = true;
        private int count = 0;
        private int HP = 10;
        public int score = 0;

        SoundEffectInstance runInstance;
        SoundEffectInstance hitGroundInstance;
        SoundEffectInstance wallSlideInstance;
        SoundEffectInstance jumpInstance;
        SoundEffectInstance spikeInstance;

        public float gravity { get; set; } = 4;
        private float walkingSpeed = 0;
        private float walkingSpeedAssign = 8;
        private double jumpHeight = 20;

        private List<SoundEffect> playerSFX;



        public Player(Vector2 positionIn, Texture2D textureInLeft, Texture2D textureInRight, Texture2D doorButtonTutorialIn, HPBar hpBarIn, IController inputHandlerIn, List<SoundEffect> playerSFXIn)
        {
            inputHandler = inputHandlerIn;
            position = positionIn;
            spritesheetLeft = textureInLeft;
            spritesheetRight = textureInRight;
            doorButtonTutorial = doorButtonTutorialIn;
            playerSFX = playerSFXIn;

            hpBar = hpBarIn;

            runInstance = playerSFX[0].CreateInstance();
            runInstance.IsLooped = true;
            runInstance.Play();
            runInstance.Pause();
            hitGroundInstance = playerSFX[1].CreateInstance();
            wallSlideInstance = playerSFX[4].CreateInstance();
            wallSlideInstance.IsLooped = true;
            wallSlideInstance.Play();
            wallSlideInstance.Pause();
            jumpInstance = playerSFX[2].CreateInstance();
            spikeInstance = playerSFX[5].CreateInstance();


            aniCreator = new AnimationCreator();

            colliManager = new CollisionManager();

            animationIdleLeft = new Animation(175);
            animationWalkingLeft = new Animation(50);
            animationAttackLeft = new Animation(75);
            animationHitLeft = new Animation(50);
            animationDieLeft = new Animation(200);
            animationJumpLeft = new Animation(100);
            animationIdleRight = new Animation(175);
            animationWalkingRight = new Animation(50);
            animationAttackRight = new Animation(75);
            animationHitRight = new Animation(50);
            animationDieRight = new Animation(200);
            animationJumpRight = new Animation(100);
            animationJumpDust = new Animation(20);
            animationDoorButtonTutorial = new Animation(999);


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
            aniCreator.CreateAniRight(animationJumpDust, 0, 4);
            animationDoorButtonTutorial.AddFrame(new Rectangle(0, 0, 16, 16));
        }

        public void Update(GameTime gametime, Tile[,] tilesIn)
        {
            if (facingRight)
            {
                downCollisionRectangle = new Rectangle((int)position.X + 50, (int)position.Y + 87, 16, 16);
                rightCollisionRectangle = new Rectangle((int)position.X + 75, (int)position.Y + 40, 16, 45);
                leftCollisionRectangle = new Rectangle((int)position.X + 30, (int)position.Y + 40, 16, 45);
            }

            else
            {
                downCollisionRectangle = new Rectangle((int)position.X + 65, (int)position.Y + 87, 16, 16);
                rightCollisionRectangle = new Rectangle((int)position.X + 90, (int)position.Y + 40, 16, 45);
                leftCollisionRectangle = new Rectangle((int)position.X + 45, (int)position.Y + 40, 16, 45);
            }


            inFrontOfDoor = false;

            CheckCollision(tilesIn);
            GameOver();

            if (!isDead)
            {
                ApplyGravity();
                HandleMovement(gametime);
                WallJump(gametime);
                DoAttack(gametime);
                FinishPose(gametime);
                playRunSFX();
            }
            if (isJumping)
                hasHitGround = false;
            FallDead(gametime);

            Reset();
        }

        private void HandleMovement(GameTime gametime)
        {
            switch (inputHandler.GetButtonPressed())
            {
                case "Left":
                    facingRight = false;
                    holdingRight = false;
                    Walk(gametime);
                    break;
                case "Right":
                    facingRight = true;
                    holdingRight = true;
                    Walk(gametime);
                    break;
                case "Jump":
                    canWallJump = true;
                    holdingSpace = true;
                    hasHitGround = false;
                    Jump(gametime);
                    isJumping = true;

                    break;
                case "LeftJump":
                    holdingSpace = true;
                    holdingRight = false;
                    facingRight = false;
                    hasHitGround = false;
                    Jump(gametime);
                    Walk(gametime);
                    isJumping = true;

                    break;
                case "RightJump":
                    holdingSpace = true;
                    holdingRight = true;
                    facingRight = true;
                    hasHitGround = false;
                    Jump(gametime);
                    Walk(gametime);
                    isJumping = true;

                    break;
                case "Attack":
                    isAttacking = true;
                    break;
                case "Up":
                    if (inFrontOfDoor)
                    {
                        goalReached = true;
                        inFrontOfDoor = false;
                    }
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
            if (isAttacking)
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

            if ((!rightColliding && facingRight) || (!leftColliding && !facingRight))
            {
                position.X += walkingSpeed;
            }
            walkingSpeedAssign = 8;
        }
        private void Jump(GameTime gametime)
        {
            if (!isJumping)
                jumpInstance.Play();

            if (jumpHeight > 0)
            {
                jumpHeight -= 0.35;
            }
            position.Y -= (float)jumpHeight;
            isGrounded = false;
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
            if (isDead)
            {
                if (facingRight)
                {
                    if (animationDieRight.UpdateFull(gametime))
                    {
                        position.X = 50;
                        position.Y = 900;
                        isDead = false;
                        facingRight = true;
                        hpBar.Update();
                    }
                }
                if (!facingRight)
                {
                    if (animationDieLeft.UpdateFull(gametime))
                    {
                        position.X = 50;
                        position.Y = 900;
                        isDead = false;
                        facingRight = true;
                        hpBar.Update();
                    }

                }
            }
        }
        public void ApplyGravity()
        {
            if (!isGrounded)
            {
                if (gravity < 15)
                {
                    gravity += 0.3f;
                }
                position.Y += gravity;
            }
        }
        private void WallJump(GameTime gametime)
        {
            if (isJumping && !isGrounded && !isIdle)
            {
                if (rightColliding)
                {
                    wallSlideInstance.Play();
                    gravity = 3;
                    facingRight = false;
                    if (!holdingRight)
                    {
                        jumpHeight = 15;
                    }
                }
                if (leftColliding)
                {
                    wallSlideInstance.Play();
                    gravity = 3;
                    facingRight = true;
                    if (holdingRight)
                    {
                        jumpHeight = 15;
                    }
                }
            }
            if (isGrounded || (!rightColliding  && !leftColliding) || isIdle)
                    wallSlideInstance.Pause();

        }
        private void CheckCollision(Tile[,] tilesIn)
        {
            foreach (Tile tile in tilesIn)
            {
                if (tile != null)
                {
                    if (colliManager.CheckCollider(rightCollisionRectangle, tile.collisionRectangle))
                    {
                        if (tile.Identity == TileIdentifier.Spike)
                        {
                            if (!isDead)
                                spikeInstance.Play();

                            isDead = true;
                        }
                        else if (tile.Identity == TileIdentifier.Coin)
                        {
                            score++;
                            tile.Remove();
                        }
                        else if (tile.Identity != TileIdentifier.Gate && tile.Identity != TileIdentifier.Coin)
                        {
                            rightColliding = true;
                            jumpHeight = 2;
                        }
                        else if (tile.Identity == TileIdentifier.Gate)
                        {
                            inFrontOfDoor = true;
                        }
                    }

                    if (colliManager.CheckCollider(leftCollisionRectangle, tile.collisionRectangle))
                    {
                        if (tile.Identity == TileIdentifier.Spike)
                        {
                            if (!isDead)
                                spikeInstance.Play();

                            isDead = true;
                        }
                        else if (tile.Identity == TileIdentifier.Coin)
                        {
                            score++;
                            tile.Remove();
                        }

                        else if (tile.Identity != TileIdentifier.Gate && tile.Identity != TileIdentifier.Coin)
                        {
                            leftColliding = true;
                            jumpHeight = 2;
                        }
                        else if (tile.Identity == TileIdentifier.Gate)
                        {
                            inFrontOfDoor = true;
                        }

                    }

                    if (gravity >= 12)
                    {
                        isFalling = true;
                    }

                    if (colliManager.CheckCollider(downCollisionRectangle, tile.collisionRectangle))
                    {
                        if (tile.Identity == TileIdentifier.Spike)
                        {
                            if (!isDead)
                                spikeInstance.Play();

                            isDead = true;

                        }
                        else if (tile.Identity == TileIdentifier.Gate)
                        {
                            inFrontOfDoor = true;
                        }
                        else if (tile.Identity == TileIdentifier.Coin)
                        {
                            score++;
                            tile.Remove();
                        }
                        else
                        {
                            if (!hasHitGround)
                            {
                                playHitGroundSFX();
                                hasHitGround = true;
                            }
                            isGrounded = true;
                            isJumping = false;
                            jumpHeight = 15;
                            gravity = 0;
                            isFalling = false;
                            break;
                        }
                    }
                    else
                    {
                        isGrounded = false;
                        isJumping = true;
                    }
                }
            }
        }
        public void TakeDamage(GameTime gametime)
        {
            if (isHurt)
            {
                if (facingRight)
                {
                    if (animationHitRight.UpdateFull(gametime))
                    {
                        if (HP > 0)
                        {
                            HP--;
                            isHurt = false;
                        }
                        else
                        {
                            isDead = true;
                            isHurt = false;
                        }
                    }
                }
                if (!facingRight)
                {
                    if (animationHitLeft.UpdateFull(gametime))
                    {
                        if (HP > 0)
                        {
                            HP--;
                            isHurt = false;
                        }
                        else
                        {
                            isDead = true;
                            isHurt = false;
                        }
                    }
                }
            }
        }
        public void FinishPose(GameTime gametime)
        {
            if (goalReached)
            {
                position.X = 50;
                position.Y = 900;
                facingRight = true;
            }
        }
        private void Reset()
        {
            rightColliding = false;
            leftColliding = false;
            holdingSpace = false;
        }
        private void playRunSFX()
        {
            if (!isJumping && !isIdle)
                runInstance.Resume();
            else
                runInstance.Pause();
        }
        private void playHitGroundSFX()
        {
            hitGroundInstance.Play();
        }
        public void GameOver()
        {
            if (hpBar.HP == 0)
            {
                hpBar.Restore();
                resetLevels = true;
            }
        }
        public void UpdateInput()
        {
            if (inputHandler is KeyboardHandler)
                inputHandler = new ControllerHandler();

            else
                inputHandler = new KeyboardHandler();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            finnDrawer = new BatchDrawer(spriteBatch, spritesheetLeft, spritesheetRight, spriteScale);
            tutorialDrawer = new BatchDrawer(spriteBatch, doorButtonTutorial, doorButtonTutorial, 2);
            hpBar.Draw(spriteBatch);

            if (isDead || goalReached)
            {
                finnDrawer.DrawAni(facingRight, position, animationDieRight, animationDieLeft);
            }

            if (!isDead)
            {
                if (inFrontOfDoor)
                    tutorialDrawer.DrawAni(facingRight, new Vector2(position.X + 25, position.Y - 50), animationDoorButtonTutorial, animationDoorButtonTutorial);

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
}
