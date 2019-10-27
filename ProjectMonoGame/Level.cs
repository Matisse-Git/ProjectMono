using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMonoGame
{
    class Level
    {
        public Texture2D tileSet;
        byte[,] byteArr = new byte[2,30];
        public ITile[,] tileArr = new ITile[2,30];

        public Level(Texture2D tileSetIn, byte[,] byteArrIn)
        {
            tileSet = tileSetIn;
            byteArr = byteArrIn;
        }

        public void CreateLevel()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (byteArr[i,j] == 1)
                    {
                        tileArr[i, j] = new NormalTile(new Vector2(j * 16*4,800 + (i * (16*4))), tileSet);
                    }
                }
            }
        }

        public void DrawLevel(SpriteBatch spritebatch)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (tileArr[i,j] != null)
                    {
                        tileArr[i, j].Draw(spritebatch);
                    }
                }
            }
        }
    }
}
