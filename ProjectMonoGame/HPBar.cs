using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMonoGame
{
    class HPBar
    {
        public int HP = 10;
        Texture2D HPTexture;
        ImageDrawer[] HPList;

        public HPBar(Texture2D HPTextureIn)
        {
            HPTexture = HPTextureIn;

            HPList = new ImageDrawer[10];

            for (int i = 0; i < 10; i++)
            {
                HPList[i] = new ImageDrawer(HPTexture, new Vector2(i * 50, 20), new Vector2(16 * 4, 16 * 4), new Vector2(16, 16));
            }

        }

        public bool Update()
        {
            if (HP > 0)
            {
                HP--;
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Restore()
        {
            HP = 10;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < HP; i++)
            {
                HPList[i].Draw(spriteBatch);
            }
        }
    }
}
