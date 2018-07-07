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

        //public static Trace CheckBulletTrace(Ray ray, MapCircle ellipse)
        //{
        //    throw new NotImplementedException();
        //    //if (rect.Height / rect.Width != (ray.Direction.Y - ray.Origin.Y) / (ray.Direction.X - ray.Origin.X))
        //    //{
        //    //    var d = ((rect.Width * (ray.Direction.Y - ray.Origin.Y)) - rect.Height * (ray.Direction.X - ray.Origin.X));
        //    //    if (d != 0)
        //    //    {
        //    //        var r = (((rect.Position.Y - ray.Origin.Y) * (ray.Direction.X - ray.Origin.X)) - (rect.Position.X - ray.Origin.X) * (ray.Direction.Y - ray.Origin.Y)) / d;
        //    //        var s = (((rect.Position.Y - ray.Origin.Y) * rect.Width) - (rect.Position.X - ray.Origin.X) * rect.Height) / d;
        //    //        if (r >= 0 && s >= 0 && s <= 1)
        //    //        {
        //    //            Vector2 position = new Vector2(rect.Position.X + r * rect.Width, rect.Position.Y + r * rect.Height);
        //    //            return new Trace(position, ray.Origin, rect, new Vector2(0, 0));
        //    //        }
        //    //    }
        //    //}

        //    //return null;


        //    // TODO: Seems like solid ray circle intersection
        //    //int rayCircleIntersection(Ray ray, Circle c, PVector S1, PVector S2)
        //    //{
        //    //    PVector e = new PVector(ray.direction.x, ray.direction.y, ray.direction.z);   // e=ray.dir
        //    //    e.normalize();                            // e=g/|g|
        //    //    PVector h = PVector.sub(c.center, ray.origin);  // h=r.o-c.M
        //    //    float lf = e.dot(h);                      // lf=e.h
        //    //    float s = sq(c.radius) - h.dot(h) + sq(lf);   // s=r^2-h^2+lf^2
        //    //    if (s < 0.0) return 0;                    // no intersection points ?
        //    //    s = sqrt(s);                              // s=sqrt(r^2-h^2+lf^2)

        //    //    int result = 0;
        //    //    if (lf < s)                               // S1 behind A ?
        //    //    {
        //    //        if (lf + s >= 0)                          // S2 before A ?}
        //    //        {
        //    //            s = -s;                               // swap S1 <-> S2}
        //    //            result = 1;                           // one intersection point
        //    //        }
        //    //    }
        //    //    else result = 2;                          // 2 intersection points

        //    //    S1.set(PVector.mult(e, lf - s)); S1.add(ray.origin); // S1=A+e*(lf-s)
        //    //    S2.set(PVector.mult(e, lf + s)); S2.add(ray.origin); // S2=A+e*(lf+s)

        //    //    // only for testing
        //    //    fill(0);
        //    //    text(" s=" + nf(s, 0, 2), 160, 40);
        //    //    text("lf=" + nf(lf, 0, 2), 240, 40);

        //    //    return result;
        //    //}
        //}

        public static Trace CheckBulletTrace(Ray ray, MapCircle circle)
        {
            Vector2 h = Vector2.Subtract(circle.Position, ray.Origin);
            float lf = Vector2.Dot(ray.Direction, h);
            float s = (circle.RadiusSquared) - Vector2.Dot(h, h) + (lf * lf);   // s=r^2-h^2+lf^2

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

            float distance = ((u.X * v.Y) - (u.Y * v.X)) / dot;
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
