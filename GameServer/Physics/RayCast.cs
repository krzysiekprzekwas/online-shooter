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

        public static Trace CheckBulletTrace(MapRect rect, Ray ray)
        {
            throw new NotImplementedException();
        }

        public static Trace CheckBulletTrace(MapEllipse ellipse, Ray ray)
        {
            throw new NotImplementedException();

            //if (rect.Height / rect.Width != (ray.Direction.Y - ray.Origin.Y) / (ray.Direction.X - ray.Origin.X))
            //{
            //    var d = ((rect.Width * (ray.Direction.Y - ray.Origin.Y)) - rect.Height * (ray.Direction.X - ray.Origin.X));
            //    if (d != 0)
            //    {
            //        var r = (((rect.Position.Y - ray.Origin.Y) * (ray.Direction.X - ray.Origin.X)) - (rect.Position.X - ray.Origin.X) * (ray.Direction.Y - ray.Origin.Y)) / d;
            //        var s = (((rect.Position.Y - ray.Origin.Y) * rect.Width) - (rect.Position.X - ray.Origin.X) * rect.Height) / d;
            //        if (r >= 0 && s >= 0 && s <= 1)
            //        {
            //            Vector2 position = new Vector2(rect.Position.X + r * rect.Width, rect.Position.Y + r * rect.Height);
            //            return new Trace(position, ray.Origin, rect, new Vector2(0, 0));
            //        }
            //    }
            //}

            //return null;
        }

        public double? GetRayToLineSegmentIntersection(Ray ray, Vector2 p1, Vector2 p2)
        {
            var v = ray.Origin - p1;
            var u = p2 - p1;
            var v3 = new Vector2(-ray.Direction.Y, ray.Direction.X);

            var dot = Vector2.Dot(u, v3);
            if (Math.Abs(dot) < 0.000001)
                return null;

            var t1 = ((u.X * v.Y) - (u.Y * v.X)) / dot;


            var t2 = (v * v3) / dot;

            //if (t1 >= 0.0 && (t2 >= 0.0 && t2 <= 1.0))
            //    return t1;

            return null;
        }
    }
}
