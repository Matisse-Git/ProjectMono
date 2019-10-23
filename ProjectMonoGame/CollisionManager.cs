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
        public bool CheckCollider(ICollidable source, ICollidable target)
        {
            if (source.collisionRectangle.Intersects(target.collisionRectangle))
                return true;
            return false;
        }

    }
}
