using System;
using System.Collections.Generic;
using System.Linq;
using GameServer.MapObjects;
using Newtonsoft.Json;

namespace GameServer.States
{
    public class MapState
    {
        private static MapState instance;
        public MapState()
        {
            MapObjects = new List<MapObject>();
            MapObjectId = 1;
        }

        public static MapState Instance
        {
            get
            {
                if (instance == null)
                    instance = new MapState();

                return instance;
            }
        }


        public int AssingMapObjectId()
        {
            int id = MapObjectId;
            MapObjectId++;
            return id;
        }


        public List<MapObject> MapObjects { get; set; }

        [JsonIgnore]
        public int MapObjectId { get; set; }

    }
}
