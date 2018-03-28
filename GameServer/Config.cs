using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    public class Config
    {
        public const int BUFFER_SIZE = 4 * 1024;
        public const int SERVER_TICK = 64;
        public const double PLAYER_SPEED = 2; // PER SECOND
        public const double GRAVITY = 10; // PER SECOND
        public const double JUMP_POWER = 2;
        public const double PLAYER_DECCELERATION = 0.2;
    }
}
