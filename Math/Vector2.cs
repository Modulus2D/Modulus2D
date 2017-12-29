using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Math
{
    public struct Vector2 : IEquatable<Vector2>
    {
        public float X;
        public float Y;

        /// <summary>
        /// Origin
        /// </summary>
        public static Vector2 Zero = new Vector2(0f, 0f);

        /// <summary>
        /// One unit in each direction
        /// </summary>
        public static Vector2 One = new Vector2(1f, 1f);

        /// <summary>
        /// Unit vector pointing in the positive X direction
        /// </summary>
        public static Vector2 Right = new Vector2(1f, 0f);

        /// <summary>
        /// Unit vector pointing in the negative X direction
        /// </summary>
        public static Vector2 Left = new Vector2(-1f, 0f);

        /// <summary>
        /// Unit vector pointing in the positive Y direction
        /// </summary>
        public static Vector2 Up = new Vector2(0f, 1f);

        /// <summary>
        /// Unit vector pointing in the negative Y direction
        /// </summary>
        public static Vector2 Down = new Vector2(0f, -1f);

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        // Normalize
        public Vector2 Normalize()
        {
            float length = Length();
            return new Vector2(X / length, Y / length);
        }

        // Normalize (static)
        public static Vector2 Normalize(Vector2 v)
        {
            return v.Normalize();
        }

        // Negate
        public Vector2 Negate()
        {
            return new Vector2(-X, -Y);
        }

        // Negate (static)
        public static Vector2 Negate(Vector2 vec)
        {
            return new Vector2(-vec.X, -vec.Y);
        }

        // Length
        public float Length()
        {
            return (float)System.Math.Sqrt(LengthSquared());
        }

        // Length squared
        public float LengthSquared()
        {
            return X * X + Y * Y;
        }

        // Length (static)
        public static float Length(Vector2 v)
        {
            return v.Length();
        }

        // Length squared (static)
        public static float LengthSquared(Vector2 v)
        {
            return v.LengthSquared();
        }

        public static float Distance(Vector2 vec1, Vector2 vec2)
        {
            return (vec1 - vec2).Length();
        }

        public static float DistanceSquared(Vector2 vec1, Vector2 vec2)
        {
            return (vec1 - vec2).LengthSquared();
        }

        public static Vector2 Max(Vector2 vec1, Vector2 vec2)
        {
            return new Vector2(System.Math.Max(vec1.X, vec2.X), System.Math.Max(vec1.Y, vec2.Y));
        }

        public static Vector2 Min(Vector2 vec1, Vector2 vec2)
        {
            return new Vector2(System.Math.Min(vec1.X, vec2.X), System.Math.Min(vec1.Y, vec2.Y));
        }

        // Vector and vector
        public Vector2 Add(Vector2 other)
        {
            return new Vector2(X + other.X, Y + other.Y);
        }

        public Vector2 Sub(Vector2 other)
        {
            return new Vector2(X - other.X, Y - other.Y);
        }

        public Vector2 Mul(Vector2 other)
        {
            return new Vector2(X * other.X, Y * other.Y);
        }

        public Vector2 Div(Vector2 other)
        {
            return new Vector2(X / other.X, Y / other.Y);
        }

        // Vector and vector (static)
        public static Vector2 Add(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 Sub(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 Mul(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vector2 Div(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X / v2.X, v1.Y / v2.Y);
        }

        // Vector and scalar
        public Vector2 Add(float other)
        {
            return new Vector2(X + other, Y + other);
        }

        public Vector2 Sub(float other)
        {
            return new Vector2(X - other, Y - other);
        }

        public Vector2 Mul(float other)
        {
            return new Vector2(X * other, Y * other);
        }

        public Vector2 Div(float other)
        {
            return new Vector2(X / other, Y / other);
        }

        // Vector and scalar (static)
        public static Vector2 Add(Vector2 vec, float other)
        {
            return new Vector2(vec.X + other, vec.Y + other);
        }

        public static Vector2 Sub(Vector2 vec, float other)
        {
            return new Vector2(vec.X - other, vec.Y - other);
        }

        public static Vector2 Mul(Vector2 vec, float other)
        {
            return new Vector2(vec.X * other, vec.Y * other);
        }

        public static Vector2 Div(Vector2 vec, float other)
        {
            return new Vector2(vec.X / other, vec.Y / other);
        }

        // Dot product
        public float Dot(Vector2 other)
        {
            return X * other.X + Y * other.Y;
        }

        // Dot product (static)
        public static float Dot(Vector2 vec1, Vector2 vec2)
        {
            return vec1.Dot(vec2);
        }

        // Equals
        public bool Equals(Vector2 other)
        {
            return other.X == X && other.Y == Y;
        }

        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y;
        }

        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y;
        }

        // Vector and vector
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return Add(v1, v2);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return Sub(v1, v2);
        }

        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return Mul(v1, v2);
        }

        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            return Div(v1, v2);
        }

        // Vector and scalar
        public static Vector2 operator +(Vector2 v1, float f)
        {
            return Add(v1, f);
        }

        public static Vector2 operator -(Vector2 v1, float f)
        {
            return Sub(v1, f);
        }

        public static Vector2 operator *(Vector2 v1, float f)
        {
            return Mul(v1, f);
        }

        public static Vector2 operator /(Vector2 v1, float f)
        {
            return Div(v1, f);
        }

        // Flipped
        public static Vector2 operator +(float f, Vector2 v1)
        {
            return Add(v1, f);
        }

        public static Vector2 operator -(float f, Vector2 v1)
        {
            return Sub(v1, f);
        }

        public static Vector2 operator *(float f, Vector2 v1)
        {
            return Mul(v1, f);
        }

        public static Vector2 operator /(float f, Vector2 v1)
        {
            return Div(v1, f);
        }

        // Negate
        public static Vector2 operator -(Vector2 v1)
        {
            return v1.Negate();
        }
    }
}
