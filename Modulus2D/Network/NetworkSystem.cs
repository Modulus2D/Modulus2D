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
        private Dictionary<uint, NetworkComponent> networkComponents = new Dictionary<uint, NetworkComponent>();

        private uint currentId = 0;

        public NetworkSystem()
        {
            filter.Add<NetworkComponent>();
        }

        public override void AddedToWorld()
        {
            World.AddCreationListener<NetworkComponent>(Added);
            World.AddDestructionListener<NetworkComponent>(Removed);
        }

        public void Added(Entity entity)
        {
            NetworkComponent network = entity.GetComponent<NetworkComponent>();

            networkComponents[currentId] = network;
            network.Id = currentId;

            currentId++;
        }

        public void Removed(Entity entity)
        {
            NetworkComponent network = entity.GetComponent<NetworkComponent>();

            networkComponents.Remove(network.Id);
        }

        public UpdatePacket Transmit()
        {
            UpdatePacket packet = new UpdatePacket();

            foreach (Components components in World.Iterate(filter))
            {
                NetworkComponent network = components.Next<NetworkComponent>();

                EntityPacket entityPacket = new EntityPacket()
                {
                    Id = network.Id
                };

                entityPacket.Updates = network.Transmit();

                packet.packets.Add(entityPacket);
            }

            return packet;
        }

        public void Receive(UpdatePacket packet)
        {
            foreach (Components components in World.Iterate(filter))
            {
                NetworkComponent network = components.Next<NetworkComponent>();
            }
        }
    }
}
