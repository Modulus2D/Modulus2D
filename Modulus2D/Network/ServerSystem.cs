using Lidgren.Network;
using Modulus2D.Entities;
using System;
using System.Collections.Generic;

namespace Modulus2D.Network
{
    public delegate void PlayerConnect();
    public delegate void PlayerDisconnect();

    /// <summary>
    /// Server-specific networking system
    /// </summary>
    public class ServerSystem : EntitySystem
    {
        public event PlayerConnect Connect;
        public event PlayerDisconnect Disconnect;

        // Used for intialization buffer
        private EntityFilter filter;

        private NetServer server;
        private NetworkSystem networkSystem;
        private Peer peer;

        private float updateTime = 0.025f; // 40 UPS
        private float accumulator = 0f;

        // Current net ID
        private uint currentNetId = 0;

        // Current builder ID
        private uint currentBuilderId = 0;

        // Maps a builder to a builder ID
        private Dictionary<Builder, uint> builderRegister;

        public ServerSystem(NetworkSystem networkSystem, int port)
        {
            this.networkSystem = networkSystem;

            builderRegister = new Dictionary<Builder, uint>();

            filter = new EntityFilter();
            filter.Add<NetworkComponent>();

            // TODO: Not hard-coded?
            NetPeerConfiguration config = new NetPeerConfiguration("Modulus")
            {
                Port = port
            };
            server = new NetServer(config);
            server.Start();
            
            peer = new Peer(server);

            peer.Connect += OnConnect;
            peer.Disconnect += OnDisconnect;

            peer.Update += OnUpdate;
        }

        private void OnConnect(NetConnection connection)
        {
            // Create initialization buffer
            AddPacket init = new AddPacket();
            
            // Add network ID and builder ID for each entity
            foreach (Components components in World.Iterate(filter))
            {
                NetworkComponent network = components.Next<NetworkComponent>();

                init.builders.Add(network.Id, network.BuilderId);
            }

            // Send initialization buffer to client
            server.SendToAll(peer.CreatePacket(init, PacketType.Add), NetDeliveryMethod.ReliableOrdered);

            // Trigger event
            Connect();
        }

        private void OnDisconnect(NetConnection connection)
        {
            Disconnect();
        }

        private void OnUpdate(UpdatePacket packet)
        {
            networkSystem.Receive(packet);
        }

        public override void Update(float deltaTime)
        {
            // Read messages from clients
            peer.ReadMessages();

            accumulator += deltaTime;

            // Send update
            if (accumulator > updateTime)
            {
                server.SendToAll(peer.CreatePacket(networkSystem.Transmit(), PacketType.Update), NetDeliveryMethod.UnreliableSequenced);

                accumulator = 0f;
            }
        }

        /// <summary>
        /// Registers a builder class. This must be done in the same order on the client and the server.
        /// </summary>
        /// <param name="builder">Builder to register</param>
        public void RegisterBuilder(Builder builder)
        {
            builderRegister.Add(builder, currentBuilderId);
            currentBuilderId++;
        }

        /// <summary>
        /// Adds an entity to the world and updates clients
        /// </summary>
        /// <param name="builder">Builder to use</param>
        public void AddEntity(Builder builder)
        {
            Entity entity = World.Add();
            builder.Build(entity);

            uint builderId = builderRegister[builder];

            NetworkComponent network = new NetworkComponent()
            {
                Id = currentNetId,
                BuilderId = builderId
            };

            builder.BuildServer(entity, network);

            AddPacket packet = new AddPacket();
            packet.builders.Add(currentNetId, builderId);

            server.SendToAll(peer.CreatePacket(packet, PacketType.Add), NetDeliveryMethod.ReliableOrdered);

            currentNetId++;
        }

        /// <summary>
        /// Removes an entity from the world and updates clients
        /// </summary>
        public void RemoveEntity(uint id)
        {
            RemovePacket packet = new RemovePacket();
            packet.entities.Add(id);

            server.SendToAll(peer.CreatePacket(packet, PacketType.Remove), NetDeliveryMethod.ReliableOrdered);
        }

        /// <summary>
        /// Sends a user-defined event to all clients.
        /// </summary>
        public void SendEvent(string name, IUpdate update)
        {
            EventPacket packet = new EventPacket()
            {
                name = name,
                update = update
            };

            server.SendToAll(peer.CreatePacket(packet, PacketType.Event), NetDeliveryMethod.ReliableOrdered);
        }

        /// <summary>
        /// Sends a user-defined event to one client.
        /// </summary>
        public void SendEvent(string name, IUpdate update, NetConnection connection)
        {
            EventPacket packet = new EventPacket()
            {
                name = name,
                update = update
            };

            server.SendMessage(peer.CreatePacket(packet, PacketType.Event), connection, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
