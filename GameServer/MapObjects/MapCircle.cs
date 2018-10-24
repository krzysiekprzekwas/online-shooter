using GameServer.Models;
using Newtonsoft.Json;
using System;

namespace GameServer.MapObjects
{
    public class MapCircle : MapObject
    {
        public MapCircle(double x, double y, double radius, TextureEnum texture = TextureEnum.Default, MapObject parent = null)
            : base(x, y, texture, parent)
        {
            Radius = radius;
        }

        public MapCircle(Vector2 pos, double radius, TextureEnum texture = TextureEnum.Default, MapObject parent = null)
            : this(pos.X, pos.Y, radius, texture, parent)
        {
        }

        public MapCircle(MapCircle c)
            : this(c.Position.X, c.Position.Y, c.Radius, c.Texture, c.Parent)
        {
        }

        public double Radius { get; set; }

        [JsonIgnore]
        public double RadiusSquared => Math.Pow(Radius, 2);

        public override string ToString()
        {
            return $"Circle<x:{Position.X} y:{Position.Y}, r:{Radius}>";
        }

        public override object Clone()
        {
            return new MapCircle(this);
        }

        public override bool Equals(MapObject other)
        {
            var c = other as MapCircle;

            if (c == null)
                return false;

            return Position.Equals(c.Position) && Radius == c.Radius;
        }
    }
}
