using GameServer.Models;
using System;

namespace GameServer.MapObjects
{
    public abstract class MapObject : ICloneable, IEquatable<MapObject>
    {
        private static int _id = 1;

        public MapObject(double x, double y, TextureEnum texture)
        {
            Position = new Vector2(x, y);
            Texture = texture;
            Id = _id++;
        }

        public int Id { get; set; }

        public Vector2 Position { get; set; }

        public TextureEnum Texture { get; set; }

        public abstract object Clone();
        public abstract bool Equals(MapObject other);
    }
}
