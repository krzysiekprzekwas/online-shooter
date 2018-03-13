using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Models
{
    public class Angle2
    {
        public Angle2() : this(0, 0)
        {

        }

        public Angle2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
    }
}
