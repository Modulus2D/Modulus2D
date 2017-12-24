using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Network
{
    /// <summary>
    /// Encoded with messages to distinguish between events and updates
    /// </summary>
    public enum PacketType
    {
        /// <summary>
        /// An update to networked entity states 
        /// </summary>
        Update = 0,
        /// <summary>
        /// A user-defined event
        /// </summary>
        Event = 1,
        /// <summary>
        /// An entity has been added to the world
        /// </summary>
        Create = 2,
        /// <summary>
        /// An entity has been removed from the world
        /// </summary>
        Remove = 3,
        /// <summary>
        /// Adds buffered entities to the world
        /// </summary>
        Buffer = 4
    }
}
