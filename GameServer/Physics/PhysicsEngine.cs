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
                if (trace.Distance > speedVector.Length() + player.Radius)
                    continue;

                // Calculate how far I can go that direction
                float moveDistance = trace.Distance - player.Radius;
                float leftDistance = moveDistance - speedVector.Length();

                speedVector = Vector3.Normalize(speedVector) * moveDistance;

                // Now player will collide so we can move him along the wall
                if(leftDistance > 0)
                {
                    speedVector += GetVectorParralelProjectionToObjectNormal(speedVector * leftDistance, trace.ObjectNormal);
                }
            }

            player.Speed = speedVector;

            // If movement is slow stop player TODO (maybe not needed)
            //if (player.Speed.LengthSquared() < 0.001)
            //    player.Speed = new Vector3(0, 0, 0);
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
