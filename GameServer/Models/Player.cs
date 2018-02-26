using GameServer.States;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GameServer
{
    public class Player
    {
        public Player()
        {
            Id = GameState.Instance.AssignPlayerId();
            Keys = new Dictionary<int, bool>();
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }


        [JsonIgnore]
        public Dictionary<int, bool> Keys;
    }
}
