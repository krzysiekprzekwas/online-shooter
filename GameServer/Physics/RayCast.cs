using GameServer.MapObjects;
using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer.Physics
{
    public class RayCast
    {
        public static Trace CheckBulletTrace(MapSphere s, Ray ray)
        {
            Vector3 closestPointOnLine = new Vector3(0, 0, 0);

            // TODO
            // Calculate distance from ray to sphere center

            return null;
        }


        public static Trace CheckBulletTrace(MapBox box, Ray ray)
        {
            foreach (var quad in box.GetQuads())
            {
                Trace trace = CheckBulletTrace(quad, ray);

                if (trace != null)
                {
                    trace.MapObject = box;
                    return trace;
                }
            }

            return null;
        }
        public static Trace CheckBulletTrace(MapQuad quad, Ray ray)
        {
            foreach (var triangle in quad.GetTriangles())
            {
                Trace trace = CheckBulletTrace(triangle, ray);
                if (trace != null)
                {
                    trace.MapObject = quad;
                    return trace;
                }
            }

            return null;
        }

        public static Trace CheckBulletTrace(MapTriangle triangle, Ray ray)
        {
            // get triangle edge vectors and plane normal
            Vector3 u = triangle.Verticies[1] - triangle.Verticies[0];
            Vector3 v = triangle.Verticies[2] - triangle.Verticies[0];
            Vector3 n = Vector3.Cross(u, v);

            // Invalid triangle (degenerate)
            if (n.LengthSquared() == 0)
                return null;

            Vector3 w0 = ray.Origin - triangle.Verticies[0];
            float a = (-1) * Vector3.Dot(n, w0);
            float b = Vector3.Dot(n, ray.Direction);

            // Parallel to triangle plane
            if (Math.Abs(b) < 1e-6f)
            {
                if (a == 0) // Ray lies inside plane
                    return new Trace(ray.Origin, ray.Origin, 0, triangle);

                return null;
            }

            // Get distance to intersection point
            float r = a / b;
            if (r < 0.0) // Ray goes different way -> no intersection
                return null;

            // Calculate intersection point
            Vector3 position = ray.Origin + (r * ray.Direction);

            // Check if point is inside T
            float uu, uv, vv, wu, wv, D;
            uu = Vector3.Dot(u, u);
            uv = Vector3.Dot(u, v);
            vv = Vector3.Dot(v, v);
            Vector3 w = position - triangle.Verticies[0];
            wu = Vector3.Dot(w, u);
            wv = Vector3.Dot(w, v);
            D = uv * uv - uu * vv;

            // Get parametric coords
            float s, t;
            s = (uv * wv - vv * wu) / D;
            if (s < 0.0 || s > 1.0)         // I is outside T
                return null;
            t = (uv * wu - uu * wv) / D;
            if (t < 0.0 || (s + t) > 1.0)  // I is outside T
                return null;

            return new Trace(position, ray.Origin, r, triangle);                       // I is in T
        }
    }
}
