using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Network
{
    /// <summary>
    /// All public properties of an IView object will be synced across the network
    /// </summary>
    public interface INetView
    {
        void Receive(NetBuffer buffer);
        void Transmit(NetBuffer buffer);
    }
}
