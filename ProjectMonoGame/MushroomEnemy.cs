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
    class MushroomEnemy : ICollidable
    {
        public Rectangle collisionRectangle { get; set; }

        private Texture2D spritesheetLeft, spritesheetRight;

        public Vector2 position;

        private AnimationCreator aniCreator;

        private BatchDrawer mushroomDrawer;

        private Animation animationIdleLeft, animationWalkingLeft,
                  animationAttackLeft, animationHitLeft,
                  animationJumpLeft, animationIdleRight,
                  animationWalkingRight, animationAttackRight,
                  animationHitRight, animationJumpRight,
                  animationDieRight, animationDieLeft;


        private float walkingSpeed = 3;
        private int spriteWidth = 32;
        private int spritesheetWidth = 1056;
        private int spriteScale = 4;

        private bool facingRight = false;
        private bool isIdle = true;
        private bool isJumping;
        private bool isAttacking;
        private bool isDead = false;

        public MushroomEnemy(Vector2 positionIn, Texture2D textureInLeft, Texture2D textureInRight)
        {
            position = positionIn;
            spritesheetLeft = textureInLeft;
            spritesheetRight = textureInRight;

            collisionRectangle = new Rectangle((int)position.X, (int)position.Y, 80, 80);

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

            aniCreator.CreateAniRight(animationIdleLeft, 0, 3, spriteWidth);
            aniCreator.CreateAniLeft(animationIdleRight, 1, 4, spritesheetWidth, spriteWidth);
            aniCreator.CreateAniRight(animationWalkingLeft, 4, 14, spriteWidth);
            aniCreator.CreateAniLeft(animationWalkingRight, 5, 15, spritesheetWidth, spriteWidth);
        }

        public void Update(GameTime gametime)
        {
            collisionRectangle = new Rectangle((int)position.X, (int)position.Y, 80, 80);
            Move(gametime);
        }

        private void DoNothing(GameTime gametime)
        {
            if (facingRight)
                animationIdleRight.Update(gametime);
            if (!facingRight)
                animationIdleLeft.Update(gametime);
        }

        public void Move(GameTime gametime)
        {
            if (facingRight)
            {
                position.X += walkingSpeed;
                animationWalkingRight.Update(gametime);
            }
            if (!facingRight)
            {
                position.X += -walkingSpeed;
                animationWalkingLeft.Update(gametime);
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            mushroomDrawer = new BatchDrawer(spriteBatch,spritesheetLeft, spritesheetRight, spriteScale);

            mushroomDrawer.DrawAni(facingRight, position,animationWalkingRight, animationWalkingLeft);
        }
    }
}