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
        public Texture2D Goal;

        byte[,] byteArr = new byte[25,40];
        public Tile[,] tileArr = new Tile[25,40];

        public Level(Texture2D tileSetIn, Texture2D SpikeTileIn, Texture2D GoalIn, byte[,] byteArrIn)
        {
            tileSet = tileSetIn;
            byteArr = byteArrIn;
            spikeTile = SpikeTileIn;
            Goal = GoalIn;

        }

        public void CreateLevel()
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    if (byteArr[i, j] == 1) 
                        tileArr[i, j] = new Tile(16,3,new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet, new Vector2(1,0), TileIdentifier.Floor);
                    if (byteArr[i, j] == 2) 
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet, new Vector2(11, 1), TileIdentifier.GroundToRightWall);
                    if (byteArr[i, j] == 3)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet, new Vector2(12, 1), TileIdentifier.GroundToLeftWall);
                    if (byteArr[i, j] == 4)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet, new Vector2(1, 1), TileIdentifier.Ground);
                    if (byteArr[i, j] == 5)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet, new Vector2(0, 1), TileIdentifier.RightWall);
                    if (byteArr[i, j] == 6)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet, new Vector2(2, 1), TileIdentifier.LeftWall);
                    if (byteArr[i, j] == 7)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet, new Vector2(0, 0), TileIdentifier.RightWallToGround);
                    if (byteArr[i, j] == 8)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (i * (16 * 3))), tileSet, new Vector2(2, 0), TileIdentifier.LeftWallToGround);
                    if (byteArr[i, j] == 9)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (i * (16 * 3))), spikeTile, new Vector2(0, 0), TileIdentifier.Spike);
                    if (byteArr[i, j] == 10)
                        tileArr[i, j] = new Tile(16, 6, new Vector2(j * 16 * 3, (int)(i * (16 * 2.85))), Goal, new Vector2(0, 0), TileIdentifier.Gate);
                    if (byteArr[i, j] == 11)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (int)(i * (16 * 2.85))), tileSet, new Vector2(0, 2), TileIdentifier.RightWallToRoof);
                    if (byteArr[i, j] == 12)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (int)(i * (16 * 2.85))), tileSet, new Vector2(2, 2), TileIdentifier.LeftWallToRoof);
                    if (byteArr[i, j] == 13)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (int)(i * (16 * 2.85))), tileSet, new Vector2(1, 2), TileIdentifier.Roof);
                    if (byteArr[i, j] == 14)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (int)(i * (16 * 2.85))), tileSet, new Vector2(3, 1), TileIdentifier.FloatingTileLeft);
                    if (byteArr[i, j] == 15)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (int)(i * (16 * 2.85))), tileSet, new Vector2(4, 1), TileIdentifier.FloatingTileCenter);
                    if (byteArr[i, j] == 16)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (int)(i * (16 * 2.85))), tileSet, new Vector2(5, 1), TileIdentifier.FloatingTileRight);
                    if (byteArr[i, j] == 17)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (int)(i * (16 * 2.85))), spikeTile, new Vector2(0, 1), TileIdentifier.Spike);
                    if (byteArr[i, j] == 18)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (int)(i * (16 * 2.85))), spikeTile, new Vector2(0, 2), TileIdentifier.Spike);
                    if (byteArr[i, j] == 19)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (int)(i * (16 * 2.85))), spikeTile, new Vector2(0, 3), TileIdentifier.Spike);
                    if (byteArr[i, j] == 20)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (int)(i * (16 * 2.85))), tileSet, new Vector2(4, 0), TileIdentifier.FloatingTileCenter);
                    if (byteArr[i, j] == 21)
                        tileArr[i, j] = new Tile(16, 3, new Vector2(j * 16 * 3, (int)(i * (16 * 2.85))), spikeTile, new Vector2(0, 4), TileIdentifier.Coin);
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
