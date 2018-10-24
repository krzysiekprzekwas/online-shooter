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
            var vectorA = new Vector2(1, 0);
            var vectorB = new Vector2(2, -10);

            var vectorACopy = new Vector2(vectorA);
            var vectorBCopy = new Vector2(vectorB);

            // Act
            var addedVector = vectorA + vectorB;

            // Assert
            var expectedVector = new Vector2(3, -10);
            Assert.AreEqual(addedVector, expectedVector);

            Assert.AreEqual(vectorACopy, vectorA); // Not changed
            Assert.AreEqual(vectorBCopy, vectorB); // Not changed
        }

        [TestMethod]
        public void ShouldSubtractCorrectly()
        {
            // Arrage
            var vectorA = new Vector2(1, 1);
            var vectorB = new Vector2(3, -10);

            var vectorACopy = new Vector2(vectorA);
            var vectorBCopy = new Vector2(vectorB);

            // Act
            var subtractedVector = vectorA - vectorB;

            // Assert
            var expectedVector = new Vector2(-2, 11);
            Assert.AreEqual(subtractedVector, expectedVector);

            Assert.AreEqual(vectorACopy, vectorA); // Not changed
            Assert.AreEqual(vectorBCopy, vectorB); // Not changed
        }

        [TestMethod]
        public void ShouldMultiplyCorrectly()
        {
            // Arrage
            var vector = new Vector2(1, 3);
            var vectorCopy = new Vector2(vector);
            var multiplier = 3.0;

            // Act
            var multipliedVector = vector * multiplier;

            // Assert
            var expectedVector = new Vector2(3, 9);
            Assert.AreEqual(multipliedVector, expectedVector);

            Assert.AreEqual(vectorCopy, vector); // Not changed
        }

        [TestMethod]
        public void ShouldDivideCorrectly()
        {
            // Arrage
            var vector = new Vector2(-1, 3);
            var vectorCopy = new Vector2(vector);
            var divider = 3.0;

            // Act
            var dividedVector = vector / divider;

            // Assert
            Assert.AreEqual(-1/3.0, dividedVector.X, 1e-6);
            Assert.AreEqual(1, dividedVector.Y);

            Assert.AreEqual(vectorCopy, vector); // Not changed
        }

        [TestMethod]
        public void ShouldCalculateLength()
        {
            // Arrage
            var vector = new Vector2(3, 4);

            // Act
            var length = vector.Length();

            // Assert
            Assert.AreEqual(5, length);
        }

        [TestMethod]
        public void ShouldCalculateSquaredLength()
        {
            // Arrage
            var vector = new Vector2(3, 4);

            // Act
            var lengthSquared = vector.LengthSquared();

            // Assert
            Assert.AreEqual(Math.Pow(5, 2), lengthSquared);
        }

        [TestMethod]
        public void ShouldBeCopyable()
        {
            // Arrage
            var vectorA = new Vector2(11, 13);

            // Act
            var vectorB = new Vector2(vectorA);

            // Assert
            Assert.AreEqual(vectorA, vectorB);
        }

        [TestMethod]
        public void ShouldAllowToCalculateDistanceFromPoints()
        {
            // Arrage
            var vectorA = new Vector2(11, 13);
            var vectorB = new Vector2(vectorA);
            var distance = 3;
            vectorB += new Vector2(1, 1).Normalize() * distance;

            // Act
            var c = Vector2.Distance(vectorA, vectorB);

            // Assert
            Assert.AreEqual(distance, c, 1e-6);
        }

        [TestMethod]
        public void ShouldAllowToCalculateSquaredDistanceFrom()
        {
            // Arrage
            var vectorA = new Vector2(11, 13);
            var vectorB = new Vector2(vectorA);
            var distance = 5;
            vectorB += new Vector2(-3, 1.5).Normalize() * distance;

            // Act
            var c = Vector2.DistanceSquared(vectorA, vectorB);

            // Assert
            Assert.AreEqual(Math.Pow(distance, 2), c, 1e-6);
        }

        [TestMethod]
        public void ShouldCalculateNormalizedVectors()
        {
            // Arrage
            var vector = new Vector2(-1, 1);
            var copy = new Vector2(vector);

            // Act
            var normalized = vector.Normalize();
            var staticalyNormalized = Vector2.Normalize(vector);

            // Assert
            var t = Math.Sqrt(2) / 2;
            var expectedVector = new Vector2(-t, t);

            Assert.AreEqual(expectedVector.X, normalized.X, 1e-6);
            Assert.AreEqual(expectedVector.Y, normalized.Y, 1e-6);
            Assert.AreEqual(expectedVector.X, staticalyNormalized.X, 1e-6);
            Assert.AreEqual(expectedVector.Y, staticalyNormalized.Y, 1e-6);
            Assert.AreEqual(vector, copy);
        }

        [TestMethod]
        public void ShouldBeInitializableWithNoArguments()
        {
            // Arrage
            var vector = new Vector2();

            // Act

            // Assert
            Assert.AreEqual(new Vector2(0, 0), vector);
        }

        [TestMethod]
        public void ShouldCorrectlyCalculateAngleBetweenVectors1()
        {
            // Arrage
            var vectorA = new Vector2(1, 1);
            var vectorB = new Vector2(1, -1);

            // Act
            var angle = Vector2.AngleBetweenVectors(vectorA, vectorB);

            // Assert
            var expectedAngle = Math.PI / 2;
            Assert.AreEqual(angle, expectedAngle, 0.0001);
        }

        [TestMethod]
        public void ShouldCorrectlyCalculateAngleBetweenVectors2()
        {
            // Arrage
            var vectorA = new Vector2(0, 3);
            var vectorB = new Vector2(-1, 0);

            // Act
            var angle = Vector2.AngleBetweenVectors(vectorA, vectorB);

            // Assert
            var expectedAngle = Math.PI / -2;
            Assert.AreEqual(angle, expectedAngle, 0.0001);
        }

        [TestMethod]
        public void ShouldCorrectlyCalculateLengthAfterPropertiesChanged()
        {
            // Arrage
            var vector = new Vector2();

            // Act
            var l0 = vector.Length();

            vector.X = 3;
            vector.Y = 4;

            var l1 = vector.Length();

            vector.X = 12;
            vector.Y = 5;

            var l2 = vector.Length();

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
            var vector = new Vector2();

            // Act
            vector.Normalize();

            // Assert
        }

        [TestMethod]
        public void ShouldNotThrowExceptionWhenTryingToSafeNormalizeZeroVector()
        {
            // Arrage
            var vector = new Vector2();

            // Act
            var normalized = vector.SafeNormalize();

            // Assert
            var expected = Vector2.ZERO_VECTOR;
            Assert.AreEqual(expected, normalized);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionWhenTryingToDivideBy0()
        {
            // Arrage
            var vector = new Vector2();

            // Act
            var divided = vector / 0;

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionWhenTryingToCalculateAngleBetweenZeroVector()
        {
            // Arrage
            var vectorA = new Vector2();
            var vectorB = new Vector2(1, 0);

            // Act
            Vector2.AngleBetweenVectors(vectorA, vectorB);

            // Assert
        }

        [TestMethod]
        public void ShouldCalculateAngleFromVector()
        {
            // Arrange
            var vector = new Vector2(-1, 1);

            // Act
            var angle = Vector2.Vector2ToRadian(vector);

            // Assert
            var expectedAngle = Math.PI * 3 / 4;
            Assert.AreEqual(expectedAngle, angle, 0.0001);
        }

        [TestMethod]
        public void ShouldCalculateAngleFromVector2()
        {
            // Arrange
            var vector = new Vector2(1, 0);

            // Act
            var angle = Vector2.Vector2ToRadian(vector);

            // Assert
            var expectedAngle = Math.PI * 3 / 2;
            Assert.AreEqual(expectedAngle, angle, 0.0001);
        }

        [TestMethod]
        public void ShouldCalculateVector2FromAngle()
        {
            // Arange
            var angle = Math.PI / 2;

            // Act
            var vector = Vector2.RadianToVector2(angle);

            // Assert
            var expectedVector = new Vector2(-1, 0);
            Assert.AreEqual(expectedVector.X, vector.X, 0.0001);
            Assert.AreEqual(expectedVector.Y, vector.Y, 0.0001);
        }
    }
}