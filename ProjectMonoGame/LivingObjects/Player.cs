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
    class Player : IMultipleCollidable, IGravitational
    {
        public Rectangle upCollisionRectangle { get; set; }
        public Rectangle downCollisionRectangle { get; set; }
        public Rectangle leftCollisionRectangle { get; set; }
        public Rectangle rightCollisionRectangle { get; set; }


        private Texture2D spritesheetLeft, spritesheetRight, jumpParticleDust;

        public Vector2 position;

        private Animation animationIdleLeft, animationWalkingLeft,
                          animationAttackLeft, animationHitLeft,
                          animationJumpLeft, animationIdleRight,
                          animationWalkingRight, animationAttackRight,
                          animationHitRight, animationJumpRight,
                          animationDieRight, animationDieLeft, animationJumpDust;

        private AnimationCreator aniCreator;

        private BatchDrawer finnDrawer;
        private ParticleDrawer partiDrawer;

        private IController inputHandler;

        private CollisionManager colliManager;


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
        public bool goalReached;

        private bool canWallJump = true;
        private int count = 0;
        private int HP = 2;


        public float gravity { get; set; } = 4;
        private float walkingSpeed = 0;
        private float walkingSpeedAssign = 8;
        private double jumpHeight = 20;




        public Player(Vector2 positionIn, Texture2D textureInLeft, Texture2D textureInRight, Texture2D jumpParticleDustIn, IController inputHandlerIn)
        {
            inputHandler = inputHandlerIn;
            position = positionIn;
            spritesheetLeft = textureInLeft;
            spritesheetRight = textureInRight;
            jumpParticleDust = jumpParticleDustIn;

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
        }

        public void Update(GameTime gametime, Tile[,] tilesIn, List<Enemy> enemyListIn)
        {
            downCollisionRectangle = new Rectangle((int)position.X + ((spriteWidth * spriteScale) / 2), (int)position.Y + ((spriteWidth * spriteScale) / 3), 1, ((spriteWidth * spriteScale) / 8));
            rightCollisionRectangle = new Rectangle((int)position.X + ((spriteWidth * spriteScale) / 8) * 4, (int)position.Y - ((spriteWidth * spriteScale) / 8), (spriteWidth * spriteScale) / 4, (spriteWidth * spriteScale) / 4);
            leftCollisionRectangle = new Rectangle((int)position.X + ((spriteWidth * spriteScale) / 8) * 2, (int)position.Y - ((spriteWidth * spriteScale) / 8), (spriteWidth * spriteScale) / 4, (spriteWidth * spriteScale) / 4);

            CheckCollision(tilesIn, enemyListIn);

            if (!isDead)
            {
                ApplyGravity();
                HandleMovement(gametime);
                WallJump(gametime);
                DoAttack(gametime);
                FinishPose(gametime);
            }
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
                    isJumping = true;
                    Jump(gametime);
                    break;
                case "LeftJump":
                    holdingSpace = true;
                    holdingRight = false;
                    facingRight = false;
                    isJumping = true;
                    Jump(gametime);
                    Walk(gametime);
                    break;
                case "RightJump":
                    holdingSpace = true;
                    holdingRight = true;
                    facingRight = true;
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
                    gravity = 3;
                    facingRight = false;
                    if (!holdingRight)
                    {
                        jumpHeight = 15;
                    }
                }
                if (leftColliding)
                {
                    gravity = 3;
                    facingRight = true;
                    if (holdingRight)
                    {
                        jumpHeight = 15;
                    }
                }
            }
        }
        private void CheckCollision(Tile[,] tilesIn, List<Enemy> enemyListIn)
        {
            foreach (Tile tile in tilesIn)
            {
                if (tile != null)
                {
                    if (colliManager.CheckCollider(rightCollisionRectangle, tile.collisionRectangle))
                    {
                        rightColliding = true;
                        jumpHeight = 2;
                        //if (tile is portalTileOne || tile is portalTileTwo || tile is portalTileThree || tile is portalTileFour)
                        //{
                        //    goalReached = true;
                        //}
                    }

                    if (colliManager.CheckCollider(leftCollisionRectangle, tile.collisionRectangle))
                    {
                        leftColliding = true;
                        jumpHeight = 2;
                        //if (tile is portalTileOne || tile is portalTileTwo || tile is portalTileThree || tile is portalTileFour)
                        //{
                        //    goalReached = true;
                        //}
                    }

                    if (gravity >= 12)
                    {
                        isFalling = true;
                    }

                    if (colliManager.CheckCollider(downCollisionRectangle, tile.collisionRectangle))
                    {
                        if (tile.Identity == TileIdentifier.Spike)
                        {
                            isDead = true;
                        }
                        //if (tile is portalTileOne || tile is portalTileTwo || tile is portalTileThree || tile is portalTileFour)
                        //{
                        //    goalReached = true;
                        //}
                        else
                        {

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
        

            foreach (Enemy enemy in enemyListIn)
            {
                if (colliManager.CheckCollider(downCollisionRectangle, enemy.collisionRectangle) || colliManager.CheckCollider(leftCollisionRectangle,
                   enemy.collisionRectangle) || colliManager.CheckCollider(rightCollisionRectangle, enemy.collisionRectangle))
                {
                    isHurt = true;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            finnDrawer = new BatchDrawer(spriteBatch, spritesheetLeft, spritesheetRight, spriteScale);

            if (isDead || goalReached)
            {
                finnDrawer.DrawAni(facingRight, position, animationDieRight, animationDieLeft);
            }

            if (!isDead)
            {

                if (isAttacking)
                    finnDrawer.DrawAni(facingRight, position, animationAttackRight, animationAttackLeft);

                if (isIdle && !isAttacking)
                {
                    finnDrawer.DrawAni(facingRight, position, animationIdleRight, animationIdleLeft);
                }

                //if (isIdle)
                //{
                //    if (isJumping && !isGrounded && !isAttacking)
                //    {
                //        if (partiDrawer != null)
                //        {
                //            partiDrawer.DrawParticle(spriteBatch, animationJumpDust);
                //        }
                //    }
                //}

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
