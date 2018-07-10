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
        public static float PLAYER_SPEED = 50; // PER SECOND
        [JsonProperty]
        public static float GRAVITY = 10; // PER SECOND
        [JsonProperty]
        public static float JUMP_POWER = 2;
        [JsonProperty]
        public static float PLAYER_DECCELERATION = 0.8f;
        [JsonProperty]
        public static float PLAYER_SIZE = 32;

        // Note that can be physics will be less acurate than this value because of float precission
        [JsonProperty]
        public static float INTERSECTION_INTERVAL = 0.01f; 

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
