using GameServer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer.MapObjects
{
    public class MapCircle : MapObject
    {

        public MapCircle(float x, float y, float diameter, TextureEnum texture = TextureEnum.Default, MapObject parent = null)
            : base(x, y, texture, parent)
        {
            Diameter = diameter;
        }

        public MapCircle(Vector2 pos, float diameter, TextureEnum texture = TextureEnum.Default, MapObject parent = null)
            : this(pos.X, pos.Y, diameter, texture, parent)
        {
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
