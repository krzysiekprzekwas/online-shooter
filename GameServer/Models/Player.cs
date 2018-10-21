using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using GameServer.Game;
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
        public PlayerWeapon PlayerWeapon { get; set; }

        public double Angle { get; set; }
        [JsonIgnore]
        public bool MouseClicked;
        [JsonIgnore]
        public string ConnectionId;
        [JsonIgnore]
        public List<KeyEnum> Keys;

        public Player()
        {
            Id = GameState.Instance.GeneratePlayerUniqueId();
            Keys = new List<KeyEnum>();

            Angle = 0;
            Position = new Vector2();
            Speed = new Vector2();
            MouseClicked = false;
            Radius = Config.PLAYER_RADIUS;
        }

        public override string ToString()
        {
            return $"Player<Id:{Id} Name:{Name}, Pos:{Position} R:{Radius}>";
        }
    }
}
