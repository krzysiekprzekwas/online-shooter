using GameServer.Physics;
using GameServer.MapObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using GameServer.Models;

namespace GameTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CubeSphereIntersection1()
        {
            // arrange  
            MapEllipse o1 = new MapEllipse(0, 0, 0, 2);
            MapRect o2 = new MapRect(0, 0, 20, 1, 1, 1);

            // act  
            bool intersects = Intersection.CheckIntersection(o2, o1);

            // assert  
            Assert.IsFalse(intersects, "Intersection error sphere and box");
        }

        [TestMethod]
        public void CubeSphereIntersection2()
        {
            // arrange  
            MapEllipse o1 = new MapEllipse(0, 0, 0, 2);
            MapRect o2 = new MapRect(0, 0, 1, 1, 1, 1);

            // act  
            bool intersects = Intersection.CheckIntersection(o2, o1);

            // assert  
            Assert.IsTrue(intersects, "Intersection error sphere and box");
        }

        [TestMethod]
        public void CubeSphereIntersection3()
        {
            // arrange  
            MapEllipse o1 = new MapEllipse(0, 0, 0, 2);
            MapRect o2 = new MapRect(0, 2, 2, 1, 1, 1);

            // act  
            bool intersects = Intersection.CheckIntersection(o2, o1);

            // assert  
            Assert.IsFalse(intersects, "Intersection error sphere and box");
        }


        [TestMethod]
        public void SphereSphereIntersection1()
        {
            // arrange  
            MapEllipse o1 = new MapEllipse(0, 0, 0, 2);
            MapEllipse o2 = new MapEllipse(0, 0, 1, 0.1f);

            // act  
            bool intersects = Intersection.CheckIntersection(o2, o1);

            // assert  
            Assert.IsTrue(intersects, "Intersection error sphere and box");
        }


        [TestMethod]
        public void SphereSphereIntersection2()
        {
            // arrange  
            MapEllipse o1 = new MapEllipse(0, 0, 0, 2);
            MapEllipse o2 = new MapEllipse(0, 0, 2, 2);

            // act  
            bool intersects = Intersection.CheckIntersection(o2, o1);

            // assert  
            Assert.IsFalse(intersects, "Intersection error sphere and box");
        }


        [TestMethod]
        public void SphereSphereIntersection3()
        {
            // arrange  
            MapEllipse o1 = new MapEllipse(0, 0, 0, (float)Math.Sqrt(2) * 2f - 0.01f);
            MapEllipse o2 = new MapEllipse(2, 0, 2, (float)Math.Sqrt(2) * 2f);

            // act  
            bool intersects = Intersection.CheckIntersection(o2, o1);

            // assert  
            Assert.IsFalse(intersects, "Intersection error sphere and box");
        }


        [TestMethod]
        public void PointBoxIntersection1()
        {
            // arrange  
            MapRect box = new MapRect(0, 0, 0, 2, 2, 2);
            Vector2 pos = new Vector2(1, 1, 1);

            // act  
            bool intersects = Intersection.CheckIntersection(box, pos);

            // assert  
            Assert.IsTrue(intersects, "Intersection error position and box");
        }

        [TestMethod]
        public void PointBoxIntersection2()
        {
            // arrange  
            MapRect box = new MapRect(0, 0, 0, 2, 2, 2);
            Vector2 pos = new Vector2(2, 0, 0);

            // act  
            bool intersects = Intersection.CheckIntersection(box, pos);

            // assert  
            Assert.IsFalse(intersects, "Intersection error position and box");
        }

        [TestMethod]
        public void BoxRayIntersection1()
        {
            // arrange  
            MapRect box = new MapRect(10, 0, 0, 1, 1, 1);
            Ray ray = new Ray(0, 0, 0, 1, 0, 0);

            // act  
            Trace trace = RayCast.CheckBulletTrace(box, ray);

            // assert  
            Assert.IsNotNull(trace, "Intersection error ray and box");
        }

        [TestMethod]
        public void BoxRayIntersection2()
        {
            // arrange  
            MapRect box = new MapRect(0, 0, 1, 2, 2, 2);
            Ray ray = new Ray(0, 0, -1, 0, 0, 1);

            // act  
            Trace trace = RayCast.CheckBulletTrace(box, ray);

            // assert  
            Assert.IsNotNull(trace, "Intersection error ray and box");
        }

        [TestMethod]
        public void BoxRayIntersection3()
        {
            // arrange  
            MapRect box = new MapRect(0, 3, 1, 2, 2, 2);
            Ray ray = new Ray(0, 0, -1, 0, 0, 1);

            // act 
            Trace trace = RayCast.CheckBulletTrace(box, ray);

            // assert  
            Assert.IsNull(trace, "Intersection error ray and box");
        }

        [TestMethod]
        public void BoxRayIntersection4()
        {
            // arrange  
            MapRect box = new MapRect(2, 0, 2, 2, 2, 2);
            Ray ray = new Ray(-2, 0.5f, 1, 2, 0, 1);

            // act 
            Trace trace = RayCast.CheckBulletTrace(box, ray);

            // assert  
            Assert.IsNotNull(trace, "Intersection error ray and box");
        }

        [TestMethod]
        public void BoxRayIntersection5()
        {
            // arrange  
            MapRect box = new MapRect(2, 0, 2, 2, 2, 2);
            Ray ray = new Ray(5, 0.75f, 3, -2, -0.3f, -2);
            // act 
            Trace trace = RayCast.CheckBulletTrace(box, ray);

            // assert  
            Assert.IsNotNull(trace, "Intersection error ray and box");
        }

        [TestMethod]
        public void BoxRayIntersection6()
        {
            // arrange  
            MapRect box = new MapRect(2, 0, 2, 2, 2, 2);
            Ray ray = new Ray(5, 0.3f, 3, -2f, 0, -2.1f);
            // act 
            Trace trace = RayCast.CheckBulletTrace(box, ray);

            // assert  
            Assert.IsNull(trace, "Intersection error ray and box");
        }

        [TestMethod]
        public void BoxRayIntersection7()
        {
            // arrange  
            MapRect box = new MapRect(0, 0, 0, 2, 2, 2);
            Ray ray = new Ray(1, 0, 2, 0, 0, 1);

            // act 
            Trace trace = RayCast.CheckBulletTrace(box, ray);

            // assert  
            Assert.IsNull(trace);
        }

        [TestMethod]
        public void BoxRayIntersection8()
        {
            // arrange  
            MapRect box = new MapRect(0, 0, 0, 1200, 1, 1);
            Ray ray = new Ray(-500, 0, -1, 0, 0, 1);

            // act 
            Trace trace = RayCast.CheckBulletTrace(box, ray);

            // assert  
            Vector2 expectedPosition = new Vector2(-500, 0, -0.5f);
            Assert.IsNotNull(trace);
            Assert.AreEqual(trace.Position, expectedPosition);
        }

        [TestMethod]
        public void TriangleRayIntersection1()
        {
            // arrange  
            Vector2 T0 = new Vector2(-2.0f, -2.0f, 3.0f);
            Vector2 T1 = new Vector2(2.0f, -2.0f, 3.0f);
            Vector2 T2 = new Vector2(-2.0f, 6.0f, 3.0f);

            MapTriangle triangle = new MapTriangle(T0, T1, T2);

            Ray ray = new Ray(0, 0, -1.0f, 0, 0, 1.0f);

            // act 
            Trace trace = RayCast.CheckBulletTrace(triangle, ray);

            // assert  
            Assert.IsNotNull(trace, "Intersection error ray and triangle");
        }

        [TestMethod]
        public void TriangleRayIntersection2()
        {
            // arrange  
            Vector2 T0 = new Vector2(-2.0f, -2.0f, 3.0f);
            Vector2 T1 = new Vector2(2.0f, -2.0f, 3.0f);
            Vector2 T2 = new Vector2(-2.0f, 6.0f, 3.0f);

            MapTriangle triangle = new MapTriangle(T0, T1, T2);

            Ray ray = new Ray(2, 0, -1.0f, 0, 0, 1.0f);

            // act 
            Trace trace = RayCast.CheckBulletTrace(triangle, ray);

            // assert  
            Assert.IsNull(trace, "Intersection error ray and triangle");
        }

        [TestMethod]
        public void QuadRayIntersection1()
        {
            // arrange  
            Vector2 Q0 = new Vector2(1, -2, 4);
            Vector2 Q1 = new Vector2(2, -2, 3);
            Vector2 Q2 = new Vector2(2, 2, 3);
            Vector2 Q3 = new Vector2(1, 2, 4);

            MapQuad quad = new MapQuad(Q0, Q1, Q2, Q3);

            Ray ray = new Ray(-2, 0, 0, 1, 0, 1);

            // act 
            Trace trace = RayCast.CheckBulletTrace(quad, ray);

            // assert  
            Assert.IsNotNull(trace, "Intersection error ray and quad");
        }
        [TestMethod]
        public void QuadRayIntersection2()
        {
            // arrange  
            Vector2 Q0 = new Vector2(1, -2, 4);
            Vector2 Q1 = new Vector2(2, -2, 3);
            Vector2 Q2 = new Vector2(2, 2, 3);
            Vector2 Q3 = new Vector2(1, 2, 4);

            MapQuad quad = new MapQuad(Q0, Q1, Q2, Q3);

            Ray ray = new Ray(3, 0, 0, -1, 0, 2);

            // act 
            Trace trace = RayCast.CheckBulletTrace(quad, ray);

            // assert  
            Assert.IsNotNull(trace, "Intersection error ray and quad");
        }
        [TestMethod]
        public void QuadRayIntersection3()
        {
            // arrange  
            Vector2 Q0 = new Vector2(1, -2, 4);
            Vector2 Q1 = new Vector2(2, -2, 3);
            Vector2 Q2 = new Vector2(2, 2, 3);
            Vector2 Q3 = new Vector2(1, 2, 4);

            MapQuad quad = new MapQuad(Q0, Q3, Q2, Q1);

            Ray ray = new Ray(-2, 0, 0, 1, 0, 1);

            // act 
            Trace trace = RayCast.CheckBulletTrace(quad, ray);

            // assert  
            Assert.IsNotNull(trace, "Intersection error ray and quad");
        }

        [TestMethod]
        public void QuadRayIntersection4()
        {
            // arrange  
            Vector2 Q0 = new Vector2(-2, -2, 2);
            Vector2 Q1 = new Vector2(-2, 2, 2);
            Vector2 Q2 = new Vector2(2, 2, 2);
            Vector2 Q3 = new Vector2(2, -2, 2);

            MapQuad quad = new MapQuad(Q0, Q1, Q2, Q3);

            Ray ray = new Ray(2, 0, 0, 0, 0, 1);

            // act 
            Trace trace = RayCast.CheckBulletTrace(quad, ray);

            // assert  
            Assert.IsNotNull(trace, "Intersection error ray and quad");
        }

        [TestMethod]
        public void QuadRayIntersection5()
        {
            // arrange  
            Vector2 Q0 = new Vector2(-3, -2, 3);
            Vector2 Q1 = new Vector2(-3, 2, 3);
            Vector2 Q2 = new Vector2(3, 2, -3);
            Vector2 Q3 = new Vector2(3, -2, -3);

            MapQuad quad = new MapQuad(Q0, Q1, Q2, Q3);

            Ray ray = new Ray(3, 0, 3, -1, -0.1f, -3);

            // act 
            Trace trace = RayCast.CheckBulletTrace(quad, ray);

            // assert  
            Assert.IsNotNull(trace, "Intersection error ray and quad");
        }

        [TestMethod]
        public void FindClosestPointOnLine1()
        {
            // arrange  
            Vector2 A = new Vector2(-1, -1, -1);
            Vector2 B = new Vector2(1, -1, -1);

            Vector2 P = new Vector2(0, 0, 0);

            // act 
            Vector2 Q = RayCast.GetClosestPointOnLine(A, B, P);

            // assert  
            Vector2 expectedQ = new Vector2(0, -1, -1);
            Assert.AreEqual(expectedQ, Q);
        }

        [TestMethod]
        public void FindClosestPointOnLine2()
        {
            // arrange  
            Vector2 A = new Vector2(-1, 0, -1);
            Vector2 B = new Vector2(1, 0, -1);

            Vector2 P = new Vector2(0, 0, 0);

            // act 
            Vector2 Q = RayCast.GetClosestPointOnLine(A, B, P);

            // assert  
            Vector2 expectedQ = new Vector2(0, 0, -1);
            Assert.AreEqual(expectedQ, Q);
        }

        [TestMethod]
        public void FindClosestPointOnLine3()
        {
            // arrange  
            Vector2 A = new Vector2(-4, -1, 0);
            Vector2 B = new Vector2(-2, 0, 0);

            Vector2 P = new Vector2(3, 0, 0);

            // act 
            Vector2 Q = RayCast.GetClosestPointOnLine(A, B, P);

            // assert  
            Vector2 expectedQ = new Vector2(2, 2, 0);
            Assert.AreEqual(expectedQ, Q);
        }

        [TestMethod]
        public void FindClosestPointOnLine4()
        {
            // arrange  
            Vector2 A = new Vector2(6, 1, 0);
            Vector2 B = new Vector2(5, 2, 0);

            Vector2 P = new Vector2(3, 3, 0);

            // act 
            Vector2 Q = RayCast.GetClosestPointOnLine(A, B, P);

            // assert  
            Vector2 expectedQ = new Vector2(3.5f, 3.5f, 0);
            Assert.AreEqual(expectedQ, Q);
        }

        [TestMethod]
        public void SphereRayIntersection1()
        {
            // arrange  
            MapEllipse sphere = new MapEllipse(0, 0, 0, 2);
            Ray ray = new Ray(-2, 0, 0, 1, 0, 0);

            // act 
            Trace trace = RayCast.CheckBulletTrace(sphere, ray);

            // assert  
            Assert.IsNotNull(trace);

            Vector2 expectedPos = new Vector2(-1, 0, 0);
            Assert.AreEqual(trace.Position, expectedPos);
        }

        [TestMethod]
        public void SphereRayIntersection2()
        {
            // arrange  
            MapEllipse sphere = new MapEllipse(0, 1, 0, 2);
            Ray ray = new Ray(1, -1, 0, 0, 1, 0);

            // act 
            Trace trace = RayCast.CheckBulletTrace(sphere, ray);

            // assert  
            Assert.IsNotNull(trace);

            Vector2 expectedPos = new Vector2(1, 1, 0);
            Assert.AreEqual(trace.Position, expectedPos);
        }

        [TestMethod]
        public void SphereRayIntersection3()
        {
            // arrange  
            MapEllipse sphere = new MapEllipse(0, 0, 0, 2);
            Ray ray = new Ray(1.5f, 0, 0, 1, 0, 0);

            // act 
            Trace trace = RayCast.CheckBulletTrace(sphere, ray);

            // assert  
            Assert.IsNull(trace);
        }

        [TestMethod]
        public void SphereRayIntersection4()
        {
            // arrange  
            MapEllipse sphere = new MapEllipse(3, 3, 0, 2);
            Ray ray = new Ray(6, 1, 0, -1, 1, 0);

            // act 
            Trace trace = RayCast.CheckBulletTrace(sphere, ray);

            // assert  
            Assert.IsNotNull(trace);

            const float TOLERANCE = 0.001f;

            Vector2 expectedPos = new Vector2(4, 3, 0);

            bool areAlmostEqual = Math.Abs(trace.Position.X - expectedPos.X) < TOLERANCE &&
                                    Math.Abs(trace.Position.Y - expectedPos.Y) < TOLERANCE &&
                                    Math.Abs(trace.Position.Z - expectedPos.Z) < TOLERANCE;

            Assert.IsTrue(areAlmostEqual);
        }

        [TestMethod]
        public void SphereRayIntersectionFromInside()
        {
            // arrange  
            MapEllipse sphere = new MapEllipse(2, 2, 0, 2);
            Ray ray = new Ray(2, 2, 0, -1, 1, 0);

            // act 
            Trace trace = RayCast.CheckBulletTrace(sphere, ray);

            // assert  
            Assert.IsNull(trace);
        }

        [TestMethod]
        public void GetVectorParralelProjectionToObjectNormal1()
        {
            // arrange  
            Vector2 speedVector = new Vector2(3, 0, 4);
            Vector2 objectNorm = new Vector2(0, 0, -1);

            // act 
            Vector2 u = PhysicsEngine.GetVectorParralelProjectionToObjectNormal(speedVector, objectNorm);

            // assert
            Vector2 expectedU = new Vector2(3, 0, 0);
            Assert.AreEqual(u, expectedU);
        }

        [TestMethod]
        public void GetVectorParralelProjectionToObjectNormal2()
        {
            // arrange  
            Vector2 speedVector = new Vector2(4, 0, 1);
            Vector2 objectNorm = new Vector2(-1, 0, -1);

            // act 
            Vector2 u = PhysicsEngine.GetVectorParralelProjectionToObjectNormal(speedVector, objectNorm);

            // assert
            Vector2 expectedU = new Vector2(1.5f, 0, -1.5f);
            Assert.AreEqual(u, expectedU);
        }

        [TestMethod]
        public void GetVectorParralelProjectionToObjectNormal3()
        {
            // arrange  
            Vector2 speedVector = new Vector2(-1, 0, 0);
            Vector2 objectNorm = new Vector2(-1, 0, 0);

            // act 
            Vector2 u = PhysicsEngine.GetVectorParralelProjectionToObjectNormal(speedVector, objectNorm);

            // assert
            Vector2 expectedU = new Vector2(0, 0, 0);
            Assert.AreEqual(u, expectedU);
        }

        [TestMethod]
        public void GetVectorParralelProjectionToObjectNormal4()
        {
            // arrange  
            Vector2 speedVector = new Vector2(-1, 0, -4);
            Vector2 objectNorm = new Vector2(1, 0, 1);

            // act 
            Vector2 u = PhysicsEngine.GetVectorParralelProjectionToObjectNormal(speedVector, objectNorm);

            // assert
            Vector2 expectedU = new Vector2(1.5f, 0, -1.5f);
            Assert.AreEqual(u, expectedU);
        }

        [TestMethod]
        public void GetTracePosition1()
        {
            // arrange  
            MapRect box = new MapRect(0, 0, 0, 2, 2, 2);
            Ray ray = new Ray(-1, 0, 4, 1, 0, -3);

            // act 
            Trace trace = RayCast.CheckBulletTrace(box, ray);

            // assert  
            Assert.IsNotNull(trace);

            const float TOLERANCE = 0.001f;

            Vector2 expectedPos = new Vector2(0, 0, 1);

            bool areAlmostEqual = Math.Abs(trace.Position.X - expectedPos.X) < TOLERANCE &&
                                    Math.Abs(trace.Position.Y - expectedPos.Y) < TOLERANCE &&
                                    Math.Abs(trace.Position.Z - expectedPos.Z) < TOLERANCE;

            Assert.IsTrue(areAlmostEqual);
        }

        [TestMethod]
        public void GetTracePosition2()
        {
            // arrange  
            MapRect box = new MapRect(0, 0, 0, 2, 2, 2);
            Ray ray = new Ray(-1, 0, 2, 0.5f, 0, -2);

            // act 
            Trace trace = RayCast.CheckBulletTrace(box, ray);

            // assert  
            Assert.IsNotNull(trace);

            Vector2 expectedPos = new Vector2(-0.75f, 0, 1);
            Assert.AreEqual(trace.Position, expectedPos);
        }

        [TestMethod]
        public void GetTraceDistance1()
        {
            // arrange  
            MapRect box = new MapRect(0, 0, 0, 2, 2, 2);
            Ray ray = new Ray(-5, -1, -3, 4, 0, 3);

            // act 
            Trace trace = RayCast.CheckBulletTrace(box, ray);

            // assert  
            Assert.AreEqual(trace.Distance, 5f);
        }
        
        [TestMethod]
        public void GetTraceDistance2()
        {
            // arrange  
            MapRect box = new MapRect(0, 0, 0, 2, 2, 2);
            Ray ray = new Ray(-6, -1, -12, 5, 0, 12);

            // act 
            Trace trace = RayCast.CheckBulletTrace(box, ray);

            // assert  
            Assert.AreEqual(trace.Distance, 13f);
        }

        [TestMethod]
        public void GetTraceSphereNormal()
        {
            // arrange  
            MapEllipse sphere = new MapEllipse(0, 0, 1, 2);
            Ray ray = new Ray(-1, -1, 0, 1, 1, 1);

            // act 
            Trace trace = RayCast.CheckBulletTrace(sphere, ray);

            // assert  
            Vector2 expectedNormal = Vector2.Normalize(new Vector2(-1, -1, -1));
            Assert.AreEqual(trace.ObjectNormal, expectedNormal);
        }

        [TestMethod]
        public void GetTraceBoxNormal()
        {
            // arrange  
            MapRect box = new MapRect(0, 0, 0, 2, 2, 2);
            Ray ray = new Ray(-2, -0.5f, 2, 1, 0.5f, -2);

            // act 
            Trace trace = RayCast.CheckBulletTrace(box, ray);

            // assert  
            Vector2 expectedNormal = Vector2.Normalize(new Vector2(-1, 0, 0));
            Assert.AreEqual(trace.ObjectNormal, expectedNormal);
        }

        [TestMethod]
        public void RotateVectorAroundYAxis1()
        {
            // arrange  
            Vector2 vector = new Vector2(1, 1, 1);
            float radians = (float)Math.PI / 2f;

            // act 
            Vector2 rotatedVector = PhysicsEngine.RotateVectorAroundYAxis(vector, radians);
            
            // assert  
            Vector2 expectedVector = new Vector2(-1, 1, 1);

            const float TOLERANCE = 0.001f;
            bool areAlmostEqual = Math.Abs(rotatedVector.X - expectedVector.X) < TOLERANCE &&
                                    Math.Abs(rotatedVector.Y - expectedVector.Y) < TOLERANCE &&
                                    Math.Abs(rotatedVector.Z - expectedVector.Z) < TOLERANCE;
            
            Assert.IsTrue(areAlmostEqual);
        }

    }
}
