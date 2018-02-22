using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class GameEvents
    {
        private GameEngine _gameEngine;
        public GameEvents(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        private void OnPlayerConnected()
        {
        }

        private void OnPlayerDisconnected()
        {

        }
    }
}
