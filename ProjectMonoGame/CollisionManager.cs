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

    public interface IMultipleCollidable
    {
        Rectangle upCollisionRectangle { get; set; }
        Rectangle downCollisionRectangle { get; set; }
        Rectangle leftCollisionRectangle { get; set; }
        Rectangle rightCollisionRectangle { get; set; }
    }

    public class CollisionManager
    {
        public bool CheckCollider(Rectangle source, Rectangle target)
        {
            if (source.Intersects(target))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
