using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GameServer.Hubs;
using GameServer.MapObjects;
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
        private GameState _gameState = GameState.Instance;
        public GameEvents GameEvents;
        public PhysicsEngine PhysicsEngine;
        private readonly IHubContext<GameHub> _hubContext;
        private IConfig _config;
        private static int bulletId = 1;

        GameState IGameEngine.GameState { get => _gameState; }

        public GameEngine(IHubContext<GameHub> hubContext, IConfig config, IWorldLoader worldLoader, IMapState mapState)
        {
            _hubContext = hubContext;
            _config = config;
            GameEvents = new GameEvents(this);
            PhysicsEngine = new PhysicsEngine(config, mapState);
            worldLoader.LoadMap();
            Ticker = new Timer(Tick, null, 0, _config.ServerTickMilliseconds);
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
                        Id = bulletId++,
                        PlayerId = player.Id,
                        Position = player.Position,
                        Radius = weapon.BulletSize,
                        Speed = player.Speed + (Vector2.RadianToVector2(player.Angle + Math.PI/2) * (weapon.BulletSpeed / _config.ServerTicksPerSecond))
                    };

                    GameState.Instance.Bullets.Add(bullet);
                }
            }
        }

        private static double MilisecondsSince(DateTime time)
        {
            return (DateTime.Now - time).TotalMilliseconds;
        }

        public void PerformSingleTick()
        {
            Tick(null);
        }

        private void Tick(object state)
        {
            // Check if dead players are aplicable
            // for resurection
            ResurectDeadPlayers();

            // Check players input and create
            // bullets if shooting available
            ApplyShooting();

            // Apply physics calculations
            PhysicsEngine.ApplyPhysics();

            // Detect collisions of bullets
            // and players and apply damage
            ApplyDamage();

            // Send new game state for 
            // every connected client
            _hubContext?.Clients.All.SendAsync("updateGameState", GameState.Instance);
        }

        private void ResurectDeadPlayers()
        {
            foreach (var player in _gameState.Players)
            {
                if (!player.IsAlive)
                {
                    player.MilisecondsToResurect -= _config.ServerTickMilliseconds;
                    if (player.MilisecondsToResurect <= 0)
                    {
                        SpawnService.SpawnPlayer(player);
                        player.Health = _config.MaxPlayerHealth;
                    }
                }
            }
        }

        private void ApplyDamage()
        {
            var bulletHitIds = new List<int>();
            foreach (var bullet in _gameState.Bullets)
            {
                var hit = false;
                if (_config.Interpolation)
                {
                    var previousPosition = bullet.Position - (bullet.Speed * (1.0 / _config.BulletDecceleraionFactorPerTick));
                    hit = CheckBulletInterpolatedHit(bullet, previousPosition);
                }
                else
                    hit = CheckBulletHitAtPosition(bullet, bullet.Position);

                if(hit)
                    bulletHitIds.Add(bullet.Id);
            }

            GameState.Instance.Bullets.RemoveAll(b => bulletHitIds.Contains(b.Id));
        }

        private bool CheckBulletInterpolatedHit(Bullet bullet, Vector2 previousPosition)
        {
            var shiftVector = bullet.Position - previousPosition;
            var length = shiftVector.Length();
            return InterpolateBulletCollisionCheck(bullet, previousPosition, shiftVector, length);
        }

        private bool InterpolateBulletCollisionCheck(Bullet bullet, Vector2 previousPosition, Vector2 currentPosition, double lastTickLength)
        {
            for (var i = 0.0; i < lastTickLength; i += _config.IntersectionInterval)
            {
                var interpolatedPosition = previousPosition + InterpolatePlayerPositionInPast(currentPosition, i);

                if (CheckBulletHitAtPosition(bullet, interpolatedPosition))
                    return true;
            }

            return false;
        }

        private static Vector2 InterpolatePlayerPositionInPast(Vector2 shiftVector, double i)
        {
            return (shiftVector.Normalize() * i);
        }

        private bool CheckBulletHitAtPosition(Bullet bullet, Vector2 position)
        {
            var bulletCopy = new Bullet(bullet)
            {
                Position = position
            };

            var colidedPlayer = CheckAnyIntersectionWithPlayers(bulletCopy);
            if (colidedPlayer != null)
            {
                var attacker = _gameState.Players.First(x => x.Id == bulletCopy.PlayerId);
                var damage = attacker.PlayerWeapon.GetWeapon().BulletDamage;
                DamagePlayer(attacker, colidedPlayer, damage);
                NotifyAttacket(attacker, position);

                return true;
            }

            return false;
        }

        private void NotifyAttacket(Player attacker, Vector2 position)
        {
            _hubContext.Clients.Client(attacker.ConnectionId).SendAsync("bulletHit", position);
        }

        private void DamagePlayer(Player attacker, Player victim, int damage)
        {
            victim.Health -= damage;

            if(victim.Health <= 0)
            {
                victim.IsAlive = false;
                victim.MilisecondsToResurect = _config.MilisecondsToResurect;
                
                _hubContext.Clients.All.SendAsync("playerKilled", new object[] { attacker.Name, victim.Name });
            }
        }

        private Player CheckAnyIntersectionWithPlayers(Bullet bullet)
        {
            foreach (var player in _gameState.Players)
            {
                if (bullet.PlayerId != player.Id && player.IsAlive && Intersection.CheckIntersection(new MapCircle(bullet.Position, bullet.Radius),
                        new MapCircle(player.Position, player.Radius)))
                {
                    return player;
                }
            }
            return null;
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
