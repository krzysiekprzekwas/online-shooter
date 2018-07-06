using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.MapObjects;
using GameServer.States;

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
                new MapRect(-14, -14, 1, 1, null, new Color(1, 0, 0), 1),
                new MapRect(14, -14, 1, 1, null, new Color(0.2f, 0.2f, 1),1),
                new MapRect(-14, 14, 1, 1, null, new Color(0.2f, 0.2f, 1),2),
                new MapRect(14, 14, 1, 1, null, new Color(0, 0, 1),2),
                new MapRect(14, 0, 1, 1, null, new Color(0, 0.2f, 1),1),
                new MapRect(14, 7, 1, 1, null, new Color(0, 0.75f, 1),1),
                new MapRect(14, -7, 1, 1, null, new Color(0.2f, 0.2f, 1),1),
                new MapRect(-14, 0, 1, 1, null, new Color(1, 0.2f, 0),1),
                new MapRect(-14, 7, 1, 1, null, new Color(0.2f, 0.2f, 1),1),
                new MapRect(-14, -7, 1, 1, null, new Color(1, 0.75f, 0),1),
                new MapRect(0, 14, 1, 1, null, new Color(0, 0.2f, 1),1),
                new MapRect(7, 14, 1, 1, null, new Color(0, 0.75f, 1)),
                new MapRect(-7, 14, 1, 1, null, new Color(0.2f, 0.2f, 1)),
                new MapRect(0, -14, 1, 1, null, new Color(1, 0.2f, 0)),
                new MapRect(7, -14, 1, 1, null, new Color(0.2f, 0.2f, 1)),
                new MapRect(-7, -14, 1, 1, null, new Color(1, 0.75f, 0)),
            };

            MapState.Instance.MapObjects.AddRange(objs);
        }

        public static void MapFirstArena()
        {
            const int g = 32;
            
            Color orange = new Color(0.9f, 0.5f, 0.2f);
            Color green = new Color(0.1f, 0.87f, 0.34f);
            Color red = new Color(0.9f, 0.1f, 0.2f);

            var objs = new List<MapObject>
            {
                new MapRect(3 * g, 0, 2 * g, 2 * g, null, orange, 1), // 1
                new MapRect(-1 * g, -3.5f * g, 2 * g, 5 * g, null, orange, 1), // 3
                new MapRect(2 * g, -5.5f * g, 4 * g, 1 * g, null, orange, 1), // 4
                new MapRect(10.5f * g, 0 * g, 1 * g, 12 * g,null, null, 1), // 7
                new MapRect(7 * g, 4 * g, 2 * g, 2 * g, null, red, 1), // 8
                new MapRect(1 * g, 4 * g, 2 * g, 2 * g,null,  red, 1), // 9
                new MapRect(-3.5f * g, 3 * g, 3 * g, 4 * g,null, null, 1), // 10
                new MapRect(-3f * g, -1.5f * g, 2 * g, 1 * g, null, green, 1), // 13
                new MapRect(9.5f * g, 6.5f * g, 1 * g, 1 * g, null, orange, 1), // 16
                new MapRect(8.5f * g, 7.5f * g, 1 * g, 1 * g, null, green, 1), // 17
                new MapRect(7.5f * g, 8.5f * g, 1 * g, 1 * g, null, orange, 1), // 18
                new MapRect(6.5f * g, 9.5f * g, 1 * g, 1 * g, null, green, 1), // 19
                new MapRect(2.5f * g, 9.5f * g, 1 * g, 1 * g, null, orange, 1), // 21
                new MapRect(-0.5f * g, 9.5f * g, 1 * g, 1 * g, null, green, 1), // 22

                new MapRect(2 * g, -2 * g, 4 * g, 2 * g, null, orange, 2), // 2
                new MapRect(7 * g, -5.5f * g, 6 * g, 1 * g, null, green, 2), // 5
                new MapRect(7 * g, -1 * g, 2 * g, 4 * g, null, orange, 2), // 6
                new MapRect(-4.5f * g, 6.5f * g, 1 * g, 5 * g, null, red, 2), // 14
                new MapRect(4 * g, 4 * g, 4 * g, 2 * g, null, green, 2), // 15
                new MapRect(1 * g, 9.5f * g, 10 * g, 1 * g, null, orange, 2), // 20
                new MapRect(-4.5f * g, -0.5f * g, 1 * g, 3 * g, null, red, 2), // 23
            };

            MapState.Instance.MapObjects.AddRange(objs);
        }

    }
}
