using GameServer.MapObjects;
using GameServer.States;

namespace GameServer.World
{
    public static class WorldLoader
    {
        public static void LoadMap()
        {
            MapObject box = new MapBox(0, 0, 0, 3, 1, 3);

            MapState.Instance.MapObjects.Add(box);
        }

    }
}
