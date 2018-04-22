using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using CoreGameServer.Game;
using CoreGameServer.MapObjects;
using CoreGameServer.Models;
using CoreGameServer.States;

namespace CoreGameServer.Physics
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
            if (speedVector.Length() > 0)
                speedVector = Vector3.Normalize(speedVector) * ((Config.PLAYER_SPEED / (float)Config.SERVER_TICK));

            // Add to current speed
            speedVector += player.Speed;

            // TODO
            // Gravitation

            // If movement is slow stop player
            if (player.Speed.Length() < 0.00001)
                player.Speed = new Vector3(0, 0, 0);

            // Check collision at new position
            player.Speed = CalculateSpeedAfterCollision(player, speedVector);
        }

        public Vector3 CalculateSpeedAfterCollision(Player player, Vector3 speedVector)
        {
            Vector3 newPosition = player.Position + speedVector + Vector3.Normalize(speedVector) * (Config.PLAYER_SIZE / 2f);

            foreach (MapObject obj in MapState.Instance.MapObjects)
            {
                MapBox box = obj as MapBox;
                if (Intersection.CheckIntersection(box, newPosition))
                    return new Vector3(0, speedVector.Y, 0);
            }

            return speedVector;
        }
    }
}
