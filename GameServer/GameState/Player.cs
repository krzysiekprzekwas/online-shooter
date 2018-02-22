using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    public class Player
    {
        public Player()
        {
            Id = GameState.Instance.AssignPlayerId();
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
