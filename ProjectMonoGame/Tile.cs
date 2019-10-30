using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMonoGame
{
    public interface ITile
    {
        Vector2 position { get; set; }
        int tileScale {get;set;}
        int tileWidth { get; set; }
        Rectangle collisionRectangle { get; set; }


        void UpdateCollision();
        void Draw(SpriteBatch spriteBatch);
    }

    public class NormalTile : ITile, ICollidable
    {
        public Rectangle collisionRectangle { get; set; }
        public Vector2 position { get; set; }
        public int tileScale { get; set; } = 4;
        public int tileWidth { get; set; } = 16;

        private int offset = 20;
        private int tileSetWidth = 240;
        private int tileSetHeight = 48;
        private Texture2D tileSet;
        private Animation tile;
        private TileDrawer tileDrawer;

        public NormalTile(Vector2 positionIn, Texture2D tileSetIn)
        {
            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, tileWidth * tileScale, tileWidth * tileScale);
            tile = new Animation(999);
            tile.AddFrame(new Rectangle((tileWidth * 1), (tileWidth * 0), tileWidth, tileWidth));
            
            position = positionIn;
            tileSet = tileSetIn;
        }

        public void UpdateCollision()
        {
            //collisionRectangle = new Rectangle((int)position.X, (int)position.Y, tileWidth * tileScale, tileWidth * tileScale);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            tileDrawer = new TileDrawer(spriteBatch,tileSet, tileScale, tileWidth);

            tileDrawer.DrawTile(position, tile);
        }
    }
}
