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
        byte[,] byteArr = new byte[25,40];
        public ITile[,] tileArr = new ITile[25,40];

        public Level(Texture2D tileSetIn, byte[,] byteArrIn)
        {
            tileSet = tileSetIn;
            byteArr = byteArrIn;
        }

        public void CreateLevel()
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    if (byteArr[i,j] == 1)
                    {
                        tileArr[i, j] = new NormalTile(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    }
                    if (byteArr[i,j] == 2)
                    {
                        tileArr[i, j] = new undergroundTile(new Vector2(j * 16 * 3,  (i * (16 * 3))), tileSet);
                    }
                    if (byteArr[i,j] == 3)
                    {
                        tileArr[i, j] = new floatingTile(new Vector2(j * 16 * 3,  (i * (16 * 3))), tileSet);
                    }
                    if (byteArr[i, j] == 4)
                    {
                        tileArr[i, j] = new rightWallTile(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    }
                    if (byteArr[i, j] == 5)
                    {
                        tileArr[i, j] = new leftWallTile(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    }
                    if (byteArr[i, j] == 6)
                    {
                        tileArr[i, j] = new groundToRightWallTile(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    }
                    if (byteArr[i, j] == 7)
                    {
                        tileArr[i, j] = new groundToLeftWallTile(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    }
                    if (byteArr[i, j] == 8)
                    {
                        tileArr[i, j] = new rightWallToGround(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    }
                    if (byteArr[i, j] == 9)
                    {
                        tileArr[i, j] = new leftWallToGround(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    }
                    if (byteArr[i, j] == 10)
                    {
                        tileArr[i, j] = new longFloatingLeftTIle(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    }
                    if (byteArr[i, j] == 11)
                    {
                        tileArr[i, j] = new longFloatingCenterTile(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    }
                    if (byteArr[i, j] == 12)
                    {
                        tileArr[i, j] = new longFloatingRightTile(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    }
                }
            }
        }

        public void DrawLevel(SpriteBatch spritebatch)
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 40; j++)
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
