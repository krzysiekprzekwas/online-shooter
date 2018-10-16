using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using GameServer.MapObjects;
using GameServer.States;
using Newtonsoft.Json;

namespace GameServer.Models
{
    public class Player
    {
        public Vector2 Position { get; set; }
        public double Radius { get; set; }
        public Vector2 Speed { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }

        [JsonIgnore]
        public string ConnectionId;
        public double Angle { get; set; }
        [JsonIgnore]
        public List<KeyEnum> Keys;

        public Player()
        {
            Id = GameState.Instance.GeneratePlayerUniqueId();
            Keys = new List<KeyEnum>();

            Angle = 0;
            Position = new Vector2();
            Speed = new Vector2();
            
            Radius = Config.PLAYER_RADIUS;
        }

        public override string ToString()
        {
            return $"Player<Id:{Id} Name:{Name}, Pos:{Position} R:{Radius}>";
        }
    }
}
