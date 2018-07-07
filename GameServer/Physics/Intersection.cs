using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using GameServer.MapObjects;

namespace GameServer.Physics
{
    public static class Intersection
    {
        private static float Distance(Vector2 p1, Vector2 p2)
        {
            return Vector2.Distance(p1, p2);
        }

        private static float DistanceSquared(Vector2 p1, Vector2 p2)
        {
            return Vector2.DistanceSquared(p1, p2);
        }

        public static bool CheckIntersection(MapCircle s1, MapCircle s2)
        {
            return (DistanceSquared(s1.Position, s2.Position) <= Math.Pow(s1.Radius + s2.Radius, 2));
        }

        public static bool CheckIntersection(MapRect b, MapCircle s)
        {
            float x = Math.Abs(s.Position.X - b.Position.X);
            float y = Math.Abs(s.Position.Y - b.Position.Y);

            if (x > b.Width / 2 + s.Radius ||
                y > b.Height / 2 + s.Radius)
            {
                return false;
            }

            if (x <= b.Width / 2 || y <= b.Height / 2)
            {
                return true;
            }

            var cornerDistance_sq = Math.Pow(x - b.Width / 2, 2) + Math.Pow(y - b.Height / 2, 2);
            return cornerDistance_sq <= s.RadiusSquared;
        }

        public static bool CheckIntersection(MapCircle s, MapRect b)
        {
            return CheckIntersection(b, s);
        }

        public static bool CheckIntersection(MapRect r1, MapRect r2)
        {
            var r1x1 = r1.Position.X - (r1.Width / 2);
            var r1x2 = r1.Position.X + (r1.Width / 2);
            var r1y1 = r1.Position.Y - (r1.Height / 2);
            var r1y2 = r1.Position.Y + (r1.Height / 2);

            var r2x1 = r2.Position.X - (r2.Width / 2);
            var r2x2 = r2.Position.X + (r2.Width / 2);
            var r2y1 = r2.Position.Y - (r2.Height / 2);
            var r2y2 = r2.Position.Y + (r2.Height / 2);

            return (r2x2 >= r1x1 && r2x1 <= r1x2) && (r2y2 >= r1y1 && r2y1 <= r1y2);
        }
        
        private static bool DoesRectContainsVertex(MapRect rect, Vector2 point)
        {
            return (point.X >= rect.Position.X - (rect.Width / 2) &&
                point.X <= rect.Position.X + (rect.Width / 2) &&
                point.Y >= rect.Position.Y - (rect.Height / 2) &&
                point.Y <= rect.Position.Y + (rect.Height / 2));
        }
    }
}
