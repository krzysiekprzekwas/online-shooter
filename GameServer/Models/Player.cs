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
            Keys = new List<string>();
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double SpeedX { get; set; }
        public double SpeedY { get; set; }
        public double SpeedZ { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }


        [JsonIgnore]
        public List<string> Keys;
    }
}
