using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Models
{
    public class Vector3d
    {
        // Vector3d (not using Vector3 because of float precission)
        public Vector3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3d() : this(0, 0, 0) { }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        // Adding 2 vectors
        public static Vector3d operator +(Vector3d a, Vector3d b)
        {
            Vector3d v = new Vector3d
            {
                X = a.X + b.X,
                Y = a.Y + b.Y,
                Z = a.Z + b.Z
            };

            return v;
        }

        // Scale operator
        public static Vector3d operator *(Vector3d v, double c)
        {
            Vector3d n = new Vector3d
            {
                X = v.X * c,
                Y = v.Y * c,
                Z = v.Z * c
            };

            return v;
        }
    }
}
