using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics.Core
{
    /// <summary>
    /// A generic render target
    /// </summary>
    public interface IRenderTarget
    {
        /// <summary>
        /// Clears the screen with the specified color
        /// </summary>
        void Clear(Color color);

        /// <summary>
        /// Sets the current shader program
        /// </summary>
        void SetShader(Shader shader);
        
        /// <summary>
        /// Draws a regular mesh
        /// </summary>
        void Draw(Mesh mesh);
        
        /// <summary>
        /// Draws an indexed mesh
        /// </summary>
        void Draw(IndexedMesh mesh);
    }
}
