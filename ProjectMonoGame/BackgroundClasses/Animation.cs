﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace ProjectMonoGame
{
    public class Animation
    {
        public  List<AnimationFrame> frames;
        public AnimationFrame currentFrame;
        private double xOffset;
        int counter = 0;
        int animationSpeed;

        public Animation(int animationSpeedIn)
        {
            frames = new List<AnimationFrame>();
            xOffset = 0;
            animationSpeed = animationSpeedIn;
        }
        public void AddFrame(Rectangle rectangle)
        {
            AnimationFrame frame = new AnimationFrame()
            {
                SourceRectangle = rectangle
            };
            frames.Add(frame);
            currentFrame = frames[0];
        }
        public void Update(GameTime gametime)
        {
            xOffset += (float)currentFrame.SourceRectangle.Width * gametime.ElapsedGameTime.Milliseconds / animationSpeed;
            if (xOffset >= currentFrame.SourceRectangle.Width)
            {
                counter++;
                if (counter >= frames.Count)
                    counter = 0;

                currentFrame = frames[counter];
                xOffset = 0;
            }
        }

       
        public bool UpdateFull(GameTime gametime)
        {
            xOffset += (float)currentFrame.SourceRectangle.Width * gametime.ElapsedGameTime.Milliseconds / animationSpeed;
            if (xOffset >= currentFrame.SourceRectangle.Width)
            {
                counter++;
                if (counter >= frames.Count)
                {
                    counter = 0;
                    return true;
                }

                currentFrame = frames[counter];
                xOffset = 0;
            }
            return false;
        }
    }
}
