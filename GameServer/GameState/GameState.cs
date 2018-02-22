using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    public class GameState
    {
        private static GameState instance;
        public GameState()
        {
            Players = new List<Player>();
        }
        
        public static GameState Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameState();

                return instance;
            }
        }
        
        public int I { get; set; }
        public List<Player> Players { get; set; }
    }
}
