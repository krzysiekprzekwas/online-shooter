using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.MapObjects
{
    class MapDisc : MapObject
    {
        public MapDisc(double x, double y, double z, double r, int tessellation = 24)
            : base(x, y, z)
        {
            Radius = r;
            Tessellation = tessellation;
        }

        public double Radius { get; set; }
        public int Tessellation { get; set; }
    }
}