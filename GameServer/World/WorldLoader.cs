using GameServer.MapObjects;
using GameServer.States;
using System.Collections.Generic;

namespace GameServer.World
{
    public static class WorldLoader
    {
        public static void LoadMap()
        {
            MapFirstArena();
        }

        public static void MapFirstRing()
        {
            var objs = new List<MapObject>
            {
                new MapBox(0, -1, 0, 30, 0.1f, 30),
                new MapBox(-14, 3, -14, 1, 1, 1, new Color(1, 0, 0), 1),
                new MapBox(14, 3, -14, 1, 1, 1, new Color(0.2f, 0.2f, 1),1),
                new MapBox(-14, 3, 14, 1, 1, 1, new Color(0.2f, 0.2f, 1),2),
                new MapBox(14, 3, 14, 1, 1, 1, new Color(0, 0, 1),2),
                new MapBox(14, 2, 0, 1, 3, 1, new Color(0, 0.2f, 1)),
                new MapBox(14, 2, 7, 1, 2, 1, new Color(0, 0.75f, 1)),
                new MapBox(14, 2, -7, 1, 2, 1, new Color(0.2f, 0.2f, 1)),
                new MapBox(-14, 2, 0, 1, 3, 1, new Color(1, 0.2f, 0)),
                new MapBox(-14, 2, 7, 1, 2, 1, new Color(0.2f, 0.2f, 1)),
                new MapBox(-14, 2, -7, 1, 2, 1, new Color(1, 0.75f, 0)),
                new MapBox(0, 2, 14, 1, 3, 1, new Color(0, 0.2f, 1)),
                new MapBox(7, 2, 14, 1, 2, 1, new Color(0, 0.75f, 1)),
                new MapBox(-7, 2, 14, 1, 2, 1, new Color(0.2f, 0.2f, 1)),
                new MapBox(0, 2, -14, 1, 3, 1, new Color(1, 0.2f, 0)),
                new MapBox(7, 2, -14, 1, 2, 1, new Color(0.2f, 0.2f, 1)),
                new MapBox(-7, 2, -14, 1, 2, 1, new Color(1, 0.75f, 0)),
            };

            MapState.Instance.MapObjects.AddRange(objs);
        }

        public static void MapFirstArena()
        {
            const int g = 32;

            const float high = g * 3;
            const float hY = high / 2;

            const float low = g * 0.5f;
            const float lY = low / 2;

            Color orange = new Color(0.9f, 0.5f, 0.2f);
            Color green = new Color(0.1f, 0.87f, 0.34f);
            Color red = new Color(0.9f, 0.1f, 0.2f);

            var objs = new List<MapObject>
            {
                new MapBox(3 * g, hY, 0, 2 * g, high, 2 * g, orange), // 1
                new MapBox(-1 * g, hY, -3.5f * g, 2 * g, high, 5 * g, orange), // 3
                new MapBox(2 * g, hY, -5.5f * g, 4 * g, high, 1 * g, orange), // 4
                new MapBox(10.5f * g, hY, 0 * g, 1 * g, high, 12 * g), // 7
                new MapBox(7 * g, hY, 4 * g, 2 * g, high, 2 * g, red), // 8
                new MapBox(1 * g, hY, 4 * g, 2 * g, high, 2 * g, red), // 9
                new MapBox(-3.5f * g, hY, 3 * g, 3 * g, high, 4 * g), // 10
                new MapBox(-3f * g, hY, -1.5f * g, 2 * g, high, 1 * g, green), // 13
                new MapBox(9.5f * g, hY, 6.5f * g, 1 * g, high, 1 * g, orange), // 16
                new MapBox(8.5f * g, hY, 7.5f * g, 1 * g, high, 1 * g, green), // 17
                new MapBox(7.5f * g, hY, 8.5f * g, 1 * g, high, 1 * g, orange), // 18
                new MapBox(6.5f * g, hY, 9.5f * g, 1 * g, high, 1 * g, green), // 19
                new MapBox(2.5f * g, hY, 9.5f * g, 1 * g, high, 1 * g, orange), // 21
                new MapBox(-0.5f * g, hY, 9.5f * g, 1 * g, high, 1 * g, green), // 22

                new MapBox(2 * g, lY, -2 * g, 4 * g, low, 2 * g, orange), // 2
                new MapBox(7 * g, lY, -5.5f * g, 6 * g, low, 1 * g, green), // 5
                new MapBox(7 * g, lY, -1 * g, 2 * g, low, 4 * g, orange), // 6
                new MapBox(-4.5f * g, lY, 6.5f * g, 1 * g, low, 5 * g, red), // 14
                new MapBox(4 * g, lY, 4 * g, 4 * g, low, 2 * g, green), // 15
                new MapBox(1 * g, lY, 9.5f * g, 10 * g, low, 1 * g, orange), // 20
                new MapBox(-4.5f * g, lY, -0.5f * g, 1 * g, low, 3 * g, red), // 23

                new MapBox(3 * g, -0.1f * g, 2 * g, 16 * g, 0.2f * g, 16 * g)
            };

            MapState.Instance.MapObjects.AddRange(objs);
        }

    }
}
