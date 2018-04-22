using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreGameServer.MapObjects
{
    public class Color
    {
        public Color(float red, float green, float blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public float Red { get; set; }
        public float Green { get; set; }
        public float Blue { get; set; }
    }
}
