using GameServer.Game;
using GameServer.MapObjects;
using GameServer.Models;
using GameServer.States;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

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
                Vector3 speedVector = player.Speed + CalculateSpeedVector(player);

                speedVector = GetNewPlayerPosition(player, speedVector);

                player.Speed = speedVector;
                player.Position += speedVector;
            }
        }

        public Vector3 GetNewPlayerPosition(Player player, Vector3 speedVector)
        {
            // Calculate length
            float speedVectorLength = speedVector.Length();

            // Get movement vectors
            if (speedVectorLength == 0)
                return new Vector3(0, 0, 0);

            Vector3 speedVectorNormalized = Vector3.Normalize(speedVector);
            Vector3 parralelVector = new Vector3(0, 0, 0);

            // Variables used to calculate speed vector
            float offset = 0f;
            float currentPrecision = speedVectorLength / 2f;

            do
            {
                // Create moved sphere
                Vector3 checkPosition = player.Position + (speedVectorNormalized * (offset + currentPrecision));
                MapSphere s = new MapSphere(checkPosition, player.Diameter);

                // Check for intersection
                MapObject intersectionObject = CheckAnyIntersectionWithWorld(s);

                // Update new position and offset
                if (intersectionObject != null) // Object found try a bit closer
                    currentPrecision /= 2f;
                else // No object found, increase offset
                {
                    offset += currentPrecision;
                    currentPrecision /= 2f;
                }

                // Parralel movemnt
                if (intersectionObject != null)
                {
                    Vector3 forwardVector = Vector3.Normalize(speedVector);
                    Vector3 leftVector = RotateVectorAroundYAxis(forwardVector, (float)Math.PI / 2f);
                    Vector3 upVector = new Vector3(0, 1, 0);

                    Ray[] rays = new Ray[]
                    {
                        new Ray(player.Position, speedVector), // FRONT
                        new Ray(player.Position + (leftVector * player.Radius), speedVector), // LEFT  + forwardVector * (player.Radius / 4f)
                        new Ray(player.Position + (-leftVector * player.Radius), speedVector), // RIGHT + forwardVector * (player.Radius / 4f)
                    };

                    Trace closestTrace = null;
                    foreach (Ray ray in rays)
                    {
                        Trace trace = null;

                        if (intersectionObject is MapBox)
                            trace = RayCast.CheckBulletTrace((MapBox)intersectionObject, ray);
                        else if (intersectionObject is MapSphere)
                            trace = RayCast.CheckBulletTrace((MapSphere)intersectionObject, ray);

                        if (trace == null)
                            continue;

                        if (closestTrace == null || trace.Distance < closestTrace.Distance)
                            closestTrace = trace;
                    }

                    if (closestTrace != null)
                    {
                        float leftDistance = speedVectorLength - offset;
                        Vector3 leftSpeedVector = Vector3.Normalize(speedVector) * leftDistance;
                        parralelVector = GetVectorParralelProjectionToObjectNormal(leftSpeedVector, closestTrace.ObjectNormal);
                    }
                }

            } // Do this as long as we reach desired precision
            while (currentPrecision >= Config.INTERSECTION_INTERVAL);

            // Update speed vector
            return (speedVectorNormalized * offset) + parralelVector;
        }

        public MapObject CheckAnyIntersectionWithWorld(MapSphere s)
        {
            // Check intersection with all map objects
            foreach (MapObject obj in MapState.Instance.MapObjects)
            {
                bool intersects = false;
                if (obj is MapBox)
                    intersects = Intersection.CheckIntersection((MapBox)obj, s);
                else if (obj is MapSphere)
                    intersects = Intersection.CheckIntersection((MapSphere)obj, s);

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
                Vector3 speedVector = player.Speed + CalculateSpeedVector(player);
                UpdatePlayerPosition(player, speedVector);
            }
        }

        public void UpdatePlayerPosition(Player player, Vector3 speedVector, int iterations = 1)
        {
            // Get movement vectors
            Trace closestTrace = null;
            Vector3 movementVector = GetPlayerMovementVector(player, speedVector, ref closestTrace);

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
                    Vector3 leftSpeedVector = Vector3.Normalize(speedVector) * leftDistance;
                    Vector3 parralelVector = GetVectorParralelProjectionToObjectNormal(leftSpeedVector, closestTrace.ObjectNormal);

                    iterations++;
                    UpdatePlayerPosition(player, parralelVector, iterations);
                    return;
                }
            }
        }

        public Vector3 GetPlayerMovementVector(Player player, Vector3 speedVector, ref Trace closestTrace)
        {
            // Get movement vectors
            if (speedVector.LengthSquared() == 0)
                return new Vector3(0, 0, 0);

            // Calculate direction vectors
            Vector3 forwardVector = Vector3.Normalize(speedVector);
            Vector3 leftVector = RotateVectorAroundYAxis(forwardVector, (float)Math.PI / 2f);
            Vector3 upVector = new Vector3(0, 1, 0);

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
            speedVector = Vector3.Normalize(speedVector) * moveDistance;

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
                if (obj is MapBox)
                    trace = RayCast.CheckBulletTrace((MapBox)obj, ray);
                else if (obj is MapSphere)
                    trace = RayCast.CheckBulletTrace((MapSphere)obj, ray);

                // No collision with this object - check next one
                if (trace == null)
                    continue;

                // Store closest traces
                if (closestTrace == null || trace.Distance < closestTrace.Distance)
                    closestTrace = trace;
            }

            return closestTrace;
        }

        private Vector3 CalculateSpeedVector(Player player)
        {
            Vector3 forwardAngle = new Vector3((float)Math.Sin(player.Angles.Y), 0, (float)Math.Cos(player.Angles.Y));
            Vector3 leftAngle = new Vector3((float)Math.Sin(player.Angles.Y - (float)Math.PI / 2), 0, (float)Math.Cos(player.Angles.Y - Math.PI / 2));
            Vector3 speedVector = new Vector3(0, 0, 0);

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
                speedVector = Vector3.Normalize(speedVector) * ((Config.PLAYER_SPEED / (float)Config.SERVER_TICK));

            return speedVector;
        }


        /// <summary>
        /// Vector rotation by specifed amount of radians, Y axis stays the same, (Counter clockwise -> PI / 2 = left rotation)
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="radians"></param>
        /// <returns>Rotated vector</returns>
        public static Vector3 RotateVectorAroundYAxis(Vector3 vector, float radians)
        {
            float ca = (float)Math.Cos(radians);
            float sa = (float)Math.Sin(radians);

            return new Vector3(ca * vector.X - sa * vector.Y, vector.Y, sa * vector.X + ca * vector.Y);
        }

        /// <summary>
        /// Projecting vector after trace to go parralel to hit surface
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="objectNormal"></param>
        /// <returns>Perpendicular vector to objectNormal</returns>
        public static Vector3 GetVectorParralelProjectionToObjectNormal(Vector3 vector, Vector3 objectNormal)
        {
            float a = Vector3.Dot(vector, objectNormal) / objectNormal.Length();
            Vector3 b = objectNormal / objectNormal.Length();
            Vector3 projection = Vector3.Negate(a * b);

            return vector + projection;
        }
    }
}
