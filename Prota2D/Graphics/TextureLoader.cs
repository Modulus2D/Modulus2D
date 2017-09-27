using NLog;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D.Graphics
{
    public class TextureLoader
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public Texture Load(string file)
        {
            try
            {
                return new Texture()
                {
                    texture = new SFML.Graphics.Texture(file)
                };
            } catch
            {
                logger.Fatal("Could not load texture: " + file);
                return new Texture();
            }
        }
    }
}
