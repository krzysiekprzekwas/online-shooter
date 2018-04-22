using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Ray
    {
        public Vector3 Origin { get; set; }
        public Vector3 Direction { get; set; }

        public Ray(Vector3 o, Vector3 d)
        {
            Origin = o;
            Direction = d;
        }
    }
}
