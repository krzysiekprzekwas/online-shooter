using GameServer.MapObjects;
using GameServer.Physics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTests
{
    [TestClass]
    public class IntersectionTests
    {
        [TestMethod]
        public void CircleCircleIntersection_ShouldReturnTrue_IfTheyIntersect()
        {
            // Arrage
            MapCircle c1 = new MapCircle(0, 0, 2);
            MapCircle c2 = new MapCircle(1, 1, 1);

            // Act
            bool intersects = Intersection.CheckIntersection(c1, c2);

            // Assert
            Assert.IsTrue(intersects);
        }

        [TestMethod]
        public void CircleCircleIntersection_ShouldReturnTrue_IfTheyTouchesEachOther()
        {
            // Arrage
            MapCircle c1 = new MapCircle(0, 0, 2);
            MapCircle c2 = new MapCircle(0, 2, 2);

            // Act
            bool intersects = Intersection.CheckIntersection(c1, c2);

            // Assert
            Assert.IsTrue(intersects);
        }

        [TestMethod]
        public void CircleCircleIntersection_ShouldReturnFalse_IfTheyDontIntersect()
        {
            // Arrage
            MapCircle c1 = new MapCircle(0, 0, 2);
            MapCircle c2 = new MapCircle(0, 2.1f, 2);

            // Act
            bool intersects = Intersection.CheckIntersection(c1, c2);

            // Assert
            Assert.IsFalse(intersects);
        }

        [TestMethod]
        public void CircleCircleIntersection_ShouldReturnTrue_IfOneIsInsideAnother()
        {
            // Arrage
            MapCircle c1 = new MapCircle(0, 0, 2);
            MapCircle c2 = new MapCircle(0, 0, 1);

            // Act
            bool intersects = Intersection.CheckIntersection(c1, c2);

            // Assert
            Assert.IsTrue(intersects);
        }

        [TestMethod]
        public void RectRectIntersection_ShouldReturnTrue_IfTheyIntersect()
        {
            // Arrange
            MapRect r1 = new MapRect(0, 0, 2, 2);
            MapRect r2 = new MapRect(1.5f, 1.5f, 1, 1);

            // Act
            bool intersects = Intersection.CheckIntersection(r1, r2);

            // Assert
            Assert.IsTrue(intersects);
        }

        [TestMethod]
        public void RectRectIntersection_ShouldReturnTrue_IfTheyIntersect2()
        {
            // Arrange
            MapRect r1 = new MapRect(0, 0, 10, 10);
            MapRect r2 = new MapRect(6, 0, 5, 1);

            // Act
            bool intersects = Intersection.CheckIntersection(r1, r2);

            // Assert
            Assert.IsTrue(intersects);
        }

        [TestMethod]
        public void RectRectIntersection_ShouldReturnFalse_IfTheyDontIntersect()
        {
            // Arrange
            MapRect r1 = new MapRect(0, 0, 2, 2);
            MapRect r2 = new MapRect(0, 3, 2, 2);

            // Act
            bool intersects = Intersection.CheckIntersection(r1, r2);

            // Assert
            Assert.IsFalse(intersects);
        }

        [TestMethod]
        public void RectRectIntersection_ShouldReturnTrue_IfTheyTouchEachOther()
        {
            // Arrange
            MapRect r1 = new MapRect(0, 0, 2, 2);
            MapRect r2 = new MapRect(2, 2, 2, 2);

            // Act
            bool intersects = Intersection.CheckIntersection(r1, r2);

            // Assert
            Assert.IsTrue(intersects);
        }

        [TestMethod]
        public void RectRectIntersection_ShouldReturnTrue_IfOneIsInsideAnother()
        {
            // Arrange
            MapRect r1 = new MapRect(0, 0, 2, 2);
            MapRect r2 = new MapRect(0, 0, 0.1f, 0.1f);

            // Act
            bool intersects = Intersection.CheckIntersection(r1, r2);

            // Assert
            Assert.IsTrue(intersects);
        }

        [TestMethod]
        public void RectCircleIntersection_ShouldReturnTrue_IfTheyIntersect()
        {
            // Arrange
            MapCircle c = new MapCircle(0, 0, 2);
            MapRect r = new MapRect(1, 0, 1, 1);

            // Act
            bool intersects = Intersection.CheckIntersection(c, r);

            // Assert
            Assert.IsTrue(intersects);
        }

        [TestMethod]
        public void RectCircleIntersection_ShouldReturnFalse_IfTheyDontIntersect()
        {
            // Arrange
            MapCircle c = new MapCircle(0, 0, 2);
            MapRect r = new MapRect(2, 0, 1, 1);

            // Act
            bool intersects = Intersection.CheckIntersection(c, r);

            // Assert
            Assert.IsFalse(intersects);
        }

        [TestMethod]
        public void RectCircleIntersection_ShouldReturnTrue_IfRectIsInsideCircle()
        {
            // Arrange
            MapCircle c = new MapCircle(0, 0, 10);
            MapRect r = new MapRect(0, 0, 1, 1);

            // Act
            bool intersects = Intersection.CheckIntersection(c, r);

            // Assert
            Assert.IsTrue(intersects);
        }

        [TestMethod]
        public void RectCircleIntersection_ShouldReturnTrue_IfCircleIsInsideRect()
        {
            // Arrange
            MapCircle c = new MapCircle(0, 0, 1);
            MapRect r = new MapRect(0, 0, 5, 5);

            // Act
            bool intersects = Intersection.CheckIntersection(r, c);

            // Assert
            Assert.IsTrue(intersects);
        }

        [TestMethod]
        public void RectCircleIntersection_ShouldReturnTrue_IfTheyTouchEachOther()
        {
            // Arrange
            MapCircle c = new MapCircle(0, 0, 2);
            MapRect r = new MapRect(2, 0, 2, 2);

            // Act
            bool intersects = Intersection.CheckIntersection(r, c);

            // Assert
            Assert.IsTrue(intersects);
        }

        [TestMethod]
        public void RectCircleIntersection_ShouldReturnFalse_IfTheyAlmostTouchEachOther()
        {
            // Arrange
            MapCircle c = new MapCircle(0, 0, 2);
            MapRect r = new MapRect(2.01f, 0, 2, 2);

            // Act
            bool intersects = Intersection.CheckIntersection(r, c);

            // Assert
            Assert.IsFalse(intersects);
        }
    }
}
