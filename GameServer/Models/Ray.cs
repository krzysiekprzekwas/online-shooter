using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

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
    }
}
