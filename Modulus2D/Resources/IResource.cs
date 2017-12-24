using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Resources
{
    public interface IResource
    {
        void Load(string path);
        void Unload();
    }
}
