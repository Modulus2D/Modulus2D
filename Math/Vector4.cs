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
        public static Vector4 Zero { get => new Vector4(0f, 0f, 0f, 0f); }

        /// <summary>
        /// One unit in each direction
        /// </summary>
        public static Vector4 One { get => new Vector4(1f, 1f, 1f, 1f); }

        /// <summary>
        /// Unit vector pointing in the positive X direction
        /// </summary>
        public static Vector4 Right { get => new Vector4(1f, 0f, 0f, 0f); }

        /// <summary>
        /// Unit vector pointing in the negative X direction
        /// </summary>
        public static Vector4 Left { get => new Vector4(-1f, 0f, 0f, 0f); }

        /// <summary>
        /// Unit vector pointing in the positive Y direction
        /// </summary>
        public static Vector4 Up { get => new Vector4(0f, 1f, 0f, 0f); }

        /// <summary>
        /// Unit vector pointing in the negative Y direction
        /// </summary>
        public static Vector4 Down { get => new Vector4(0f, -1f, 0f, 0f); }

        /// <summary>
        /// Unit vector pointing in the positive Z direction
        /// </summary>
        public static Vector4 Forward { get => new Vector4(0f, 0f, 1f, 0f); }

        /// <summary>
        /// Unit vector pointing in the negative Z direction
        /// </summary>
        public static Vector4 Backward { get => new Vector4(0f, 0f, -1f, 0f); }

        /// <summary>
        /// Unit vector pointing in the positive W direction
        /// </summary>
        public static Vector4 Ana { get => new Vector4(0f, 0f, 0f, 1f); }
        
        /// <summary>
        /// Unit vector pointing in the negative W direction
        /// </summary>
        public static Vector4 Kata { get => new Vector4(0f, 0f, 0f, -1f); }

        /// <summary>
        /// Create a 4D vector with each element equal to the given value
        /// </summary>
        /// <param name="value"></param>
        public Vector4(float value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }

        /// <summary>
        /// Create a 4D vector with the given elements
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Vector4(float x, float y, float z,float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        
        // Negate (static)
        public static Vector4 Negate(Vector4 vec)
        {
            return new Vector4(-vec.X, -vec.Y, -vec.Z, -vec.W);
        }

        // Normalize
        public void Normalize()
        {
            float length = Length();
            X /= length;
            Y /= length;
            Z /= length;
            W /= length;
        }

        // Normalize (static)
        public static Vector4 Normalize(Vector4 vec)
        {
            vec.Normalize();
            return vec;
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
            return (float)System.Math.Sqrt(DistanceSquared(vec1, vec2));
        }

        public static float DistanceSquared(Vector4 vec1, Vector4 vec2)
        {
            return (vec1.X - vec2.X) * (vec1.X - vec2.X) + (vec1.Y - vec2.Y) * (vec1.Y - vec2.Y) +
                (vec1.Z - vec2.Z) * (vec1.Z - vec2.Z) + (vec1.W - vec2.W) * (vec1.W - vec2.W);
        }

        public static Vector4 Max(Vector4 vec1, Vector4 vec2)
        {
            return new Vector4(System.Math.Max(vec1.X, vec2.X), System.Math.Max(vec1.Y, vec2.Y), System.Math.Max(vec1.Z, vec2.Z), System.Math.Max(vec1.W, vec2.W));
        }

        public static Vector4 Min(Vector4 vec1, Vector4 vec2)
        {
            return new Vector4(System.Math.Min(vec1.X, vec2.X), System.Math.Min(vec1.Y, vec2.Y), System.Math.Min(vec1.Z, vec2.Z), System.Math.Min(vec1.W, vec2.W));
        }
        
        // Vector and vector (static)
        public static Vector4 Add(Vector4 vec1, Vector4 vec2)
        {
            return new Vector4(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z, vec1.W + vec2.W);
        }

        public static Vector4 Sub(Vector4 vec1, Vector4 vec2)
        {
            return new Vector4(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z, vec1.W - vec2.W);
        }

        public static Vector4 Mul(Vector4 vec1, Vector4 vec2)
        {
            return new Vector4(vec1.X * vec2.X, vec1.Y * vec2.Y, vec1.Z * vec2.Z, vec1.W * vec2.W);
        }

        public static Vector4 Div(Vector4 vec1, Vector4 vec2)
        {
            return new Vector4(vec1.X / vec2.X, vec1.Y / vec2.Y, vec1.Z / vec2.Z, vec1.W / vec2.W);
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
        public static Vector4 operator +(Vector4 vec1, Vector4 vec2)
        {
            return Add(vec1, vec2);
        }

        public static Vector4 operator -(Vector4 vec1, Vector4 vec2)
        {
            return Sub(vec1, vec2);
        }

        public static Vector4 operator *(Vector4 vec1, Vector4 vec2)
        {
            return Mul(vec1, vec2);
        }

        public static Vector4 operator /(Vector4 vec1, Vector4 vec2)
        {
            return Div(vec1, vec2);
        }

        // Vector and scalar
        public static Vector4 operator +(Vector4 vec1, float f)
        {
            return Add(vec1, f);
        }

        public static Vector4 operator -(Vector4 vec1, float f)
        {
            return Sub(vec1, f);
        }

        public static Vector4 operator *(Vector4 vec1, float f)
        {
            return Mul(vec1, f);
        }

        public static Vector4 operator /(Vector4 vec1, float f)
        {
            return Div(vec1, f);
        }

        // Flipped
        public static Vector4 operator +(float f, Vector4 vec1)
        {
            return Add(vec1, f);
        }

        public static Vector4 operator -(float f, Vector4 vec1)
        {
            return Sub(vec1, f);
        }

        public static Vector4 operator *(float f, Vector4 vec1)
        {
            return Mul(vec1, f);
        }

        public static Vector4 operator /(float f, Vector4 vec1)
        {
            return Div(vec1, f);
        }

        // Negate
        public static Vector4 operator -(Vector4 vec)
        {
            return Negate(vec);
        }
    }
}
