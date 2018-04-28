using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using GameServer;
using GameServer.Game;
using GameServer.Models;
using GameServer.States;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameTests
{
    [TestClass]
    public class PhysicsIntegrationTest
    {
        [TestMethod]
        public void PhysicsEngineSimulationDiffOnTicks()
        {
            // arrange  
            Config.PLAYER_DECCELERATION = 0;
            float vectorLength = (Config.PLAYER_SPEED / (float)Config.SERVER_TICK);
            const float TICKS = 100;
            const float MAX_DIFF_PER_TICK = 0.0125f;
            Vector3 initialPosition = new Vector3(0, Config.PLAYER_SIZE / 2, 0);

            var servicesProvider = Startup.BuildDi();
            GameEngine GE = servicesProvider.GetRequiredService<GameEngine>();
            GE.Ticker.Dispose();

            Player player = new Player()
            {
                Position = initialPosition,
                Angles = new Vector2(0, (float)Math.PI / 4)
            };

            GameState.Instance.value.Players.Add(player);

            // act  
            player.Keys.Add("w");
            for(int i = 1; i <= TICKS; i++)
                GE.PhysicsEngine.ApplyPhysics();

            // assert  
            float actualDistance = Vector3.Distance(initialPosition, player.Position);
            float expectedMaxDistance = TICKS * vectorLength;
            float distanceDiff = actualDistance - expectedMaxDistance;
            float maxDiff = TICKS * MAX_DIFF_PER_TICK;
            Assert.IsTrue(distanceDiff < maxDiff);
        }

        [TestMethod]
        public void PhysicsEngineSimulationHugToWall()
        {
            // arrange  
            Config.PLAYER_DECCELERATION = 0;
            const float TICKS = 200;
            Vector3 initialPosition = new Vector3(0, Config.PLAYER_SIZE / 2, 0);

            var servicesProvider = Startup.BuildDi();
            GameEngine GE = servicesProvider.GetRequiredService<GameEngine>();
            GE.Ticker.Dispose();

            Player player = new Player()
            {
                Position = initialPosition,
                Angles = new Vector2(0, (float)Math.PI / 4)
            };

            GameState.Instance.value.Players.Add(player);

            // act  
            player.Keys.Add("w");
            for (int i = 1; i <= TICKS; i++)
                GE.PhysicsEngine.ApplyPhysics();

            // assert
            Assert.IsTrue(player.Position.Z < 96 - player.Radius);
        }

        //[TestMethod]
        //public void PhysicsEngineSimulationPassingNextToWalls()
        //{
        //    // arrange  
        //    Config.PLAYER_DECCELERATION = 0;
        //    const float TICKS = 150;
        //    Vector3 initialPosition = new Vector3(0, Config.PLAYER_SIZE / 2, 64);

        //    var servicesProvider = Startup.BuildDi();
        //    GameEngine GE = servicesProvider.GetRequiredService<GameEngine>();
        //    GE.Ticker.Dispose();

        //    Player player = new Player()
        //    {
        //        Position = initialPosition,
        //        Angles = new Vector2(0, (float)Math.PI / 4)
        //    };

        //    GameState.Instance.value.Players.Add(player);

        //    // act  
        //    player.Keys.Add("w");
        //    for (int i = 1; i <= TICKS; i++)
        //        GE.PhysicsEngine.ApplyPhysics();

        //    // assert
        //    Assert.IsTrue(player.Position.X > 64);
        //}

        [TestMethod]
        public void WalkingByChangingClingingWalls()
        {
            // arrange  
            Config.PLAYER_DECCELERATION = 0;
            Vector3 initialPosition = new Vector3(59.41173f, Config.PLAYER_SIZE / 2, 79.99442f);

            var servicesProvider = Startup.BuildDi();
            GameEngine GE = servicesProvider.GetRequiredService<GameEngine>();
            GE.Ticker.Dispose();

            Player player = new Player()
            {
                Position = initialPosition,
                Angles = new Vector2(0, (float)Math.PI / 4)
            };

            GameState.Instance.value.Players.Add(player);

            // act  
            player.Keys.Add("w");
            GE.PhysicsEngine.ApplyPhysics();

            // assert
            Assert.AreNotEqual(player.Position, initialPosition);
        }
        
    }
}
