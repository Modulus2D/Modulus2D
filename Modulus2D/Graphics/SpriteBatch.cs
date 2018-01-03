using Modulus2D.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics
{
    public class SpriteBatch {    
        private uint maxSprites = 2048;
        public uint MaxSprites { get => maxSprites; set => maxSprites = value; }

        // 32 pixels per meter
        public static float PixelsToMeters = 0.03125f;

        // Index of current sprite
        private int index = 0;

        private ITarget target;

        private OrthoCamera camera;
        public OrthoCamera Camera { get => camera; set => camera = value; }

        private float[] vertices;
        private uint[] indices;
        private VertexArray vertexArray;

        private Shader shader;
        private Uniform viewProj;

        private Texture texture;

        public SpriteBatch(ITarget target, OrthoCamera camera)
        {
            this.target = target;
            this.camera = camera;

            vertices = new float[MaxSprites * 16];
            indices = new uint[MaxSprites * 6];

            CreateDefaultShader();

            vertexArray = new VertexArray()
            {
                Attribs = new VertexAttrib[] {
                    new VertexAttrib // position
                    {
                        Size = 2,
                        Normalized = false,
                    },
                    new VertexAttrib // UV
                    {
                        Size = 2,
                        Normalized = false,
                    }
                }
            };
        }

        private void CreateDefaultShader()
        {
            shader = new Shader();

            shader.AddVertex(@"
                #version 150 core

                in vec2 vertexPosition;
                in vec2 vertexUV;

                out vec2 UV;

                uniform mat4 viewProj;

                void main()
                {
                    gl_Position = viewProj * vec4(vertexPosition, 0.0, 1.0);

                    UV = vertexUV;
                }
            ");

            shader.AddFragment(
            @"
                #version 150 core

                in vec2 UV;

                out vec4 color;

                uniform sampler2D tex;

                void main()
                {
                    color = texture(tex, UV);
                }
            ");

            shader.Link();

            viewProj = shader.GetUniform("viewProj");
        }

        /// <summary>
        /// Full draw function with all parameters
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="position">Position</param>
        /// <param name="scale">Scale</param>
        /// <param name="uv1">Lower left UV</param>
        /// <param name="uv2">Top right UV</param>
        /// <param name="rotation">Rotation</param>
        public void Draw(Texture texture, Vector2 position, Vector2 scale, Vector2 uv1, Vector2 uv2, float rotation)
        {
            if (index >= MaxSprites)
            {
                // Render if MaxSprites is exceeded
                Flush();
            }

            if (this.texture == null)
            {
                this.texture = texture;
            }
            else if (this.texture != texture)
            {
                Flush();

                this.texture = texture;
            }
            
            // UVs determine the size of the sprite relative to the size of the texture
            float halfWidth = texture.Width * (uv2.X - uv1.X) * scale.X * PixelsToMeters * 0.5f;
            float halfHeight = texture.Height * (uv2.Y - uv1.Y) * scale.Y * PixelsToMeters * 0.5f;

            // 4 values per vertex, 4 * 4 = 16
            int firstValue = index * 16;

            // 6 indices per sprite
            int firstIndex = index * 6;
            
            if (rotation == 0f)
            {
                // Lower left
                vertices[firstValue + 0] = position.X - halfWidth; // Position X
                vertices[firstValue + 1] = position.Y - halfHeight; // Position Y
                vertices[firstValue + 2] = uv1.X; // UV X
                vertices[firstValue + 3] = uv2.Y; // UV Y

                // Lower right
                vertices[firstValue + 4] = position.X + halfWidth; // Position X
                vertices[firstValue + 5] = position.Y - halfHeight; // Position Y
                vertices[firstValue + 6] = uv2.X; // UV X
                vertices[firstValue + 7] = uv2.Y; // UV Y

                // Upper right
                vertices[firstValue + 8] = position.X + halfWidth; // Position X
                vertices[firstValue + 9] = position.Y + halfHeight; // Position Y
                vertices[firstValue + 10] = uv2.X; // UV X
                vertices[firstValue + 11] = uv1.Y; // UV Y

                // Upper left
                vertices[firstValue + 12] = position.X - halfWidth; // Position X
                vertices[firstValue + 13] = position.Y + halfHeight; // Position Y
                vertices[firstValue + 14] = uv1.X; // UV X
                vertices[firstValue + 15] = uv1.Y; // UV Y
            }
            else
            {
                // Rotate if necessary
                float cos = (float)System.Math.Cos(rotation);
                float sin = (float)System.Math.Sin(rotation);
                
                // Lower left
                vertices[firstValue + 0] = position.X - halfWidth * sin - halfHeight * cos; // Position X
                vertices[firstValue + 1] = position.Y - halfWidth * cos + halfHeight * sin; // Position Y
                vertices[firstValue + 2] = uv1.X; // UV X
                vertices[firstValue + 3] = uv2.Y; // UV Y

                // Lower right
                vertices[firstValue + 4] = position.X - halfWidth * sin + halfHeight * cos; // Position X
                vertices[firstValue + 5] = position.Y - halfWidth * cos - halfHeight * sin; // Position Y
                vertices[firstValue + 6] = uv2.X; // UV X
                vertices[firstValue + 7] = uv2.Y; // UV Y

                // Upper right
                vertices[firstValue + 8] = position.X + halfWidth * sin + halfHeight * cos; // Position X
                vertices[firstValue + 9] = position.Y + halfWidth * cos - halfHeight * sin; // Position Y
                vertices[firstValue + 10] = uv2.X; // UV X
                vertices[firstValue + 11] = uv1.Y; // UV Y

                // Upper left
                vertices[firstValue + 12] = position.X + halfWidth * sin - halfHeight * cos; // Position X
                vertices[firstValue + 13] = position.Y + halfWidth * cos + halfHeight * sin; // Position Y
                vertices[firstValue + 14] = uv1.X; // UV X
                vertices[firstValue + 15] = uv1.Y; // UV Y
            }

            // 4 vertices per sprite
            uint firstVertex = (uint)index * 4;

            // Create two triangles from coordinates
            indices[firstIndex + 0] = firstVertex + 3; // Upper left
            indices[firstIndex + 1] = firstVertex + 0; // Lower left
            indices[firstIndex + 2] = firstVertex + 1; // Lower right
            indices[firstIndex + 3] = firstVertex + 3; // Upper left
            indices[firstIndex + 4] = firstVertex + 2; // Upper right
            indices[firstIndex + 5] = firstVertex + 1; // Lower right

            // Advance to next sprite
            index++;
        }

        /// <summary>
        /// Renders the current sprites and resets
        /// </summary>
        public void Flush()
        {
            // Update camera
            shader.Set(viewProj, camera.Update());

            // Update vertices
            // 4 components per vertex, 4 vertices per sprite
            vertexArray.Upload(vertices, index * 16);

            // Bind texture
            target.SetTexture(texture);

            // Bind shader
            target.SetShader(shader);

            // Draw vertices
            // 6 indices per sprite
            target.Draw(vertexArray, indices, index * 6);

            // Reset
            index = 0;
        }
        
        public void Draw(Texture texture, Vector2 position)
        {
            Draw(texture, position, Vector2.One, Vector2.Zero, Vector2.One, 0f);
        }

        public void Draw(Texture texture, Vector2 position, float rotation)
        {
            Draw(texture, position, Vector2.One, Vector2.Zero, Vector2.One, rotation);
        }

        public void Draw(Texture texture, Vector2 position, Vector2 scale)
        {
            Draw(texture, position, scale, Vector2.Zero, Vector2.One, 0f);
        }

        public void Draw(Texture texture, Vector2 position, Vector2 scale, float rotation)
        {
            Draw(texture, position, scale, Vector2.Zero, Vector2.One, rotation);
        }
    }
}