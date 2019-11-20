using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMonoGame
{
    class ParticleManager
    {
        Texture2D particleTexture;
        public ParticleManager(Texture2D particleTextureIn)
        {
            particleTexture = particleTextureIn;
        }
    }
}
