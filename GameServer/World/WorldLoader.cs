using System.Collections.Generic;
using System.Linq;
using GameServer.Game;
using GameServer.MapObjects;
using GameServer.Models;
using GameServer.States;

namespace GameServer.World
{
    public interface IWorldLoader
    {
        void LoadMap();
    }

    public class WorldLoader : IWorldLoader
    {
        private static IMapState _mapState;

        public WorldLoader(IMapState mapState)
        {
            _mapState = mapState;
        }

        public void LoadMap()
        {
            MapFirstArena();
        }

        public void MapFirstRing()
        {
            var objs = new List<MapObject>
            {
                new MapRect(-14, -14, 1, 1),
                new MapRect(14, -14, 1, 1),
                new MapRect(-14, 14, 1, 1),
                new MapRect(14, 14, 1, 1),
                new MapRect(14, 0, 1, 1),
                new MapRect(14, 7, 1, 1),
                new MapRect(14, -7, 1, 1),
                new MapRect(-14, 0, 1, 1),
                new MapRect(-14, 7, 1, 1),
                new MapRect(-14, -7, 1, 1),
                new MapRect(0, 14, 1, 1),
                new MapRect(7, 14, 1, 1),
                new MapRect(-7, 14, 1, 1),
                new MapRect(0, -14, 1, 1),
                new MapRect(7, -14, 1, 1),
                new MapRect(-7, -14, 1, 1),
            };

            objs.ForEach(x => _mapState.AddMapObject(x));
            
            var spawnPoints = new List<SpawnPoint>
            {
                new SpawnPoint(-30, -30),
                new SpawnPoint(-10, -30),
                new SpawnPoint(10, -30),
                new SpawnPoint(30, -30),
            };

            SpawnService.SpawnPoints = spawnPoints;
        }

        public void MapFirstArena()
        {
            const int g = 32;

            var objs = new List<MapObject>
            {
                new MapRect(3 * g, 0, 2 * g, 2 * g), // 1
                new MapRect(-1 * g, -3.5f * g, 2 * g, 5 * g), // 3
                new MapRect(2 * g, -5.5f * g, 4 * g, 1 * g), // 4
                new MapRect(10.5f * g, 0 * g, 1 * g, 12 * g), // 7
                new MapRect(7 * g, 4 * g, 2 * g, 2 * g), // 8
                new MapRect(1 * g, 4 * g, 2 * g, 2 * g), // 9
                new MapRect(-3.5f * g, 3 * g, 3 * g, 4 * g), // 10
                new MapRect(-3f * g, -1.5f * g, 2 * g, 1 * g), // 13
                new MapRect(9.5f * g, 6.5f * g, 1 * g, 1 * g), // 16
                new MapRect(8.5f * g, 7.5f * g, 1 * g, 1 * g), // 17
                new MapRect(7.5f * g, 8.5f * g, 1 * g, 1 * g), // 18
                new MapRect(6.5f * g, 9.5f * g, 1 * g, 1 * g), // 19
                new MapRect(2.5f * g, 9.5f * g, 1 * g, 1 * g), // 21
                new MapRect(-0.5f * g, 9.5f * g, 1 * g, 1 * g), // 22

                new MapRect(2 * g, -2 * g, 4 * g, 2 * g), // 2
                new MapRect(7 * g, -5.5f * g, 6 * g, 1 * g), // 5
                new MapRect(7 * g, -1 * g, 2 * g, 4 * g), // 6
                new MapRect(-4.5f * g, 6.5f * g, 1 * g, 5 * g), // 14
                new MapRect(4 * g, 4 * g, 4 * g, 2 * g), // 15
                new MapRect(1 * g, 9.5f * g, 10 * g, 1 * g), // 20
                new MapRect(-4.5f * g, -0.5f * g, 1 * g, 3 * g), // 23
            };

            objs.ForEach(x => _mapState.AddMapObject(x));

            var spawnPoints = new List<SpawnPoint>
            {
                new SpawnPoint(-3 * g, 0),
                new SpawnPoint(-3 * g, 7 * g),
                new SpawnPoint(9 * g, -1 * g),
                new SpawnPoint(1 * g, -4 * g),
            };

            SpawnService.SpawnPoints = spawnPoints;
        }

    }
}
