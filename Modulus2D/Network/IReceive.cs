using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Network
{
    /// <summary>
    /// An interface for receiving authoritative information. 
    /// </summary>
    public interface IReceive
    {
        void Receive(IUpdate update);
    }
}
