using GameServer.Models;
using GameServer.States;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Numerics;

namespace GameServer
{
    public class Player
    {
        public Player()
        {
            Id = GameState.Instance.AssignPlayerId();
            Keys = new List<string>();
            Angles = new Vector2();
            Position = new Vector3();
            Speed = new Vector3();

            IsJumping = false;
        }

        public Vector3 Position { get; set; }
        public Vector3 Speed { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }


        public Vector2 Angles { get; set; }

        [JsonIgnore]
        public List<string> Keys;

        [JsonIgnore]
        public bool IsJumping;
    }
}
