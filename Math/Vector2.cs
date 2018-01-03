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
        public static Vector2 Zero { get => new Vector2(0f, 0f); }

        /// <summary>
        /// One unit in each direction
        /// </summary>
        public static Vector2 One { get => new Vector2(1f, 1f); }

        /// <summary>
        /// Unit vector pointing in the positive X direction
        /// </summary>
        public static Vector2 Right { get => new Vector2(1f, 0f); }

        /// <summary>
        /// Unit vector pointing in the negative X direction
        /// </summary>
        public static Vector2 Left { get => new Vector2(-1f, 0f); }

        /// <summary>
        /// Unit vector pointing in the positive Y direction
        /// </summary>
        public static Vector2 Up { get => new Vector2(0f, 1f); }

        /// <summary>
        /// Unit vector pointing in the negative Y direction
        /// </summary>
        public static Vector2 Down { get => new Vector2(0f, -1f); }

        /// <summary>
        /// Create a 2D vector with each element equal to the given value
        /// </summary>
        /// <param name="value"></param>
        public Vector2(float value)
        {
            X = value;
            Y = value;
        }

        /// <summary>
        /// Create a 2D vector with the given elements
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        // Negate (static)
        public static Vector2 Negate(Vector2 vec)
        {
            vec.X = -vec.X;
            vec.Y = -vec.Y;

            return vec;
        }

        // Normalize
        public void Normalize()
        {
            float length = Length(this);
            X /= length;
            Y /= length;
        }

        // Normalize (static)
        public static Vector2 Normalize(Vector2 vec)
        {
            float length = Length(vec);
            vec.X /= length;
            vec.Y /= length;

            return vec;
        }

        // Length
        public float Length()
        {
            return Length(this);
        }

        // Length (static)
        public static float Length(Vector2 vec)
        {
            return (float)System.Math.Sqrt(LengthSquared(vec));
        }

        // Length squared
        public float LengthSquared()
        {
            return LengthSquared(this);
        }

        // Length squared (static)
        public static float LengthSquared(Vector2 vec)
        {
            return vec.X * vec.X + vec.Y * vec.Y;
        }

        public static float Distance(Vector2 vec1, Vector2 vec2)
        {
            return (float)System.Math.Sqrt(DistanceSquared(vec1, vec2));
        }

        public static float DistanceSquared(Vector2 vec1, Vector2 vec2)
        {
            return (vec1.X - vec2.X) * (vec1.X - vec2.X) + (vec1.Y - vec2.Y) * (vec1.Y - vec2.Y);
        }

        public static Vector2 Max(Vector2 vec1, Vector2 vec2)
        {
            return new Vector2(System.Math.Max(vec1.X, vec2.X), System.Math.Max(vec1.Y, vec2.Y));
        }

        public static Vector2 Min(Vector2 vec1, Vector2 vec2)
        {
            return new Vector2(System.Math.Min(vec1.X, vec2.X), System.Math.Min(vec1.Y, vec2.Y));
        }
        
        // Vector and vector (static)
        public static Vector2 Add(Vector2 vec1, Vector2 vec2)
        {
            vec1.X += vec2.X;
            vec1.Y += vec2.Y;

            return vec1;
        }

        public static Vector2 Sub(Vector2 vec1, Vector2 vec2)
        {
            vec1.X -= vec2.X;
            vec1.Y -= vec2.Y;

            return vec1;
        }

        public static Vector2 Mul(Vector2 vec1, Vector2 vec2)
        {
            vec1.X *= vec2.X;
            vec1.Y *= vec2.Y;

            return vec1;
        }

        public static Vector2 Div(Vector2 vec1, Vector2 vec2)
        {
            vec1.X /= vec2.X;
            vec1.Y /= vec2.Y;

            return vec1;
        }

        // Vector and scalar
        public static Vector2 Add(Vector2 vec, float other)
        {
            vec.X += other;
            vec.Y += other;

            return vec;
        }

        public static Vector2 Sub(Vector2 vec, float other)
        {
            vec.X -= other;
            vec.Y -= other;

            return vec;
        }

        public static Vector2 Mul(Vector2 vec, float other)
        {
            vec.X *= other;
            vec.Y *= other;

            return vec;
        }

        public static Vector2 Div(Vector2 vec, float other)
        {
            vec.X /= other;
            vec.Y /= other;

            return vec;
        }
        
        // Dot product (static)
        public static float Dot(Vector2 vec1, Vector2 vec2)
        {
            return vec1.X * vec2.X + vec1.Y * vec2.Y;
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
        public static Vector2 operator +(Vector2 vec1, Vector2 vec2)
        {
            return Add(vec1, vec2);
        }

        public static Vector2 operator -(Vector2 vec1, Vector2 vec2)
        {
            return Sub(vec1, vec2);
        }

        public static Vector2 operator *(Vector2 vec1, Vector2 vec2)
        {
            return Mul(vec1, vec2);
        }

        public static Vector2 operator /(Vector2 vec1, Vector2 vec2)
        {
            return Div(vec1, vec2);
        }

        // Vector and scalar
        public static Vector2 operator +(Vector2 vec1, float f)
        {
            return Add(vec1, f);
        }

        public static Vector2 operator -(Vector2 vec1, float f)
        {
            return Sub(vec1, f);
        }

        public static Vector2 operator *(Vector2 vec1, float f)
        {
            return Mul(vec1, f);
        }

        public static Vector2 operator /(Vector2 vec1, float f)
        {
            return Div(vec1, f);
        }

        // Flipped
        public static Vector2 operator +(float f, Vector2 vec1)
        {
            return Add(vec1, f);
        }

        public static Vector2 operator -(float f, Vector2 vec1)
        {
            return Sub(vec1, f);
        }

        public static Vector2 operator *(float f, Vector2 vec1)
        {
            return Mul(vec1, f);
        }

        public static Vector2 operator /(float f, Vector2 vec1)
        {
            return Div(vec1, f);
        }

        // Negate
        public static Vector2 operator -(Vector2 vec)
        {
            return Negate(vec);
        }
    }
}
