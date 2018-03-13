using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.MapObjects
{
    class MapCylinder : MapObject
    {
        public MapCylinder(double x, double y, double z, double h, double diameterTop, double diameterBottom, int tessellation = 24, Color color = null)
            : base(x, y, z, color)
        {
            Height = h;
            DiameterBottom = diameterBottom;
            DiameterTop = diameterTop;
            Tessellation = tessellation;
        }

        public MapCylinder(double x, double y, double z, double h, double diameter, int tessellation = 24, Color color = null)
            : base(x, y, z, color)
        {
            Height = h;
            DiameterBottom = diameter;
            DiameterTop = diameter;
            Tessellation = tessellation;
        }

        public int Tessellation { get; set; }
        public double DiameterTop { get; set; }
        public double DiameterBottom { get; set; }
        public double Height { get; set; }
    }
}