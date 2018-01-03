using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Math
{
    // TODO: WRITE UNIT TESTS!
    public struct Vector3 : IEquatable<Vector3>
    {
        public float X;
        public float Y;
        public float Z;

        /// <summary>
        /// Origin
        /// </summary>
        public static Vector3 Zero { get => new Vector3(0f, 0f, 0f); }

        /// <summary>
        /// One unit in each direction
        /// </summary>
        public static Vector3 One { get => new Vector3(1f, 1f, 1f); }

        /// <summary>
        /// Unit vector pointing in the positive X direction
        /// </summary>
        public static Vector3 Right { get => new Vector3(1f, 0f, 0f); }

        /// <summary>
        /// Unit vector pointing in the negative X direction
        /// </summary>
        public static Vector3 Left { get => new Vector3(-1f, 0f, 0f); }

        /// <summary>
        /// Unit vector pointing in the positive Y direction
        /// </summary>
        public static Vector3 Up { get => new Vector3(0f, 1f, 0f); }

        /// <summary>
        /// Unit vector pointing in the negative Y direction
        /// </summary>
        public static Vector3 Down { get => new Vector3(0f, -1f, 0f); }

        /// <summary>
        /// Unit vector pointing in the positive Z direction
        /// </summary>
        public static Vector3 Forward { get => new Vector3(0f, 0f, 1f); }

        /// <summary>
        /// Unit vector pointing in the negative Z direction
        /// </summary>
        public static Vector3 Backward { get => new Vector3(0f, 0f, -1f); }

        /// <summary>
        /// Create a 3D vector with each element equal to the given value
        /// </summary>
        /// <param name="value"></param>
        public Vector3(float value)
        {
            X = value;
            Y = value;
            Z = value;
        }

        /// <summary>
        /// Create a 3D vector with the given elements
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // Negate (static)
        public static Vector3 Negate(Vector3 vec)
        {
            return new Vector3(-vec.X, -vec.Y, -vec.Z);
        }

        // Normalize
        public void Normalize()
        {
            float length = Length(this);
            X /= length;
            Y /= length;
            Z /= length;
        }

        // Normalize (static)
        public static Vector3 Normalize(Vector3 vec)
        {
            vec.Normalize();
            return vec;
        }

        // Length
        public float Length()
        {
            return (float)System.Math.Sqrt(LengthSquared());
        }

        // Length (static)
        public static float Length(Vector3 vec)
        {
            return vec.Length();
        }

        // Length squared
        public float LengthSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        // Length squared (static)
        public static float LengthSquared(Vector3 vec)
        {
            return vec.LengthSquared();
        }

        public static float Distance(Vector3 vec1, Vector3 vec2)
        {
            return (float)System.Math.Sqrt(DistanceSquared(vec1, vec2));
        }

        public static float DistanceSquared(Vector3 vec1, Vector3 vec2)
        {
            return (vec1.X - vec2.X) * (vec1.X - vec2.X) + (vec1.Y - vec2.Y) * (vec1.Y - vec2.Y) + (vec1.Z - vec2.Z) * (vec1.Z - vec2.Z);
        }

        public static Vector3 Max(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3(System.Math.Max(vec1.X, vec2.X), System.Math.Max(vec1.Y, vec2.Y), System.Math.Max(vec1.Z, vec2.Z));
        }

        public static Vector3 Min(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3(System.Math.Min(vec1.X, vec2.X), System.Math.Min(vec1.Y, vec2.Y), System.Math.Min(vec1.Z, vec2.Z));
        }

        // Vector and vector (static)
        public static Vector3 Add(Vector3 vec1, Vector3 vec2)
        {
            vec1.X += vec2.X;
            vec1.Y += vec2.Y;
            vec1.Z += vec2.Z;

            return vec1;
        }

        public static Vector3 Sub(Vector3 vec1, Vector3 vec2)
        {
            vec1.X -= vec2.X;
            vec1.Y -= vec2.Y;
            vec1.Z -= vec2.Z;

            return vec1;
        }

        public static Vector3 Mul(Vector3 vec1, Vector3 vec2)
        {
            vec1.X *= vec2.X;
            vec1.Y *= vec2.Y;
            vec1.Z *= vec2.Z;

            return vec1;
        }

        public static Vector3 Div(Vector3 vec1, Vector3 vec2)
        {
            vec1.X /= vec2.X;
            vec1.Y /= vec2.Y;
            vec1.Z /= vec2.Z;

            return vec1;
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
        public static Vector3 operator +(Vector3 vec1, Vector3 vec2)
        {
            return Add(vec1, vec2);
        }

        public static Vector3 operator -(Vector3 vec1, Vector3 vec2)
        {
            return Sub(vec1, vec2);
        }

        public static Vector3 operator *(Vector3 vec1, Vector3 vec2)
        {
            return Mul(vec1, vec2);
        }

        public static Vector3 operator /(Vector3 vec1, Vector3 vec2)
        {
            return Div(vec1, vec2);
        }

        // Vector and scalar
        public static Vector3 operator +(Vector3 vec1, float f)
        {
            return Add(vec1, f);
        }

        public static Vector3 operator -(Vector3 vec1, float f)
        {
            return Sub(vec1, f);
        }

        public static Vector3 operator *(Vector3 vec1, float f)
        {
            return Mul(vec1, f);
        }

        public static Vector3 operator /(Vector3 vec1, float f)
        {
            return Div(vec1, f);
        }

        // Flipped
        public static Vector3 operator +(float f, Vector3 vec1)
        {
            return Add(vec1, f);
        }

        public static Vector3 operator -(float f, Vector3 vec1)
        {
            return Sub(vec1, f);
        }

        public static Vector3 operator *(float f, Vector3 vec1)
        {
            return Mul(vec1, f);
        }

        public static Vector3 operator /(float f, Vector3 vec1)
        {
            return Div(vec1, f);
        }

        // Negate
        public static Vector3 operator -(Vector3 vec)
        {
            return Negate(vec);
        }
    }
}
