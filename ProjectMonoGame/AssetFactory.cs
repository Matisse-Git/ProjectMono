using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace ProjectMonoGame
{
    class AssetFactory<AssetType>
    {
        public List<string> assetNames = new List<string>();
        public List<AssetType> textures = new List<AssetType>();

        public ContentManager content;

        public AssetFactory(ContentManager contentIn)
        {
            content = contentIn;
        }

        public void Add(string assetPathIn, string assetTagIn)
        {
            assetNames.Add(assetTagIn);
            textures.Add(content.Load<AssetType>(assetPathIn));
        }

        public AssetType Find(string assetTagIn)
        {
            for (int i = 0; i < assetNames.Count; i++)
            {
                if (assetNames[i] == assetTagIn)
                    return textures[i];
            }
            return default(AssetType);
        }
    }
}
