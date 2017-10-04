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

namespace Modulus2D.Core
{
    public class Map
    {
        private Texture texture;
        private TmxMap map;
        private TmxTileset tiles;

        public Map(string file)
        {
            map = new TmxMap(file);

            // Load tileset
            texture = new Texture("Resources/Textures/MapTiles.png");
            tiles = map.Tilesets[0];
        }

        public void Draw(SpriteBatch batch)
        {
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

                        float uvX = tiles.TileWidth * row;
                        float uvY = tiles.TileWidth * column;

                        batch.DrawRegion(texture, new Vector2(tile.X, tile.Y), new Vector2(uvX, uvY), new Vector2(uvX + tiles.TileWidth, uvY + tiles.TileHeight));
                    }
                }
            }
        }
    }
}
