using GameServer.MapObjects;
using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Physics
{
    public static class Intersection
    {
        public static double DistanceSquared(Vector3d v1, Vector3d v2)
        {
            double dx = Math.Abs(v1.X - v2.X);
            double dy = Math.Abs(v1.Y - v2.Y);
            double dz = Math.Abs(v1.Z - v2.Z);

            return Math.Pow(dx, 2) + Math.Pow(dy, 2) + Math.Pow(dz, 2);
        }

        public static double Distance(Vector3d v1, Vector3d v2)
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
            double distanceSquared = Math.Pow(s.DiameterX, 2);

            Vector3d c1 = new Vector3d()
            {
                X = c.Position.X - (c.Width / 2),
                Y = c.Position.Y - (c.Height / 2),
                Z = c.Position.Z - (c.Depth / 2)
            };

            Vector3d c2 = new Vector3d()
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
    }
}
