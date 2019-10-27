using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectMonoGame
{
    public interface ICollidable
    {
        Rectangle collisionRectangle { get; set; }
    }

    public class CollisionManager
    {
        public bool CheckCollider(Rectangle source, Rectangle target)
        {
            if (source.Intersects(target))
                return true;
            return false;
        }

    }
}
