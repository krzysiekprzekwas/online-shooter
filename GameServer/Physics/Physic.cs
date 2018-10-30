using System;
using GameServer.Models;

namespace GameServer.Physics
{
    public class Physic
    {
        public static Vector2 GetLeftParallelVectorToIntersectionNormal(Vector2 movementVector, double intersectionDistance, Vector2 intersectionNormal)
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
            return projectionVector.Normalize() * Vector2.Dot(vector, projectionVector) / projectionVector.Length();
        }
        
        public static Vector2 RotateVector(Vector2 v, double angle)
        {
            var ca = Math.Cos(angle);
            var sa = Math.Sin(angle);
            return new Vector2(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y);
        }
    }
}
