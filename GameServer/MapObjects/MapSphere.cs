using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.MapObjects
{
    class MapSphere : MapObject
    {
        public MapSphere(double x, double y, double z, double dx, double dy, double dz)
            : base(x, y, z)
        {

            DiameterX = dx;
            DiameterY = dy;
            DiameterZ = dz;
        }

        public MapSphere(double x, double y, double z, double diameter)
            : base(x, y, z)
        {
            DiameterX = diameter;
            DiameterY = diameter;
            DiameterZ = diameter;
        }


        public double DiameterX { get; set; }
        public double DiameterY { get; set; }
        public double DiameterZ { get; set; }
    }
}