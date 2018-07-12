using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace GameServer.Physics
{
    public class Physic
    {
        public void A(Vector2 movementVector, float intersectionDistance, Vector2 intersectionNormal)
        {
            float movementVectorLength = movementVector.Length();
            var realMovementDirectionVector = Vector2.Normalize(movementVector) * intersectionDistance;

            var leftMovementDirectionVector = movementVector - realMovementDirectionVector;

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
    }
}


//float a = Vector2.Dot(vector, objectNormal) / objectNormal.Length();
//Vector2 b = objectNormal / objectNormal.Length();
//Vector2 projection = Vector2.Negate(a * b);

//            return vector + projection;
