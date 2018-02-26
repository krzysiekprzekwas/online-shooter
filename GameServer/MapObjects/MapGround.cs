using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.MapObjects
{
    class MapGround : MapObject
    {
        public MapGround(double x, double y, double z, double w, double h)
            : base(x, y, z)
        {
            Height = h;
            Width = w;
        }

        public double Width { get; set; }
        public double Height { get; set; }
    }
}