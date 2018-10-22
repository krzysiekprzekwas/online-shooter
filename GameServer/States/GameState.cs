using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models;
using Newtonsoft.Json;

namespace GameServer.States
{
    public class GameState
    {
        private static GameState _instance;

        // Gamestate properties
        public List<Player> Players { get; set; }
        public List<Bullet> Bullets { get; set; }
        public int PlayerId { get; set; }

        private GameState()
        {
            Players = new List<Player>();
            Bullets = new List<Bullet>();
            PlayerId = 1;
        }

        public int GeneratePlayerUniqueId()
        {
            return PlayerId++;
        }

        public static GameState Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameState();

                return _instance;
            }
        }
    }
}
