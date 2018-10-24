using GameServer.Models;
using System;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;

namespace GameServer.MapObjects
{
    public abstract class MapObject : ICloneable, IEquatable<MapObject>
    {
        private static int _id = 1;

        public MapObject(double x, double y, TextureEnum texture, MapObject parent)
        {
            Position = new Vector2(x, y);
            Parent = parent;
            Texture = texture;
            Id = _id++;
        }

        public int Id { get; set; }

        public Vector2 Position { get; set; }

        public TextureEnum Texture { get; set; }

        public MapObject Parent { get; set; }

        public abstract object Clone();
        public abstract bool Equals(MapObject other);
    }
}
