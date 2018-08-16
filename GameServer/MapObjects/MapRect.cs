using GameServer.Models;
using System;

namespace GameServer.MapObjects
{
    public class MapRect : MapObject
    {
        public string Type = @"box";
        public MapRect(float x, float y, float w, float h, TextureEnum texture = TextureEnum.Default, MapObject parent = null)
            : base(x, y, texture, parent)
        {
            Width = w;
            Height = h;
        }
        
        public float Width { get; set; }
        public float Height { get; set; }

        public Vector2[] GetVerticies()
        {
            float hw = Width / 2.0f;
            float hh = Height / 2.0f;

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
