using System.Collections.Generic;
using GameServer.MapObjects;
using Newtonsoft.Json;

namespace GameServer.States
{
    public interface IMapState
    {
        List<MapObject> MapObjects { get; set; }
    }
    public class MapState : IMapState
    {
        public MapState()
        {
            MapObjects = new List<MapObject>();
        }

        public List<MapObject> MapObjects { get; set; }
    }
}
