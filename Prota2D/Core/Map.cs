using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Prota2D.Core
{
    public class Map
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Map(string file)
        {
            TmxMap map = new TmxMap(file);
            
            foreach(TmxLayer layer in map.Layers)
            {
                foreach (TmxLayerTile tile in layer.Tiles)
                {
                    //logger.Debug(tile.X);
                }
            }
        }
    }
}
