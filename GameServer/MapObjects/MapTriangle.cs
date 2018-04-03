using System;
using System.Linq;
using System.Numerics;

namespace GameServer.MapObjects
{
    public class MapTriangle : MapObject
    {
        public string Type = @"triangle";

        public MapTriangle(Vector3 v1, Vector3 v2, Vector3 v3, Color color = null, int texture = 0)
            : base((v1.X + v2.X + v3.X) / 3.0f, (v1.Y + v2.Y + v3.Y) / 3.0f, (v1.X + v2.X + v3.X) / 3.0f, color, texture)
        {
            float maxX = new[] { v1.X, v2.X, v3.X }.Max();
            float minX = new[] { v1.X, v2.X, v3.X }.Min();

            float maxY = new[] { v1.Y, v2.Y, v3.Y }.Max();
            float minY = new[] { v1.Y, v2.Y, v3.Y }.Min();

            float maxZ = new[] { v1.Z, v2.Z, v3.Z }.Max();
            float minZ = new[] { v1.Z, v2.Z, v3.Z }.Min();

            Width = maxX = minX;
            Height = maxY - minY;
            Depth = maxZ - minZ;

            Verticies = new Vector3[] { v1, v2, v3 };
        }

        public float Width { get; set; }
        public float Height { get; set; }
        public float Depth { get; set; }

        public Vector3[] Verticies;
    }
}
