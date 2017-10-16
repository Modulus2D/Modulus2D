using Microsoft.Xna.Framework;
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
    public enum PacketType {
        /// <summary>
        /// An update to networked entity states 
        /// </summary>
        Update = 0,
        /// <summary>
        /// A user-defined event
        /// </summary>
        Event = 1,
        /// <summary>
        /// One or more entities have been added to the world
        /// </summary>
        Add = 2,
        /// <summary>
        /// One or more entities have been removed from the world
        /// </summary>
        Remove = 3
    }

    /// <summary>
    /// Base packet class
    /// </summary>
    [Serializable]
    public class Packet { }

    /// <summary>
    /// Describes an update to networked entity states
    /// </summary>
    [Serializable]
    public class UpdatePacket : Packet
    {
        public Dictionary<uint, List<IUpdate>> packets;

        public UpdatePacket()
        {
            packets = new Dictionary<uint, List<IUpdate>>();
        }
    }

    /// <summary>
    /// Describes a user-defined event
    /// </summary>
    [Serializable]
    public class EventPacket : Packet
    {
        public string name;
        public IUpdate update;
    }

    /// <summary>
    /// Describes one or more entities being added to the world
    /// </summary>
    [Serializable]
    public class AddPacket : Packet
    {
        public Dictionary<uint, uint> builders;

        public AddPacket()
        {
            builders = new Dictionary<uint, uint>();
        }
    }

    /// <summary>
    /// Describes one or more entities being removed from the world
    /// </summary>
    [Serializable]
    public class RemovePacket : Packet
    {
        public List<uint> entities;

        public RemovePacket()
        {
            entities = new List<uint>();
        }
    }
}
