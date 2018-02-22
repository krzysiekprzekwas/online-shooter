using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    public abstract class MapObject
    {
        public MapObject(double x, double y, double z)
        {
            Id = MapState.Instance.AssingMapObjectId();

            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int Id { get; set; }
    }
}
