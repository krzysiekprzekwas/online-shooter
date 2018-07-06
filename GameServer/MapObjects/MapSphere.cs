using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer.MapObjects
{
    public class MapEllipse : MapObject
    {

        public MapEllipse(float x, float y, float diameter, MapObject parent = null, Color color = null, int texture = 0)
            : base(x, y, parent, color, texture)
        {
            Diameter = diameter;
        }

        public MapEllipse(Vector2 pos, float diameter, MapObject parent = null, Color color = null, int texture = 0)
            : base(pos.X, pos.Y, parent, color, texture)
        {
            Diameter = diameter;
        }


        public float Diameter { get; set; }

        [JsonIgnore]
        public float Radius
        {
            get
            {
                return Diameter / 2f;
            }
            set
            {
                Diameter = value * 2f;
            }
        }

        [JsonIgnore]
        public float RadiusSquared
        {
            get
            {
                return (float)Math.Pow(Radius, 2);
            }
        }


    }
}
