using GameServer.Physics;
using GameServer.MapObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_CubSphereIntersection1()
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
        public void Test_CubSphereIntersection2()
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
        public void Test_CubSphereIntersection3()
        {
            // arrange  
            MapSphere o1 = new MapSphere(0, 0, 0, 2);
            MapBox o2 = new MapBox(0, 2, 2, 1, 1, 1);

            // act  
            bool intersects = Intersection.CheckIntersection(o2, o1);

            // assert  
            Assert.IsFalse(intersects, "Intersection error sphere and box");
        }

    }
}
