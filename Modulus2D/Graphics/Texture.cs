using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenGL;
using System.Drawing.Imaging;

namespace Modulus2D.Graphics
{
    public class Texture
    {
        private uint id;
        private Bitmap bitmap;

        public int Width { get => bitmap.Width; }
        public int Height { get => bitmap.Height; }

        public Texture(string file)
        {
            bitmap = (Bitmap)Image.FromFile(file);
            
            Gl.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            id = Gl.GenTexture();

            Gl.BindTexture(TextureTarget.Texture2d, id);

            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, bitmap.Width, bitmap.Height,
                0, OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            Gl.GenerateMipmap(TextureTarget.Texture2d);
        }

        public void Bind()
        {
            Gl.BindTexture(TextureTarget.Texture2d, id);
        }
    }
}
    