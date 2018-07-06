using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Numerics;
using GameServer.MapObjects;
using GameServer.States;
using Newtonsoft.Json;

namespace GameServer.Models
{
    public class Player
    {
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        [JsonIgnore]
        public WebSocket WebSocket;

        [JsonIgnore]
        public IPAddress IpAddress;
        public float Angle { get; set; }
        [JsonIgnore]
        private MapEllipse _worldObject;
        public MapEllipse WorldObject
        {
            get
            {
                if (_worldObject == null)
                    _worldObject = new MapEllipse(Position.X, Position.Y, Diameter);
                else
                    _worldObject.Position = Position;

                return _worldObject;
            }
        }
        [JsonIgnore]
        public List<string> Keys;
        [JsonIgnore]
        public bool IsJumping;
        private float _playerRadius;
        [JsonIgnore]
        public float Radius
        {
            get
            {
                return _playerRadius;
            }
            set
            {
                if (value > 0)
                    _playerRadius = value;
            }
        }

        [JsonIgnore]
        public float Diameter
        {
            get
            {
                return _playerRadius * 2f;
            }
            set
            {
                if(value > 0)
                    _playerRadius = value / 2f;
            }
        }

        public Player()
        {
            Id = GameState.Instance.value.Players.Count + 1;
            Keys = new List<string>();
            Angle = 0.0f;
            Position = new Vector2(0, 0);
            Speed = new Vector2(0, 0);

            IsJumping = false;
            Diameter = Config.PLAYER_SIZE;
        }

        public Player DeepCopy()
        {
            var copy = new Player
            {
                Id = Id,
                Keys = Keys,
                Angle = Angle,
                Position = Position,
                Speed = Speed,
                IsJumping = IsJumping,
                Name = Name
            };

            return copy;
        }
    }
}
