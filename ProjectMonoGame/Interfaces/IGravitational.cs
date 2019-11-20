using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMonoGame
{
    interface IGravitational
    {
        float gravity { get; set; }
        void ApplyGravity();
    }
}
