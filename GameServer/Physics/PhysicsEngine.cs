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
            foreach (Player player in GameState.Instance.Players)
            {
                // Deccelerate player every tick
                float x = player.Speed.X * Config.PLAYER_DECCELERATION;
                float y = player.Speed.Y * Config.PLAYER_DECCELERATION;
                float z = player.Speed.Z * Config.PLAYER_DECCELERATION;
                player.Speed = new Vector3(x, y, z);

                // Process movement caused by input
                ProcessPlayerInput(player);

                // If movement is slow stop player
                float px = player.Position.X;
                float py = player.Position.Y;
                float pz = player.Position.Z;

                // Update position if player is moving
                if (Math.Abs(player.Speed.X) > 0.00000001)
                    px += player.Speed.X;

                if (Math.Abs(player.Speed.Z) > 0.00000001)
                    pz += player.Speed.Z;
                
                if (Math.Abs(player.Speed.Y) > 0.00000001)
                    py += player.Speed.Y;

                // Save new positon
                player.Position = new Vector3(px, py, pz);
            }
        }

        public void ProcessPlayerInput(Player player)
        {
            Vector2 forwardAngle = new Vector2((float)Math.Sin(player.Angles.Y), (float)Math.Cos(player.Angles.Y));
            Vector2 leftAngle = new Vector2((float)Math.Sin(player.Angles.Y - (float)Math.PI / 2), (float)Math.Cos(player.Angles.Y - Math.PI / 2));
            Vector3 speedVector = new Vector3();

            if (player.Keys.Contains("w"))
            {
                speedVector.X = forwardAngle.X * Config.PLAYER_SPEED / Config.SERVER_TICK;
                speedVector.Z = forwardAngle.Y * Config.PLAYER_SPEED / Config.SERVER_TICK;
            }
            if (player.Keys.Contains("s"))
            {
                speedVector.X = -forwardAngle.X * Config.PLAYER_SPEED / Config.SERVER_TICK;
                speedVector.Z = -forwardAngle.Y * Config.PLAYER_SPEED / Config.SERVER_TICK;
            }
            if (player.Keys.Contains("a"))
            {
                speedVector.X = leftAngle.X * Config.PLAYER_SPEED / Config.SERVER_TICK;
                speedVector.Z = leftAngle.Y * Config.PLAYER_SPEED / Config.SERVER_TICK;
            }
            if (player.Keys.Contains("d"))
            {
                speedVector.X = -leftAngle.X * Config.PLAYER_SPEED / Config.SERVER_TICK;
                speedVector.Z = -leftAngle.Y * Config.PLAYER_SPEED / Config.SERVER_TICK;
            }

            speedVector.Y = -1 * Config.GRAVITY / Config.SERVER_TICK;
            if (player.Keys.Contains("sp"))
            {
                if (!player.IsJumping)
                {
                    player.IsJumping = true;
                    speedVector.Y += Config.JUMP_POWER;
                }
            }


            CheckCollision(player, speedVector);

            if (speedVector.Y == 0)
            {
                player.IsJumping = false;
            }

            // Update player speed
            float sx = player.Speed.X;
            float sy = player.Speed.Y;
            float sz = player.Speed.Z;

            sx += speedVector.X;
            sy += speedVector.Y;
            sz += speedVector.Z;

            player.Speed = new Vector3(sx, sy, sz);
        }

        public void CheckCollision(Player player, Vector3 speedVector)
        {
            Vector3 oldPosition = player.Position;
            Vector3 newPosition = player.Position + speedVector;

            foreach (MapObject obj in MapState.Instance.MapObjects)
            {
                if (obj is MapBox)
                {
                    MapBox box = obj as MapBox;

                    double left = box.Position.X - (box.Width / 2);
                    double right = box.Position.X + (box.Width / 2);
                    double top = box.Position.Z + (box.Depth / 2);
                    double bottom = box.Position.Z - (box.Depth / 2);

                    double up = box.Position.Y + (box.Height / 2);

                    if (player.Position.X >= left &&
                        player.Position.X <= right &&
                        player.Position.Z <= top &&
                        player.Position.Z >= bottom &&
                        player.Position.Y >= up &&
                        player.Position.Y < up + 1 &&
                        speedVector.Y < 0)
                        speedVector.Y = 0;
                }
            }
        }
    }
}
