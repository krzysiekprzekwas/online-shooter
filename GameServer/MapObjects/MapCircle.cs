using GameServer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.MapObjects
{
    public class MapCircle : MapObject
    {

        public MapCircle(double x, double y, double diameter, TextureEnum texture = TextureEnum.Default, MapObject parent = null)
            : base(x, y, texture, parent)
        {
            Diameter = diameter;
        }

        public MapCircle(Vector2 pos, double diameter, TextureEnum texture = TextureEnum.Default, MapObject parent = null)
            : this(pos.X, pos.Y, diameter, texture, parent)
        {
        }


        public double Diameter { get; set; }

        [JsonIgnore]
        public double Radius
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
        public double RadiusSquared
        {
            get
            {
                return Math.Pow(Radius, 2);
            }
        }
    }
}
