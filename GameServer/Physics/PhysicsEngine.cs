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
            foreach(Player player in GameState.Instance.Players)
            {
                ProcessPlayerInput(player);

                if(Math.Abs(player.SpeedX) > 0.00000001)
                {
                    player.X += player.SpeedX;
                    player.SpeedX *= 0.2;
                }

                if (Math.Abs(player.SpeedZ) > 0.00000001)
                {
                    player.Z += player.SpeedZ;
                    player.SpeedZ *= 0.2;
                }


                if (Math.Abs(player.SpeedY) > 0.00000001)
                {
                    player.Y += player.SpeedY;
                    player.SpeedY *= 0.2;
                }
            }



        }

        public void ProcessPlayerInput(Player player)
        {
            Angle2 forwardAngle = new Angle2(Math.Sin(player.Angles.Y), Math.Cos(player.Angles.Y));
            Angle2 leftAngle = new Angle2(Math.Sin(player.Angles.Y - Math.PI / 2), Math.Cos(player.Angles.Y - Math.PI / 2));
            List<double> speedVector = new List<double>(3) { 0, 0, 0 };

            if (player.Keys.Contains("w"))
            {
                speedVector[0] = forwardAngle.X * Config.PLAYER_SPEED / Config.SERVER_TICK;
                speedVector[2] = forwardAngle.Y * Config.PLAYER_SPEED / Config.SERVER_TICK;
            }
            if (player.Keys.Contains("s"))
            {
                speedVector[0] = -forwardAngle.X * Config.PLAYER_SPEED / Config.SERVER_TICK;
                speedVector[2] = -forwardAngle.Y * Config.PLAYER_SPEED / Config.SERVER_TICK;
            }
            if (player.Keys.Contains("a"))
            {
                speedVector[0] = leftAngle.X * Config.PLAYER_SPEED / Config.SERVER_TICK;
                speedVector[2] = leftAngle.Y * Config.PLAYER_SPEED / Config.SERVER_TICK;
            }
            if (player.Keys.Contains("d"))
            {
                speedVector[0] = -leftAngle.X * Config.PLAYER_SPEED / Config.SERVER_TICK;
                speedVector[2] = -leftAngle.Y * Config.PLAYER_SPEED / Config.SERVER_TICK;
            }

            speedVector[1] = -0.1;

            CheckCollision(player, speedVector);

            player.SpeedX += speedVector[0];
            player.SpeedY += speedVector[1];
            player.SpeedZ += speedVector[2];
        }

        public void CheckCollision(Player player, List<double> speedVector)
        {
            foreach(MapObject obj in MapState.Instance.MapObjects)
            {
                if(obj is MapBox)
                {
                    MapBox box = obj as MapBox;

                    double left = box.X - (box.Width / 2);
                    double right = box.X + (box.Width / 2);
                    double top = box.Z + (box.Depth / 2);
                    double bottom = box.Z - (box.Depth / 2);

                    double up = box.Y + (box.Height / 2);

                    if (player.X >= left &&
                        player.X <= right &&
                        player.Z <= top &&
                        player.Z >= bottom &&
                        player.Y >= up)
                        speedVector[1] = 0;
                }
            }
        }
    }
}
