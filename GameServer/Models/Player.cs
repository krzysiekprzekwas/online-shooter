using GameServer.Models;
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
            Angles = new Angle2();
            Position = new Vector3d();
            Speed = new Vector3d();
        }

        public Vector3d Position { get; set; }
        public Vector3d Speed { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }


        public Angle2 Angles { get; set; }

        [JsonIgnore]
        public List<string> Keys;
    }
}
