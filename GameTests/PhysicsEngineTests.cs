using System;
using System.Collections.Generic;
using GameServer;
using GameServer.Game;
using GameServer.MapObjects;
using GameServer.Models;
using GameServer.Physics;
using GameServer.States;
using GameServer.World;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameTests
{
    [TestClass]
    public class PhysicsEngineTests
    {
        private Config _config;
        private MapState _mapState;
        private Player _player;
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
                Position = new Vector2(0, 0),
                Angle = 0.0,
                Keys = new List<KeyEnum>(),
            };

            GameState.Instance.Players.Add(_player);
        }

        [TestMethod]
        public void ShouldCalculateSpeedVectorFromPlayerInput()
        {
            // Arrange
            _player.Keys.Add(KeyEnum.Up);
            _player.Keys.Add(KeyEnum.Left);

            // Act
            var normalizedSpeedVector = _gameEngine.PhysicsEngine.GetSpeedFromPlayerInput(_player).Normalize();

            // Assert
            var expectedSpeedVectorDirection = Vector2.Normalize(new Vector2(-1, -1));
            Assert.AreEqual(expectedSpeedVectorDirection.X, normalizedSpeedVector.X, 1e-6);
            Assert.AreEqual(expectedSpeedVectorDirection.Y, normalizedSpeedVector.Y, 1e-6);
        }
        
        [TestMethod]
        public void ShouldCalcualteSpeedVectorAsEmpty_WhenPlayerInputsOppositeDirections()
        {
            // Arrange
            _player.Keys.Add(KeyEnum.Down);
            _player.Keys.Add(KeyEnum.Up);

            // Act
            var speedVector = _gameEngine.PhysicsEngine.GetSpeedFromPlayerInput(_player);

            // Assert
            var expectedSpeedVectorDirection = new Vector2(0, 0);
            Assert.AreEqual(expectedSpeedVectorDirection, speedVector);
        }

        [TestMethod]
        public void ShouldNotThrowExceptionsWhenPlayerIsStandingStill()
        {
            // Arrange

            // Act
            for (int i = 1; i <= 200; i++)
                _gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            Assert.AreEqual(_player.Position.Y, 0);
            Assert.AreEqual(_player.Position.X, 0);
        }

        [TestMethod]
        public void ShouldAllowMovement()
        {
            // Arrange

            // Act  
            _player.Keys.Add(KeyEnum.Up);
            _player.Keys.Add(KeyEnum.Left);
            _player.Angle = Math.PI / 4;
            for (int i = 1; i <= 200; i++)
                _gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            Assert.AreNotEqual(_player.Position.Y, 0);
            Assert.AreNotEqual(_player.Position.X, 0);
        }

        [TestMethod]
        public void ShouldNotAllowPassingThroughWalls()
        {
            // Arrange
            var r = _player.Radius;
            var mapRect = new MapRect(0, r * 2, 2, 2);
            _mapState.MapObjects.Add(mapRect);
            var config = new Config();

            // Act  
            _player.Keys.Add(KeyEnum.Down);
            for (int i = 1; i <= 200; i++)
            {
                _gameEngine.PhysicsEngine.ApplyPhysics();

                var playerObject = new MapCircle(_player.Position, _player.Radius);
                Assert.IsFalse(Intersection.CheckIntersection(playerObject, mapRect));
            }

            // Assert
            Assert.AreNotEqual(_player.Position.Y, 0);
            Assert.AreEqual(_player.Position.Y, mapRect.Position.Y - (mapRect.Height / 2) - _player.Radius, config.IntersectionInterval);
        }
        
        [TestMethod]
        public void ShouldStuckPlayerBetweenTwoWalls()
        {
            // Arrange
            var r = _player.Radius;
            var mapRect1 = new MapRect(r, 3 * r, 4 * r, 2 * r);
            var mapRect2 = new MapRect(4 * r, r, 2 * r, 6 * r);

            _mapState.MapObjects.Add(mapRect1);
            _mapState.MapObjects.Add(mapRect2);

            // Act  
            _player.Keys = new List<KeyEnum> { KeyEnum.Down };
            for (var i = 1; i <= 200; i++)
                _gameEngine.PhysicsEngine.ApplyPhysics();

            _player.Keys = new List<KeyEnum> { KeyEnum.Right };
            for (var i = 1; i <= 200; i++)
                _gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            var expectedY = mapRect1.Position.Y - (mapRect1.Height / 2) - _player.Radius;
            var expectedX = mapRect2.Position.X - (mapRect2.Width / 2) - _player.Radius;

            Assert.AreEqual(_player.Position.Y, expectedY, _config.IntersectionInterval);
            Assert.AreEqual(_player.Position.X, expectedX, _config.IntersectionInterval);
        }

        [TestMethod]
        public void ShouldAllowParallelMovement()
        {
            // Arrange
            _player.Position.Y += 0.0001;
            var r = _player.Radius;
            var mapRect1 = new MapRect(0, -2 * r, 100 * r, 2 * r);
            _mapState.MapObjects.Add(mapRect1);

            // Act
            _player.Keys = new List<KeyEnum> { KeyEnum.Up, KeyEnum.Right };
            for (var i = 1; i <= 200; i++)
                _gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            Assert.IsTrue(_player.Position.X > 0);
        }

        [TestMethod]
        public void ShouldAllowParallelMovement2()
        {
            // Arrange
            var r = _player.Radius;
            var mapRect1 = new MapRect(4 * r, 0, 2 * r, 1000 * r);
            _mapState.MapObjects.Add(mapRect1);

            // Act
            _player.Keys = new List<KeyEnum> { KeyEnum.Down, KeyEnum.Right };
            for (var i = 1; i <= 200; i++)
                _gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            Assert.IsTrue(_player.Position.X < _player.Position.Y);
        }

        [TestMethod]
        public void ShouldDeccelerateBulletsCorrectly()
        {
            // Arrange
            var bullet = new Bullet
            {
                Position = new Vector2(0, 0),
                Speed = new Vector2(0, 10)
            };
            GameState.Instance.Bullets.Add(bullet);
            _config.BulletDecceleraionPerTick = 0.5;

            // Act
            _gameEngine.PhysicsEngine.ApplyPhysics();
            _gameEngine.PhysicsEngine.ApplyPhysics();
            _gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            Assert.AreEqual(1.25, bullet.Speed.Y);
            Assert.AreEqual(17.5, bullet.Position.Y);
        }

        [TestMethod]
        public void ShouldDecceleratePlayersCorrectlyWhenNoKeysInput()
        {
            // Arrange
            var serverTicks = 3;
            _player.Speed = new Vector2(128, 128);
            _config.PlayerDeccelerationPerTick = 0.5;

            // Act
            for(var i = 0; i < serverTicks; i++)
                _gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            Assert.AreEqual(16, _player.Speed.X);
            Assert.AreEqual(16, _player.Speed.Y);
            Assert.AreEqual(224, _player.Position.X, serverTicks * _config.IntersectionInterval);
            Assert.AreEqual(224, _player.Position.Y, serverTicks * _config.IntersectionInterval);
        }

        [TestMethod]
        public void ShouldDecceleratePlayerCorrectlyWhenMovingInOppositeDirection()
        {
            // Arrange
            var serverTicks = 2;
            _player.Speed = Vector2.LEFT_VECTOR * 100;
            _player.Keys.Add(KeyEnum.Right);
            _config.PlayerDeccelerationPerTick = 0.8;
            _config.PlayerSpeedPerTick = 10;

            // Act
            for (var i = 0; i < serverTicks; i++)
                _gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            Assert.AreEqual(-49.6, _player.Speed.X);
            Assert.AreEqual(-152, _player.Position.X, serverTicks * _config.IntersectionInterval);
        }

        [TestMethod]
        public void ShouldDecceleratePlayerCorrectlyWhenMovingSameDirection()
        {
            // Arrange
            var serverTicks = 3;
            _player.Speed = Vector2.RIGHT_VECTOR * 100;
            _player.Keys.Add(KeyEnum.Right);
            _config.PlayerDeccelerationPerTick = 0.5;
            _config.PlayerSpeedPerTick = 50;

            // Act
            for (var i = 0; i < serverTicks; i++)
                _gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            Assert.AreEqual(56.25, _player.Speed.X);
            Assert.AreEqual(387.5, _player.Position.X, serverTicks * _config.IntersectionInterval);
        }
    }
}
