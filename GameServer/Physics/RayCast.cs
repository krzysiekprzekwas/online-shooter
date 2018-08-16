using GameServer.MapObjects;
using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Physics
{
    public class RayCast
    {
        public static Trace CheckBulletTrace(Ray ray, MapRect rect)
        {
            Trace closestTrace = null;

            var verticies = rect.GetVerticies();
            for(int i = 0; i < verticies.Length - 1; i++)
            {
                Trace trace = CheckBulletTrace(ray, verticies[i], verticies[i + 1]);
                if (trace != null && (closestTrace == null || trace.Distance < closestTrace.Distance))
                    closestTrace = trace;
            }

            if(closestTrace != null)
                closestTrace.MapObject = rect;

            return closestTrace;
        }

        public static Trace CheckBulletTrace(Ray ray, MapCircle circle)
        {
            Vector2 h = Vector2.Subtract(circle.Position, ray.Origin);
            var lf = Vector2.Dot(ray.Direction, h);
            var s = (circle.RadiusSquared) - Vector2.Dot(h, h) + (lf * lf);   // s=r^2-h^2+lf^2

            if (s < 0.0 || lf < 0.0) return null;                    // no intersection points ?
            s = (float)Math.Sqrt(s);                              // s=sqrt(r^2-h^2+lf^2)

            s = (lf < s && lf + s >= 0) ? -s : s; // S1 behind A ? AND S2 before A ? -> swap S1 <-> S2

            Vector2 position = ray.Origin + Vector2.Multiply(ray.Direction, lf - s);
            Vector2 normal = Vector2.Normalize(circle.Position - position);

            return new Trace(position, ray.Origin, circle, normal);
        }

        public static Trace CheckBulletTrace(Ray ray, Vector2 p1, Vector2 p2)
        {
            var v = ray.Origin - p1;
            var u = p2 - p1;
            var v3 = new Vector2(-ray.Direction.Y, ray.Direction.X);

            var dot = Vector2.Dot(u, v3);
            if (Math.Abs(dot) < 0.000001)
                return null;

            var distance = ((u.X * v.Y) - (u.Y * v.X)) / dot;
            var t2 = Vector2.Dot(v, v3) / dot;

            if (distance >= 0.0 && (t2 >= 0.0 && t2 <= 1.0))
            {
                var hitPosition = ray.Origin + (ray.Direction * distance);
                var normal = new Vector2(u.Y, -u.X);
                var trace = new Trace(hitPosition, ray.Origin, null, normal);
                return trace;
            }

            return null;
        }

    }
}
