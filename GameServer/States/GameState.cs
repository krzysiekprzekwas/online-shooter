using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.States
{
    public class GameState
    {
        private static GameState instance;
        public GameState()
        {
            Players = new List<Player>();
            PlayerId = 1;
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

        public int AssignPlayerId()
        {
            int id = PlayerId;
            PlayerId++;
            return id;
        }
        
        public List<Player> Players { get; set; }
        

        [JsonIgnore]
        public int PlayerId { get; set; }
    }
}
