using Lidgren.Network;
using Modulus2D.Entities;
using System;
using System.Collections.Generic;

namespace Modulus2D.Network
{
    /// <summary>
    /// Client-specific networking system
    /// </summary>
    public class ClientSystem : EntitySystem
    {
        private NetClient client;
        private NetworkSystem networkSystem;
        private Peer peer;

        private float updateTime = 0.025f; // 40 UPS
        private float accumulator = 0f;

        // Current builder ID
        private uint currentBuilderId = 0;

        // Maps a builder ID to a builder
        private Dictionary<uint, Builder> builderRegister;

        public ClientSystem(NetworkSystem networkSystem, string host, int port)
        {
            this.networkSystem = networkSystem;

            builderRegister = new Dictionary<uint, Builder>();

            NetPeerConfiguration config = new NetPeerConfiguration("Modulus");
            client = new NetClient(config);
            client.Start();

            client.Connect(host, port);

            peer = new Peer(client);

            peer.Connect += OnConnect;
            peer.Disconnect += OnDisconnect;
            peer.Update += OnUpdate;
            peer.Add += OnAdd;
        }

        public void OnConnect(NetConnection connection)
        {
        }

        public void OnDisconnect(NetConnection connection)
        {

        }

        public void OnAdd(Dictionary<uint, uint> builders)
        {
            foreach (KeyValuePair<uint, uint> pair in builders)
            {
                uint id = pair.Key;
                Builder builder = builderRegister[pair.Value];

                Entity entity = World.Add();
                builder.Build(entity);
                builder.BuildGraphics(entity);

                NetworkComponent network = new NetworkComponent()
                {
                    Id = id
                };

                builder.BuildClient(entity, network);
                
                Console.WriteLine("ADD");
            }
        }

        public void OnUpdate(UpdatePacket packet)
        {
            networkSystem.Receive(packet);
        }

        public void RegisterEvent(string name, NetEvent netEvent)
        {
            peer.RegisterEvent(name, netEvent);
        }

        public override void Update(float deltaTime)
        {
            peer.ReadMessages();

            accumulator += deltaTime;

            // Send update
            if (accumulator > updateTime)
            {
                client.SendMessage(peer.CreatePacket(networkSystem.Transmit(), PacketType.Update), NetDeliveryMethod.UnreliableSequenced);

                accumulator = 0f;

                // Log ping
                /*if (client.ServerConnection != null)
                {
                    Console.WriteLine("Ping: " + client.ServerConnection.AverageRoundtripTime);
                }*/
            }
        }

        /// <summary>
        /// Registers a builder class. This must be done in the same order on the client and the server.
        /// </summary>
        /// <param name="builder">Builder to register</param>
        public void RegisterBuilder(Builder builder)
        {
            builderRegister.Add(currentBuilderId, builder);
            currentBuilderId++;
        }

        /// <summary>
        /// Sends a user-defined event to the server
        /// </summary>
        public void SendEvent(string name, IUpdate update)
        {
            EventPacket packet = new EventPacket()
            {
                name = name,
                update = update
            };

            client.SendMessage(peer.CreatePacket(packet, PacketType.Event), NetDeliveryMethod.ReliableOrdered);
        }
    }
}
