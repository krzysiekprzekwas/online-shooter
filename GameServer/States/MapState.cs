using System.Collections.Generic;
using GameServer.MapObjects;
using Newtonsoft.Json;

namespace GameServer.States
{
    public interface IMapState
    {
        int AssingMapObjectId();
        void AddMapObject(MapObject mapObject);
        List<MapObject> MapObjects { get; set; }
        int MapObjectId { get; set; }
    }
    public class MapState : IMapState
    {
        public MapState()
        {
            MapObjects = new List<MapObject>();
            MapObjectId = 1;
        }

        public int AssingMapObjectId()
        {
            int id = MapObjectId;
            MapObjectId++;
            return id;
        }

        public void AddMapObject(MapObject mapObject)
        {
            MapObjects.Add(mapObject);
        }

        public List<MapObject> MapObjects { get; set; }

        [JsonIgnore]
        public int MapObjectId { get; set; }

    }
}
