using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.MapObjects
{
    class MapTorus : MapObject
    {
        public MapTorus(double x, double y, double z, double diameter, double thickness, int tessellation = 24)
            : base(x, y, z)
        {
            Diameter = diameter;
            Thickness = thickness;
            Tessellation = tessellation;
        }

        public double Diameter { get; set; }
        public double Thickness { get; set; }
        public int Tessellation { get; set; }
    }
}