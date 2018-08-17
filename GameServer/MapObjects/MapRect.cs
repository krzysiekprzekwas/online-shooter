using GameServer.Models;
using System;

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
    }
}
