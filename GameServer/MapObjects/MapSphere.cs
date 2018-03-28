using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.MapObjects
{
    public class MapSphere : MapObject
    {

        public MapSphere(float x, float y, float z, float diameter, Color color = null)
            : base(x, y, z, color)
        {
            Diameter = diameter;
        }


        public float Diameter { get; set; }
    }
}