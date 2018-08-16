using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Ray
    {
        public Vector2 Origin { get; set; }

        private Vector2 _direction;
        // Is a vector of length 1
        public Vector2 Direction
        {

            get
            {
                return _direction;
            }
            set
            {
                if (value.LengthSquared() != 1)
                    value = Vector2.Normalize(value);

                _direction = value;
            }
        }

        public Ray(Vector2 o, Vector2 d)
        {
            Origin = o;
            Direction = d;
        }

        public Ray(float x, float y, float dx, float dy)
            : this(new Vector2(x, y), new Vector2(dx, dy))
        {

        }
    }
}
