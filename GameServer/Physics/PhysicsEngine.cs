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
                player.Speed.X *= Config.PLAYER_DECCELERATION;
                player.Speed.Z *= Config.PLAYER_DECCELERATION;
                player.Speed.Y *= Config.PLAYER_DECCELERATION;

                ProcessPlayerInput(player);

                if (Math.Abs(player.Speed.X) > 0.00000001)
                {
                    player.Position.X += player.Speed.X;
                }

                if (Math.Abs(player.Speed.Z) > 0.00000001)
                {
                    player.Position.Z += player.Speed.Z;
                }


                if (Math.Abs(player.Speed.Y) > 0.00000001)
                {
                    player.Position.Y += player.Speed.Y;
                }
            }



        }

        public void ProcessPlayerInput(Player player)
        {
            Angle2 forwardAngle = new Angle2(Math.Sin(player.Angles.Y), Math.Cos(player.Angles.Y));
            Angle2 leftAngle = new Angle2(Math.Sin(player.Angles.Y - Math.PI / 2), Math.Cos(player.Angles.Y - Math.PI / 2));
            Vector3d speedVector = new Vector3d();

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

            player.Speed.X += speedVector.X;
            player.Speed.Y += speedVector.Y;
            player.Speed.Z += speedVector.Z;
        }

        public void CheckCollision(Player player, Vector3d speedVector)
        {
            Vector3d oldPosition = player.Position;
            Vector3d newPosition = player.Position + speedVector;

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
