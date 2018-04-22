using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models;

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
