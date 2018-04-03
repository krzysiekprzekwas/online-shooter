using GameServer.MapObjects;
using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer.Physics
{
    public static class Intersection
    {
        public static double DistanceSquared(Vector3 v1, Vector3 v2)
        {
            double dx = Math.Abs(v1.X - v2.X);
            double dy = Math.Abs(v1.Y - v2.Y);
            double dz = Math.Abs(v1.Z - v2.Z);

            return Math.Pow(dx, 2) + Math.Pow(dy, 2) + Math.Pow(dz, 2);
        }

        public static double Distance(Vector3 v1, Vector3 v2)
        {
            return Math.Sqrt(DistanceSquared(v1, v2));
        }

        // Checking intersection for each type of MapObjects

        public static bool CheckIntersection(MapSphere s, MapBox c)
        {
            return CheckIntersection(c, s);
        }
        public static bool CheckIntersection(MapBox c, MapSphere s)
        {
            double distanceSquared = Math.Pow(s.Diameter / 2, 2);

            Vector3 c1 = new Vector3()
            {
                X = c.Position.X - (c.Width / 2),
                Y = c.Position.Y - (c.Height / 2),
                Z = c.Position.Z - (c.Depth / 2)
            };

            Vector3 c2 = new Vector3()
            {
                X = c.Position.X + (c.Width / 2),
                Y = c.Position.Y + (c.Height / 2),
                Z = c.Position.Z + (c.Depth / 2)
            };

            if (s.Position.X < c1.X) distanceSquared -= Math.Pow(s.Position.X - c1.X, 2);
            else if (s.Position.X > c2.X) distanceSquared -= Math.Pow(s.Position.X - c2.X, 2);
            if (s.Position.Y < c1.Y) distanceSquared -= Math.Pow(s.Position.Y - c1.Y, 2);
            else if (s.Position.Y > c2.Y) distanceSquared -= Math.Pow(s.Position.Y - c2.Y, 2);
            if (s.Position.Z < c1.Z) distanceSquared -= Math.Pow(s.Position.Z - c1.Z, 2);
            else if (s.Position.Z > c2.Z) distanceSquared -= Math.Pow(s.Position.Z - c2.Z, 2);

            return distanceSquared > 0;
        }

        public static bool CheckIntersection(MapSphere s1, MapSphere s2)
        {
            double distanceSquared = DistanceSquared(s1.Position, s2.Position);
            double diameterSquared = Math.Pow((s1.Diameter / 2) + (s2.Diameter / 2), 2);
            
            return diameterSquared > distanceSquared;
        }

        public static bool CheckIntersection(Vector3 position, MapBox c)
        {
            return CheckIntersection(c, position);
        }
        public static bool CheckIntersection(MapBox c, Vector3 position)
        {
            float hw = c.Width / 2f;
            float hh = c.Height / 2f;
            float hz = c.Depth / 2f;

            return ((position.X >= c.Position.X - hw && position.X <= c.Position.X + hw) &&
                (position.Y >= c.Position.Y - hh && position.Y <= c.Position.Y + hh) &&
                (position.Z >= c.Position.Z - hz && position.Z <= c.Position.Z + hz));
        }

        //public static bool CheckIntersection(MapBox box, Ray ray)
        //{

        //    // 1.
        //    Vector3 dS21 = S2.sub(S1);
        //    Vector3 dS31 = S3.sub(S1);
        //    Vector3 n = dS21.cross(dS31);

        //    // 2.
        //    Vector3 dR = R1.sub(R2);

        //    float ndotdR = n.dot(dR);

        //    if (Math.abs(ndotdR) < 1e-6f)
        //    { // Choose your tolerance
        //        return false;
        //    }

        //    float t = -n.dot(R1.sub(S1)) / ndotdR;
        //    Vector3 M = R1.add(dR.scale(t));

        //    // 3.
        //    Vector3 dMS1 = M.sub(S1);
        //    float u = dMS1.dot(dS21);
        //    float v = dMS1.dot(dS31);

        //    // 4.
        //    return (u >= 0.0f && u <= dS21.dot(dS21)
        //         && v >= 0.0f && v <= dS31.dot(dS31));
        //}

        //public class Test
        //{
        //    static class Vector3
        //    {
        //        public float x, y, z;

        //        public Vector3(float x, float y, float z)
        //        {
        //            this.x = x;
        //            this.y = y;
        //            this.z = z;
        //        }

        //        public Vector3 add(Vector3 other)
        //        {
        //            return new Vector3(x + other.x, y + other.y, z + other.z);
        //        }

        //        public Vector3 sub(Vector3 other)
        //        {
        //            return new Vector3(x - other.x, y - other.y, z - other.z);
        //        }

        //        public Vector3 scale(float f)
        //        {
        //            return new Vector3(x * f, y * f, z * f);
        //        }

        //        public Vector3 cross(Vector3 other)
        //        {
        //            return new Vector3(y * other.z - z * other.y,
        //                               z - other.x - x * other.z,
        //                               x - other.y - y * other.x);
        //        }

        //        public float dot(Vector3 other)
        //        {
        //            return x * other.x + y * other.y + z * other.z;
        //        }
        //    }
            
        //}
    }
}
