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
        private NetSystem networkSystem;
        private Peer peer;

        private float updateTime = 1 / 30f;
        private float accumulator = 0f;

        public ClientSystem(NetSystem networkSystem, string host, int port)
        {
            this.networkSystem = networkSystem;

            NetPeerConfiguration config = new NetPeerConfiguration("Modulus");
            client = new NetClient(config);
            client.Start();

            client.Connect(host, port);

            peer = new Peer(client);

            peer.Connect += OnConnect;
            peer.Disconnect += OnDisconnect;
            peer.Update += OnUpdate;
        }

        public void OnConnect(NetConnection connection)
        {
        }

        public void OnDisconnect(NetConnection connection)
        {

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
        /// Sends a user-defined event to the server
        /// </summary>
        public void SendEvent(string name, params object[] args)
        {
            EventPacket packet = new EventPacket()
            {
                name = name,
                args = args
            };

            client.SendMessage(peer.CreatePacket(packet, PacketType.Event), NetDeliveryMethod.ReliableOrdered);
        }
    }
}
