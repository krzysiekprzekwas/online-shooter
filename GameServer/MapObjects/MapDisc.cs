using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.MapObjects
{
    class MapDisc : MapObject
    {
        public MapDisc(double x, double y, double z, double r, int tessellation = 24, Color color = null)
            : base(x, y, z, color)
        {
            Radius = r;
            Tessellation = tessellation;
        }

        public double Radius { get; set; }
        public int Tessellation { get; set; }
    }
}