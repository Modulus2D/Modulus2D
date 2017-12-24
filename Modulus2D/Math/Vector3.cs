using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Math
{
    public struct Vector3 : IEquatable<Vector3>
    {
        public float X;
        public float Y;
        public float Z;

        /// <summary>
        /// Origin
        /// </summary>
        public static Vector3 Zero = new Vector3(0f, 0f, 0f);

        /// <summary>
        /// One unit in each direction
        /// </summary>
        public static Vector3 One = new Vector3(1f, 1f, 0f);

        /// <summary>
        /// Unit vector pointing in the positive X direction
        /// </summary>
        public static Vector3 Right = new Vector3(1f, 0f, 0f);

        /// <summary>
        /// Unit vector pointing in the negative X direction
        /// </summary>
        public static Vector3 Left = new Vector3(-1f, 0f, 0f);

        /// <summary>
        /// Unit vector pointing in the positive Y direction
        /// </summary>
        public static Vector3 Up = new Vector3(0f, 1f, 0f);

        /// <summary>
        /// Unit vector pointing in the negative Y direction
        /// </summary>
        public static Vector3 Down = new Vector3(0f, -1f, 0f);

        /// <summary>
        /// Unit vector pointing in the positive Z direction
        /// </summary>
        public static Vector3 Forward = new Vector3(0f, 0f, 1f);

        /// <summary>
        /// Unit vector pointing in the negative Z direction
        /// </summary>
        public static Vector3 Backward = new Vector3(0f, 0f, -1f);

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // Normalize
        public Vector3 Normalize()
        {
            float length = Length();
            return new Vector3(X / length, Y / length, Z / length);
        }

        // Normalize (static)
        public static Vector3 Normalize(Vector3 v)
        {
            return v.Normalize();
        }

        // Negate
        public Vector3 Negate()
        {
            return new Vector3(-X, -Y, -Z);
        }

        // Negate (static)
        public static Vector3 Negate(Vector3 vec)
        {
            return new Vector3(-vec.X, -vec.Y, -vec.Z);
        }

        // Length
        public float Length()
        {
            return (float)System.Math.Sqrt(LengthSquared());
        }

        // Length squared
        public float LengthSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        // Length (static)
        public static float Length(Vector3 v)
        {
            return v.Length();
        }

        // Length squared (static)
        public static float LengthSquared(Vector3 v)
        {
            return v.LengthSquared();
        }

        public static float Distance(Vector3 vec1, Vector3 vec2)
        {
            return (vec1 - vec2).Length();
        }

        public static float DistanceSquared(Vector3 vec1, Vector3 vec2)
        {
            return (vec1 - vec2).LengthSquared();
        }

        public static Vector3 Max(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3(System.Math.Max(vec1.X, vec2.X), System.Math.Max(vec1.Y, vec2.Y), System.Math.Max(vec1.Z, vec2.Z));
        }

        public static Vector3 Min(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3(System.Math.Min(vec1.X, vec2.X), System.Math.Min(vec1.Y, vec2.Y), System.Math.Min(vec1.Z, vec2.Z));
        }

        // Vector and vector
        public Vector3 Add(Vector3 other)
        {
            return new Vector3(X + other.X, Y + other.Y, Z + other.Z);
        }

        public Vector3 Sub(Vector3 other)
        {
            return new Vector3(X - other.X, Y - other.Y, Z - other.Z);
        }

        public Vector3 Mul(Vector3 other)
        {
            return new Vector3(X * other.X, Y * other.Y, Z * other.Z);
        }

        public Vector3 Div(Vector3 other)
        {
            return new Vector3(X / other.X, Y / other.Y, Z / other.Z);
        }

        // Vector and vector (static)
        public static Vector3 Add(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public Vector3 Sub(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public Vector3 Mul(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }

        public Vector3 Div(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z);
        }

        // Vector and scalar
        public Vector3 Add(float other)
        {
            return new Vector3(X + other, Y + other, Z + other);
        }

        public Vector3 Sub(float other)
        {
            return new Vector3(X - other, Y - other, Z - other);
        }

        public Vector3 Mul(float other)
        {
            return new Vector3(X * other, Y * other, Z * other);
        }

        public Vector3 Div(float other)
        {
            return new Vector3(X / other, Y / other, Z / other);
        }

        // Vector and scalar (static)
        public static Vector3 Add(Vector3 vec, float other)
        {
            return new Vector3(vec.X + other, vec.Y + other, vec.Z + other);
        }

        public static Vector3 Sub(Vector3 vec, float other)
        {
            return new Vector3(vec.X - other, vec.Y - other, vec.Z - other);
        }

        public static Vector3 Mul(Vector3 vec, float other)
        {
            return new Vector3(vec.X * other, vec.Y * other, vec.Z * other);
        }

        public static Vector3 Div(Vector3 vec, float other)
        {
            return new Vector3(vec.X / other, vec.Y / other, vec.Z / other);
        }

        // Dot product
        public float Dot(Vector3 other)
        {
            return X * other.X + Y * other.Y + Z * other.Z;
        }

        // Dot product (static)
        public static float Dot(Vector3 vec1, Vector3 vec2)
        {
            return vec1.Dot(vec2);
        }

        // Cross product
        public Vector3 Cross(Vector3 other)
        {
            return new Vector3(Y * other.Z - Z * other.Y, Z * other.X - X * other.Z, X * other.Y - Y * other.X);
        }

        // Cross product (static)
        public static Vector3 Cross(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3(vec1.Y * vec2.Z - vec1.Z * vec2.Y, vec1.Z * vec2.X - vec1.X * vec2.Z, vec1.X * vec2.Y - vec1.Y * vec2.X);
        }

        // Equals
        public bool Equals(Vector3 other)
        {
            return other.X == X && other.Y == Y && other.Z == Z;
        }

        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z;
        }

        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z;
        }

        // Vector and vector
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return v1.Add(v2);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return v1.Sub(v2);
        }

        // Vector and scalar
        public static Vector3 operator +(Vector3 v1, float f)
        {
            return Add(v1, f);
        }

        public static Vector3 operator -(Vector3 v1, float f)
        {
            return Sub(v1, f);
        }

        public static Vector3 operator *(Vector3 v1, float f)
        {
            return Mul(v1, f);
        }

        public static Vector3 operator /(Vector3 v1, float f)
        {
            return Div(v1, f);
        }

        // Flipped
        public static Vector3 operator +(float f, Vector3 v1)
        {
            return Add(v1, f);
        }

        public static Vector3 operator -(float f, Vector3 v1)
        {
            return Sub(v1, f);
        }

        public static Vector3 operator *(float f, Vector3 v1)
        {
            return Mul(v1, f);
        }

        public static Vector3 operator /(float f, Vector3 v1)
        {
            return Div(v1, f);
        }

        // Negate
        public static Vector3 operator -(Vector3 v1)
        {
            return v1.Negate();
        }
    }
}
