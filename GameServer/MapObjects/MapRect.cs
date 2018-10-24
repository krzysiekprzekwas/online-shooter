using GameServer.Models;
using System;
using GameServer.States;

namespace GameServer.MapObjects
{
    public class MapRect : MapObject
    {
        public string Type = @"box";
        public MapRect(double x, double y, double w, double h, TextureEnum texture = TextureEnum.Default, MapObject parent = null)
            : base(x, y, texture, parent)
        {
            Width = w;
            Height = h;
        }

        public MapRect(Vector2 pos, double w, double h, TextureEnum texture = TextureEnum.Default, MapObject parent = null)
            : this(pos.X, pos.Y, w, h, texture, parent)
        {
        }

        public MapRect(MapRect r)
            : this(r.Position.X, r.Position.Y, r.Width, r.Height, r.Texture, r.Parent)
        {
        }
        
        public double Width { get; set; }
        public double Height { get; set; }

        public Vector2[] GetVerticies()
        {
            double hw = Width / 2.0;
            double hh = Height / 2.0;

            return new Vector2[]
            {
                // Top
                new Vector2(Position.X - hw, Position.Y - hh), // Left
                new Vector2(Position.X + hw, Position.Y - hh), // Right
                // Bottom
                new Vector2(Position.X + hw, Position.Y + hh), // Right
                new Vector2(Position.X - hw, Position.Y + hh), // Left
            };
        }

        public override string ToString()
        {
            return $"Rect<x:{Position.X} y:{Position.Y}, w:{Width} h:{Height}>";
        }

        public override object Clone()
        {
            return new MapRect(this);
        }

        public override bool Equals(MapObject other)
        {
            var r = other as MapRect;

            if (r == null)
                return false;

            return Position.Equals(r.Position) && Width == r.Width && Height == r.Height;
        }
    }
}
