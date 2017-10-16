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
    /// <summary>
    /// Networking system for both server and client, generates transmissions and receives updates
    /// </summary>
    public class NetworkSystem : EntitySystem
    {
        private EntityFilter filter;

        public NetworkSystem()
        {
            filter = new EntityFilter();
            filter.Add<NetworkComponent>();
        }

        public UpdatePacket Transmit()
        {
            UpdatePacket packet = new UpdatePacket();

            foreach (Components components in World.Iterate(filter))
            {
                NetworkComponent network = components.Next<NetworkComponent>();
                
                packet.packets[network.Id] = network.Transmit();
            }

            return packet;
        }

        public void Receive(UpdatePacket packet)
        {
            foreach (Components components in World.Iterate(filter))
            {
                NetworkComponent network = components.Next<NetworkComponent>();
                
                if (packet.packets.TryGetValue(network.Id, out List<IUpdate> updates))
                {
                    network.ReceiveUpdate(updates);
                }
            }
        }
    }
}
