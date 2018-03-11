using GameServer.Game;
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
            if (player.Keys.Contains("w"))
                player.SpeedX += 0.1;
            if (player.Keys.Contains("s"))
                player.SpeedX -= 0.1;
            if (player.Keys.Contains("a"))
                player.SpeedZ += 0.1;
            if (player.Keys.Contains("d"))
                player.SpeedZ -= 0.1;
        }
    }
}
