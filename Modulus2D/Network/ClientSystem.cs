using Ionic.Zlib;
using Lidgren.Network;
using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Modulus2D.Network
{

    public class ClientSystem : EntitySystem
    {
        NetClient client;
        NetworkSystem networkSystem;
        Networker networker;

        private float updateTime = 0.025f; // 40 UPS
        private float accumulator = 0f;

        public ClientSystem(NetworkSystem networkSystem, string host, int port)
        {
            this.networkSystem = networkSystem;

            NetPeerConfiguration config = new NetPeerConfiguration("Modulus");
            client = new NetClient(config);

            client.Start();
            client.Connect(host, port);

            networker = new Networker(client);
        }

        public override void Update(float deltaTime)
        {
            networker.ReadMessages();

            accumulator += deltaTime;

            // Send update
            if (accumulator > updateTime)
            {
                client.SendMessage(networker.CreateUpdate(networkSystem.Transmit()), NetDeliveryMethod.UnreliableSequenced);

                accumulator = 0f;
            }
        }
    }
}
