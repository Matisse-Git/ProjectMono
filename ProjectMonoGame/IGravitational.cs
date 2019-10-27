using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMonoGame
{
    interface IGravitational
    {
        int gravity { get; set; }
        void ApplyGravity(bool isGrounded);
    }
}
