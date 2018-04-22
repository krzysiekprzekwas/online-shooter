using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.MapObjects
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
