using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Network
{
    /// <summary>
    /// An interface for transmitting authoritative information
    /// </summary>
    public interface ITransmit
    {
        IUpdate Send();
    }
}
