using GameServer.MapObjects;
using GameServer.Models;
using GameServer.Physics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTests
{
    [TestClass]
    public class Vector2Tests
    {
        [TestMethod]
        public void ShouldAddCorrectly()
        {
            // Arrage
            var a = new Vector2(1, 0);
            var b = new Vector2(2, -10);

            var aCopy = new Vector2(a);
            var bCopy = new Vector2(b);

            // Act
            var c = a + b;

            // Assert
            var expectedVector = new Vector2(3, -10);
            Assert.AreEqual(c, expectedVector);

            Assert.AreEqual(aCopy, a); // Not changed
            Assert.AreEqual(bCopy, b); // Not changed
        }

        [TestMethod]
        public void ShouldSubtractCorrectly()
        {
            // Arrage
            var a = new Vector2(1, 1);
            var b = new Vector2(3, -10);

            var aCopy = new Vector2(a);
            var bCopy = new Vector2(b);

            // Act
            var c = a - b;

            // Assert
            var expectedVector = new Vector2(-2, 11);
            Assert.AreEqual(c, expectedVector);

            Assert.AreEqual(aCopy, a); // Not changed
            Assert.AreEqual(bCopy, b); // Not changed
        }

        [TestMethod]
        public void ShouldMultiplyCorrectly()
        {
            // Arrage
            var a = new Vector2(1, 3);
            var aCopy = new Vector2(a);
            var b = 3.0;

            // Act
            var c = a * b;

            // Assert
            var expectedVector = new Vector2(3, 9);
            Assert.AreEqual(c, expectedVector);

            Assert.AreEqual(aCopy, a); // Not changed
        }

        [TestMethod]
        public void ShouldDivideCorrectly()
        {
            // Arrage
            var a = new Vector2(-1, 3);
            var aCopy = new Vector2(a);
            var b = 3.0;

            // Act
            var c = a / b;

            // Assert
            Assert.AreEqual(-1/3.0, c.X, 1e-6);
            Assert.AreEqual(1, c.Y);

            Assert.AreEqual(aCopy, a); // Not changed
        }

        [TestMethod]
        public void ShouldCalculateLength()
        {
            // Arrage
            var a = new Vector2(3, 4);

            // Act
            var c = a.Length();

            // Assert
            Assert.AreEqual(5, c);
        }

        [TestMethod]
        public void ShouldCalculateSquaredLength()
        {
            // Arrage
            var a = new Vector2(3, 4);

            // Act
            var c = a.LengthSquared();

            // Assert
            Assert.AreEqual(Math.Pow(5, 2), c);
        }

        [TestMethod]
        public void ShouldBeCopyable()
        {
            // Arrage
            var a = new Vector2(11, 13);

            // Act
            var b = new Vector2(a);

            // Assert
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void ShouldAllowToCalculateDistanceFromPoints()
        {
            // Arrage
            var a = new Vector2(11, 13);
            var b = new Vector2(a);
            var distance = 3;
            b += new Vector2(1, 1).Normalize() * distance;

            // Act
            var c = Vector2.Distance(a, b);

            // Assert
            Assert.AreEqual(distance, c, 1e-6);
        }

        [TestMethod]
        public void ShouldAllowToCalculateSquaredDistanceFrom()
        {
            // Arrage
            var a = new Vector2(11, 13);
            var b = new Vector2(a);
            var distance = 5;
            b += new Vector2(-3, 1.5).Normalize() * distance;

            // Act
            var c = Vector2.DistanceSquared(a, b);

            // Assert
            Assert.AreEqual(Math.Pow(distance, 2), c, 1e-6);
        }

        [TestMethod]
        public void ShouldCalculateNormalizedVectors()
        {
            // Arrage
            var a = new Vector2(-1, 1);
            var copy = new Vector2(a);

            // Act
            var b = a.Normalize();
            var c = Vector2.Normalize(a);

            // Assert
            var t = Math.Sqrt(2) / 2;
            var expectedVector = new Vector2(-t, t);

            Assert.AreEqual(expectedVector.X, b.X, 1e-6);
            Assert.AreEqual(expectedVector.Y, b.Y, 1e-6);
            Assert.AreEqual(expectedVector.X, c.X, 1e-6);
            Assert.AreEqual(expectedVector.Y, c.Y, 1e-6);
            Assert.AreEqual(a, copy);
        }

        [TestMethod]
        public void ShouldBeInitializableWithNoArguments()
        {
            // Arrage
            var a = new Vector2();

            // Act

            // Assert
            Assert.AreEqual(new Vector2(0, 0), a);
        }

        [TestMethod]
        public void ShouldCorrectlyCalculateAngleBetweenVectors1()
        {
            // Arrage
            var a = new Vector2(1, 1);
            var b = new Vector2(1, -1);

            // Act
            var angle = Vector2.AngleBetweenVectors(a, b);

            // Assert
            var expectedAngle = Math.PI / 2;
            Assert.AreEqual(angle, expectedAngle, 0.0001);
        }

        [TestMethod]
        public void ShouldCorrectlyCalculateAngleBetweenVectors2()
        {
            // Arrage
            var a = new Vector2(0, 3);
            var b = new Vector2(-1, 0);

            // Act
            var angle = Vector2.AngleBetweenVectors(a, b);

            // Assert
            var expectedAngle = Math.PI / -2;
            Assert.AreEqual(angle, expectedAngle, 0.0001);
        }

        [TestMethod]
        public void ShouldCorrectlyCalculateLengthAfterPropertiesChanged()
        {
            // Arrage
            var a = new Vector2();

            // Act
            var l0 = a.Length();

            a.X = 3;
            a.Y = 4;

            var l1 = a.Length();

            a.X = 12;
            a.Y = 5;

            var l2 = a.Length();

            // Assert
            var expectedL0 = 0;
            var expectedL1 = 5;
            var expectedL2 = 13;

            Assert.AreEqual(expectedL0, l0);
            Assert.AreEqual(expectedL1, l1);
            Assert.AreEqual(expectedL2, l2);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldThrowExceptionWhenTryingToNormalizeZeroVector()
        {
            // Arrage
            var a = new Vector2();

            // Act
            var v = a.Normalize();

            // Assert
        }

        [TestMethod]
        public void ShouldNotThrowExceptionWhenTryingToSafeNormalizeZeroVector()
        {
            // Arrage
            var a = new Vector2();

            // Act
            var v = a.SafeNormalize();

            // Assert
            var expected = Vector2.ZERO_VECTOR;
            Assert.AreEqual(expected, v);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionWhenTryingToDivideBy0()
        {
            // Arrage
            var a = new Vector2();

            // Act
            var v = a / 0;

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionWhenTryingToCalculateAngleBetweenZeroVector()
        {
            // Arrage
            var a = new Vector2();
            var b = new Vector2(1, 0);

            // Act
            var v = Vector2.AngleBetweenVectors(a, b);

            // Assert
        }
    }
}