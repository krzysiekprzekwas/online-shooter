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
        }

        public static SpawnPoint GetRandomSpawnPoint()
        {
            var spawnPoints = SpawnPoints.Where(s => !IsOccupied(s)).ToList();

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
