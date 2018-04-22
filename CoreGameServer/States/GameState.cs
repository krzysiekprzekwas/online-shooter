using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreGameServer.Models;
using Newtonsoft.Json;

namespace CoreGameServer.States
{
    public class GameState
    {
        private static GameState instance;
        public GameStateValue value;

        public GameState()
        {
            value = new GameStateValue();
            value.Players = new List<Player>();
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
    }

    public struct GameStateValue
    {
        public List<Player> Players { get; set; }


        [JsonIgnore]
        public int PlayerId { get; set; }
    }
}
