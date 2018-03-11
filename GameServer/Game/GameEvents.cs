using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Game
{
    public class GameEvents
    {
        private GameEngine _gameEngine;
        public GameEvents(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        public void OnPlayerConnected(Player player)
        {
        }

        public void OnPlayerDisconnected(Player player)
        {
        }
    }
}
