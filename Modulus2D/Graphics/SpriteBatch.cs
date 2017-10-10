using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics
{
    public class SpriteBatch
    {
        private uint maxSprites = 2048;
        public uint MaxSprites { get => maxSprites; set => maxSprites = value; }

        // 32 pixels per meter
        public static float PixelsToMeters = 0.03125f;

        private Vertex[] vertices;
        private List<SpriteInfo> infos = new List<SpriteInfo>();

        private uint index = 0;

        private RenderTarget target;

        private OrthoCamera camera;
        public OrthoCamera Camera { get => camera; set => camera = value; }

        public SpriteBatch(RenderTarget target)
        {
            this.target = target;

            vertices = new Vertex[4 * MaxSprites];
        }

        public void Begin()
        {
            // Update camera
            target.SetView(camera.View);

            index = 0;
            infos.Clear();
        }

        // Draw with all parameters
        public void Draw(Texture texture, Vector2 position, Vector2 scale, Vector2 uv1, Vector2 uv2, float rotation)
        {
            if (index >= 4 * MaxSprites)
            {
                // Render if MaxSprites is exceeded
                End();

                index = 0;
                infos.Clear();
            }

            if (infos.Count == 0)
            {
                infos.Add(new SpriteInfo(new RenderStates(texture)));
            } else if (texture != infos[infos.Count - 1].states.Texture)
            {
                // Render on texture change
                infos.Add(new SpriteInfo(new RenderStates(texture)));
            }

            float halfWidth = texture.Size.X * scale.X * PixelsToMeters * 0.5f;
            float halfHeight = texture.Size.Y * scale.Y * PixelsToMeters * 0.5f;

            if (rotation == 0f)
            {
                vertices[index + 0] = new Vertex(new Vector2f(position.X - halfWidth,
                                                            position.Y - halfHeight), new Vector2f(uv1.X, uv1.Y));
                vertices[index + 1] = new Vertex(new Vector2f(position.X + halfWidth,
                                                            position.Y - halfHeight), new Vector2f(uv2.X, uv1.Y));
                vertices[index + 2] = new Vertex(new Vector2f(position.X + halfWidth,
                                                            position.Y + halfHeight), new Vector2f(uv2.X, uv2.Y));
                vertices[index + 3] = new Vertex(new Vector2f(position.X - halfWidth,
                                                            position.Y + halfHeight), new Vector2f(uv1.X, uv2.Y));
            }
            else
            {
                // Rotate if necessary
                float cos = (float)Math.Cos(rotation);
                float sin = (float)Math.Sin(rotation);

                vertices[index + 0] = new Vertex(new Vector2f(position.X - halfWidth * sin + halfHeight * cos,
                                                            position.Y - halfWidth * cos - halfHeight * sin), new Vector2f(uv1.X, uv1.Y));
                vertices[index + 1] = new Vertex(new Vector2f(position.X - halfWidth * sin - halfHeight * cos,
                                                            position.Y - halfWidth * cos + halfHeight * sin), new Vector2f(uv2.X, uv1.Y));
                vertices[index + 2] = new Vertex(new Vector2f(position.X + halfWidth * sin - halfHeight * cos,
                                                            position.Y + halfWidth * cos + halfHeight * sin), new Vector2f(uv2.X, uv2.Y));
                vertices[index + 3] = new Vertex(new Vector2f(position.X + halfWidth * sin + halfHeight * cos,
                                                            position.Y + halfWidth * cos - halfHeight * sin), new Vector2f(uv1.X, uv2.Y));
            }

            index += 4;
            infos[infos.Count - 1].length += 4;
        }

        public void Draw(Texture texture, Vector2 position)
        {
            Draw(texture, position, new Vector2(1f, 1f), new Vector2(0f, 0f), new Vector2(texture.Size.X, texture.Size.Y), 0f);
        }

        public void Draw(Texture texture, Vector2 position, float rotation)
        {
            Draw(texture, position, new Vector2(1f, 1f), new Vector2(0f, 0f), new Vector2(texture.Size.X, texture.Size.Y), rotation);
        }

        public void Draw(Texture texture, Vector2 position, Vector2 scale)
        {
            Draw(texture, position, scale, new Vector2(0f, 0f), new Vector2(texture.Size.X, texture.Size.Y), 0f);
        }

        public void Draw(Texture texture, Vector2 position, Vector2 scale, float rotation)
        {
            Draw(texture, position, scale, new Vector2(0f, 0f), new Vector2(texture.Size.X, texture.Size.Y), rotation);
        }

        public void Draw(VertexArray vertices, RenderStates states)
        {
            target.Draw(vertices, states);
        }

        public static void DrawRegion(Texture texture, Vector2 position, Vector2 uv1, Vector2 uv2, VertexArray array)
        {
            float halfWidth = 32f * PixelsToMeters * 0.5f;
            float halfHeight = 32f * PixelsToMeters * 0.5f;

            array.Append(new Vertex(new Vector2f(position.X - halfWidth,
                                                        position.Y - halfHeight), new Vector2f(uv1.X, uv1.Y)));
            array.Append(new Vertex(new Vector2f(position.X + halfWidth,
                                                        position.Y - halfHeight), new Vector2f(uv2.X, uv1.Y)));
            array.Append(new Vertex(new Vector2f(position.X + halfWidth,
                                                        position.Y + halfHeight), new Vector2f(uv2.X, uv2.Y)));
            array.Append(new Vertex(new Vector2f(position.X - halfWidth,
                                                        position.Y + halfHeight), new Vector2f(uv1.X, uv2.Y)));
        }

        public void End()
        {
            uint current = 0;

            for (int i = 0; i < infos.Count; i++)
            {
                SpriteInfo info = infos[i];

                target.Draw(vertices, current, info.length, PrimitiveType.Quads, info.states);
                current += info.length;
            }
        }
    }

    class SpriteInfo
    {
        public RenderStates states;
        public uint length;

        public SpriteInfo(RenderStates states)
        {
            this.states = states;
        }
    }
}
