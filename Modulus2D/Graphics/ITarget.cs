using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics
{
    /// <summary>
    /// A generic render target
    /// </summary>
    public interface ITarget
    {
        /// <summary>
        /// Width of render target in pixels
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Height of render target in pixels
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Sets the color to clear the target with
        /// </summary>
        Color ClearColor { get; set; }

        /// <summary>
        /// Clears the render target
        /// </summary>
        void Clear();

        /// <summary>
        /// Binds the given texture
        /// </summary>
        void SetTexture(Texture texture);

        /// <summary>
        /// Binds the given shader
        /// </summary>
        void SetShader(Shader shader);

        /// <summary>
        /// Draws the given vertex array
        /// </summary>
        void Draw(VertexArray array, int count);

        /// <summary>
        /// Draws the given vertex array with the given indices
        /// </summary>
        void Draw(VertexArray array, uint[] indices, int count);
    }
}
