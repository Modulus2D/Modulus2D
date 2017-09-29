using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Math
{
    public class Vector2
    {
        private float x;
        private float y;

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }

        public static Vector2 Zero { get => new Vector2(0f, 0f); }
        public static Vector2 Unit { get => new Vector2(1f, 1f); }
        public static Vector2 Up { get => new Vector2(0f, 1f); }
        public static Vector2 Down { get => new Vector2(0f, -1f); }
        public static Vector2 Right { get => new Vector2(1f, 0f); }
        public static Vector2 Left { get => new Vector2(-1f, 0f); }

        public Vector2(float initX, float initY)
        {
            X = initX;
            Y = initY;
        }

        public Vector2 Normalize()
        {
            float length = Length();
            return new Vector2(X / length, Y / length);
        }

        public float Length()
        {
            return (float)System.Math.Sqrt(LengthSquared());
        }

        public float LengthSquared()
        {
            return X * X + Y * Y;
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

        // Dot product
        public float Dot(Vector2 other)
        {
            return X * other.X + Y * other.Y;
        }

        // Vector and vector
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return v1.Add(v2);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return v1.Sub(v2);
        }

        // Vector and scalar
        public static Vector2 operator +(Vector2 v1, float f)
        {
            return v1.Add(f);
        }

        public static Vector2 operator -(Vector2 v1, float f)
        {
            return v1.Sub(f);
        }

        public static Vector2 operator *(Vector2 v1, float f)
        {
            return v1.Mul(f);
        }

        public static Vector2 operator /(Vector2 v1, float f)
        {
            return v1.Div(f);
        }
    }
}
