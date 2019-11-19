using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMonoGame
{
    class Backdrop
    {
        private Texture2D backdrop;

        private BatchDrawer backdropDrawer;

        private Animation backdropAni;

        private int scale;
        private int width;
        private int counter = 0;
        public Vector2 position;

        public Backdrop(Texture2D backdropIn, int frameWidthIn, int frameHeightIn, int scaleIn, int widthIn, Vector2 positionIn)
        {
            backdrop = backdropIn;
            scale = scaleIn;
            width = widthIn;
            position = positionIn;

            backdropAni = new Animation(999);

            backdropAni.AddFrame(new Rectangle((int)positionIn.X, (int)positionIn.Y, frameWidthIn, frameHeightIn));
        }

        public void Draw(SpriteBatch spritebatch)
        {
            backdropDrawer = new BatchDrawer(spritebatch, backdrop, backdrop, scale, width);

            backdropDrawer.DrawAni(true, new Vector2(0,0), backdropAni, backdropAni);

        }
    }
}
