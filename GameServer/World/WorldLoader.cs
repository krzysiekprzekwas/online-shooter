using GameServer.MapObjects;
using GameServer.States;

namespace GameServer.World
{
    public static class WorldLoader
    {
        public static void LoadMap()
        {
            MapObject box = new MapBox(0, -1, 0, 30, 0.1, 30);

            MapState.Instance.MapObjects.Add(box);
        }

    }
}
