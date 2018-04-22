using System;
using System.Numerics;

namespace GameServer.MapObjects
{
    public class MapBox : MapObject
    {
        public string Type = @"box";
        public MapBox(float x, float y, float z, float w, float h, float d, MapObject parent = null, Color color = null, int texture =0)
            : base(x, y, z, parent, color, texture)
        {
            Width = w;
            Height = h;
            Depth = d;
            TextureId = TextureId;
        }

        public float Width { get; set; }
        public float Height { get; set; }
        public float Depth { get; set; }

        public Vector3[] GetVerticies()
        {
            float hw = Width / 2.0f;
            float hh = Height / 2.0f;
            float hd = Depth / 2.0f;

            return new Vector3[]
            {
                // Top
                new Vector3(Position.X - hw, Position.Y - hh, Position.Z - hd), // Left, front
                new Vector3(Position.X + hw, Position.Y - hh, Position.Z - hd), // Right, front
                new Vector3(Position.X + hw, Position.Y - hh, Position.Z + hd), // Right, back
                new Vector3(Position.X - hw, Position.Y - hh, Position.Z + hd), // Left, back
                // Bottom
                new Vector3(Position.X - hw, Position.Y + hh, Position.Z - hd), // Left, front
                new Vector3(Position.X + hw, Position.Y + hh, Position.Z - hd), // Right, front
                new Vector3(Position.X + hw, Position.Y + hh, Position.Z + hd), // Right, back
                new Vector3(Position.X - hw, Position.Y + hh, Position.Z + hd) // Left, back
            };
        }

        public MapQuad[] GetQuads()
        {
            Vector3[] v = GetVerticies();

            return new MapQuad[]
            {
                new MapQuad(v[0], v[1], v[2], v[3], this), // bottom
                new MapQuad(v[4], v[5], v[6], v[7], this), // top
                new MapQuad(v[0], v[1], v[5], v[4], this), // front
                new MapQuad(v[1], v[2], v[6], v[5], this), // right
                new MapQuad(v[3], v[2], v[6], v[7], this), // back
                new MapQuad(v[0], v[3], v[7], v[4], this), // left
            };
        }
    }
}
