using GameServer.Physics;
using GameServer.MapObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GameServer.Models;
using GameServer.States;

namespace GameTests
{
    [TestClass]
    public class RayCastTests
    {
        [TestMethod]
        public void RectRayCasting_ShouldReturnHitPosition_1()
        {
            // arrange
            MapRect rect = new MapRect(0, 0, 1, 1);
            Ray ray = new Ray(0, -2, 0, 1);

            // act
            Trace trace = RayCast.CheckBulletTrace(ray, rect);

            // assert  
            Assert.IsNotNull(trace);
            Assert.AreEqual(trace.Position, new Vector2(0, -0.5));
        }

        [TestMethod]
        public void RectRayCasting_ShouldReturnHitPosition_2()
        {
            // arrange
            MapRect rect = new MapRect(1.5, -0.5, 1, 1);
            Ray ray = new Ray(2, -2, -1, 1);

            // act
            Trace trace = RayCast.CheckBulletTrace(ray, rect);

            // assert  
            Assert.IsNotNull(trace);
            Assert.AreEqual(trace.Position, new Vector2(1, -1));
        }

        [TestMethod]
        public void RectRayCasting_ShouldReturnHitPosition_3()
        {
            // arrange
            MapRect rect = new MapRect(10, 10, 1, 1);
            Ray ray = new Ray(10, 12, 0, -1);

            // act
            Trace trace = RayCast.CheckBulletTrace(ray, rect);

            // assert  
            Assert.IsNotNull(trace);
            Assert.AreEqual(trace.Position, new Vector2(10, 10.5));
        }

        [TestMethod]
        public void RectRayCasting_ShouldReturnNull_WhenInOppositeDirection()
        {
            // arrange
            MapRect rect = new MapRect(1, 0, 2, 2);
            Ray ray = new Ray(-3, 0, -1, 0);

            // act
            Trace trace = RayCast.CheckBulletTrace(ray, rect);

            // assert  
            Assert.IsNull(trace);
        }

        [TestMethod]
        public void CircleRayCasting_ShouldReturnHitPosition_1()
        {
            // arrange
            MapCircle circle = new MapCircle(10, 10, 1);
            Ray ray = new Ray(0, 0, 1, 1);

            // act
            Trace trace = RayCast.CheckBulletTrace(ray, circle);

            // assert  
            Assert.IsNotNull(trace);

            var c = Math.Sqrt(2) / 2;
            Vector2 expectedPosition = circle.Position - new Vector2(c, c);
            Assert.AreEqual(expectedPosition.X, trace.Position.X, 1e-6);
            Assert.AreEqual(expectedPosition.Y, trace.Position.Y, 1e-6);
        }

        [TestMethod]
        public void CircleRayCasting_ShouldReturnHitPosition_2()
        {
            // arrange
            MapCircle circle = new MapCircle(10, 10, 1);
            Ray ray = new Ray(20, 10, -1, 0);

            // act
            Trace trace = RayCast.CheckBulletTrace(ray, circle);

            // assert  
            Assert.IsNotNull(trace);
            Assert.AreEqual(trace.Position, new Vector2(11, 10));
        }

        [TestMethod]
        public void CircleRayCasting_ShouldReturnHitPosition_3()
        {
            // arrange
            MapCircle circle = new MapCircle(1, 0, 1);
            Ray ray = new Ray(5, 0, -1, 0);

            // act
            Trace trace = RayCast.CheckBulletTrace(ray, circle);

            // assert  
            Assert.IsNotNull(trace);
            Assert.AreEqual(trace.Position, new Vector2(2, 0));
        }

        [TestMethod]
        public void CircleRayCasting_ShouldReturnNull_WhenInOppositeDirection()
        {
            // arrange
            MapCircle circle = new MapCircle(1, 0, 1);
            Ray ray = new Ray(-3, 0, -1, 0);

            // act
            Trace trace = RayCast.CheckBulletTrace(ray, circle);

            // assert  
            Assert.IsNull(trace);
        }
    }
}
