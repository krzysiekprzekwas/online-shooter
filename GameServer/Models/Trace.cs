using GameServer.MapObjects;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer.Models
{
    public class Trace
    {
        public Trace(Vector3 position, Vector3 source, float distance, MapObject mapObject)
        {
            Position = position;
            Source = source;
            Distance = distance;
            MapObject = mapObject;
        }

        public Vector3 Position { get; set; }

        public Vector3 Source { get; set; }
        public float Distance { get; set; }

        public MapObject MapObject { get; set; }
    }
}
