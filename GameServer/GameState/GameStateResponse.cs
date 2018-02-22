using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class GameStateResponse
    {
        public GameStateResponse()
        {
            Type = "gamestate";
        }

        public GameState GameState { get; set; }
        private string Type { get; set; }
    }
}
