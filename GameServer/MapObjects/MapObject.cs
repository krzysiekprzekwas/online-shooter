using GameServer.Models;
using GameServer.States;
using System;
using System.Collections;

namespace GameServer.MapObjects
{
    public abstract class MapObject : ICloneable, IEquatable<MapObject>
    {
        public MapObject(double x, double y, TextureEnum texture, MapObject parent)
        {
            Id = MapState.Instance.AssingMapObjectId();

            Position = new Vector2(x, y);
            Parent = parent;

            Texture = texture;
        }

        public Vector2 Position { get; set; }
        public int Id { get; set; }

        public TextureEnum Texture { get; set; }

        public MapObject Parent { get; set; }

        public abstract object Clone();
        public abstract bool Equals(MapObject other);
    }
}
