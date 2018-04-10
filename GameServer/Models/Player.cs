using System;
using GameServer.Models;
using GameServer.States;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GameServer.MapObjects;

namespace GameServer
{
    public class Player
    {
        public Player()
        {
            Id = GameState.Instance.value.Players.Count + 1;
            Keys = new List<string>();
            Angles = new Vector2(0, 0);
            Position = new Vector3(0, 0, 0);
            Speed = new Vector3(0, 0, 0);

            IsJumping = false;
            Diameter = Config.PLAYER_SIZE;
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
        private MapSphere _worldObject = null;
        public MapSphere WorldObject
        {
            get
            {
                if (_worldObject == null)
                    _worldObject = new MapSphere(Position.X, Position.Y, Position.Z, Diameter);
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
    }
}
