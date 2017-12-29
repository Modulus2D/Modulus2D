using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Math
{
    public struct Vector4 : IEquatable<Vector4>
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        /// <summary>
        /// Origin
        /// </summary>
        public static Vector4 Zero = new Vector4(0f, 0f, 0f, 0f);

        /// <summary>
        /// One unit in each direction
        /// </summary>
        public static Vector4 One = new Vector4(1f, 1f, 0f, 0f);

        /// <summary>
        /// Unit vector pointing in the positive X direction
        /// </summary>
        public static Vector4 Right = new Vector4(1f, 0f, 0f, 0f);

        /// <summary>
        /// Unit vector pointing in the negative X direction
        /// </summary>
        public static Vector4 Left = new Vector4(-1f, 0f, 0f, 0f);

        /// <summary>
        /// Unit vector pointing in the positive Y direction
        /// </summary>
        public static Vector4 Up = new Vector4(0f, 1f, 0f, 0f);

        /// <summary>
        /// Unit vector pointing in the negative Y direction
        /// </summary>
        public static Vector4 Down = new Vector4(0f, -1f, 0f, 0f);

        /// <summary>
        /// Unit vector pointing in the positive Z direction
        /// </summary>
        public static Vector4 Forward = new Vector4(0f, 0f, 1f, 0f);

        /// <summary>
        /// Unit vector pointing in the negative Z direction
        /// </summary>
        public static Vector4 Backward = new Vector4(0f, 0f, -1f, 0f);

        /// <summary>
        /// Unit vector pointing in the positive W direction
        /// </summary>
        public static Vector4 Ana = new Vector4(0f, 0f, 0f, 1f);
        
        /// <summary>
        /// Unit vector pointing in the negative W direction
        /// </summary>
        public static Vector4 Kata = new Vector4(0f, 0f, 0f, -1f);

        public Vector4(float x, float y, float z,float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        // Normalize
        public Vector4 Normalize()
        {
            float length = Length();
            return new Vector4(X / length, Y / length, Z / length, W / length);
        }

        // Normalize (static)
        public static Vector4 Normalize(Vector4 v)
        {
            return v.Normalize();
        }

        // Negate
        public Vector4 Negate()
        {
            return new Vector4(-X, -Y, -Z, -W);
        }

        // Negate (static)
        public static Vector4 Negate(Vector4 vec)
        {
            return new Vector4(-vec.X, -vec.Y, -vec.Z, -vec.W);
        }

        // Length
        public float Length()
        {
            return (float)System.Math.Sqrt(LengthSquared());
        }

        // Length squared
        public float LengthSquared()
        {
            return X * X + Y * Y + Z * Z + W * W;
        }

        // Length (static)
        public static float Length(Vector4 v)
        {
            return v.Length();
        }

        // Length squared (static)
        public static float LengthSquared(Vector4 v)
        {
            return v.LengthSquared();
        }

        public static float Distance(Vector4 vec1, Vector4 vec2)
        {
            return (vec1 - vec2).Length();
        }

        public static float DistanceSquared(Vector4 vec1, Vector4 vec2)
        {
            return (vec1 - vec2).LengthSquared();
        }

        public static Vector4 Max(Vector4 vec1, Vector4 vec2)
        {
            return new Vector4(System.Math.Max(vec1.X, vec2.X), System.Math.Max(vec1.Y, vec2.Y), System.Math.Max(vec1.Z, vec2.Z), System.Math.Max(vec1.W, vec2.W));
        }

        public static Vector4 Min(Vector4 vec1, Vector4 vec2)
        {
            return new Vector4(System.Math.Min(vec1.X, vec2.X), System.Math.Min(vec1.Y, vec2.Y), System.Math.Min(vec1.Z, vec2.Z), System.Math.Min(vec1.W, vec2.W));
        }

        // Vector and vector
        public Vector4 Add(Vector4 other)
        {
            return new Vector4(X + other.X, Y + other.Y, Z + other.Z, W + other.W);
        }

        public Vector4 Sub(Vector4 other)
        {
            return new Vector4(X - other.X, Y - other.Y, Z - other.Z, W - other.W);
        }

        public Vector4 Mul(Vector4 other)
        {
            return new Vector4(X * other.X, Y * other.Y, Z * other.Z, W * other.W);
        }

        public Vector4 Div(Vector4 other)
        {
            return new Vector4(X / other.X, Y / other.Y, Z / other.Z, W / other.W);
        }

        // Vector and vector (static)
        public static Vector4 Add(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }

        public static Vector4 Sub(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }

        public static Vector4 Mul(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z, v1.W * v2.W);
        }

        public static Vector4 Div(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z, v1.W / v2.W);
        }

        // Vector and scalar
        public Vector4 Add(float other)
        {
            return new Vector4(X + other, Y + other, Z + other, W + other);
        }

        public Vector4 Sub(float other)
        {
            return new Vector4(X - other, Y - other, Z - other, W - other);
        }

        public Vector4 Mul(float other)
        {
            return new Vector4(X * other, Y * other, Z * other, W * other);
        }

        public Vector4 Div(float other)
        {
            return new Vector4(X / other, Y / other, Z / other, W / other);
        }

        // Vector and scalar (static)
        public static Vector4 Add(Vector4 vec, float other)
        {
            return new Vector4(vec.X + other, vec.Y + other, vec.Z + other, vec.W + other);
        }

        public static Vector4 Sub(Vector4 vec, float other)
        {
            return new Vector4(vec.X - other, vec.Y - other, vec.Z - other, vec.W - other);
        }

        public static Vector4 Mul(Vector4 vec, float other)
        {
            return new Vector4(vec.X * other, vec.Y * other, vec.Z * other, vec.W * other);
        }

        public static Vector4 Div(Vector4 vec, float other)
        {
            return new Vector4(vec.X / other, vec.Y / other, vec.Z / other, vec.W / other);
        }

        // Dot product
        public float Dot(Vector4 other)
        {
            return X * other.X + Y * other.Y + Z * other.Z + W * other.W;
        }

        // Dot product (static)
        public static float Dot(Vector4 vec1, Vector4 vec2)
        {
            return vec1.Dot(vec2);
        }

        // Equals
        public bool Equals(Vector4 other)
        {
            return other.X == X && other.Y == Y && other.Z == Z && other.W == W;
        }

        public static bool operator ==(Vector4 lhs, Vector4 rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z && lhs.W == rhs.W;
        }

        public static bool operator !=(Vector4 lhs, Vector4 rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z || lhs.W != rhs.W;
        }

        // Vector and vector
        public static Vector4 operator +(Vector4 v1, Vector4 v2)
        {
            return Add(v1, v2);
        }

        public static Vector4 operator -(Vector4 v1, Vector4 v2)
        {
            return Sub(v1, v2);
        }

        public static Vector4 operator *(Vector4 v1, Vector4 v2)
        {
            return Mul(v1, v2);
        }

        public static Vector4 operator /(Vector4 v1, Vector4 v2)
        {
            return Div(v1, v2);
        }

        // Vector and scalar
        public static Vector4 operator +(Vector4 v1, float f)
        {
            return Add(v1, f);
        }

        public static Vector4 operator -(Vector4 v1, float f)
        {
            return Sub(v1, f);
        }

        public static Vector4 operator *(Vector4 v1, float f)
        {
            return Mul(v1, f);
        }

        public static Vector4 operator /(Vector4 v1, float f)
        {
            return Div(v1, f);
        }

        // Flipped
        public static Vector4 operator +(float f, Vector4 v1)
        {
            return Add(v1, f);
        }

        public static Vector4 operator -(float f, Vector4 v1)
        {
            return Sub(v1, f);
        }

        public static Vector4 operator *(float f, Vector4 v1)
        {
            return Mul(v1, f);
        }

        public static Vector4 operator /(float f, Vector4 v1)
        {
            return Div(v1, f);
        }

        // Negate
        public static Vector4 operator -(Vector4 v1)
        {
            return v1.Negate();
        }
    }
}
