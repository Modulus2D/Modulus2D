using Modulus2D.Entities;
using Modulus2D.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Modulus2D.Map
{
    public class MapComponent : IComponent
    {
        private string filename;
        private TmxMap map;
        private TmxTileset tiles;
        private Texture texture;

        private float uvUnitX;
        private float uvUnitY;
        private float tileWorldWidth;
        private float tileWorldHeight;

        public MapComponent(string filename)
        {
            Filename = filename;
        }

        public string Filename { get => filename; set => filename = value; }
        public TmxMap Map { get => map; set => map = value; }
        public Texture Texture { get => texture; set => texture = value; }
        public TmxTileset Tiles { get => tiles; set => tiles = value; }

        public float UvUnitX { get => uvUnitX; set => uvUnitX = value; }
        public float UvUnitY { get => uvUnitY; set => uvUnitY = value; }
        public float TileWorldWidth { get => tileWorldWidth; set => tileWorldWidth = value; }
        public float TileWorldHeight { get => tileWorldHeight; set => tileWorldHeight = value; }
    }
}
