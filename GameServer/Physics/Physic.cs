using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models;

namespace GameServer.Physics
{
    public class Physic
    {
        public static Vector2 GetLeftParallelVectorToIntersectionNormal(Vector2 movementVector, float intersectionDistance, Vector2 intersectionNormal)
        {
            var movementVectorLength = movementVector.Length();
            var realMovementDirectionVector = Vector2.Normalize(movementVector) * intersectionDistance;

            var leftMovementDirectionVector = movementVector - realMovementDirectionVector;
            return GetParallelVectorToNormal(leftMovementDirectionVector, intersectionNormal);
        }

        public static Vector2 GetParallelVectorToNormal(Vector2 vector, Vector2 normal)
        {
            return ProjectVector(vector, RotateVector(normal, (float)(Math.PI / 2)));
        }

        public static Vector2 ProjectVector(Vector2 vector, Vector2 projectionVector)
        {
            var m = (projectionVector.Y) / (projectionVector.X);

            var x = (m * vector.Y + vector.X) / (m * m + 1);
            var y = (m * m * vector.Y + m * vector.X) / (m * m + 1);

            return new Vector2(x, y);
        }

        public static Vector2 RotateVector(Vector2 v, float angle)
        {
            var ca = (float)Math.Cos(angle);
            var sa = (float)Math.Sin(angle);
            return new Vector2(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y);
        }

        public static Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle));
        }

        public static float VectorToAngle(Vector2 vector)
        {
            if (vector.LengthSquared() != 1)
                vector = Vector2.Normalize(vector);

            return (float)Math.Asin(vector.X);
        }
    }
}
