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
            PlayerId = 1;

            MapObjects = new List<MapObject>();
            MapObjectId = 1;
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

        public int AssingMapObjectId()
        {
            int id = MapObjectId;
            MapObjectId++;
            return id;
        }
        
        public List<Player> Players { get; set; }

        public List<MapObject> MapObjects { get; set; }

        [JsonIgnore]
        public int PlayerId { get; set; }

        [JsonIgnore]
        public int MapObjectId { get; set; }
    }
}
