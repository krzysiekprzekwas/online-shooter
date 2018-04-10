using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    public class Config
    {
        [JsonProperty]
        public const int BUFFER_SIZE = 4 * 1024;
        [JsonProperty]
        public const int SERVER_TICK = 64;
        [JsonProperty]
        public const float PLAYER_SPEED = 50; // PER SECOND
        [JsonProperty]
        public const float GRAVITY = 10; // PER SECOND
        [JsonProperty]
        public const float JUMP_POWER = 2;
        [JsonProperty]
        public const float PLAYER_DECCELERATION = 0.2f;
        [JsonProperty]
        public const float PLAYER_SIZE = 32;

        public const float INTERSECTION_INTERVAL = 0.01f;

        private static Config instance;
        public static Config Instance
        {
            get
            {
                if (instance == null)
                    instance = new Config();

                return instance;
            }
        }
    }
}
