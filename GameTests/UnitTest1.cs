using GameServer.Physics;
using GameServer.MapObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GameTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_CubeSphereIntersection1()
        {
            // arrange  
            MapSphere o1 = new MapSphere(0, 0, 0, 2);
            MapBox o2 = new MapBox(0, 0, 20, 1, 1, 1);

            // act  
            bool intersects = Intersection.CheckIntersection(o2, o1);

            // assert  
            Assert.IsFalse(intersects, "Intersection error sphere and box");
        }

        [TestMethod]
        public void Test_CubeSphereIntersection2()
        {
            // arrange  
            MapSphere o1 = new MapSphere(0, 0, 0, 2);
            MapBox o2 = new MapBox(0, 0, 1, 1, 1, 1);

            // act  
            bool intersects = Intersection.CheckIntersection(o2, o1);

            // assert  
            Assert.IsTrue(intersects, "Intersection error sphere and box");
        }

        [TestMethod]
        public void Test_CubeSphereIntersection3()
        {
            // arrange  
            MapSphere o1 = new MapSphere(0, 0, 0, 2);
            MapBox o2 = new MapBox(0, 2, 2, 1, 1, 1);

            // act  
            bool intersects = Intersection.CheckIntersection(o2, o1);

            // assert  
            Assert.IsFalse(intersects, "Intersection error sphere and box");
        }


        [TestMethod]
        public void Test_SphereSphereIntersection1()
        {
            // arrange  
            MapSphere o1 = new MapSphere(0, 0, 0, 2);
            MapSphere o2 = new MapSphere(0, 0, 1, 0.1);

            // act  
            bool intersects = Intersection.CheckIntersection(o2, o1);

            // assert  
            Assert.IsTrue(intersects, "Intersection error sphere and box");
        }


        [TestMethod]
        public void Test_SphereSphereIntersection2()
        {
            // arrange  
            MapSphere o1 = new MapSphere(0, 0, 0, 2);
            MapSphere o2 = new MapSphere(0, 0, 2, 2);

            // act  
            bool intersects = Intersection.CheckIntersection(o2, o1);

            // assert  
            Assert.IsFalse(intersects, "Intersection error sphere and box");
        }


        [TestMethod]
        public void Test_SphereSphereIntersection3()
        {
            // arrange  
            MapSphere o1 = new MapSphere(0, 0, 0, Math.Sqrt(2) * 2 - 0.01);
            MapSphere o2 = new MapSphere(2, 0, 2, Math.Sqrt(2) * 2);

            // act  
            bool intersects = Intersection.CheckIntersection(o2, o1);

            // assert  
            Assert.IsFalse(intersects, "Intersection error sphere and box");
        }
    }
}
