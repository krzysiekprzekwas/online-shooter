using GameServer.Physics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameTests
{
    [TestClass]
    public class PhysicTests
    {
        [TestMethod]
        public void Physic_ShouldCorrectlyRotateVector1()
        {
            // Arrage
            var vector = new Vector2(10, 0);
            var angle = (float)Math.PI / 4;

            // Act
            var rotatedVector = Physic.RotateVector(vector, angle);

            // Assert
            var expectedVector = Vector2.Normalize(new Vector2(1, 1)) * vector.Length();
            Assert.AreEqual(rotatedVector, expectedVector);
        }

        [TestMethod]
        public void Physic_ShouldCorrectlyRotateVectorByNegativeAngle()
        {
            // Arrage
            var vector = new Vector2(0, -3);
            var angle = (float)Math.PI / -2;

            // Act
            var rotatedVector = Physic.RotateVector(vector, angle);

            // Assert
            var expectedVector = new Vector2(-3, 0);
            Assert.AreEqual(rotatedVector.X, expectedVector.X, 0.0001);
            Assert.AreEqual(rotatedVector.Y, expectedVector.Y, 0.0001);
        }

        [TestMethod]
        public void Physic_ShouldCorrectlyProjectVectorOntoAnother1()
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
        public void Physic_ShouldCorrectlyProjectVectorOntoAnother2()
        {
            // Arrage
            var vector = new Vector2(-2, -1);
            var projectionVector = new Vector2(-1, 1);

            // Act
            var projectedVector = Physic.ProjectVector(vector, projectionVector);

            // Assert
            var expectedVector = new Vector2(-0.5f, 0.5f);
            Assert.AreEqual(projectedVector, expectedVector);
        }

        [TestMethod]
        public void Physic_ShouldCorrectlyCalculateParallelVectorToNormal1()
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
        public void Physic_ShouldCorrectlyCalculateParallelVectorToNormal2()
        {
            // Arrage
            var vector = new Vector2(-2, -1);
            var normalvector = new Vector2(1, 1);

            // Act
            var parallelVector = Physic.GetParallelVectorToNormal(vector, normalvector);

            // Assert
            var expectedVector = new Vector2(-0.5f, 0.5f);
            Assert.AreEqual(parallelVector.X, expectedVector.X, 0.0001);
            Assert.AreEqual(parallelVector.Y, expectedVector.Y, 0.0001);
        }

        [TestMethod]
        public void Physic_ShouldCorrectlyCalculateParallelVectorToNormal3()
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
        public void Physic_ShouldCorrectlyCalculateParallelVectorToNormal4()
        {
            // Arrage
            var vector = new Vector2(0, 3);
            var normalvector = new Vector2(-1, -1);

            // Act
            var parallelVector = Physic.GetParallelVectorToNormal(vector, normalvector);

            // Assert
            var expectedVector = Vector2.Normalize(new Vector2(-1, 1)) * ((vector.Length() * (float)Math.Sqrt(2)) / 2);
            Assert.AreEqual(parallelVector.X, expectedVector.X, 0.0001);
            Assert.AreEqual(parallelVector.Y, expectedVector.Y, 0.0001);
        }

        [TestMethod]
        public void Physic_ShouldCorrectlyCalculateAngleBetweenVectors1()
        {
            // Arrage
            var a = new Vector2(1, 1);
            var b = new Vector2(1, -1);

            // Act
            var angle = Physic.GetAngleBetweenVectors(a, b);

            // Assert
            var expectedAngle = Math.PI / 2;
            Assert.AreEqual(angle, expectedAngle, 0.0001);
        }

        [TestMethod]
        public void Physic_ShouldCorrectlyCalculateAngleBetweenVectors2()
        {
            // Arrage
            var a = new Vector2(0, 3);
            var b = new Vector2(-1, 0);

            // Act
            var angle = Physic.GetAngleBetweenVectors(a, b);

            // Assert
            var expectedAngle = Math.PI / -2;
            Assert.AreEqual(angle, expectedAngle, 0.0001);
        }

        [TestMethod]
        public void Physic_ShouldCalculateParallelVectorToIntersectionNormal1()
        {
            // Arrage
            var movementVector = new Vector2(0, 2);
            var intersectionDistance = 1f;
            var intersectionNormal = new Vector2(-1, -1);

            // Act
            var parallelVector = Physic.GetLeftParallelVectorToIntersectionNormal(movementVector, intersectionDistance, intersectionNormal);

            // Assert
            var expectedVector = Vector2.Normalize(new Vector2(-1, 1)) * (((movementVector.Length() - intersectionDistance) * (float)Math.Sqrt(2)) / 2);
            Assert.AreEqual(parallelVector.X, expectedVector.X, 0.0001);
            Assert.AreEqual(parallelVector.Y, expectedVector.Y, 0.0001);
        }


        [TestMethod]
        public void Physic_ShouldCalculateParallelVectorToIntersectionNormal2()
        {
            // Arrage
            var movementVector = new Vector2(-3, 3);
            var intersectionDistance = (float)Math.Sqrt(2);
            var intersectionNormal = new Vector2(-1, 0);

            // Act
            var parallelVector = Physic.GetLeftParallelVectorToIntersectionNormal(movementVector, intersectionDistance, intersectionNormal);

            // Assert
            var expectedVector = Vector2.Normalize(new Vector2(0, 1)) * (((movementVector.Length() - intersectionDistance) * (float)Math.Sqrt(2)) / 2);
            Assert.AreEqual(parallelVector.X, expectedVector.X, 0.0001);
            Assert.AreEqual(parallelVector.Y, expectedVector.Y, 0.0001);
        }

        [TestMethod]
        public void Physic_ShouldCalculateVectorFromAngle()
        {
            // Arrange
            var angle = (float)Math.PI * 3 / 2;

            // Act
            var vector = Physic.AngleToVector(angle);

            // Assert
            var expectedVector = new Vector2(-1, 0);
            Assert.AreEqual(expectedVector.X, vector.X, 0.0001);
            Assert.AreEqual(expectedVector.Y, vector.Y, 0.0001);
        }

        [TestMethod]
        public void Physic_ShouldCalculateAngleFromVector()
        {
            // Arrange
            var vector = new Vector2(-1, 1);

            // Act
            var angle = Physic.VectorToAngle(vector);

            // Assert
            var expectedAngle = (float)Math.PI * -1 / 4;
            Assert.AreEqual(expectedAngle, angle, 0.0001);
        }
    }
}
