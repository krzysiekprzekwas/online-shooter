using GameServer.MapObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Models
{
    public class Trace
    {
        public Trace(Vector2 position, Vector2 source, MapObject mapObject, Vector2 objectNormal, float distance = -1)
        {
            Position = position;
            Source = source;
            MapObject = mapObject;
            ObjectNormal = objectNormal;

            Distance = distance;
            if(Distance == -1)
                Distance = Vector2.Distance(position, source);
        }

        public Vector2 Position { get; private set; }

        public Vector2 Source { get; private set; }

        public double Distance { get; private set; }

        public MapObject MapObject { get; set; }

        private Vector2 _objectNormal;
        public Vector2 ObjectNormal
        {
            get
            {
                return _objectNormal;
            }
            private set
            {
                _objectNormal = Vector2.Normalize(value);
            }
        }
    }
}
