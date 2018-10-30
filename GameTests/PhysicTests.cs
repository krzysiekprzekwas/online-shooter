using GameServer.Physics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using GameServer.Models;

namespace GameTests
{
    [TestClass]
    public class PhysicTests
    {
        [TestMethod]
        public void ShouldCorrectlyRotateVector1()
        {
            // Arrage
            var vector = new Vector2(10, 0);
            var angle = Math.PI / 4;

            // Act
            var rotatedVector = Physic.RotateVector(vector, angle);

            // Assert
            var expectedVector = Vector2.Normalize(new Vector2(1, 1)) * vector.Length();
            Assert.AreEqual(expectedVector.X, rotatedVector.X, 1e-6);
            Assert.AreEqual(expectedVector.Y, rotatedVector.Y, 1e-6);
        }

        [TestMethod]
        public void ShouldCorrectlyRotateVectorByNegativeAngle()
        {
            // Arrage
            var vector = new Vector2(0, -3);
            var angle = Math.PI / -2;

            // Act
            var rotatedVector = Physic.RotateVector(vector, angle);

            // Assert
            var expectedVector = new Vector2(-3, 0);
            Assert.AreEqual(rotatedVector.X, expectedVector.X, 0.0001);
            Assert.AreEqual(rotatedVector.Y, expectedVector.Y, 0.0001);
        }

        [TestMethod]
        public void ShouldCorrectlyProjectVectorOntoAnother1()
        {
            // Arrage
            var vector = new Vector2(2, 1);
            var projectionVector = new Vector2(1, 0);

            // Act
            var projectedVector = Physic.ProjectVector(vector, projectionVector);

            // Assert
            var expectedVector = new Vector2(2, 0);
            Assert.AreEqual(projectedVector, expectedVector);
        }

        [TestMethod]
        public void ShouldCorrectlyProjectVectorOntoAnother2()
        {
            // Arrage
            var vector = new Vector2(-2, -1);
            var projectionVector = new Vector2(-1, 1);

            // Act
            var projectedVector = Physic.ProjectVector(vector, projectionVector);

            // Assert
            var expectedVector = new Vector2(-0.5, 0.5);
            Assert.AreEqual(projectedVector.X, expectedVector.X, 0.0001);
            Assert.AreEqual(projectedVector.Y, expectedVector.Y, 0.0001);
        }

        [TestMethod]
        public void ShouldCorrectlyProjectVectorOntoAnother3()
        {
            // Arrage
            var vector = new Vector2(2, 1);
            var projectionVector = new Vector2(-1, 0);

            // Act
            var projectedVector = Physic.ProjectVector(vector, projectionVector);

            // Assert
            var expectedVector = new Vector2(2, 0);
            Assert.AreEqual(projectedVector, expectedVector);
        }

        [TestMethod]
        public void ShouldCorrectlyProjectVectorOntoAnother4()
        {
            // Arrage
            var vector = new Vector2(0.55242717280199, 0.55242717280199);
            var projectionVector = Vector2.UP_VECTOR;

            // Act
            var projectedVector = Physic.ProjectVector(vector, projectionVector);

            // Assert
            var expectedVector = new Vector2(0, 0.55242717280199);
            Assert.AreEqual(projectedVector, expectedVector);
        }

        [TestMethod]
        public void ShouldCorrectlyCalculateParallelVectorToNormal1()
        {
            // Arrage
            var vector = new Vector2(2, 1);
            var normalVector = new Vector2(0, -1);

            // Act
            var parallelVector = Physic.GetParallelVectorToNormal(vector, normalVector);

            // Assert
            var expectedVector = new Vector2(2, 0);
            Assert.AreEqual(parallelVector.X, expectedVector.X, 0.0001);
            Assert.AreEqual(parallelVector.Y, expectedVector.Y, 0.0001);
        }

        [TestMethod]
        public void ShouldCorrectlyCalculateParallelVectorToNormal2()
        {
            // Arrage
            var vector = new Vector2(-2, -1);
            var normalvector = new Vector2(1, 1);

            // Act
            var parallelVector = Physic.GetParallelVectorToNormal(vector, normalvector);

            // Assert
            var expectedVector = new Vector2(-0.5, 0.5);
            Assert.AreEqual(parallelVector.X, expectedVector.X, 0.0001);
            Assert.AreEqual(parallelVector.Y, expectedVector.Y, 0.0001);
        }

        [TestMethod]
        public void ShouldCorrectlyCalculateParallelVectorToNormal3()
        {
            // Arrage
            var vector = new Vector2(-1, -1);
            var normalvector = new Vector2(1, 0);

            // Act
            var parallelVector = Physic.GetParallelVectorToNormal(vector, normalvector);

            // Assert
            var expectedVector = new Vector2(0, -1);
            Assert.AreEqual(parallelVector.X, expectedVector.X, 0.0001);
            Assert.AreEqual(parallelVector.Y, expectedVector.Y, 0.0001);
        }

        [TestMethod]
        public void ShouldCorrectlyCalculateParallelVectorToNormal4()
        {
            // Arrage
            var vector = new Vector2(0, 3);
            var normalvector = new Vector2(-1, -1);

            // Act
            var parallelVector = Physic.GetParallelVectorToNormal(vector, normalvector);

            // Assert
            var expectedVector = Vector2.Normalize(new Vector2(-1, 1)) * ((vector.Length() * Math.Sqrt(2)) / 2);
            Assert.AreEqual(parallelVector.X, expectedVector.X, 0.0001);
            Assert.AreEqual(parallelVector.Y, expectedVector.Y, 0.0001);
        }

        [TestMethod]
        public void ShouldCalculateParallelVectorToIntersectionNormal1()
        {
            // Arrage
            var movementVector = new Vector2(0, 2);
            var intersectionDistance = 1;
            var intersectionNormal = new Vector2(-1, -1);

            // Act
            var parallelVector = Physic.GetLeftParallelVectorToIntersectionNormal(movementVector, intersectionDistance, intersectionNormal);

            // Assert
            var expectedVector = Vector2.Normalize(new Vector2(-1, 1)) * (((movementVector.Length() - intersectionDistance) * Math.Sqrt(2)) / 2);
            Assert.AreEqual(parallelVector.X, expectedVector.X, 0.0001);
            Assert.AreEqual(parallelVector.Y, expectedVector.Y, 0.0001);
        }


        [TestMethod]
        public void ShouldCalculateParallelVectorToIntersectionNormal2()
        {
            // Arrage
            var movementVector = new Vector2(-3, 3);
            var intersectionDistance = Math.Sqrt(2);
            var intersectionNormal = new Vector2(-1, 0);

            // Act
            var parallelVector = Physic.GetLeftParallelVectorToIntersectionNormal(movementVector, intersectionDistance, intersectionNormal);

            // Assert
            var expectedVector = Vector2.Normalize(new Vector2(0, 1)) * (((movementVector.Length() - intersectionDistance) * Math.Sqrt(2)) / 2);
            Assert.AreEqual(parallelVector.X, expectedVector.X, 0.0001);
            Assert.AreEqual(parallelVector.Y, expectedVector.Y, 0.0001);
        }

        [TestMethod]
        public void ShouldCalculateVectorFromAngle()
        {
            // Arrange
            var angle = -Math.PI;

            // Act
            var vector = new Vector2(angle).Normalize();

            // Assert
            var expectedVector = new Vector2(-1, 0);
            Assert.AreEqual(expectedVector.X, vector.X, 0.0001);
            Assert.AreEqual(expectedVector.Y, vector.Y, 0.0001);
        }
    }
}
