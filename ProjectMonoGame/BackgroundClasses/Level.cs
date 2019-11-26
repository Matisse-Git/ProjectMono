using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMonoGame
{
    public class Level
    {
        public Texture2D tileSet;
        public Texture2D spikeTile;
        public Texture2D portalTileOne;
        public Texture2D portalTileTwo;
        public Texture2D portalTileThree;
        public Texture2D portalTileFour;
        byte[,] byteArr = new byte[25,40];
        public ITile[,] tileArr = new ITile[25,40];

        public Level(Texture2D tileSetIn, Texture2D SpikeTileIn, Texture2D portalTileOneIn, Texture2D portalTileTwoIn, Texture2D portalTileThreeIn, Texture2D portalTileFourIn, byte[,] byteArrIn)
        {
            tileSet = tileSetIn;
            byteArr = byteArrIn;
            spikeTile = SpikeTileIn;
            portalTileOne = portalTileOneIn;
            portalTileTwo = portalTileTwoIn;
            portalTileThree = portalTileThreeIn;
            portalTileFour = portalTileFourIn;
        }

        public void CreateLevel()
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    if (byteArr[i,j] == 1)
                        tileArr[i, j] = new NormalTile(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    if (byteArr[i,j] == 2)
                        tileArr[i, j] = new undergroundTile(new Vector2(j * 16 * 3,  (i * (16 * 3))), tileSet);
                    if (byteArr[i,j] == 3)
                        tileArr[i, j] = new floatingTile(new Vector2(j * 16 * 3,  (i * (16 * 3))), tileSet);
                    if (byteArr[i, j] == 4)
                        tileArr[i, j] = new rightWallTile(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    if (byteArr[i, j] == 5)
                        tileArr[i, j] = new leftWallTile(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    if (byteArr[i, j] == 6)
                        tileArr[i, j] = new groundToRightWallTile(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    if (byteArr[i, j] == 7)
                        tileArr[i, j] = new groundToLeftWallTile(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    if (byteArr[i, j] == 8)
                        tileArr[i, j] = new rightWallToGround(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    if (byteArr[i, j] == 9)
                        tileArr[i, j] = new leftWallToGround(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    if (byteArr[i, j] == 10)
                        tileArr[i, j] = new longFloatingLeftTIle(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    if (byteArr[i, j] == 11)
                        tileArr[i, j] = new longFloatingCenterTile(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    if (byteArr[i, j] == 12)
                        tileArr[i, j] = new longFloatingRightTile(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    if (byteArr[i, j] == 13)
                        tileArr[i, j] = new spikeTile(new Vector2(j * 16 * 3, (i * (16 * 3))), spikeTile);
                    if (byteArr[i,j] == 14)
                        tileArr[i, j] = new portalTileOne(new Vector2(j * 16 * 3, (i * (16 * 3))), portalTileOne);
                    if (byteArr[i, j] == 15)
                        tileArr[i, j] = new portalTileTwo(new Vector2(j * 16 * 3, (i * (16 * 3))), portalTileTwo);
                    if (byteArr[i, j] == 16)
                        tileArr[i, j] = new portalTileThree(new Vector2(j * 16 * 3, (i * (16 * 3))), portalTileThree);
                    if (byteArr[i, j] == 17)
                        tileArr[i, j] = new portalTileFour(new Vector2(j * 16 * 3, (i * (16 * 3))), portalTileFour);
                    if (byteArr[i, j] == 18)
                        tileArr[i, j] = new roofToLeftWall(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    if (byteArr[i, j] == 19)
                        tileArr[i, j] = new roofToRightWall(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
                    if (byteArr[i, j] == 20)
                        tileArr[i, j] = new roofTile(new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet);
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
