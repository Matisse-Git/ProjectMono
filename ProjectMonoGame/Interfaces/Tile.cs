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
        Spike
    }

    public class Tile
    {
        public Rectangle collisionRectangle;
        public Vector2 position;

        private int tileScale;
        private int tileWidth;
        private int offset = 0;
        private int tileSetWidth = 240;
        private int tileSetHeight = 48;
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

            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
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

//    public interface ITile
//    {
//        Rectangle collisionRectangle { get; set; }
//        Vector2 position { get; set; }
//        int tileScale { get; set; }
//        int tileWidth { get; set; }
//        void Draw(SpriteBatch spriteBatch);
//    }

//    public class NormalTile : ITile, ICollidable, ILandable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 0;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public NormalTile(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle((tileWidth * 1), (tileWidth * 0), tileWidth, tileWidth));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }

//    public class undergroundTile : ITile
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 30;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public undergroundTile(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle((tileWidth * 1), (tileWidth * 1), tileWidth, tileWidth));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }

//    }

//    public class floatingTile : ITile, ICollidable, ILandable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public floatingTile(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle((tileWidth * 4), (tileWidth * 0), tileWidth, tileWidth));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }

//    }

//    public class rightWallTile : ITile, ICollidable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public rightWallTile(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle((tileWidth * 2), (tileWidth * 1), tileWidth, tileWidth));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }

//    }

//    public class leftWallTile : ITile, ICollidable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public leftWallTile(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle((tileWidth * 0), (tileWidth * 1), tileWidth, tileWidth));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }

//    public class groundToLeftWallTile : ITile, ICollidable, ILandable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public groundToLeftWallTile(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle((tileWidth * 11), (tileWidth * 1), tileWidth, tileWidth));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }

//    public class groundToRightWallTile : ITile, ICollidable, ILandable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public groundToRightWallTile(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle((tileWidth * 12), (tileWidth * 1), tileWidth, tileWidth));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }

//    public class roofToRightWall : ITile, ICollidable, ILandable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public roofToRightWall(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle((tileWidth * 0), (tileWidth * 2), tileWidth, tileWidth));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }

//    public class roofToLeftWall : ITile, ICollidable, ILandable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public roofToLeftWall(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle((tileWidth * 2), (tileWidth * 2), tileWidth, tileWidth));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }

//    public class rightWallToGround : ITile, ICollidable, ILandable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public rightWallToGround(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle((tileWidth * 0), (tileWidth * 0), tileWidth, tileWidth));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }

//    public class leftWallToGround : ITile, ICollidable, ILandable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public leftWallToGround(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle((tileWidth * 2), (tileWidth * 0), tileWidth, tileWidth));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }

//    public class longFloatingLeftTIle : ITile, ICollidable, ILandable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public longFloatingLeftTIle(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle((tileWidth * 3), (tileWidth * 1), tileWidth, tileWidth));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }

//    public class longFloatingCenterTile : ITile, ICollidable, ILandable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public longFloatingCenterTile(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle((tileWidth * 4), (tileWidth * 1), tileWidth, tileWidth));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }

//    public class longFloatingRightTile : ITile, ICollidable, ILandable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public longFloatingRightTile(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle((tileWidth * 5), (tileWidth * 1), tileWidth, tileWidth));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }

//    public class spikeTile : ITile, ICollidable, ILandable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public spikeTile(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle(0, 0, 212, 212));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }
//    public class portalTileOne : ITile, ICollidable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public portalTileOne(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle(0, 0, 200, 300));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }

//    public class portalTileTwo : ITile, ICollidable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public portalTileTwo(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle(0, 0, 200, 300));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }

//    public class portalTileThree : ITile, ICollidable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public portalTileThree(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle(0, 0, 200, 300));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }
//    public class portalTileFour : ITile, ICollidable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public portalTileFour(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle(0, 0, 200, 300));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }

//    public class roofTile : ITile, ICollidable
//    {
//        public Rectangle collisionRectangle { get; set; }
//        public Vector2 position { get; set; }
//        public int tileScale { get; set; } = 3;
//        public int tileWidth { get; set; } = 16;

//        private int offset = 10;
//        private int tileSetWidth = 240;
//        private int tileSetHeight = 48;
//        private Texture2D tileSet;
//        private Animation tile;
//        private TileDrawer tileDrawer;

//        public roofTile(Vector2 positionIn, Texture2D tileSetIn)
//        {
//            collisionRectangle = new Rectangle((int)positionIn.X, (int)positionIn.Y - tileWidth * tileScale + offset, (tileWidth * tileScale), tileWidth * tileScale);
//            tile = new Animation(999);
//            tile.AddFrame(new Rectangle((tileWidth * 1), (tileWidth * 2), tileWidth, tileWidth));

//            position = positionIn;
//            tileSet = tileSetIn;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            tileDrawer = new TileDrawer(spriteBatch, tileSet, tileScale, tileWidth);

//            tileDrawer.DrawTile(position, tile);
//        }
//    }
//}
