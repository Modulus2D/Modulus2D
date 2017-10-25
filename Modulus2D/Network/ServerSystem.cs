using Lidgren.Network;
using Modulus2D.Entities;
using System;
using System.Collections.Generic;

namespace Modulus2D.Network
{
    public delegate void PlayerConnect(NetPlayer player);
    public delegate void PlayerDisconnect(NetPlayer player);

    /// <summary>
    /// Server-specific networking system
    /// </summary>
    public class ServerSystem : EntitySystem
    {
        public event PlayerConnect Connect;
        public event PlayerDisconnect Disconnect;

        private NetServer server;
        private NetSystem networkSystem;
        private Peer peer;

        private float updateTime = 1 / 30f;
        private float accumulator = 0f;

        private Dictionary<NetConnection, NetPlayer> players;

        // Current net ID
        private uint currentNetId = 0;

        public ServerSystem(NetSystem networkSystem, int port)
        {
            this.networkSystem = networkSystem;

            players = new Dictionary<NetConnection, NetPlayer>();

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
            NetPlayer player = new NetPlayer()
            {
                connection = connection
            };
            players.Add(connection, player);

            /*// Create initialization buffer
            AddPacket init = new AddPacket();
            
            // Add network ID and builder ID for each entity
            foreach (Components components in World.Iterate(filter))
            {
                NetworkComponent network = components.Next<NetworkComponent>();

                init.builders.Add(network.Id, network.BuilderId);
            }

            // Send initialization buffer to client
            server.SendToAll(peer.CreatePacket(init, PacketType.Add), NetDeliveryMethod.ReliableOrdered);
            */

            // Trigger event
            Connect(player);
        }

        private void OnDisconnect(NetConnection connection)
        {
            // Disconnect(players[connection]);
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
        /// Allocates a network ID for a new entity
        /// </summary>
        /// <returns></returns>
        public uint AllocateId()
        {
            currentNetId++;
            return (currentNetId - 1);
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
        public void SendEvent(string name, params object[] args)
        {
            EventPacket packet = new EventPacket()
            {
                name = name,
                args = args,
            };

            server.SendToAll(peer.CreatePacket(packet, PacketType.Event), NetDeliveryMethod.ReliableOrdered);
        }

        /// <summary>
        /// Sends a user-defined event to one client.
        /// </summary>
        public void SendEventToPlayer(string name, NetPlayer player, params object[] args)
        {
            EventPacket packet = new EventPacket()
            {
                name = name,
                args = args,
            };

            server.SendMessage(peer.CreatePacket(packet, PacketType.Event), player.connection, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
