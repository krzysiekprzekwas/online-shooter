using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using GameServer.States;
using Newtonsoft.Json;

namespace GameServer.Models
{
    public class Player
    {
        public Player()
        {
            Id = GameState.Instance.value.Players.Count + 1;
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

        public Player DeepCopy()
        {
            var copy = new Player
            {
                Id = Id,
                Keys = Keys,
                Angles = Angles,
                Position = Position,
                Speed = Speed,
                IsJumping = IsJumping,
                Name = Name
            };

            return copy;
        }

        [JsonIgnore]
        public List<string> Keys;

        [JsonIgnore]
        public bool IsJumping;
    }
}
