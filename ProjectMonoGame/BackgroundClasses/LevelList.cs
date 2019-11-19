﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMonoGame
{
    public class LevelList
    {
        public Texture2D tileSet;
        public Texture2D spikeTile;
        public Texture2D portalTileOne;
        public Texture2D portalTileTwo;
        public Texture2D portalTileThree;
        public Texture2D portalTileFour;

        Level levelOne;
        Level levelTwo;
        Level levelThree;

        byte[,] byteArrLevelOne;
        byte[,] byteArrLevelTwo;
        byte[,] byteArrLevelThree;


        public int currentLevelInt = 0;
        public Level currentLevel;

        public LevelList(Texture2D tileSetIn, Texture2D SpikeTileIn, Texture2D portalTileOneIn, Texture2D portalTileTwoIn, Texture2D portalTileThreeIn, Texture2D portalTileFourIn)
        {
            tileSet = tileSetIn;
            spikeTile = SpikeTileIn;
            portalTileOne = portalTileOneIn;
            portalTileTwo = portalTileTwoIn;
            portalTileThree = portalTileThreeIn;
            portalTileFour = portalTileFourIn;

            byteArrLevelOne = new byte[,]
            {
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,16,17,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,14,15,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,1,1,1,1,1,1,1,1,1,1,1,1,7  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,2,2,2,2,2,2,2,2,2,2,2,2,2  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,2,2,2,2,2,2,2,2,2,2,2,2,2  },
               {6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,7,2,2,2,2,2,2,2,2,2,2,2,2,2  },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2  },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2  },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2  }
            };


            byteArrLevelTwo = new byte[,]
            {
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,16,17,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,14,15,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,1,9,0,0,0,0,0,0,8,1,1,1,1,1,1,1,1,1,1,1,1,7  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,5,2,2,2,2,2,2,2,2,2,2,2,2,2  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,2,4,13,13,13,13,13,13,5,2,2,2,2,2,2,2,2,2,2,2,2,2  },
               {6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,7,2,6,1,1,1,1,1,1,7,2,2,2,2,2,2,2,2,2,2,2,2,2  },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2  },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2  },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2  }
            };

            byteArrLevelThree = new byte[,]
{
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,8,1,9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,19,2,18,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,16,17,0,5  },
               {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,2,4,13,13,13,13,13,13,13,13,0,0,0,0,0,14,15,0,5  },
               {6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,7,2,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,7 },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2  },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2  },
               {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2  }
};


        }

        public void Update()
        {
            switch (currentLevelInt)
            {
                case 0:
                    levelOne = new Level(tileSet, spikeTile, portalTileOne, portalTileTwo, portalTileThree, portalTileFour, byteArrLevelOne);

                    currentLevel = levelOne;
                    break;
                case 1:
                    levelOne = null;
                    levelTwo = new Level(tileSet, spikeTile, portalTileOne, portalTileTwo, portalTileThree, portalTileFour, byteArrLevelTwo);

                    currentLevel = levelTwo;
                    break;
                case 2:
                    levelOne = null;
                    levelTwo = null;
                    levelThree = new Level(tileSet, spikeTile, portalTileOne, portalTileTwo, portalTileThree, portalTileFour, byteArrLevelThree);

                    currentLevel = levelThree;
                    break;
                default:
                    break;
            }
            currentLevel.CreateLevel();
        }
    }
}
