using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.MapObjects
{
    public class MapSphere : MapObject
    {

        public MapSphere(double x, double y, double z, double diameter, Color color = null)
            : base(x, y, z, color)
        {
            Diameter = diameter;
        }


        public double Diameter { get; set; }
    }
}