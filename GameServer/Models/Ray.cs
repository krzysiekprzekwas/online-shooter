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

        private Vector3 _direction;
        // Is a vector of length 1
        public Vector3 Direction
        {

            get
            {
                return _direction;
            }
            set
            {
                if (value.LengthSquared() != 1)
                    value = Vector3.Normalize(value);

                _direction = value;
            }
        }

        public Ray(Vector3 o, Vector3 d)
        {
            Origin = o;
            Direction = d;
        }

        public Ray(float x, float y, float z, float dx, float dy, float dz)
            : this(new Vector3(x, y, z), new Vector3(dx, dy, dz))
        {

        }
    }
}
