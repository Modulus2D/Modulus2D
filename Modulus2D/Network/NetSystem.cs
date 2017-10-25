using Modulus2D.Entities;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Network
{
    /// <summary>
    /// Called when an update is received
    /// </summary>
    public delegate void ReceiveUpdate();

    /// <summary>
    /// System for synchronizing entities across the network
    /// </summary>
    public class NetSystem : EntitySystem
    {
        public event ReceiveUpdate ReceiveUpdate;

        private EntityFilter filter;
        private ComponentStorage<NetComponent> netComponents;

        private Dictionary<uint, Entity> networkedEntities;

        private Stopwatch stopwatch;
        private float lastDelta;

        /// <summary>
        /// Time between last two updates
        /// </summary>
        public float LastDelta { get => lastDelta; }

        public NetSystem()
        {
            networkedEntities = new Dictionary<uint, Entity>();

            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public override void OnAdded()
        {
            netComponents = World.GetStorage<NetComponent>();

            filter = new EntityFilter(netComponents);

            World.AddCreatedListener<NetComponent>((entity) =>
            {
                NetComponent network = entity.GetComponent<NetComponent>();
                networkedEntities.Add(network.Id, entity);
            });
        }

        public UpdatePacket Transmit()
        {
            UpdatePacket packet = new UpdatePacket();

            foreach (int id in World.Iterate(filter))
            {
                NetComponent network = netComponents.Get(id);
                
                packet.packets[network.Id] = network.Transmit();
            }

            return packet;
        }

        public void Receive(UpdatePacket packet)
        {
            lastDelta = (float)stopwatch.Elapsed.TotalSeconds;
            stopwatch.Restart();

            ReceiveUpdate?.Invoke();

            foreach (int id in World.Iterate(filter))
            {
                NetComponent network = netComponents.Get(id);
                
                if (packet.packets.TryGetValue(network.Id, out List<IUpdate> updates))
                {
                    network.ReceiveUpdate(updates);
                }
            }
        }

        public Entity GetByNetId(uint id)
        {
            return networkedEntities[id];
        }
    }
}
