using System;
using System.Collections.Generic;
using System.Numerics;
using GameServer.Game;
using GameServer.MapObjects;
using GameServer.Models;
using GameServer.States;

namespace GameServer.Physics
{
    public class PhysicsEngine
    {
        private GameEngine _gameEngine;
        public PhysicsEngine(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        public void ApplyPhysics()
        {
            foreach (Player player in GameState.Instance.value.Players)
            {
                player.Speed *= Config.PLAYER_DECCELERATION;
                Vector2 speedVector = player.Speed + CalculateSpeedVector(player);

                speedVector = GetNewPlayerPosition(player, speedVector);

                player.Speed = speedVector;
                player.Position += speedVector;
            }
        }

        public MapObject GetIntersectionObjectTowardsDirection(out float offset, Player player, Vector2 speedVectorNormalized, float speedVectorLength)
        {
            // Variables used to calculate speed vector
            offset = 0f;
            float currentPrecision = speedVectorLength / 2f;
            MapObject intersectionObject = null;

            do
            {
                // Create moved sphere
                Vector2 checkPosition = player.Position + (speedVectorNormalized * (offset + currentPrecision));
                MapEllipse s = new MapEllipse(checkPosition, player.Diameter);

                // Check for intersection
                intersectionObject = CheckAnyIntersectionWithWorld(s);

                // Update new position and offset
                if (intersectionObject != null) // Object found try a bit closer
                    currentPrecision /= 2f;
                else // No object found, increase offset
                {
                    offset += currentPrecision;
                    currentPrecision /= 2f;
                }


            } // Do this as long as we reach desired precision
            while (currentPrecision >= Config.INTERSECTION_INTERVAL);

            return intersectionObject;
        }

        public Vector2 GetNewPlayerPosition(Player player, Vector2 speedVector)
        {
            // Calculate length
            float speedVectorLength = speedVector.Length();

            // Get movement vectors
            if (speedVectorLength == 0)
                return new Vector2(0, 0);

            Vector2 speedVectorNormalized = Vector2.Normalize(speedVector);

            // Get intersection object
            MapObject intersectionObject = GetIntersectionObjectTowardsDirection(out float offset, player, speedVectorNormalized, speedVectorLength);

            Vector2 parralelVector = GetParralelMovementVector(player, speedVector, speedVectorLength, intersectionObject, offset);

            // Update speed vector
            return (speedVectorNormalized * offset) + parralelVector;
        }

        private Vector2 GetParralelMovementVector(Player player, Vector2 speedVector, float speedVectorLength, MapObject intersectionObject, float offset)
        {
            Vector2 parralelVector = new Vector2(0, 0);

            // Parralel movemnt
            if (intersectionObject != null)
            {
                Vector2 forwardVector = Vector2.Normalize(speedVector);
                Vector2 leftVector = RotateVectorAroundYAxis(forwardVector, (float)Math.PI / 2f);
                Vector2 upVector = new Vector2(0, 1);

                Ray[] rays = new Ray[]
                {
                        new Ray(player.Position, speedVector), // FRONT
                        new Ray(player.Position + (leftVector * player.Radius), speedVector), // LEFT  + forwardVector * (player.Radius / 4f)
                        new Ray(player.Position + (-leftVector * player.Radius), speedVector), // RIGHT + forwardVector * (player.Radius / 4f)
                };

                Trace closestTrace = GetClosestTrace(rays);

                if (closestTrace != null)
                {
                    float leftDistance = speedVectorLength - offset;
                    Vector2 leftSpeedVector = Vector2.Normalize(speedVector) * leftDistance;
                    parralelVector = GetVectorParralelProjectionToObjectNormal(leftSpeedVector, closestTrace.ObjectNormal);
                }
            }

            return parralelVector;
        }

        public MapObject CheckAnyIntersectionWithWorld(MapEllipse s)
        {
            // Check intersection with all map objects
            foreach (MapObject obj in MapState.Instance.MapObjects)
            {
                bool intersects = false;
                if (obj is MapRect)
                    intersects = Intersection.CheckIntersection((MapRect)obj, s);
                else if (obj is MapEllipse)
                    intersects = Intersection.CheckIntersection((MapEllipse)obj, s);

                if (intersects)
                    return obj;
            }

            return null;
        }


        public void ApplyPhysicsRayCast()
        {
            foreach (Player player in GameState.Instance.value.Players)
            {
                // Deccelerate player every tick
                player.Speed *= Config.PLAYER_DECCELERATION;

                // Process movement caused by input
                Vector2 speedVector = player.Speed + CalculateSpeedVector(player);
                UpdatePlayerPosition(player, speedVector);
            }
        }

        public void UpdatePlayerPosition(Player player, Vector2 speedVector, int iterations = 1)
        {
            // Get movement vectors
            Trace closestTrace = null;
            Vector2 movementVector = GetPlayerMovementVector(player, speedVector, ref closestTrace);

            // Save data about movement
            player.Position += movementVector;

            // TODO Maybe assign speedVector if collision detection is also client sided
            player.Speed = movementVector; // Assign movement vector not speed vector (does not matter where player wanted to go, just where he moved)

            if (closestTrace != null)
            {
                // Calculate movement
                float leftDistance = 0.5f * (speedVector.Length() - closestTrace.Distance + player.Radius);

                // Now player will collide so we can move him along the wall
                if (leftDistance > 0.001 && iterations <= 1)
                {
                    Vector2 leftSpeedVector = Vector2.Normalize(speedVector) * leftDistance;
                    Vector2 parralelVector = GetVectorParralelProjectionToObjectNormal(leftSpeedVector, closestTrace.ObjectNormal);

                    iterations++;
                    UpdatePlayerPosition(player, parralelVector, iterations);
                    return;
                }
            }
        }

        public Vector2 GetPlayerMovementVector(Player player, Vector2 speedVector, ref Trace closestTrace)
        {
            // Get movement vectors
            if (speedVector.LengthSquared() == 0)
                return new Vector2(0, 0);

            // Calculate direction vectors
            Vector2 forwardVector = Vector2.Normalize(speedVector);
            Vector2 leftVector = RotateVectorAroundYAxis(forwardVector, (float)Math.PI / 2f);
            Vector2 upVector = new Vector2(0, 1);

            // Create player ray
            // TODO? Maybe add forward vector (forwardVector * player.Radius)
            Ray[] rays = new Ray[]
            {
                new Ray(player.Position, speedVector), // FRONT
                //new Ray(player.Position + (leftVector * player.Radius), speedVector), // LEFT  + forwardVector * (player.Radius / 4f)
                //new Ray(player.Position + (-leftVector * player.Radius), speedVector), // RIGHT + forwardVector * (player.Radius / 4f)
            };

            // Check collision
            closestTrace = GetClosestTrace(rays);

            // No obstacles
            if (closestTrace == null)
                return speedVector;

            // Obstacle is too far, we dont consider it
            if (closestTrace.Distance > speedVector.Length() + player.Radius)
                return speedVector;

            // Calculate how far I can go that direction
            float moveDistance = closestTrace.Distance - player.Radius;
            speedVector = Vector2.Normalize(speedVector) * moveDistance;

            return speedVector;
        }

        private Trace GetClosestTrace(Ray[] rays)
        {
            Trace closestTrace = null;

            // Get closest trace for any of rays
            foreach (Ray ray in rays)
            {
                Trace trace = GetClosestTrace(ray);
                if (trace == null)
                    continue;

                if (closestTrace == null || trace.Distance < closestTrace.Distance)
                    closestTrace = trace;
            }

            return closestTrace;
        }


        private Trace GetClosestTrace(Ray ray)
        {
            Trace closestTrace = null;

            // Check collision
            foreach (MapObject obj in MapState.Instance.MapObjects)
            {
                Trace trace = null;
                if (obj is MapRect)
                    trace = RayCast.CheckBulletTrace((MapRect)obj, ray);
                else if (obj is MapEllipse)
                    trace = RayCast.CheckBulletTrace((MapEllipse)obj, ray);

                // No collision with this object - check next one
                if (trace == null)
                    continue;

                // Store closest traces
                if (closestTrace == null || trace.Distance < closestTrace.Distance)
                    closestTrace = trace;
            }

            return closestTrace;
        }

        private Vector2 CalculateSpeedVector(Player player)
        {
            Vector2 forwardAngle = new Vector2((float)Math.Sin(player.Angle), (float)Math.Cos(player.Angle));
            Vector2 leftAngle = new Vector2((float)Math.Sin(player.Angle - (float)Math.PI / 2), (float)Math.Cos(player.Angle - (float)Math.PI / 2));
            Vector2 speedVector = new Vector2(0, 0);

            // First get direction
            if (player.Keys.Contains("w"))
                speedVector += forwardAngle;
            if (player.Keys.Contains("s"))
                speedVector -= forwardAngle;
            if (player.Keys.Contains("a"))
                speedVector += leftAngle;
            if (player.Keys.Contains("d"))
                speedVector -= leftAngle;

            // Scale vector to be speed length
            if (speedVector.LengthSquared() > 0)
            {
                Vector2 normalizedSpeed = Vector2.Normalize(speedVector);
                float vectorLength = Config.PLAYER_SPEED / (float)Config.SERVER_TICK;
                speedVector = normalizedSpeed * vectorLength;
            }

            return speedVector;
        }


        /// <summary>
        /// Vector rotation by specifed amount of radians, Y axis stays the same, (Counter clockwise -> PI / 2 = left rotation)
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="radians"></param>
        /// <returns>Rotated vector</returns>
        public static Vector2 RotateVectorAroundYAxis(Vector2 vector, float radians)
        {
            float ca = (float)Math.Cos(radians);
            float sa = (float)Math.Sin(radians);

            return new Vector2(ca * vector.X - sa * vector.Y, sa * vector.X + ca * vector.Y);
        }

        /// <summary>
        /// Projecting vector after trace to go parralel to hit surface
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="objectNormal"></param>
        /// <returns>Perpendicular vector to objectNormal</returns>
        public static Vector2 GetVectorParralelProjectionToObjectNormal(Vector2 vector, Vector2 objectNormal)
        {
            float a = Vector2.Dot(vector, objectNormal) / objectNormal.Length();
            Vector2 b = objectNormal / objectNormal.Length();
            Vector2 projection = Vector2.Negate(a * b);

            return vector + projection;
        }
    }
}
