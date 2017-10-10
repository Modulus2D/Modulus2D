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
        public List<EntityPacket> packets = new List<EntityPacket>();
    }

    [Serializable]
    public class EntityPacket
    {
        private uint id;
        private List<IUpdate> updates;

        public uint Id { get => id; set => id = value; }
        public List<IUpdate> Updates { get => updates; set => updates = value; }
    }

    [Serializable]
    public class EventPacket
    {
        private string name;

        public string Name { get => name; set => name = value; }
    }
}
