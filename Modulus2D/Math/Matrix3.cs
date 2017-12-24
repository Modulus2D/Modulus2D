using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Math
{
    /// <summary>
    /// A 3x3 matrix class for 2D homogeneous coordinates
    /// </summary>
    public class Matrix3
    {
        float[] elements;

        /// <summary>
        /// Elements of the matrix
        /// 
        /// Indices are column-major
        /// 0   3   6
        /// 1   4   7
        /// 2   5   8
        /// </summary>
        public float[] Elements { get => elements; set => elements = value; }
        
        public Matrix3()
        {
            Elements = new float[] {
                1f, 0f, 0f, // Column 1
                0f, 1f, 0f, // Column 2
                0f, 0f, 1f, // Column 3
            };
        }

        public Matrix3(float[] elements)
        {
            Elements = elements;
        }

        /// <summary>
        /// Returns a translation matrix with the given vector
        /// </summary>
        /// <param name="translation">Vector to use as translation</param>
        public static Matrix3 Translate(Vector2 translation)
        {
            Matrix3 mat = new Matrix3();

            mat.Set(0, 2, translation.X);
            mat.Set(1, 2, translation.Y);

            return mat;
        }

        /// <summary>
        /// Returns a scale matrix with the given vector
        /// </summary>
        /// <param name="scale">Vector to use as scale</param>
        public static Matrix3 Scale(Vector2 scale)
        {
            Matrix3 mat = new Matrix3();

            mat.Set(0, 0, scale.X);
            mat.Set(1, 1, scale.Y);

            return mat;
        }

        /// <summary>
        /// Returns a rotation matrix with the given angle
        /// </summary>
        /// <param name="scale">Angle to use for rotation</param>
        public static Matrix3 Rotate(float angle)
        {
            Matrix3 mat = new Matrix3();

            float cos = (float)System.Math.Cos(angle);
            float sin = (float)System.Math.Sin(angle);

            mat.Set(0, 0, cos);
            mat.Set(1, 1, cos);
            mat.Set(1, 0, -sin);
            mat.Set(0, 1, sin);

            return mat;
        }

        /// <summary>
        /// Multiply two matrices
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns>The product of the two matrices</returns>
        public static Matrix3 Mul(Matrix3 m1, Matrix3 m2)
        {
            float[] elements = new float[9];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    float sum = 0f;

                    for (int k = 0; k < 3; k++)
                    {
                        sum += m1.Get(i, k) * m2.Get(k, j);
                    }

                    elements[3 * j + i] = sum;
                }
            }

            return new Matrix3(elements);
        }

        public static Matrix3 operator *(Matrix3 m1, Matrix3 m2)
        {
            return Mul(m1, m2);
        }

        public void Set(int row, int col, float value)
        {
            Elements[3 * col + row] = value;
        }

        public float Get(int row, int col)
        {
            return Elements[3 * col + row];
        }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < 3; i++)
            {
                result += "[ ";

                for (int j = 0; j < 3; j++)
                {
                    result += Elements[j * 3 + i] + " ";
                }

                result += "]\n";
            }

            return result;
        }
    }
}
