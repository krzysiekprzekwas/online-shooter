using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Vector2 : IEquatable<object>, ICloneable
    {
        /*
         * Vector logic
         */

        private double _x;
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _lengthSquared = null;
                _length = null;

                _x = value;
            }
        }

        private double _y;
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _lengthSquared = null;
                _length = null;

                _y = value;
            }
        }

        public Vector2(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        public Vector2(Vector2 vector)
            : this(vector.X, vector.Y)
        {
            _length = vector._length;
            _lengthSquared = vector._lengthSquared;
        }
        
        /*
         * Vector operations
         */
        private double? _lengthSquared = null;
        public double LengthSquared()
        {
            if (!_lengthSquared.HasValue)
                _lengthSquared = Math.Pow(X, 2) + Math.Pow(Y, 2);

            return _lengthSquared.Value;
        }

        private double? _length = null;
        public double Length()
        {
            if (!_length.HasValue)
                _length = Math.Sqrt(this.LengthSquared());

            return _length.Value;
        }

        public Vector2 Normalize()
        {
            if (IsDegenerated())
                throw new Exception("Degenerated vector given. Cannot normalzie zero vector.");

            var v = new Vector2(this);
            v /= Length();
            return v;
        }

        public Vector2 SafeNormalize()
        {
            try
            {
                return Normalize();
            }
            catch
            {
                return new Vector2();
            }
        }

        public bool IsDegenerated()
        {
            return LengthSquared() == 0;
        }

        /*
         * Static vector operations
         */
        public static double Dot(Vector2 a, Vector2 b)
        {
            return (a.X * b.X) + (a.Y * b.Y);
        }

        public static double DistanceSquared(Vector2 a, Vector2 b)
        {
            return Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2);
        }

        public static double Distance(Vector2 a, Vector2 b)
        {
            return Math.Sqrt(DistanceSquared(a, b));
        }

        public static Vector2 Normalize(Vector2 v)
        {
            return v.Normalize();
        }

        public static Vector2 RadianToVector2(double radian)
        {
            return new Vector2(Math.Cos(radian), Math.Sin(radian));
        }

        public static double AngleBetweenVectors(Vector2 a, Vector2 b)
        {
            if (a.IsDegenerated() || b.IsDegenerated())
                throw new ArgumentException("One of given vectors is degenerated. Cannot calculate angle between them");

            var sin = b.X * a.Y - a.X * b.Y;
            var cos = b.X * a.X + b.Y * a.Y;

            return Math.Atan2(sin, cos);
        }

        /*
         * Mathematical operations
         */
        // Adding vectors
        public static Vector2 Add(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return Add(a, b);
        }

        // Subtracting vectors
        public static Vector2 Subtract(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return Subtract(a, b);
        }

        // Multiplying vectors
        public static Vector2 Multiply(Vector2 a, double b)
        {
            return new Vector2(a.X * b, a.Y * b);
        }
        public static Vector2 operator *(Vector2 a, double b)
        {
            return Multiply(a, b);
        }

        // Dividing vectors
        public static Vector2 Divide(Vector2 a, double b)
        {
            if (b == 0)
                throw new ArgumentException("Division by 0");

            return Multiply(a, 1.0 / b);
        }
        public static Vector2 operator /(Vector2 a, double b)
        {
            return Divide(a, b);
        }

        // Comparing vectors
        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            var v = obj as Vector2;
            if (v == null)
                return false;

            return (X == v.X && Y == v.Y);
        }

        public override string ToString()
        {
            return $"<{X} {Y}>";
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }
        
        public object Clone()
        {
            return new Vector2(this);
        }

        /*
         * Predefined direction vectors
         */
        public static readonly Vector2 UP_VECTOR = new Vector2(0, -1);
        public static readonly Vector2 DOWN_VECTOR = new Vector2(0, 1);
        public static readonly Vector2 LEFT_VECTOR = new Vector2(-1, 0);
        public static readonly Vector2 RIGHT_VECTOR = new Vector2(1, 0);
        public static readonly Vector2 ZERO_VECTOR = new Vector2(0, 0);
    }
}
