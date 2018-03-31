using GameServer.Physics;
using GameServer.MapObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;

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
            MapSphere o2 = new MapSphere(0, 0, 1, 0.1f);

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
            MapSphere o1 = new MapSphere(0, 0, 0, (float)Math.Sqrt(2) * 2f - 0.01f);
            MapSphere o2 = new MapSphere(2, 0, 2, (float)Math.Sqrt(2) * 2f);

            // act  
            bool intersects = Intersection.CheckIntersection(o2, o1);

            // assert  
            Assert.IsFalse(intersects, "Intersection error sphere and box");
        }


        [TestMethod]
        public void Test_PointBoxIntersection1()
        {
            // arrange  
            MapBox box = new MapBox(0, 0, 0, 2, 2, 2);
            Vector3 pos = new Vector3(1, 1, 1);

            // act  
            bool intersects = Intersection.CheckIntersection(box, pos);

            // assert  
            Assert.IsTrue(intersects, "Intersection error position and box");
        }

        [TestMethod]
        public void Test_PointBoxIntersection2()
        {
            // arrange  
            MapBox box = new MapBox(0, 0, 0, 2, 2, 2);
            Vector3 pos = new Vector3(2, 0, 0);

            // act  
            bool intersects = Intersection.CheckIntersection(box, pos);

            // assert  
            Assert.IsFalse(intersects, "Intersection error position and box");
        }
    }
}
