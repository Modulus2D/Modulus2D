using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Network
{
    /// <summary>
    /// An interface implemented by components to allow the transmission and reception of data over the network
    /// </summary>
    public interface INetComponent
    {
        IUpdate Transmit();
        void Receive(IUpdate update);
    }
}
