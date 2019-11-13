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

        public Backdrop(Texture2D backdropIn)
        {
            backdrop = backdropIn;

            backdropAni = new Animation(999);

            backdropAni.AddFrame(new Rectangle(0, 0, 1920, 2000));
        }

        public void Draw(SpriteBatch spritebatch)
        {
            backdropDrawer = new BatchDrawer(spritebatch, backdrop, backdrop, 1, 1920);

            backdropDrawer.DrawAni(true, new Vector2(0, 0), backdropAni, backdropAni);

        }
    }
}
