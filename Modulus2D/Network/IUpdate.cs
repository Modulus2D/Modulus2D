using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Network
{
    /// <summary>
    /// An interface used to send information for networked components
    /// To sync a variable across the network, define it as a public field
    /// </summary>
    public interface IUpdate
    {
    }
}
