using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    public static class MapController
    {
        public static void LoadMap()
        {
            MapObject box = new MapBox(0, 0, 0, 3, 1, 3);

            MapState.Instance.MapObjects.Add(box);
        }

    }
}
