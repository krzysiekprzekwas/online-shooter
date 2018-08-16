using System;
using System.Collections.Generic;
using System.Text;
using GameServer;
using GameServer.Game;
using GameServer.MapObjects;
using GameServer.Models;
using GameServer.Physics;
using GameServer.States;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameTests
{
    [TestClass]
    public class PhysicsEngineTests
    {
        [TestMethod]
        public void PhysicsEngine_ShouldCalculateSpeedVectorFromPlayerInput()
        {
            // Arrange
            var player = new Player();
            player.Keys.Add("w");
            player.Keys.Add("a");

            // Act
            var speedVector = PhysicsEngine.GetSpeedFromPlayerInput(player);

            // Assert
            var expectedSpeedVectorDirection = Vector2.Normalize(new Vector2(-1, 1));
            Assert.AreEqual(expectedSpeedVectorDirection, Vector2.Normalize(speedVector));
        }
        
        [TestMethod]
        public void PhysicsEngine_ShouldCalcualteSpeedVectorAsEmpty_WhenPlayerInputsOppositeDirections()
        {
            // Arrange
            var player = new Player();
            player.Keys.Add("s");
            player.Keys.Add("w");

            // Act
            var speedVector = PhysicsEngine.GetSpeedFromPlayerInput(player);

            // Assert
            var expectedSpeedVectorDirection = new Vector2(0, 0);
            Assert.AreEqual(expectedSpeedVectorDirection, speedVector);
        }

        [TestMethod]
        public void PhysicsEngine_ShouldAllowMovement()
        {
            // Arrange
            var gameEngine = CreateGameEngineAndAddPlayer(out Player player);

            // Act  
            player.Keys.Add("w");
            player.Angle = (float)Math.PI / 4f;
            for (int i = 1; i <= 200; i++)
                gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            Assert.AreNotEqual(player.Position.Y, 0);
            Assert.AreNotEqual(player.Position.X, 0);
        }

        [TestMethod]
        public void PhysicsEngine_ShouldNotAllowPassingThroughWalls()
        {
            // Arrange
            var gameEngine = CreateGameEngineAndAddPlayer(out Player player);
            var mapRect = new MapRect(0, 30, 2, 2);
            MapState.Instance.MapObjects = new List<MapObject> { mapRect };

            // Act  
            player.Keys.Add("w");
            for (int i = 1; i <= 200; i++)
            {
                gameEngine.PhysicsEngine.ApplyPhysics();
                Assert.IsFalse(Intersection.CheckIntersection(player.WorldObject, mapRect));
            }

            // Assert
            Assert.AreNotEqual(player.Position.Y, 0);
            Assert.AreEqual(player.Position.Y, mapRect.Position.Y - (mapRect.Height / 2) - player.Radius, Config.INTERSECTION_INTERVAL * 2);
        }
        
        [TestMethod]
        public void PhysicsEngine_ShouldStuckPlayerBetweenTwoWalls()
        {
            // Arrange
            var gameEngine = CreateGameEngineAndAddPlayer(out Player player);
            var r = player.Radius;
            var mapRect1 = new MapRect(r, 3 * r, 4 * r, 2 * r);
            var mapRect2 = new MapRect(4 * r, r, 2 * r, 6 * r);
            MapState.Instance.MapObjects = new List<MapObject> { mapRect1, mapRect2 };

            // Act  
            player.Keys = new List<string> { "w" };
            for (int i = 1; i <= 200; i++)
                gameEngine.PhysicsEngine.ApplyPhysics();

            player.Keys = new List<string> { "a" };
            for (int i = 1; i <= 200; i++)
                gameEngine.PhysicsEngine.ApplyPhysics();

            // Assert
            var expectedY = mapRect1.Position.Y - (mapRect1.Height / 2) - player.Radius;
            var expectedX = mapRect2.Position.X - (mapRect2.Width / 2) - player.Radius;

            Assert.AreEqual(player.Position.Y, expectedY, Config.INTERSECTION_INTERVAL * 2);
            Assert.AreEqual(player.Position.X, expectedX, Config.INTERSECTION_INTERVAL * 2);
        }


        // Helper methods
        private static GameEngine CreateGameEngineAndAddPlayer(out Player player)
        {
            var servicesProvider = Startup.BuildDi();
            GameEngine GE = servicesProvider.GetRequiredService<GameEngine>();
            GE.Ticker.Dispose();

            player = new Player()
            {
                Position = new Vector2(0, 0),
                Angle = 0.0f
            };

            GameState.Instance.Players.Add(player);

            return GE;
        }
    }
}
