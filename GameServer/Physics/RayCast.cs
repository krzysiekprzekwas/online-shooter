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
        private static float PARRALEL_TOLLERANCE = 0.9f;

        /// <summary>
        /// Get closest point on line
        /// </summary>
        /// <param name="A">First point lying on line</param>
        /// <param name="B">Second point lying on line</param>
        /// <param name="P">Relative point for which we will be looking for closest point</param>
        /// <returns>Closest point on line A to point p</returns>
        public static Vector3 GetClosestPointOnLine(Vector3 A, Vector3 B, Vector3 P)
        {
            Vector3 AB = B - A;
            Vector3 AP = P - A;
            Vector3 QP = AP - Vector3.Multiply(AB, Vector3.Dot(AP, AB) / AB.LengthSquared());
            Vector3 Q = P - QP;

            return Q;
        }

        public static Trace CheckBulletTrace(MapSphere s, Ray ray)
        {
            // Ray started from inside sphere
            if((s.Position - ray.Origin).LengthSquared() < s.RadiusSquared)
                return null;

            // Get closest point on line to sphere center
            Vector3 closestPointOnLine = GetClosestPointOnLine(ray.Origin + ray.Direction, ray.Origin, s.Position);

            // Get triangle parameters
            float aSquared = (closestPointOnLine - s.Position).LengthSquared();
            
            float radiusSquared = (float)Math.Pow(s.Diameter / 2, 2);

            // Ray not going through the sphere
            if (aSquared > radiusSquared)
                return null;

            // Half the size of ray length inside sphere
            float b = (float)Math.Sqrt(radiusSquared - aSquared);

            // Touching sphere at exactly one point
            if(b == 0)
            {
                float d = (closestPointOnLine - ray.Origin).Length();
                return new Trace(closestPointOnLine, ray.Origin, d, s);
            }

            // Find relative point
            Vector3 relativeVector = closestPointOnLine - ray.Origin;
            Vector3 relativeRay = Vector3.Normalize(relativeVector);
            float relativeDistance = relativeVector.Length();

            // Sphere behind the ray
            // Dot returns angle between normalized vectors
            // 1 = heading same direction
            // -1 = heading opposite direction
            // 0 = vectors are parralel
            float dot = Vector3.Dot(relativeRay, ray.Direction);
            if(dot < PARRALEL_TOLLERANCE)
                return null;

            // Create trace object
            float dist = relativeDistance - b;
            Vector3 position = ray.Origin + dist * ray.Direction;
                
            return new Trace(position, ray.Origin, dist, s);
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
