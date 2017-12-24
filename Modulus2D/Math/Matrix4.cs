using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Math
{
    /// <summary>
    /// A 4x4 matrix class for 3D homogeneous coordinates
    /// </summary>
    public class Matrix4
    {
        float[] elements;

        /// <summary>
        /// Elements of the matrix
        /// 
        /// Indices are column-major
        /// 0   4   8   12
        /// 1   5   9   13
        /// 2   6   10  14
        /// 3   7   11  15
        /// </summary>
        public float[] Elements { get => elements; set => elements = value; }

        public Matrix4()
        {
            Elements = new float[] {
                1f, 0f, 0f, 0f, // Column 1
                0f, 1f, 0f, 0f, // Column 2
                0f, 0f, 1f, 0f, // Column 3
                0f, 0f, 0f, 1f, // Column 4
            };
        }

        public Matrix4(float[] elements)
        {
            Elements = elements;
        }

        /// <summary>
        /// Returns a translation matrix with the given vector
        /// </summary>
        /// <param name="translation">Vector to use as translation</param>
        public static Matrix4 Translate(Vector3 translation)
        {
            Matrix4 mat = new Matrix4();

            mat.Set(0, 3, translation.X);
            mat.Set(1, 3, translation.Y);
            mat.Set(2, 3, translation.Z);

            return mat;
        }

        /// <summary>
        /// Returns a scale matrix with the given vector
        /// </summary>
        /// <param name="scale">Vector to use as scale</param>
        public static Matrix4 Scale(Vector3 scale)
        {
            Matrix4 mat = new Matrix4();

            mat.Set(0, 0, scale.X);
            mat.Set(1, 1, scale.Y);
            mat.Set(2, 2, scale.Z);

            return mat;
        }

        /// <summary>
        /// Returns a rotation matrix in the X direction given the angle
        /// </summary>
        /// <param name="angle">Angle to use for rotation</param>
        /// <returns></returns>
        public static Matrix4 RotateX(float angle)
        {
            float cos = (float)System.Math.Cos(angle);
            float sin = (float)System.Math.Sin(angle);
            
            return RotateX(cos, sin);
        }

        /// <summary>
        /// Returns a rotation matrix in the X direction given the angle
        /// </summary>
        /// <returns></returns>
        public static Matrix4 RotateX(float cos, float sin)
        {
            Matrix4 mat = new Matrix4();
            
            mat.Set(1, 1, cos);
            mat.Set(1, 2, -sin);
            mat.Set(2, 1, sin);
            mat.Set(2, 2, cos);

            return mat;
        }

        /// <summary>
        /// Returns a rotation matrix in the Y direction given the angle
        /// </summary>
        /// <param name="angle">Angle to use for rotation</param>
        /// <returns></returns>
        public static Matrix4 RotateY(float angle)
        {
            float cos = (float)System.Math.Cos(angle);
            float sin = (float)System.Math.Sin(angle);

            return RotateY(cos, sin);
        }

        /// <summary>
        /// Returns a rotation matrix in the Y direction given the angle
        /// </summary>
        /// <returns></returns>
        public static Matrix4 RotateY(float cos, float sin)
        {
            Matrix4 mat = new Matrix4();

            mat.Set(0, 0, cos);
            mat.Set(0, 2, sin);
            mat.Set(2, 0, -sin);
            mat.Set(2, 2, cos);

            return mat;
        }

        /// <summary>
        /// Returns a rotation matrix in the Z direction given the angle
        /// </summary>
        /// <param name="angle">Angle to use for rotation</param>
        /// <returns></returns>
        public static Matrix4 RotateZ(float angle)
        {
            float cos = (float)System.Math.Cos(angle);
            float sin = (float)System.Math.Sin(angle);

            return RotateZ(cos, sin);
        }

        /// <summary>
        /// Returns a rotation matrix in the Z direction given the angle
        /// </summary>
        /// <returns></returns>
        public static Matrix4 RotateZ(float cos, float sin)
        {
            Matrix4 mat = new Matrix4();

            mat.Set(0, 0, cos);
            mat.Set(0, 1, -sin);
            mat.Set(1, 0, sin);
            mat.Set(1, 1, cos);

            return mat;
        }

        /// <summary>
        /// Returns an orthographic projection matrix
        /// </summary>
        /// <returns></returns>
        public static Matrix4 Ortho(float right, float left, float top, float bottom, float far, float near)
        {
            Matrix4 mat = new Matrix4();
            
            mat.Set(0, 0, 2f/(right-left));
            mat.Set(1, 1, 2f/(top-bottom));
            mat.Set(2, 2, -2f/(far-near));
            mat.Set(0, 3, -(right+left)/(right-left));
            mat.Set(1, 3, -(top + bottom) / (top - bottom));
            mat.Set(2, 3, -(far + near) / (far - near));

            return mat;
        }

        /// <summary>
        /// Multiply two matrices
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns>The product of the two matrices</returns>
        public static Matrix4 Mul(Matrix4 m1, Matrix4 m2)
        {
            float[] elements = new float[16];

            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    float sum = 0f;

                    for(int k = 0; k < 4; k++)
                    {
                        sum += m1.Get(i, k) * m2.Get(k, j);
                    }

                    elements[4 * j + i] = sum;
                }
            }
            
            return new Matrix4(elements);
        }

        public static Matrix4 operator *(Matrix4 m1, Matrix4 m2)
        {
            return Mul(m1, m2);
        }

        public void Set(int row, int col, float value)
        {
            Elements[4 * col + row] = value;
        }

        public float Get(int row, int col)
        {
            return Elements[4 * col + row];
        }

        public override string ToString()
        {
            string result = "";

            for(int i = 0; i < 4; i++)
            {
                result += "[ ";

                for(int j = 0; j < 4; j++)
                {
                    result += Elements[j * 4 + i] + " ";
                }

                result += "]\n";
            }

            return result;
        }
    }
}
