using GameServer.Models;
using GameServer.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameServer.Game
{
    public class SpawnService
    {
        public static List<SpawnPoint> SpawnPoints;
        private static readonly Random random = new Random();

        public static void SpawnPlayer(Player player)
        {
            var spawnPoint = GetRandomSpawnPoint();

            var position = spawnPoint.Position.Clone() as Vector2;
            player.Position = position;

            var weaponEnum = WeaponService.GetDefaultWeaponEnum();
            player.PlayerWeapon = new PlayerWeapon(weaponEnum);
        }

        public static SpawnPoint GetRandomSpawnPoint()
        {
            if(SpawnPoints == null || SpawnPoints.Count == 0)
            {
                throw new NullReferenceException("No spawnpoints loaded");
            }

            var spawnPoints = SpawnPoints.Where(s => !IsOccupied(s)).ToList();
            if (spawnPoints.Count == 0)
                spawnPoints = SpawnPoints;

            var randomIndex = random.Next(spawnPoints.Count);
            return spawnPoints[randomIndex];
        }

        public static bool IsOccupied(SpawnPoint spawnPoint)
        {
            var currentPlayers = new Player[GameState.Instance.Players.Count];
            GameState.Instance.Players.CopyTo(currentPlayers);

            foreach(var player in currentPlayers)
            {
                var distance = Vector2.Distance(player.Position, spawnPoint.Position);
                if (distance < player.Radius * 2)
                    return true;
            }

            return false;
        }

    }
}
