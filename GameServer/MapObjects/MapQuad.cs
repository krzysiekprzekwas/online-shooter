using System;
using System.Linq;
using System.Numerics;

namespace GameServer.MapObjects
{
    public class MapQuad : MapObject
    {
        public string Type = @"quad";

        public MapQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Color color = null, int texture = 0)
            : base((v1.X + v2.X + v3.X + v4.X) / 4.0f, (v1.Y + v2.Y + v3.Y + v4.Y) / 4.0f, (v1.Z + v2.Z + v3.Z + v4.Z) / 4.0f, color, texture)
        {
            float maxX = new[] { v1.X, v2.X, v3.X, v4.X }.Max();
            float minX = new[] { v1.X, v2.X, v3.X, v4.X }.Min();

            float maxY = new[] { v1.Y, v2.Y, v3.Y, v4.Y }.Max();
            float minY = new[] { v1.Y, v2.Y, v3.Y, v4.Y }.Min();

            float maxZ = new[] { v1.Z, v2.Z, v3.Z, v4.Z }.Max();
            float minZ = new[] { v1.Z, v2.Z, v3.Z, v4.Z }.Min();

            Width = maxX - minX;
            Height = maxY - minY;
            Depth = maxZ - minZ;

            Verticies = new Vector3[] { v1, v2, v3, v4 };
        }

        public float Width { get; set; }
        public float Height { get; set; }
        public float Depth { get; set; }

        public Vector3[] Verticies;

        public MapTriangle[] GetTriangles()
        {
            return new MapTriangle[]
            {
                new MapTriangle(Verticies[0], Verticies[1], Verticies[2]),
                new MapTriangle(Verticies[0], Verticies[2], Verticies[3])
            };
        }
    }
}
