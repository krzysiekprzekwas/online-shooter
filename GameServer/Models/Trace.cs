using GameServer.MapObjects;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer.Models
{
    public class Trace
    {
        public Trace(Vector3 position, Vector3 source, float distance, MapObject mapObject, Vector3 objectNormal)
        {
            Position = position;
            Source = source;
            Distance = distance;
            MapObject = mapObject;
            ObjectNormal = objectNormal;
        }

        public Vector3 Position { get; private set; }

        public Vector3 Source { get; private set; }
        public float Distance { get; private set; }

        public MapObject MapObject { get; set; }

        private Vector3 _objectNormal;
        public Vector3 ObjectNormal
        {
            get
            {
                return _objectNormal;
            }
            private set
            {
                _objectNormal = Vector3.Normalize(value);
            }
        }
    }
}
