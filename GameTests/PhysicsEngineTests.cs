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
        [TestMethod]
        public void ShouldCalculateSpeedVectorFromPlayerInput()
        {
            // Arrange
            var config = new Config();
            var player = new Player(config);
            var physicsEngine = new PhysicsEngine(config,null);
            player.Keys.Add(KeyEnum.Up);
            player.Keys.Add(KeyEnum.Left);

            // Act
            

            var nromalizedSpeedVector = physicsEngine.GetSpeedFromPlayerInput(player).Normalize();

            // Assert
            var expectedSpeedVectorDirection = Vector2.Normalize(new Vector2(-1, -1));
            Assert.AreEqual(expectedSpeedVectorDirection.X, nromalizedSpeedVector.X, 1e-6);
            Assert.AreEqual(expectedSpeedVectorDirection.Y, nromalizedSpeedVector.Y, 1e-6);
        }
        
        [TestMethod]
        public void ShouldCalcualteSpeedVectorAsEmpty_WhenPlayerInputsOppositeDirections()
        {
            // Arrange
            var config = new Config();
            var player = new Player(config);
            var physicsEngine = new PhysicsEngine(config, null);
            player.Keys.Add(KeyEnum.Down);
            player.Keys.Add(KeyEnum.Up);

            // Act
            var speedVector = physicsEngine.GetSpeedFromPlayerInput(player);

            // Assert
            var expectedSpeedVectorDirection = new Vector2(0, 0);
            Assert.AreEqual(expectedSpeedVectorDirection, speedVector);
        }

        [TestMethod]
        public void ShouldNotThrowExceptionsWhenPlayerIsStandingStill()
        {
            // Arrange
            var gameEngine = CreateGameEngineAndAddPlayer(out Player player);

            // Act
            for (int i = 1; i <= 200; i++)
                gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            Assert.AreEqual(player.Position.Y, 0);
            Assert.AreEqual(player.Position.X, 0);
        }

        [TestMethod]
        public void ShouldAllowMovement()
        {
            // Arrange
            var gameEngine = CreateGameEngineAndAddPlayer(out Player player);

            // Act  
            player.Keys.Add(KeyEnum.Up);
            player.Keys.Add(KeyEnum.Left);
            player.Angle = Math.PI / 4;
            for (int i = 1; i <= 200; i++)
                gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            Assert.AreNotEqual(player.Position.Y, 0);
            Assert.AreNotEqual(player.Position.X, 0);
        }

        [TestMethod]
        public void ShouldNotAllowPassingThroughWalls()
        {
            // Arrange
            var mapState = new MapState();
            var gameEngine = CreateGameEngineAndAddPlayer(out Player player,mapState);
            var r = player.Radius;
            var mapRect = new MapRect(0, r * 2, 2, 2);
            mapState.MapObjects.Add(mapRect);
            var config = new Config();

            // Act  
            player.Keys.Add(KeyEnum.Down);
            for (int i = 1; i <= 200; i++)
            {
                gameEngine.PhysicsEngine.ApplyPhysics();

                var playerObject = new MapCircle(player.Position, player.Radius);
                Assert.IsFalse(Intersection.CheckIntersection(playerObject, mapRect));
            }

            // Assert
            Assert.AreNotEqual(player.Position.Y, 0);
            Assert.AreEqual(player.Position.Y, mapRect.Position.Y - (mapRect.Height / 2) - player.Radius, config.IntersectionInterval * 2);
        }
        
        [TestMethod]
        public void ShouldStuckPlayerBetweenTwoWalls()
        {
            // Arrange
            var mapState = new MapState();
            var config = new Config();
            var gameEngine = CreateGameEngineAndAddPlayer(out Player player,mapState);
            var r = player.Radius;
            var mapRect1 = new MapRect(r, 3 * r, 4 * r, 2 * r);
            var mapRect2 = new MapRect(4 * r, r, 2 * r, 6 * r);

            mapState.MapObjects.Add(mapRect1);
            mapState.MapObjects.Add(mapRect2);

            // Act  
            player.Keys = new List<KeyEnum> { KeyEnum.Down };
            for (var i = 1; i <= 200; i++)
                gameEngine.PhysicsEngine.ApplyPhysics();

            player.Keys = new List<KeyEnum> { KeyEnum.Right };
            for (var i = 1; i <= 200; i++)
                gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            var expectedY = mapRect1.Position.Y - (mapRect1.Height / 2) - player.Radius;
            var expectedX = mapRect2.Position.X - (mapRect2.Width / 2) - player.Radius;

            Assert.AreEqual(player.Position.Y, expectedY, config.IntersectionInterval * 2);
            Assert.AreEqual(player.Position.X, expectedX, config.IntersectionInterval * 2);
        }

        [TestMethod]
        public void ShouldAllowParallelMovement()
        {
            // Arrange
            var mapState = new MapState();
            var gameEngine = CreateGameEngineAndAddPlayer(out Player player);
            player.Position.Y += 0.0001;
            var r = player.Radius;
            var mapRect1 = new MapRect(0, -2 * r, 100 * r, 2 * r);
            mapState.MapObjects.Add(mapRect1);

            // Act
            player.Keys = new List<KeyEnum> { KeyEnum.Up, KeyEnum.Right };
            for (var i = 1; i <= 200; i++)
                gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            Assert.IsTrue(player.Position.X > 0);
        }

        [TestMethod]
        public void ShouldAllowParallelMovement2()
        {
            // Arrange
            var mapState = new MapState();
            var gameEngine = CreateGameEngineAndAddPlayer(out Player player, mapState);
            var r = player.Radius;
            var mapRect1 = new MapRect(4 * r, 0, 2 * r, 1000 * r);
            mapState.MapObjects.Add(mapRect1);

            // Act
            player.Keys = new List<KeyEnum> { KeyEnum.Down, KeyEnum.Right };
            for (var i = 1; i <= 200; i++)
                gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            Assert.IsTrue(player.Position.X < player.Position.Y);
        }

        // Helper methods
        private static GameEngine CreateGameEngineAndAddPlayer(out Player player, MapState mapState = null)
        {
            if(mapState==null)
                mapState=new MapState();

            GameEngine GE = new GameEngine(null,new Config(),new WorldLoader(mapState),mapState);
            GE.Ticker.Dispose();

            player = new Player(new Config())
            {
                Position = new Vector2(0, 0),
                Angle = 0.0
            };

            GameState.Instance.Players.Add(player);

            return GE;
        }
    }
}
