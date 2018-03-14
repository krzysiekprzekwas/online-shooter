using GameServer.Game;
using GameServer.Models;
using GameServer.States;
using System;
using System.Collections.Generic;
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
            }



        }

        public void ProcessPlayerInput(Player player)
        {
            Angle2 forwardAngle = new Angle2(Math.Sin(player.Angles.Y), Math.Cos(player.Angles.Y));
            Angle2 leftAngle = new Angle2(Math.Sin(player.Angles.Y - Math.PI / 2), Math.Cos(player.Angles.Y - Math.PI / 2));

            if (player.Keys.Contains("w"))
            {
                player.SpeedX += forwardAngle.X * Config.PLAYER_SPEED / Config.SERVER_TICK;
                player.SpeedZ += forwardAngle.Y * Config.PLAYER_SPEED / Config.SERVER_TICK;
            }
            if (player.Keys.Contains("s"))
            {
                player.SpeedX -= forwardAngle.X * Config.PLAYER_SPEED / Config.SERVER_TICK;
                player.SpeedZ -= forwardAngle.Y * Config.PLAYER_SPEED / Config.SERVER_TICK;
            }
            if (player.Keys.Contains("a"))
            {
                player.SpeedX += leftAngle.X * Config.PLAYER_SPEED / Config.SERVER_TICK;
                player.SpeedZ += leftAngle.Y * Config.PLAYER_SPEED / Config.SERVER_TICK;
            }
            if (player.Keys.Contains("d"))
            {
                player.SpeedX -= leftAngle.X * Config.PLAYER_SPEED / Config.SERVER_TICK;
                player.SpeedZ -= leftAngle.Y * Config.PLAYER_SPEED / Config.SERVER_TICK;
            }
        }
    }
}
