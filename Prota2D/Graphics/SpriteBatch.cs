using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Graphics
{
    public class SpriteBatch
    {
        private uint maxSprites = 100000;
        private uint cutoff;

        // 32 pixels per meter
        public float pixelsToMeters = 0.03125f;

        private Vertex[] vertices;
        private uint start = 0;
        private uint end = 0;

        private RenderWindow window;

        private RenderStates states = new RenderStates();
        private Texture lastTexture;

        public uint MaxSprites { get => maxSprites; set => maxSprites = value; }

        public SpriteBatch(Window window)
        {
            this.window = window.RenderWindow;

            vertices = new Vertex[4 * MaxSprites];

            cutoff = 4 * maxSprites;
        }

        public void Begin()
        {
            start = 0;
            end = 0;
        }

        private void DrawInit(Texture texture)
        {
            if (lastTexture == null)
            {
                lastTexture = texture;
            }

            if (texture != lastTexture)
            {
                // Render on texture change
                Flush();
                lastTexture = texture;
            }

            if (end >= cutoff)
            {
                // Reset on overflow
                Flush();
                Begin();
            }
        }

        public void Draw(Texture texture, Vector2 position, float rotation)
        {
            DrawInit(texture);

            float halfWidth = texture.Size.X * pixelsToMeters * 0.5f;
            float halfHeight = texture.Size.Y * pixelsToMeters * 0.5f;
            
            if (rotation == 0f)
            {
                vertices[end + 0] = new Vertex(new Vector2f(position.X - halfWidth,
                                                            position.Y - halfHeight), new Vector2f(0f, 0f));
                vertices[end + 1] = new Vertex(new Vector2f(position.X + halfWidth,
                                                            position.Y - halfHeight), new Vector2f(texture.Size.X, 0f));
                vertices[end + 2] = new Vertex(new Vector2f(position.X + halfWidth,
                                                            position.Y + halfHeight), new Vector2f(texture.Size.X, texture.Size.X));
                vertices[end + 3] = new Vertex(new Vector2f(position.X - halfWidth,
                                                            position.Y + halfHeight), new Vector2f(0f, texture.Size.X));
            } else
            {
                // Rotate if necessary
                float cos = (float)Math.Cos(rotation);
                float sin = (float)Math.Sin(rotation);

                vertices[end + 0] = new Vertex(new Vector2f(position.X - halfWidth * sin + halfHeight * cos,
                                                            position.Y - halfWidth * cos - halfHeight * sin), new Vector2f(0f, 0f));
                vertices[end + 1] = new Vertex(new Vector2f(position.X - halfWidth * sin - halfHeight * cos,
                                                            position.Y - halfWidth * cos + halfHeight * sin), new Vector2f(texture.Size.X, 0f));
                vertices[end + 2] = new Vertex(new Vector2f(position.X + halfWidth * sin - halfHeight * cos,
                                                            position.Y + halfWidth * cos + halfHeight * sin), new Vector2f(texture.Size.X, texture.Size.Y));
                vertices[end + 3] = new Vertex(new Vector2f(position.X + halfWidth * sin + halfHeight * cos,
                                                            position.Y + halfWidth * cos - halfHeight * sin), new Vector2f(0f, texture.Size.Y));
            }

            end += 4;
        }

        public void DrawRegion(Texture texture, Vector2 position, Vector2 uv1, Vector2 uv2)
        {
            DrawInit(texture);

            float halfWidth = (uv2.X - uv1.X) * pixelsToMeters * 0.5f;
            float halfHeight = (uv2.Y - uv1.Y) * pixelsToMeters * 0.5f;
            
            vertices[end + 0] = new Vertex(new Vector2f(position.X - halfWidth,
                                                        position.Y - halfHeight), new Vector2f(uv1.X, uv1.Y));
            vertices[end + 1] = new Vertex(new Vector2f(position.X + halfWidth,
                                                        position.Y - halfHeight), new Vector2f(uv2.X, uv1.Y));
            vertices[end + 2] = new Vertex(new Vector2f(position.X + halfWidth,
                                                        position.Y + halfHeight), new Vector2f(uv2.X, uv2.Y));
            vertices[end + 3] = new Vertex(new Vector2f(position.X - halfWidth,
                                                        position.Y + halfHeight), new Vector2f(uv1.X, uv2.Y));

            end += 4;
        }


        public void End()
        {
            if (vertices.Length > 0)
            {
                Flush();
            }

            lastTexture = null;
        }

        private void Flush()
        {
            window.Draw(vertices, start, end - start, PrimitiveType.Quads, new RenderStates(lastTexture));

            start = end;
        }
    }
}
