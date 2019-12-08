using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMonoGame
{
    public enum TileIdentifier
    {
        Ground,
        Floor,
        LeftWall,
        RightWall,
        GroundToRightWall,
        GroundToLeftWall,
        RightWallToGround,
        LeftWallToGround,
        RightWallToRoof,
        LeftWallToRoof,
        Roof,
        FloatingTileLeft,
        FloatingTileCenter,
        FloatingTileRight,
        Spike,
        Gate
    }

    public class Tile
    {
        public Rectangle collisionRectangle;
        public Vector2 position;

        private int tileScale;
        private int tileWidth;
        private Texture2D tileSet;
        protected Animation tile;
        private TileDrawer tileDrawer;
        
        public TileIdentifier Identity;


        public Tile(int tileWidthIn, int tileScaleIn, Vector2 positionIn, Texture2D tileSetIn, Vector2 tilePos, TileIdentifier IdentityIn)
        {
            tileScale = tileScaleIn;
            tileWidth = tileWidthIn;
            position = positionIn;
            tileSet = tileSetIn;
            Identity = IdentityIn;

            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale, (tileWidth * tileScale), tileWidth * tileScale);
            tile = new Animation(999);
            tile.AddFrame(new Rectangle((tileWidthIn * (int)tilePos.X), (tileWidthIn * (int)tilePos.Y), tileWidthIn, tileWidthIn));

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

            tileDrawer.DrawTile(position, tile);
        }
    }
}