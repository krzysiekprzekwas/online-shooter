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
                // Deccelerate player every tick
                player.Speed *= Config.PLAYER_DECCELERATION;

                // Process movement caused by input
                CalculatePlayerSpeed(player);

                // Save new positon
                player.Position += player.Speed;
            }
        }

        public void CalculatePlayerSpeed(Player player)
        {
            // Get movement vectors
            Vector3 speedVector = player.Speed + CalculateSpeedVector(player);
            Vector3 gravityVector = new Vector3(0, -(Config.PLAYER_SPEED / (float)Config.SERVER_TICK), 0);

            if (speedVector.LengthSquared() == 0)
                return;

            // Create player ray
            Ray ray = new Ray(player.Position + new Vector3(0, player.Radius, 0), speedVector);

            Vector3 newPosition = player.Position + speedVector + Vector3.Normalize(speedVector) * (Config.PLAYER_SIZE / 2f);

            // Find closest object
            Trace closestTrace = null;
            float closestDist = -1;

            // Check collision
            foreach (MapObject obj in MapState.Instance.MapObjects)
            {
                Trace trace = null;
                if (obj is MapBox)
                    trace = RayCast.CheckBulletTrace((MapBox)obj, ray);
                else if (obj is MapSphere)
                    trace = RayCast.CheckBulletTrace((MapSphere)obj, ray);

                // No collision with this obeject - check next one
                if (trace == null)
                    continue;

                // Trace is so far that we dont consider it
                float speedLength = speedVector.Length();
                if (trace.Distance > speedLength + player.Radius)
                    continue;

                // Calculate how far I can go that direction
                float moveDistance = trace.Distance - player.Radius;
                if(closestDist == -1 || moveDistance < closestDist)
                {
                    closestTrace = trace;
                    closestDist = moveDistance;
                }
            }

            // Some object on the way found
            if(closestTrace != null)
            {
                // Calculate movement
                float leftDistance = speedVector.Length() - closestDist - player.Radius;

                speedVector = Vector3.Normalize(speedVector) * closestDist;

                // Now player will collide so we can move him along the wall
                if (leftDistance > 0)
                {
                    Vector3 parralelVector = GetVectorParralelProjectionToObjectNormal(speedVector * leftDistance, closestTrace.ObjectNormal);
                    speedVector += parralelVector;
                }
            }

            // Change player speed
            player.Speed = speedVector;
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

        public static Vector3 GetVectorParralelProjectionToObjectNormal(Vector3 vector, Vector3 objectNormal)
        {
            float a = Vector3.Dot(vector, objectNormal) / objectNormal.Length();
            Vector3 b = objectNormal / objectNormal.Length();
            Vector3 projection = Vector3.Negate(a * b);
            
            return vector + projection;
        }
    }
}
