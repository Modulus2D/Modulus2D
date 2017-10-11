using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Network
{
    public enum DataType {
        Update = 4,
        Event = 1
    }

    [Serializable]
    public class UpdatePacket
    {
        public List<EntityPacket> packets;
    }

    [Serializable]
    public class EntityPacket
    {
        public uint id;
        public List<IUpdate> updates;
    }

    [Serializable]
    public class EventPacket
    {
        public string name;
        public IUpdate update;
    }

    [Serializable]
    public class InitialPacket
    {
        public List<EventPacket> events;
    }
}
