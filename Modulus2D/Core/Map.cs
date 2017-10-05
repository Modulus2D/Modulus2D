using Microsoft.Xna.Framework;
using NLog;
using Modulus2D.Graphics;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;
using Modulus2D.Entities;

namespace Modulus2D.Core
{
    public class Map
    {
        private VertexArray array;
        private RenderStates states;
        private string file;

        public void Load(string file)
        {
            this.file = file;

            TmxMap map = new TmxMap(file);

            array = new VertexArray(PrimitiveType.Quads);

            // Load tileset
            Texture texture = new Texture("Resources/Textures/MapTiles.png");
            states = new RenderStates(texture);
            TmxTileset tiles = map.Tilesets[0];

            foreach (TmxLayer layer in map.Layers)
            {
                foreach (TmxLayerTile tile in map.Layers[0].Tiles)
                {
                    if (tile.Gid != 0)
                    {
                        int frame = tile.Gid - 1;
                        int columns = tiles.Columns.Value;

                        int column = frame % columns;
                        int row = (int)Math.Floor(frame / (double)columns);

                        float uvX = tiles.TileWidth * column;
                        float uvY = tiles.TileWidth * row;

                        // Draw into array
                        SpriteBatch.DrawRegion(texture, new Vector2(tile.X, tile.Y), new Vector2(uvX, uvY), new Vector2(uvX + tiles.TileWidth, uvY + tiles.TileHeight), array);
                    }
                }
            }
        }

        public void Reload()
        {
            Load(file);
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(array, states);
        }
    }
}
