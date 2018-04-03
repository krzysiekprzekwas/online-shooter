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

        public static bool CheckIntersection(MapBox box, Ray ray, out Vector3 point)
        {
            point = new Vector3(0, 0, 0);

            foreach (var quad in box.GetQuads())
            {
                bool intersects = CheckIntersection(quad: quad, ray: ray, point: out point);
                if (intersects)
                    return true;
            }

            return false;
        }
        public static bool CheckIntersection(MapQuad quad, Ray ray, out Vector3 point)
        {
            point = new Vector3(0, 0, 0);

            foreach (var triangle in quad.GetTriangles())
            {
                bool intersects = CheckIntersection(triangle: triangle, ray: ray, point: out point);
                if (intersects)
                    return true;
            }

            return false;
        }

        public static bool CheckIntersection(MapTriangle triangle, Ray ray, out Vector3 point)
        {
            point = new Vector3(0, 0, 0);

            // get triangle edge vectors and plane normal
            Vector3 u = triangle.Verticies[1] - triangle.Verticies[0];
            Vector3 v = triangle.Verticies[2] - triangle.Verticies[0];
            Vector3 n = Vector3.Cross(u, v);

            // Invalid triangle (degenerate)
            if (n.LengthSquared() == 0)
                return false;

            Vector3 w0 = ray.Origin - triangle.Verticies[0];
            float a = (-1) * Vector3.Dot(n, w0);
            float b = Vector3.Dot(n, ray.Direction);

            // Parallel to triangle plane
            if (Math.Abs(b) < 1e-6f)
            {
                if (a == 0) // Ray lies inside plane
                    return false;

                return false;
            }

            // Get distance to intersection point
            float r = a / b;
            if (r < 0.0) // Ray goes different way -> no intersection
                return false;

            // Calculate intersection point
            point = ray.Origin + (r * ray.Direction);

            // Check if point is inside T
            float uu, uv, vv, wu, wv, D;
            uu = Vector3.Dot(u, u);
            uv = Vector3.Dot(u, v);
            vv = Vector3.Dot(v, v);
            Vector3 w = point - triangle.Verticies[0];
            wu = Vector3.Dot(w, u);
            wv = Vector3.Dot(w, v);
            D = uv * uv - uu * vv;

            // Get parametric coords
            float s, t;
            s = (uv * wv - vv * wu) / D;
            if (s < 0.0 || s > 1.0)         // I is outside T
                return false;
            t = (uv * wu - uu * wv) / D;
            if (t < 0.0 || (s + t) > 1.0)  // I is outside T
                return false;

            return true;                       // I is in T
        }
    }
}
