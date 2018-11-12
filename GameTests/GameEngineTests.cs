using GameServer;
using GameServer.Game;
using GameServer.MapObjects;
using GameServer.Models;
using GameServer.States;
using GameServer.World;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameTests
{
    [TestClass]
    public class GameEngineTests
    {
        private Config _config;
        private MapState _mapState;
        private Player _player;
        private Player _attacker;
        private GameEngine _gameEngine;

        [TestInitialize]
        public void PhysicsEngineInitalization()
        {
            _config = new Config();
            _mapState = new MapState();

            _config = new Config();
            _gameEngine = new GameEngine(null, _config, new WorldLoader(_mapState), _mapState);
            _gameEngine.Ticker.Dispose();

            _mapState.MapObjects = new List<MapObject>();

            GameState.Instance.Players = new List<Player>();
            GameState.Instance.Bullets = new List<Bullet>();
            GameState.Instance.PlayerId = 1;

            _player = new Player(_config)
            {
                Id = 1,
                Position = new Vector2(0, 0),
                Angle = 0.0,
                Keys = new List<KeyEnum>(),
                PlayerWeapon = new PlayerWeapon(WeaponEnum.SingleShotTheGreatBarrel)
            };

            _attacker = new Player(_config)
            {
                Id = 2,
                Position = new Vector2(0, 100),
                Angle = 0.0,
                Keys = new List<KeyEnum>(),
                PlayerWeapon = new PlayerWeapon(WeaponEnum.SingleShotTheGreatBarrel)
            };

            GameState.Instance.Players.Add(_player);
            GameState.Instance.Players.Add(_attacker);
        }

        [TestMethod]
        public void ShouldNotFindInterpolatedHitWhenItsTurnedOffByConfiguration()
        {
            // Arrange
            var bulletRadius = 5;
            var bullet = new Bullet
            {
                Id = 1,
                PlayerId = 2,
                Position = new Vector2(-_player.Radius - bulletRadius - 1),
                Radius = bulletRadius,
                Speed = new Vector2((_player.Radius * 2) + (bulletRadius * 2) + 2)
            };
            GameState.Instance.Bullets.Add(bullet);

            _config.Interpolation = true;

            // Act
            _gameEngine.PerformSingleTick();

            // Assert
            Assert.AreEqual(0, GameState.Instance.Bullets.Count);
            Assert.AreNotEqual(_player.MaxHealth, _player.Health);
        }

        [TestMethod]
        public void ShouldFindInterpolatedBulletHit()
        {
            // Arrange
            var bulletRadius = 5;
            var bullet = new Bullet
            {
                Id = 1,
                PlayerId = 2,
                Position = new Vector2(-_player.Radius - bulletRadius - 1),
                Radius = bulletRadius,
                Speed = new Vector2((_player.Radius * 2) + (bulletRadius * 2) + 2)
            };
            GameState.Instance.Bullets.Add(bullet);

            _config.Interpolation = false;

            // Act
            _gameEngine.PerformSingleTick();

            // Assert
            Assert.AreEqual(1, GameState.Instance.Bullets.Count);
            Assert.AreEqual(_player.MaxHealth, _player.Health);
        }
    }
}
