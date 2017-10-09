using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Network
{
    public class NetworkSystem : EntitySystem
    {
        private EntityFilter filter = new EntityFilter();

        public NetworkSystem()
        {
            filter.Add<NetworkComponent>();
        }

        public void Transmit()
        {
            foreach (Components components in World.Iterate(filter))
            {
                NetworkComponent network = components.Next<NetworkComponent>();
            }
        }

        public void Receive()
        {
            foreach (Components components in World.Iterate(filter))
            {
                NetworkComponent network = components.Next<NetworkComponent>();
            }
        }
    }
}
