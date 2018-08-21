using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameServer
{
    public class Config
    {
        // TODO: change static to const
        [JsonProperty]
        public static int BUFFER_SIZE = 4 * 1024;
        [JsonProperty]
        public static int SERVER_TICK = 64;
        [JsonProperty]
        public static double PLAYER_SPEED = 50; // PER SECOND
        [JsonProperty]
        public static double GRAVITY = 10; // PER SECOND
        [JsonProperty]
        public static double JUMP_POWER = 2;
        [JsonProperty]
        public static double PLAYER_DECCELERATION = 0.8;
        [JsonProperty]
        public static double PLAYER_RADIUS = 32;
        [JsonProperty]
        public static double INTERSECTION_INTERVAL = 0.01; 

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
