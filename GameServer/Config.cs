using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    public class Config
    {
        public const int BUFFER_SIZE = 4 * 1024;
        public const int SERVER_TICK = 64;
        public const float PLAYER_SPEED = 2; // PER SECOND
        public const float GRAVITY = 10; // PER SECOND
        public const float JUMP_POWER = 2;
        public const float PLAYER_DECCELERATION = 0.2f;
    }
}
