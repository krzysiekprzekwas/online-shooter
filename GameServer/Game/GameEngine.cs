using System;
using System.Collections.Generic;
using System.Threading;
using GameServer.Hubs;
using GameServer.Models;
using GameServer.Physics;
using GameServer.States;
using GameServer.World;
using Microsoft.AspNetCore.SignalR;

namespace GameServer.Game
{
    public interface IGameEngine
    {
        bool AddPlayer(Player player);

        bool RemovePlayer(Player player);

        GameState GameState { get; }
    }

    public class GameEngine : IGameEngine
    {
        public Timer Ticker;
        public List<Player> Players = new List<Player>();
        private GameState _gameState = GameState.Instance;
        public GameEvents GameEvents;
        public PhysicsEngine PhysicsEngine;
        private readonly IHubContext<GameHub> _hubContext;
        private IConfig _config;

        GameState IGameEngine.GameState { get => _gameState; }

        public GameEngine(IHubContext<GameHub> hubContext, IConfig config, IWorldLoader worldLoader, IMapState mapState)
        {
            _hubContext = hubContext;
            _config = config;
            GameEvents = new GameEvents(this);
            PhysicsEngine = new PhysicsEngine(config, mapState);
            worldLoader.LoadMap();
            Ticker = new Timer(Tick, null, 0, 1000 / _config.ServerTick);
        }

        private void ApplyShooting()
        {
            foreach (Player player in GameState.Instance.Players)
            {

                var weapon = WeaponService.GetWeaponFromWeaponEnumOrNull(player.PlayerWeapon.WeaponEnum);

                if (player.MouseClicked && MilisecondsSince(player.PlayerWeapon.LastShotDate) > weapon.ShootTime)
                {
                    //Shoot
                    player.PlayerWeapon.LastShotDate = DateTime.Now;

                    var bullet = new Bullet()
                    {
                        Angle = player.Angle,
                        PlayerId = player.Id,
                        Position = player.Position,
                        Radius = weapon.BulletSize,
                        Speed = (player.Speed  + Vector2.RadianToVector2(player.Angle + Math.PI/2)) * weapon.BulletSpeed
                    };

                    GameState.Instance.Bullets.Add(bullet);
                }
            }
        }

        private static double MilisecondsSince(DateTime time)
        {
            return (DateTime.Now - time).TotalMilliseconds;
        }

        private void Tick(object state)
        {
            ApplyShooting();

            PhysicsEngine.ApplyPhysics();

            var currentPlayers = new Player[GameState.Instance.Players.Count];
            GameState.Instance.Players.CopyTo(currentPlayers);

            // Send game state for every connected client
             _hubContext.Clients.All.SendAsync("updateGameState", GameState.Instance);
        }

        public bool AddPlayer(Player player)
        {
            GameState.Instance.Players.Add(player);

            GameEvents.OnPlayerAdded(player);

            return true;
        }

        public bool RemovePlayer(Player player)
        {
            GameEvents.OnPlayerRemoved(player);

            GameState.Instance.Players.Remove(player);

            return true;
        }
    }
}
