using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.MapObjects
{
    public class MapSphere : MapObject
    {
        public MapSphere(double x, double y, double z, double dx, double dy, double dz, Color color = null)
            : base(x, y, z, color)
        {

            DiameterX = dx;
            DiameterY = dy;
            DiameterZ = dz;
        }

        public MapSphere(double x, double y, double z, double diameter, Color color = null)
            : base(x, y, z, color)
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