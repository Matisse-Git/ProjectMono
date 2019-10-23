using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMonoGame
{
    class Camera2D
    {

        private Viewport viewPort;
        public Vector2 position { get; set; }


        public Camera2D(Viewport viewPortIn)
        {
            viewPort = viewPortIn;
            position = Vector2.Zero;
        }

        public Matrix GetViewMatrix()
        {
            Matrix matrix = Matrix.CreateTranslation(new Vector3(-position, 0));
                                                                    
            return matrix;
        }


    }
}
